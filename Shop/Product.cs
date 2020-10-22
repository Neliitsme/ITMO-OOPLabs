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
            if (!string.IsNullOrEmpty(product.Name))
            {
                return true;
            }

            throw new ArgumentException("Product is missing some field assignment or contains values that are not allowed");
        }

        public string Name { get; set; }
        public string Id { get; set; }

        public Product() => Id = Guid.NewGuid().ToString();

        public Product(string name)
        {
            Name = name;
            Id = Guid.NewGuid().ToString();
        }
    }
}