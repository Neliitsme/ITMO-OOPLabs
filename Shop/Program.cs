using System;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            var network = new ShopNetwork();
            Shop ashan = new Shop {Name = "ashan", Address = "bruh street"};
            network.AddShop(ashan);
            network.AddShop(new Shop("name", "nice street"));
            network.AddShop(new Shop("Ikea", "street name, street number"));

            network[network.GetShopId("Ikea", "street name, street number")].AddProduct(new Product("Table"), 200, 4999);
            var ikea = network.GetShop(network.GetShopId("Ikea", "street name, street number"));
            ikea.AddProduct(new Product("Office Chair"), 1337, 14041);
            Product ikeaPencil = new Product("Pencil");
            ikea.AddProduct(ikeaPencil);

            ashan.AddProduct(ikeaPencil, 999, 10);
            ashan.AddProduct(new Product("Milk"), 100, 98);
            ashan.AddProduct(new Product("Cola"), 100, 123);

            network[network.GetShopId("name", "nice street")].AddProduct(new Product("bepis"));
            network[network.GetShopId("name", "nice street")].AddProduct(new Product("bepis"));
            network[network.GetShopId("name", "nice street")].AddProduct(new Product("bepis"));
            network[network.GetShopId("name", "nice street")].AddProduct(ikeaPencil);
            network[network.GetShopId("name", "nice street")].AddProduct(ikeaPencil);
            network[network.GetShopId("name", "nice street")].AddProduct(ikeaPencil);

            network.PrintAllShops();
            ikea.PrintShopContents();
            ashan.PrintShopContents();
            network[network.GetShopId("name", "nice street")].PrintShopContents();

            network.FindWhereCheapest(ikeaPencil.Id);
            network.FindWhereCheapestBatch(ikeaPencil.Id, 100);

            ashan.BuyProductBatch(ikeaPencil.Id, 999);
            ashan.PrintShopContents();
            // ashan.BuyProductBatch(ikeaPencil.Id);
            ikea.WhatCanIBuy(1000000);
            ikea.ChangeProductPrice(ikea.GetProductId("Office Chair"), 1337);
            ikea.PrintShopContents();
        }
    }
}
