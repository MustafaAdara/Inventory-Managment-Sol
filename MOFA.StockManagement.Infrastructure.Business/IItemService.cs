using AutoMapper;
using MOFA.StockManagement.Domain.Entities;
using MOFA.StockManagement.Domain.Patterns.Interfaces;
using MOFA.StockManagement.Domain.Patterns.Interfaces.Trackable;
using MOFA.StockManagement.Infrastructure.Data.Contexts;
using MOFA.StockManagement.Infrastructure.Models;
using MOFA.StockManagement.Infrastructure.Queries;
using SkiaSharp;
using ZXing;
using ZXing.SkiaSharp;

namespace MOFA.StockManagement.Infrastructure.Business
{
    public interface IItemService : IServiceBase<Item, ItemModel, Guid>
    {
        public Task<PaginationModel<ItemModel>> SelectPaginationSearchAsync(string searchBy, int page, string sortBy, int pageSize = 10, bool sortAsc = true);
    }
    public class ItemService : ServiceBase<Item, ItemModel, Guid>, IItemService
    {
        private readonly ITrackableRepository<Item, DbContextBase> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<DbContextBase> _unitOfWork;
        public ItemService(
            ITrackableRepository<Item, DbContextBase> repository, IMapper mapper, IUnitOfWork<DbContextBase> unitOfWork)
                            : base(repository, mapper, unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<PaginationModel<ItemModel>> SelectPaginationSearchAsync(string searchBy, int page, string sortBy, int pageSize = 10, bool sortAsc = true)
        {
            try
            {

                var totalCount = await _repository.CountSearchAsync(searchBy);
                var entities = await _repository.SelectSearchAsync(searchBy, page, sortBy, pageSize, sortAsc);

                var result = _mapper.Map<IEnumerable<ItemModel>>(entities);
                var paginationModel = new PaginationModel<ItemModel>()
                {
                    CurrentPage = page,
                    Data = (IList<ItemModel>)result,
                    PageSize = pageSize,
                    Count = totalCount
                };
                return paginationModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override async Task<ItemModel> AddAsync(ItemModel model)
        {
            try
            {
                var entity = _mapper.Map<Item>(model);
                entity.CreatedAt = DateTime.UtcNow;
                // Check if an item with the same SKU or Barcode already exists
                var existingItem = _repository.ItemSKUExists(entity);

                if (existingItem != null)
                    throw new Exception($"An item with the same SKU ({model.SKU}) or Barcode ({model.BarCode}) already exists.");
                else
                {
                    entity.SKU = GenerateSKU(entity.Id, entity.Name, entity.CreatedAt);
                }

                if (string.IsNullOrEmpty(entity.BarCode))
                {
                    entity.BarCode = $"{entity.SKU}-{DateTime.UtcNow:yyMMddHHmm}"; // Example: "001-BW-2312051430"
                }

                // Generate Barcode Image as Base64 PNG
                entity.BarCodeImg = GenerateBarcodeImage(entity.BarCode);

                entity.CreatedAt = DateTime.UtcNow;

                 _repository.Insert(entity);
                await _unitOfWork.SaveChangesAsync();  

                return _mapper.Map<ItemModel>(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating an item.", ex);
            }
        }
        public static string GenerateBarcodeImage(string barcodeText)
        {
            try
            {
                var writer = new BarcodeWriter
                {
                    Format = BarcodeFormat.CODE_128, //  Choose barcode format (EAN_13, UPC_A, etc.)
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Width = 500,  // Set high resolution for printing
                        Height = 200
                    }
                };

                using var bitmap = writer.Write(barcodeText); // Generate barcode image
                using var image = SKImage.FromBitmap(bitmap);
                using var data = image.Encode(SKEncodedImageFormat.Png, 100); //  Convert to PNG format

                return Convert.ToBase64String(data.ToArray()); // Convert PNG to Base64
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating barcode PNG", ex);
            }
        }
        public static string GenerateSKU(Guid id, string name, DateTime createdAt)
        {
            string nameInitials = string.Join("", name.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                .Select(word => word[0])).ToUpper();
            string yearMonth = createdAt.ToString("yyMM");
            return $"{id:D3}-{nameInitials}-{yearMonth}";
        }
    }
}
