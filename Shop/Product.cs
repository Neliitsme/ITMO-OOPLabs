using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Shop
{
    public class Product
    {
        public static bool CheckProduct(Product product)
        {
            if (!string.IsNullOrEmpty(product.Name) && product.Price <= 0 && product.Quantity <= 0)
            {
                return true;
            }

            throw new ArgumentException("Product is missing some field assignment or contains values that are not allowed");
        }

        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Id { get; set; }

        public Product()
        {
            Id = Guid.NewGuid().ToString(); 
            Price = 1;
            Quantity = 1;
        }
        public Product(string name, int quantity = 1, int price = 1)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            Id = Guid.NewGuid().ToString();
        }
    }
}