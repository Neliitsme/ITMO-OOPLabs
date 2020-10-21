using System;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            var network = new ShopNetwork();
            Shop shop = new Shop();
            shop.Name = "ashan";
            shop.Address = "bruh street";
            network.AddShop(shop);
            network.CreateShop("name", "nice street");
            network.PrintAllShops();
        }
    }
}
