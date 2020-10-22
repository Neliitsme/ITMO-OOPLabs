using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Xml.Schema;

namespace Shop
{
    public class Shop
    {
        public class StoredProduct
        {
            public Product ProductInfo { get; set; }
            public int Price { get; set; }
            public int Quantity { get; set; }
        }

        public static bool CheckShop(Shop shop)
        {
            if (!string.IsNullOrEmpty(shop.Name) && !string.IsNullOrEmpty(shop.Address))
            {
                return true;
            }
            throw new ArgumentException("Shop is missing some field assignment or contains values that are not allowed");
        }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Address { get; set; }

        public readonly Dictionary<string, StoredProduct> ProductCollection = new Dictionary<string, StoredProduct>();

        public Shop() => Id = Guid.NewGuid().ToString();

        public Shop(string shopName, string address)
        {
            Name = shopName;
            Address = address;
            Id = Guid.NewGuid().ToString();
        }

        public string GetProductId(string productName)
        {
            foreach (var product in ProductCollection)
            {
                if (product.Value.ProductInfo.Name == productName)
                {
                    return product.Value.ProductInfo.Id;
                }
            }

            throw new ArgumentException("No product with such name exists in this shop");
        }

        public Product GetProduct(string productId)
        {
            if (ProductCollection.ContainsKey(productId))
            {
                return ProductCollection[productId].ProductInfo;
            }

            throw new ArgumentException("No product with such name exists");
        }

        public void RemoveProduct(string productId)
        {
            if (ProductCollection.ContainsKey(productId))
            {
                ProductCollection.Remove(productId);
                return;
            }
            throw new ArgumentException("No product with such ID exists in this shop");
        }

        public void RemoveProduct(Product product)
        {
            if (ProductCollection.ContainsKey(product.Id))
            {
                ProductCollection.Remove(product.Id);
                return;
            }
            throw new ArgumentException("No such product exists in this shop");
        }

        public void AddProduct(Product product, int quantity = 1, int price = 1)
        {
            if (quantity <= 0 || price <= 0)
            {
                throw new ArgumentException("Quantity and/or price below zero is now allowed");
            }

            Product.CheckProduct(product);
            if (ProductCollection.ContainsKey(product.Id))
            {
                ProductCollection[product.Id].Quantity += quantity;
                ProductCollection[product.Id].Price = price;
                return;
            }

            ProductCollection.Add(product.Id, new StoredProduct(){ProductInfo = product, Price = price, Quantity = quantity});
        }

        public void ChangeProductPrice(string productId, int newPrice)
        {
            if (ProductCollection.ContainsKey(productId))
            {
                ProductCollection[productId].Price = newPrice;
                return;
            }

            throw new ArgumentException("No product with such ID exists in this shop");
        }

        public void PrintShopContents()
        {
            Console.WriteLine("List of all products in " + Name);
            foreach (var product in ProductCollection)
            {
                Product.CheckProduct(product.Value.ProductInfo);
                Console.WriteLine($"\tName: {product.Value.ProductInfo.Name}; \tPrice: {product.Value.Price}; \tQuantity: {product.Value.Quantity}; \tId: {product.Value.ProductInfo.Id}");
            }
        }

        public void WhatCanIBuy(int budget)
        {
            Console.WriteLine("You can buy:");
            foreach (var product in ProductCollection)
            {
                Product.CheckProduct(product.Value.ProductInfo);
                int totalAmount = budget / product.Value.Price;
                if (totalAmount > product.Value.Quantity)
                {
                    totalAmount = product.Value.Quantity;
                }

                Console.WriteLine($"\t{product.Value.ProductInfo.Name}: Quantity - {totalAmount}, for {totalAmount * product.Value.Price}");
            }
        }

        public int BuyProductBatch(string productId, int quantity = 1)
        {
            if (ProductCollection.ContainsKey(productId))
            {
                Product.CheckProduct(ProductCollection[productId].ProductInfo);
                if (ProductCollection[productId].Quantity < quantity)
                {
                    throw new ArgumentException("Shop doesn't have the required amount of product you're trying to buy");
                }
                ProductCollection[productId].Quantity -= quantity;

                int totalPrice = ProductCollection[productId].Price * quantity;

                Console.WriteLine($"Total for {quantity} of \"{ProductCollection[productId].ProductInfo.Name}\" is: {totalPrice}");

                if (ProductCollection[productId].Quantity == 0)
                {
                    ProductCollection.Remove(productId);
                }

                return totalPrice;
            }

            throw new ArgumentException("There's no product with such name");
        }

    }
}