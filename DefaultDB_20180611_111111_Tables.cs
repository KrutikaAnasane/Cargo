using FluentMigrator;
using System;
namespace OrderManagementSystem.Migrations.DefaultDB
{
    [Migration(20180611111111)]
    public class DefaultDB_20180611_111111_Tables : Migration
    {
        public override void Up()
        {
            Create.Schema("orderManagement");

            Create.Table("Region").InSchema("orderManagement")
               .WithColumn("RegionId").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("Description").AsString(200).NotNullable();

            Create.Table("Supplier").InSchema("orderManagement")
                .WithColumn("SupplierId").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Company name").AsString(200).NotNullable()
                .WithColumn("Contact name").AsString(200).NotNullable()
                .WithColumn("Contact title").AsString(200).Nullable()
                .WithColumn("Phone").AsInt64().Nullable()
                .WithColumn("Region").AsInt32().NotNullable()
                 .ForeignKey( "FK_SupplierRegion", "orderManagement", "Region", "RegionId");

            Create.Table("Product").InSchema("orderManagement")
                .WithColumn("ProductId").AsInt32().Identity().PrimaryKey().NotNullable()
              .WithColumn("Product Name").AsString(200).NotNullable()
              .WithColumn("Price").AsInt32().Nullable()
              .WithColumn("SupplierId").AsInt32().NotNullable()
              .ForeignKey("FK_SupplierProduct", "orderManagement", "Supplier", "SupplierId");


            Create.Table("Customer").InSchema("orderManagement")
                .WithColumn("CustomerId").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Customer name").AsString(200).NotNullable()
                .WithColumn("Contact").AsInt64().Nullable()
                .WithColumn("City").AsString(200).NotNullable()
               .WithColumn("Region").AsInt32().NotNullable()
                 .ForeignKey("FK_CustomerRegion", "orderManagement","Region", "RegionId");

            Create.Table("Shipper").InSchema("orderManagement")
                .WithColumn("ShipperId").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Company name").AsString(200).NotNullable()
                .WithColumn("Region").AsInt32().NotNullable()
                 .ForeignKey("FK_ShipperRegion", "orderManagement", "Region", "RegionId")
                .WithColumn("Phone").AsInt64().Nullable()
                .WithColumn("ShippedQuantity").AsInt32().NotNullable();

            Create.Table("Order").InSchema("orderManagement")
                .WithColumn("OrderId").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("CustomerId").AsInt32().NotNullable()
                .ForeignKey("FK_CustomerOrder", "orderManagement", "Customer", "CustomerId")
                .WithColumn("ShipperId").AsInt32().NotNullable()
                .ForeignKey("FK_ShipperOrder", "orderManagement", "Shipper", "ShipperId")
                .WithColumn("Order date").AsDateTime().NotNullable()
                .WithColumn("TrackingId").AsInt32().NotNullable();

            Create.Table("OrderProduct").InSchema("orderManagement")
           .WithColumn("Id").AsInt32()
           .Identity().PrimaryKey().NotNullable()
           .WithColumn("OrderId").AsInt32().NotNullable()
           .ForeignKey("FK_ProductOrder1", "orderManagement", "Order", "OrderId")
           .WithColumn("ProductId").AsInt32().NotNullable()
           .ForeignKey("FK_ProductOrder2", "orderManagement", "Product", "ProductId")
           .WithColumn("Quantity").AsInt32().NotNullable();

   /*         Execute.Sql(
           @"INSERT INTO oms.OrderProduct (OrderId, ProductId)
            SELECT o.OrderId, o.ProductId
            FROM oms.Order o
            WHERE o.ProductId IS NOT NULL");       */

            Create.Table("Inventory").InSchema("orderManagement")
            .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
            .WithColumn("Date").AsDateTime().NotNullable()
            .WithColumn("ProductId").AsInt32().NotNullable()
            .ForeignKey("FK_ProductInventory", "orderManagement", "Product", "ProductId")
            .WithColumn("Quantity").AsInt32().NotNullable()
            .WithColumn("Direction").AsInt16().NotNullable();
        }
        public override void Down()
        {
        }
    }
}

