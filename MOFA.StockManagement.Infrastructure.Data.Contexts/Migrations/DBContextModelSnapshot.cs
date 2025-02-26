﻿// <auto-generated />
using System;
using MOFA.StockManagement.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.Consumer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("Phone")
                        .HasMaxLength(32)
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("Consumers", "sales");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BarCode")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("BarCodeImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<Guid>("ItemTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BarCode")
                        .IsUnique();

                    b.HasIndex("ItemTypeId");

                    b.HasIndex("SKU")
                        .IsUnique();

                    b.ToTable("Items", "sales");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.ItemType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<Guid?>("ParentItemTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("ItemTypes", "config");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.ItemUnitOfMeasure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ConversionFactor")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsBase")
                        .HasColumnType("bit");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<Guid>("UnitOfMeasureId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("UnitOfMeasureId");

                    b.ToTable("ItemUnitOfMeasures", "config");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ConsumerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DestinationWarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<Guid?>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint");

                    b.Property<Guid?>("WarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ConsumerId");

                    b.HasIndex("DestinationWarehouseId");

                    b.HasIndex("Number")
                        .IsUnique();

                    b.HasIndex("SupplierId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Orders", "operation");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.OrderDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("OrderItemStatus")
                        .HasColumnType("tinyint");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails", "operation");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.OrderSerial", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Year")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("OrderSerials", "config");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.StockBalance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<float>("MinimumStockLevel")
                        .HasColumnType("real");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("StockBalances", "operation");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Suppliers", "sales");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.SupplierItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateSupplied")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Unit")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("SupplierId");

                    b.ToTable("SupplierItems", "sales");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ConsumerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DestinationWarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<Guid?>("SourceWarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SupplierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("TransactionType")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("ConsumerId");

                    b.HasIndex("DestinationWarehouseId");

                    b.HasIndex("OrderId")
                        .IsUnique()
                        .HasFilter("[OrderId] IS NOT NULL");

                    b.HasIndex("SourceWarehouseId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Transactions", "operation");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.TransactionDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderDetailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("UnitCost")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderDetailId");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionDetails", "operation");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.UnitOfMeasure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("UnitName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("UnitOfMeasures", "config");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.Warehouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Capacity")
                        .HasColumnType("real");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<byte>("IsFull")
                        .HasColumnType("tinyint");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Warehouses", "config");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.Item", b =>
                {
                    b.HasOne("MOFA.StockManagement.Domain.Entities.ItemType", "ItemType")
                        .WithMany("Items")
                        .HasForeignKey("ItemTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ItemType");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.ItemUnitOfMeasure", b =>
                {
                    b.HasOne("MOFA.StockManagement.Domain.Entities.Item", "Item")
                        .WithMany("ItemUnitOfMeasures")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MOFA.StockManagement.Domain.Entities.UnitOfMeasure", "UnitOfMeasure")
                        .WithMany("ItemUnitOfMeasures")
                        .HasForeignKey("UnitOfMeasureId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("UnitOfMeasure");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.Order", b =>
                {
                    b.HasOne("MOFA.StockManagement.Domain.Entities.Consumer", "Consumer")
                        .WithMany("Orders")
                        .HasForeignKey("ConsumerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MOFA.StockManagement.Domain.Entities.Warehouse", "DestinationWarehouse")
                        .WithMany()
                        .HasForeignKey("DestinationWarehouseId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MOFA.StockManagement.Domain.Entities.Supplier", "Supplier")
                        .WithMany("Orders")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MOFA.StockManagement.Domain.Entities.Warehouse", "Warehouse")
                        .WithMany("Orders")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Consumer");

                    b.Navigation("DestinationWarehouse");

                    b.Navigation("Supplier");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.OrderDetail", b =>
                {
                    b.HasOne("MOFA.StockManagement.Domain.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MOFA.StockManagement.Domain.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.OrderSerial", b =>
                {
                    b.HasOne("MOFA.StockManagement.Domain.Entities.Warehouse", "Warehouse")
                        .WithMany("OrderSerials")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.StockBalance", b =>
                {
                    b.HasOne("MOFA.StockManagement.Domain.Entities.Item", "Item")
                        .WithMany("StockBalances")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MOFA.StockManagement.Domain.Entities.Warehouse", "Warehouse")
                        .WithMany("StockBalances")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.SupplierItem", b =>
                {
                    b.HasOne("MOFA.StockManagement.Domain.Entities.Item", "Item")
                        .WithMany("SupplierItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MOFA.StockManagement.Domain.Entities.Supplier", "Supplier")
                        .WithMany("SupplierItems")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("MOFA.StockManagement.Domain.Entities.Consumer", null)
                        .WithMany("Transactions")
                        .HasForeignKey("ConsumerId");

                    b.HasOne("MOFA.StockManagement.Domain.Entities.Warehouse", "DestinationWarehouse")
                        .WithMany("DestinationTransactions")
                        .HasForeignKey("DestinationWarehouseId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MOFA.StockManagement.Domain.Entities.Order", "Order")
                        .WithOne("Transaction")
                        .HasForeignKey("MOFA.StockManagement.Domain.Entities.Transaction", "OrderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MOFA.StockManagement.Domain.Entities.Warehouse", "SourceWarehouse")
                        .WithMany("SourceTransactions")
                        .HasForeignKey("SourceWarehouseId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MOFA.StockManagement.Domain.Entities.Supplier", "Supplier")
                        .WithMany("Transactions")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("DestinationWarehouse");

                    b.Navigation("Order");

                    b.Navigation("SourceWarehouse");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.TransactionDetail", b =>
                {
                    b.HasOne("MOFA.StockManagement.Domain.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MOFA.StockManagement.Domain.Entities.OrderDetail", "OrderDetail")
                        .WithMany()
                        .HasForeignKey("OrderDetailId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MOFA.StockManagement.Domain.Entities.Transaction", "Transaction")
                        .WithMany("TransactionDetails")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("OrderDetail");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.Consumer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.Item", b =>
                {
                    b.Navigation("ItemUnitOfMeasures");

                    b.Navigation("StockBalances");

                    b.Navigation("SupplierItems");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.ItemType", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.Supplier", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("SupplierItems");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.Transaction", b =>
                {
                    b.Navigation("TransactionDetails");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.UnitOfMeasure", b =>
                {
                    b.Navigation("ItemUnitOfMeasures");
                });

            modelBuilder.Entity("MOFA.StockManagement.Domain.Entities.Warehouse", b =>
                {
                    b.Navigation("DestinationTransactions");

                    b.Navigation("OrderSerials");

                    b.Navigation("Orders");

                    b.Navigation("SourceTransactions");

                    b.Navigation("StockBalances");
                });
#pragma warning restore 612, 618
        }
    }
}
