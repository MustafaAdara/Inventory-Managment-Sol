using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "config");

            migrationBuilder.EnsureSchema(
                name: "sales");

            migrationBuilder.EnsureSchema(
                name: "operation");

            migrationBuilder.CreateTable(
                name: "Centers",
                schema: "config",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsMofa = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consumers",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Phone = table.Column<int>(type: "int", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemTypes",
                schema: "config",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ParentItemTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasures",
                schema: "config",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                schema: "config",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Capacity = table.Column<float>(type: "real", nullable: false),
                    IsFull = table.Column<byte>(type: "tinyint", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    BarCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItemTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ItemTypes_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalSchema: "config",
                        principalTable: "ItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "operation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DestinationWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupplierID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "sales",
                        principalTable: "Consumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalSchema: "sales",
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Warehouses_DestinationWarehouseId",
                        column: x => x.DestinationWarehouseId,
                        principalSchema: "config",
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalSchema: "config",
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderSerials",
                schema: "config",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSerials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSerials_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalSchema: "config",
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemUnitOfMeasures",
                schema: "config",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitOfMeasureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsBase = table.Column<bool>(type: "bit", nullable: false),
                    ConversionFactor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemUnitOfMeasures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemUnitOfMeasures_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "sales",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemUnitOfMeasures_UnitOfMeasures_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalSchema: "config",
                        principalTable: "UnitOfMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockBalances",
                schema: "operation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    MinimumStockLevel = table.Column<float>(type: "real", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockBalances_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "sales",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockBalances_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalSchema: "config",
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierItems",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateSupplied = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Unit = table.Column<byte>(type: "tinyint", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "sales",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierItems_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "sales",
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                schema: "operation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderItemStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "sales",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "operation",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "operation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SourceWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DestinationWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransactionType = table.Column<byte>(type: "tinyint", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "sales",
                        principalTable: "Consumers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "operation",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "sales",
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Warehouses_DestinationWarehouseId",
                        column: x => x.DestinationWarehouseId,
                        principalSchema: "config",
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Warehouses_SourceWarehouseId",
                        column: x => x.SourceWarehouseId,
                        principalSchema: "config",
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionDetails",
                schema: "operation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionDetails_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "sales",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionDetails_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalSchema: "operation",
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionDetails_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalSchema: "operation",
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTypeId",
                schema: "sales",
                table: "Items",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasures_ItemId",
                schema: "config",
                table: "ItemUnitOfMeasures",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemUnitOfMeasures_UnitOfMeasureId",
                schema: "config",
                table: "ItemUnitOfMeasures",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ConsumerId",
                schema: "operation",
                table: "Order",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DestinationWarehouseId",
                schema: "operation",
                table: "Order",
                column: "DestinationWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Number",
                schema: "operation",
                table: "Order",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_SupplierID",
                schema: "operation",
                table: "Order",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_WarehouseId",
                schema: "operation",
                table: "Order",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ItemId",
                schema: "operation",
                table: "OrderDetails",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                schema: "operation",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSerials_WarehouseId",
                schema: "config",
                table: "OrderSerials",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockBalances_ItemId",
                schema: "operation",
                table: "StockBalances",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StockBalances_WarehouseId",
                schema: "operation",
                table: "StockBalances",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierItems_ItemId",
                schema: "sales",
                table: "SupplierItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierItems_SupplierId",
                schema: "sales",
                table: "SupplierItems",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_ItemId",
                schema: "operation",
                table: "TransactionDetails",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_OrderDetailId",
                schema: "operation",
                table: "TransactionDetails",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_TransactionId",
                schema: "operation",
                table: "TransactionDetails",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ConsumerId",
                schema: "operation",
                table: "Transactions",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DestinationWarehouseId",
                schema: "operation",
                table: "Transactions",
                column: "DestinationWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OrderId",
                schema: "operation",
                table: "Transactions",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SourceWarehouseId",
                schema: "operation",
                table: "Transactions",
                column: "SourceWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SupplierId",
                schema: "operation",
                table: "Transactions",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Centers",
                schema: "config");

            migrationBuilder.DropTable(
                name: "ItemUnitOfMeasures",
                schema: "config");

            migrationBuilder.DropTable(
                name: "OrderSerials",
                schema: "config");

            migrationBuilder.DropTable(
                name: "StockBalances",
                schema: "operation");

            migrationBuilder.DropTable(
                name: "SupplierItems",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "TransactionDetails",
                schema: "operation");

            migrationBuilder.DropTable(
                name: "UnitOfMeasures",
                schema: "config");

            migrationBuilder.DropTable(
                name: "OrderDetails",
                schema: "operation");

            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "operation");

            migrationBuilder.DropTable(
                name: "Items",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "operation");

            migrationBuilder.DropTable(
                name: "ItemTypes",
                schema: "config");

            migrationBuilder.DropTable(
                name: "Consumers",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Suppliers",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "Warehouses",
                schema: "config");
        }
    }
}
