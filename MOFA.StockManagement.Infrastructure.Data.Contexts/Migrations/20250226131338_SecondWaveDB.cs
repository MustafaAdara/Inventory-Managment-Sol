using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOFA.StockManagement.Infrastructure.Data.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class SecondWaveDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Consumers_ConsumerId",
                schema: "operation",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Suppliers_SupplierID",
                schema: "operation",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Warehouses_DestinationWarehouseId",
                schema: "operation",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Warehouses_WarehouseId",
                schema: "operation",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Order_OrderId",
                schema: "operation",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Order_OrderId",
                schema: "operation",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Centers",
                schema: "config");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                schema: "operation",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                schema: "operation",
                newName: "Orders",
                newSchema: "operation");

            migrationBuilder.RenameColumn(
                name: "SupplierID",
                schema: "operation",
                table: "Orders",
                newName: "SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_WarehouseId",
                schema: "operation",
                table: "Orders",
                newName: "IX_Orders_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_SupplierID",
                schema: "operation",
                table: "Orders",
                newName: "IX_Orders_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Number",
                schema: "operation",
                table: "Orders",
                newName: "IX_Orders_Number");

            migrationBuilder.RenameIndex(
                name: "IX_Order_DestinationWarehouseId",
                schema: "operation",
                table: "Orders",
                newName: "IX_Orders_DestinationWarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ConsumerId",
                schema: "operation",
                table: "Orders",
                newName: "IX_Orders_ConsumerId");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "config",
                table: "Warehouses",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BarCodeImg",
                schema: "sales",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                schema: "operation",
                table: "Orders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_Code",
                schema: "config",
                table: "Warehouses",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_BarCode",
                schema: "sales",
                table: "Items",
                column: "BarCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_SKU",
                schema: "sales",
                table: "Items",
                column: "SKU",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                schema: "operation",
                table: "OrderDetails",
                column: "OrderId",
                principalSchema: "operation",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Consumers_ConsumerId",
                schema: "operation",
                table: "Orders",
                column: "ConsumerId",
                principalSchema: "sales",
                principalTable: "Consumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Suppliers_SupplierId",
                schema: "operation",
                table: "Orders",
                column: "SupplierId",
                principalSchema: "sales",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Warehouses_DestinationWarehouseId",
                schema: "operation",
                table: "Orders",
                column: "DestinationWarehouseId",
                principalSchema: "config",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Warehouses_WarehouseId",
                schema: "operation",
                table: "Orders",
                column: "WarehouseId",
                principalSchema: "config",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Orders_OrderId",
                schema: "operation",
                table: "Transactions",
                column: "OrderId",
                principalSchema: "operation",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                schema: "operation",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Consumers_ConsumerId",
                schema: "operation",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Suppliers_SupplierId",
                schema: "operation",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Warehouses_DestinationWarehouseId",
                schema: "operation",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Warehouses_WarehouseId",
                schema: "operation",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Orders_OrderId",
                schema: "operation",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_Code",
                schema: "config",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Items_BarCode",
                schema: "sales",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_SKU",
                schema: "sales",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                schema: "operation",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "config",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "BarCodeImg",
                schema: "sales",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Orders",
                schema: "operation",
                newName: "Order",
                newSchema: "operation");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                schema: "operation",
                table: "Order",
                newName: "SupplierID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_WarehouseId",
                schema: "operation",
                table: "Order",
                newName: "IX_Order_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_SupplierId",
                schema: "operation",
                table: "Order",
                newName: "IX_Order_SupplierID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Number",
                schema: "operation",
                table: "Order",
                newName: "IX_Order_Number");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DestinationWarehouseId",
                schema: "operation",
                table: "Order",
                newName: "IX_Order_DestinationWarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ConsumerId",
                schema: "operation",
                table: "Order",
                newName: "IX_Order_ConsumerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                schema: "operation",
                table: "Order",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Centers",
                schema: "config",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsMofa = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Consumers_ConsumerId",
                schema: "operation",
                table: "Order",
                column: "ConsumerId",
                principalSchema: "sales",
                principalTable: "Consumers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Suppliers_SupplierID",
                schema: "operation",
                table: "Order",
                column: "SupplierID",
                principalSchema: "sales",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Warehouses_DestinationWarehouseId",
                schema: "operation",
                table: "Order",
                column: "DestinationWarehouseId",
                principalSchema: "config",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Warehouses_WarehouseId",
                schema: "operation",
                table: "Order",
                column: "WarehouseId",
                principalSchema: "config",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Order_OrderId",
                schema: "operation",
                table: "OrderDetails",
                column: "OrderId",
                principalSchema: "operation",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Order_OrderId",
                schema: "operation",
                table: "Transactions",
                column: "OrderId",
                principalSchema: "operation",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
