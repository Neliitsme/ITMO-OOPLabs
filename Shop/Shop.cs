using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Xml.Schema;

namespace Shop
{
    public class Shop
    {
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
        public Dictionary<string, Product> ProductInfoDictionary = new Dictionary<string, Product>();

        public Shop() => Id = Guid.NewGuid().ToString();

        public Shop(string shopName, string address)
        {
            Name = shopName;
            Address = address;
            Id = Guid.NewGuid().ToString();
        }

        public void CreateProduct(string productName, int productQuantity, int productPrice)
        {
            Product product = new Product();
            product.Name = productName;
            product.Quantity = productQuantity;
            product.Price = productPrice;
            Product.CheckProduct(product);
            ProductInfoDictionary.Add(product.Id, product);
        }

        public string GetProductId(string productName)
        {
            foreach (var product in ProductInfoDictionary)
            {
                if (product.Value.Name == productName)
                {
                    return product.Value.Id;
                }
            }

            throw new ArgumentException("No product with such name exists");
        }


        public Product GetProduct(string productNameOrId)
        {
            if (ProductInfoDictionary.ContainsKey(productNameOrId))
            {
                return ProductInfoDictionary[productNameOrId];
            }

            foreach (var product in ProductInfoDictionary)
            {
                if (product.Value.Name == productNameOrId)
                {
                    return product.Value;
                }
            }

            throw new ArgumentException("No product with such name exists");
        }

        public void RemoveProduct(string productId)
        {
            if (ProductInfoDictionary.ContainsKey(productId))
            {
                ProductInfoDictionary.Remove(productId);
            }
            throw new ArgumentException("No product with such ID exists in this shop");
        }

        public void RemoveProduct(Product product)
        {
            if (ProductInfoDictionary.ContainsValue(product))
            {
                ProductInfoDictionary.Remove(product.Id);
            }
            throw new ArgumentException("No such product exists in this shop");
        }

        public void AddProductBatch(Product product)//Todo: Check and prolly re-make
        {
            Product.CheckProduct(product);

            if (ProductInfoDictionary.ContainsKey(product.Id) && product.Name == ProductInfoDictionary[product.Id].Name)
            {
                ProductInfoDictionary[product.Id].Quantity += product.Quantity;
                ProductInfoDictionary[product.Id].Price = product.Price;
                return;
            }

            Product tempProd = new Product(product.Name, product.Quantity, product.Price);
            tempProd.Id = product.Id;
            
            ProductInfoDictionary.Add(tempProd.Id, tempProd);
        }

        public void ChangeProductPrice(string productId, int newPrice)//Todo: Check and prolly re-make
        {
            ProductInfoDictionary[productId].Price = newPrice;
        }

        public void PrintShopContents()
        {
            Console.WriteLine("List of all products in " + Name);
            foreach (var product in ProductInfoDictionary)
            {
                Product.CheckProduct(product.Value);
                Console.WriteLine($"\tName: {product.Value.Name}; \tPrice: {product.Value.Price}; \tQuantity: {product.Value.Quantity}; \tId: {product.Value.Id}");
            }
        }

        public void WhatCanIBuy(int budget)
        {
            Console.WriteLine("You can buy:");
            foreach (var product in ProductInfoDictionary)
            {
                Product.CheckProduct(product.Value);
                int totalAmount = budget / product.Value.Price;
                if (totalAmount > product.Value.Quantity)
                {
                    totalAmount = product.Value.Quantity;
                }

                Console.WriteLine($"\t{product.Value.Name}: Quantity - {totalAmount}, for {totalAmount * product.Value.Price}");
            }
        }

        public int BuyProductBatch(string productName, int quantity)//Todo: Check and prolly re-make
        {
            foreach (var product in ProductInfoDictionary)
            {
                Product.CheckProduct(product.Value);
                if (product.Value.Name == productName)
                {
                    if (product.Value.Quantity < quantity)
                    {
                        throw new ArgumentException("Shop doesn't have the required amount of product you're trying to buy");
                    }

                    product.Value.Quantity -= quantity;
                    Console.WriteLine("Total was: " + product.Value.Price * quantity);
                    return product.Value.Price * quantity;
                }
            }

            throw new ArgumentException("There's no product with such name");
        }

    }
}