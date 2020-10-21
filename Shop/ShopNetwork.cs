using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Shop
{
    public class ShopNetwork
    {
        private Dictionary<string , Shop> _shopNetwork = new Dictionary<string, Shop>();

        public void CreateShop(string shopName, string shopAddress)
        {
            Shop shop = new Shop();
            shop.Id = Guid.NewGuid().ToString();
            shop.Name = shopName;
            shop.Address = shopAddress;
            Shop.CheckShop(shop);
            _shopNetwork.Add(shop.Id, shop);
        }

        public Shop GetShop(string shopName, string shopAddress)
        {
            foreach (var shop in _shopNetwork)
            {
                if (shop.Value.Name == shopName && shop.Value.Address == shopAddress)
                {
                    return shop.Value;
                }
            }

            throw new ArgumentException("No shop with such name and address exists");
        }

        public Shop GetShop(string shopId)
        {
            if (_shopNetwork.ContainsKey(shopId))
            {
                return _shopNetwork[shopId];
            }

            throw new ArgumentException("No shop with such ID exists");

        }

        public void AddShop(Shop shop)
        {
            Shop.CheckShop(shop);
            _shopNetwork.Add(shop.Id, shop);
        }

        public void RemoveShop(Shop shop)
        {
            if (_shopNetwork.ContainsValue(shop))
            {
                _shopNetwork.Remove(shop.Id);
            }
            throw new ArgumentException("No such shop exists in the network");
        }

        public void RemoveShop(string shopId)
        {
            if (_shopNetwork.ContainsKey(shopId))
            {
                _shopNetwork.Remove(shopId);
            }
            throw new ArgumentException("No shop with such ID exists in the network");
        }

        public void FindWhereCheapest(string productName)//Todo: Check and prolly re-make
        {
            Shop cheapestShop = null;
            int lowestPrice = 0;

            foreach (var shop in _shopNetwork)
            {
                Shop.CheckShop(shop.Value);
                foreach (var product in shop.Value.ProductInfoDictionary)
                {
                    Product.CheckProduct(product.Value);
                    if (product.Value.Name == productName)
                    {
                        if (lowestPrice == 0 || product.Value.Price < lowestPrice)
                        {
                            lowestPrice = product.Value.Price;
                            cheapestShop = shop.Value;
                        }
                    }
                }
            }

            if (lowestPrice == 0 || cheapestShop == null)
            {
                Console.WriteLine("Couldn't find product witch such name in any shop in this network");
                return;
            }

            Console.WriteLine($"Lowest price of \"{productName}\": {lowestPrice}; In: {cheapestShop.Name}");
        }

        public void FindWhereCheapestBatch(string productName, int quantity)//Todo: Check and prolly re-make
        {
            Shop cheapestShop = null;
            int lowestPriceForBatch = 0;

            foreach (var shop in _shopNetwork)
            {
                Shop.CheckShop(shop.Value);
                foreach (var product in shop.Value.ProductInfoDictionary)
                {
                    Product.CheckProduct(product.Value);
                    if (product.Value.Name == productName && product.Value.Quantity >= quantity)
                    {
                        if (lowestPriceForBatch == 0 || product.Value.Price < lowestPriceForBatch)
                        {
                            lowestPriceForBatch = product.Value.Price * quantity;
                            cheapestShop = shop.Value;
                        }
                    }
                }
            }

            if (lowestPriceForBatch == 0 || cheapestShop == null)
            {
                Console.WriteLine("Couldn't find product witch such name in any shop in this network or quantity given was too high");
                return;
            }

            Console.WriteLine($"Lowest price for batch of \"{productName}\": {lowestPriceForBatch}; In: {cheapestShop.Name}");
        }

        public void PrintAllShops()
        {
            Console.WriteLine("List of all shops in the network:");
            foreach (var shop in _shopNetwork)
            {
                Shop.CheckShop(shop.Value);
                Console.WriteLine($"\tName: {shop.Value.Name}; Address: {shop.Value.Address}; Id: {shop.Value.Id}");
            }
        }

        public string GetShopId(string shopName, string shopAddress)
        {
            foreach (var shop in _shopNetwork)
            {
                if (shop.Value.Name == shopName && shop.Value.Address == shopAddress)
                {
                    return shop.Value.Id;
                }
            }

            throw new ArgumentException("No shop with such name and address exists");
        }

    }
}