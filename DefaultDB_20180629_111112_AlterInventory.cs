using FluentMigrator;
using System;
namespace OrderManagementSystem.Migrations.DefaultDB
{
    [Migration(20180629111112)]
    public class DefaultDB_20180629_111112_AlterInventory : Migration
    {
        public override void Up()
        {
            Delete.Column("ShippedQuantity").FromTable("Shipper").InSchema("orderManagement");

            Create.Table("ShipmentOrder").InSchema("orderManagement")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ShipperId").AsInt32().NotNullable()
                .ForeignKey("FK_ShipperShipment", "orderManagement", "Shipper", "ShipperId")
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("ShipmentFrom").AsString(200).Nullable()
                .WithColumn("ShipmentTo").AsString(200).Nullable()
                .WithColumn("CustomerId").AsInt32().NotNullable()
                .ForeignKey("FK_CustomerShipment", "orderManagement", "Customer", "CustomerId")
                .WithColumn("OrderId").AsInt32().NotNullable()
                .ForeignKey("FK_OrderShipment", "orderManagement", "Order", "OrderId")
                .WithColumn("Status").AsInt16().NotNullable();

            Create.Table("ShipmentProduct").InSchema("orderManagement")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                 .WithColumn("OrderId").AsInt32().NotNullable()
                .ForeignKey("FK_OrderShipmentP", "orderManagement", "Order", "OrderId")
                 .WithColumn("OrderProductId").AsInt32().NotNullable()
                .ForeignKey("FK_OrderPShipmentP", "orderManagement", "OrderProduct", "Id")
                .WithColumn("ShippedQuantity").AsInt32().NotNullable();

            Alter.Table("Inventory").InSchema("orderManagement")
                .AddColumn("SupplierId").AsInt32().Nullable()
                .ForeignKey("FK_SupplierInventory", "orderManagement", "Supplier", "SupplierId")
                .AddColumn("OrderId").AsInt32().Nullable()
                .ForeignKey("FK_OrderInventory", "orderManagement", "Order", "OrderId")
                .AddColumn("ShipmentId").AsInt32().Nullable()
                .ForeignKey("FK_InventoryShipment", "orderManagement", "ShipmentOrder", "Id");



        }
        public override void Down()
        {
        }
    }
}

