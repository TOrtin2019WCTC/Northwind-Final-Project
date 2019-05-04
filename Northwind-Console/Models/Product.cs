using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NLog;

namespace NorthwindConsole.Models
{


    public class Product
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage = "ENTER THE NAME!")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "ENTER QUANTITY PER UNIT")]
        public string QuantityPerUnit { get; set; }
        [Required(ErrorMessage = "ENTER UNIT PRICE")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(-9999999999999999.99, 9999999999999999.99)]
        public decimal? UnitPrice { get; set; }
        [Required(ErrorMessage = "ENTER UNITS IN STOCK")]
        [Range(0, int.MaxValue, ErrorMessage = "ENTER A VALID NUMBER")]
        public Int16? UnitsInStock { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "ENTER A VALID NUMBER")]
        public Int16? UnitsOnOrder { get; set; }
        [Required(ErrorMessage = "ENTER REORDER LEVEL")]
        [Range(0, int.MaxValue, ErrorMessage = "ENTER A VALID NUMBER")]
        public Int16? ReorderLevel { get; set; }
        [Required(ErrorMessage = "ENTER Y/N FOR DISCONTINUED")]
        public bool Discontinued { get; set; }

        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Supplier Supplier { get; set; }

        public static void displayAllProducts(Logger logger)
        {
            logger.Info("Choice: Display all products");
            var db = new NorthwindContext();

            var productQuery = db.Products.OrderBy(p => p.ProductID);
            logger.Info(productQuery.Count());
            foreach (var p in productQuery)
            {
                Console.WriteLine($"{p.ProductName}");

            }

            Console.WriteLine("\n\nPress any key to return to menu");
            Console.ReadLine();


        }

        public static void addProducts(Logger logger)
        {
            var db = new NorthwindContext();

            Product product = new Product();

            logger.Info("Choice: Add new product");
            Console.WriteLine("Enter product name: ");
            product.ProductName = Console.ReadLine().ToLower();
            Console.WriteLine("Enter Quantity per unit: ");
            product.QuantityPerUnit = Console.ReadLine();



            Console.WriteLine("Enter unit price: ");

            Decimal unitPrice = Decimal.Parse(Console.ReadLine());
            product.UnitPrice = unitPrice;



            Console.WriteLine("Enter units in stock: ");
            Int16 unitsInStock = Int16.Parse(Console.ReadLine());
            product.UnitsInStock = unitsInStock;

            Console.WriteLine("Enter units on order: ");
            Int16 unitsOnOrder = Int16.Parse(Console.ReadLine());
            product.UnitsOnOrder = unitsOnOrder;

            Console.WriteLine("Enter reorder level: ");
            Int16 reorderLevel = Int16.Parse(Console.ReadLine());
            product.ReorderLevel = reorderLevel;

            Console.WriteLine("Enter Discontinued Y/N");
            string disc = Console.ReadLine().ToLower();
            bool discontinued;

            if (disc != null && disc.Equals("y") || disc.Equals("n"))
            {
                if (disc.Equals("y"))
                {
                    discontinued = true;
                    product.Discontinued = discontinued;
                }
                else if (disc.Equals("n"))
                {
                    discontinued = false;
                    product.Discontinued = discontinued;
                }


                Console.WriteLine("Enter Category Name: ");
                var categoryName = Console.ReadLine().ToLower();
                var categoryQuery = db.Categories.Where(c => c.CategoryName.Equals(categoryName));
                var categoryID = 0;

                foreach (var ca in categoryQuery)
                {
                    categoryID = ca.CategoryId;
                }

                product.CategoryId = categoryID;

                Console.WriteLine("Enter Supplier name: ");
                var supplierName = Console.ReadLine();
                var supplierQuery = db.Suppliers.Where(s => s.CompanyName.Equals(supplierName));
                var supplierID = 0;

                foreach (var s in supplierQuery)
                {
                    supplierID = s.SupplierId;
                }

                product.SupplierId = supplierID;

                ValidationContext context = new ValidationContext(product, null, null);
                List<ValidationResult> results = new List<ValidationResult>();

                var isValid = Validator.TryValidateObject(product, context, results, true);

                //var isProductValid = true;

                if (db.Products.Any(p => p.ProductName.ToLower() == product.ProductName))
                {
                    //isProductValid = false;
                    isValid = false;
                    results.Add(new ValidationResult("Product already exists", new string[] { product.ProductName }));


                }


                if (isValid)
                {
                    logger.Info("Validation Passed");
                    db.addProduct(product);
                    logger.Info($"Product {product.ProductName} added");
                }
                else if (!isValid)
                {
                    foreach (var result in results)
                    {
                        logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                    }
                    //logger.Error("Product already exists");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();



        }

        public static void searchProducts(Logger logger)
        {
            logger.Info("Choice: Search Products");
            var db = new NorthwindContext();

            Console.WriteLine("Enter product name: ");
            string name = Console.ReadLine().ToLower();

            logger.Info($"User search for {name.ToUpper()}");


            var searchProduct = db.Products.Where(p => p.ProductName.Equals(name));

            if (searchProduct.Any())
            {

                Console.WriteLine($"Search Results for {name.ToUpper()}");
                foreach (var p in searchProduct)
                {
                    Console.WriteLine($"Product ID: {p.ProductID}");
                    Console.WriteLine($"Supplier ID: {p.SupplierId}");
                    Console.WriteLine($"Category ID: {p.CategoryId}");
                    Console.WriteLine($"Quantity Per Unit: {p.QuantityPerUnit}");
                    Console.WriteLine($"Unit Price: {p.UnitPrice}");
                    Console.WriteLine($"Units In Stock: {p.UnitsInStock}");
                    Console.WriteLine($"Units On Order: {p.UnitsOnOrder}");
                    Console.WriteLine($"Reorder Level: {p.ReorderLevel}");
                    Console.WriteLine($"Discontinued: {p.Discontinued}");
                    Console.WriteLine("---------------------");
                }
            }
            else
            {
                Console.WriteLine($"There were {searchProduct.Count()} products that matched \"{name.ToUpper()}\"");
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }


        public static void displayDiscontinuedProducts(Logger logger)
        {
            logger.Info("Choice: Display Discontinued Products");
            var db = new NorthwindContext();

            Console.WriteLine("Discontinued Products");
            Console.WriteLine("-----------------------");

            var ProductQuery = db.Products.Where(p => p.Discontinued == true);


            foreach (var p in ProductQuery)
            {

                Console.WriteLine($"{p.ProductName}");
            }

            Console.WriteLine("\n\nPress any key to return to menu");
            Console.ReadLine();
        }

        public static void displayActiveProducts(Logger logger)
        {

            logger.Info("Choice: Display Active Products");
            var db = new NorthwindContext();

            Console.WriteLine("Active Products");
            Console.WriteLine("-----------------------");

            var ProductQuery = db.Products.Where(p => p.Discontinued == false);


            foreach (var p in ProductQuery)
            {

                Console.WriteLine($"{p.ProductName}");
            }

            Console.WriteLine("\n\nPress any key to return to menu");
            Console.ReadLine();

        }
    }
}
