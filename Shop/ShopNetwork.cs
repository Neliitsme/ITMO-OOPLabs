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
            if (_shopNetwork.ContainsKey(shop.Id) || _shopNetwork.ContainsValue(shop))
            {
                throw new ArgumentException("Can't add same shop to the network more than once");
            }
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

        public void FindWhereCheapest(string productId)
        {
            Shop cheapestShop = null;
            int lowestPrice = 0;

            foreach (var shop in _shopNetwork)
            {
                Shop.CheckShop(shop.Value);
                if (shop.Value.ProductCollection.ContainsKey(productId))
                {
                    Product.CheckProduct(shop.Value.ProductCollection[productId].ProductInfo);

                    if (lowestPrice == 0 || shop.Value.ProductCollection[productId].Price < lowestPrice)
                    {
                        lowestPrice = shop.Value.ProductCollection[productId].Price;
                        cheapestShop = shop.Value;
                    }
                }
            }

            if (lowestPrice == 0 || cheapestShop == null)
            {
                Console.WriteLine("Couldn't find product witch such name in any shop in this network");
                return;
            }

            Console.WriteLine($"Lowest price of \"{cheapestShop.ProductCollection[productId].ProductInfo.Name}\": {lowestPrice}; In: {cheapestShop.Name}");
        }

        public void FindWhereCheapestBatch(string productId, int quantity)
        {
            Shop cheapestShop = null;
            int lowestPriceForBatch = 0;

            foreach (var shop in _shopNetwork)
            {
                Shop.CheckShop(shop.Value);
                if (shop.Value.ProductCollection.ContainsKey(productId))
                {
                    Product.CheckProduct(shop.Value.ProductCollection[productId].ProductInfo);
                    if (shop.Value.ProductCollection[productId].Quantity >= quantity)
                    {
                        if (lowestPriceForBatch == 0 || shop.Value.ProductCollection[productId].Price * quantity < lowestPriceForBatch)
                        {
                            lowestPriceForBatch = shop.Value.ProductCollection[productId].Price * quantity;
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

            Console.WriteLine($"Lowest price for {quantity} of \"{cheapestShop.ProductCollection[productId].ProductInfo.Name}\": {lowestPriceForBatch}; In: {cheapestShop.Name}");
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

        public Shop this[string shopId]
        {
            get
            {
                if (_shopNetwork.ContainsKey(shopId))
                {
                    return _shopNetwork[shopId];
                }

                throw new ArgumentException("No shop with such ID is present in the network");
            }
        }

    }
}