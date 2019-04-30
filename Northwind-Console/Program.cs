using System;
using System.Linq;
using NLog;
using NorthwindConsole.Models;

namespace NorthwindConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("Program started");
            try
            {


                string choice;
                do
                {
                    Console.Clear();
                    Console.WriteLine("1) Add Product");
                    Console.WriteLine("2) Display all Products");
                    Console.WriteLine("3) Display Active Products");
                    Console.WriteLine("4) Display Discontinued Products");
                    Console.WriteLine("5) Search Products");
                    Console.WriteLine("6) Add Category");
                    Console.WriteLine("7) Edit Category");
                    Console.WriteLine("\"q\" to quit");
                    choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Product.addProducts(logger);
                            break;
                        case "2":
                            Product.displayAllProducts(logger);
                            break;
                        case "3":
                            Product.displayActiveProducts(logger);
                            break;
                        case "4":
                            Product.displayDiscontinuedProducts(logger);
                            break;
                        case "5":
                            Product.searchProducts(logger);
                            break;
                        case "6":
                            Category.addCategories(logger);
                            break;
                        case "7":

                            var db = new NorthwindContext();

                            Console.WriteLine("Choose category ID to edit: ");

                            var category = Category.GetCategory(db);


                            if (category != null)
                            {
                                Category UpdatedCategory = Category.InputCategory(db);

                                if (UpdatedCategory != null)
                                {
                                    UpdatedCategory.CategoryId = category.CategoryId;
                                    db.EditCategory(UpdatedCategory);
                                    logger.Info($"Category Id: {UpdatedCategory.CategoryId} updated");
                                }
                            }

                            Console.WriteLine();
                            Console.WriteLine("Press any key to return to menu");
                            Console.ReadLine();
                            break;
                        default:
                            Console.WriteLine("No option chosen");
                            break;
                    }



                    //if (choice == "1")
                    //{

                    //    var db = new NorthwindContext();

                    //    Product product = new Product();

                    //    logger.Info("Choice: Add new product");
                    //    Console.WriteLine("Enter product name: ");
                    //    product.ProductName = Console.ReadLine().ToLower();
                    //    Console.WriteLine("Enter Quantity per unit: ");
                    //    product.QuantityPerUnit = Console.ReadLine();



                    //    Console.WriteLine("Enter unit price: ");

                    //    Decimal unitPrice = Decimal.Parse(Console.ReadLine());
                    //    product.UnitPrice = unitPrice;



                    //    Console.WriteLine("Enter units in stock: ");
                    //    Int16 unitsInStock = Int16.Parse(Console.ReadLine());
                    //    product.UnitsInStock = unitsInStock;

                    //    Console.WriteLine("Enter units on order: ");
                    //    Int16 unitsOnOrder = Int16.Parse(Console.ReadLine());
                    //    product.UnitsOnOrder = unitsOnOrder;

                    //    Console.WriteLine("Enter reorder level: ");
                    //    Int16 reorderLevel = Int16.Parse(Console.ReadLine());
                    //    product.ReorderLevel = reorderLevel;

                    //    Console.WriteLine("Enter Discontinued Y/N");
                    //    bool discontinued;

                    //    if (Console.ReadLine().Equals("T"))
                    //    {
                    //        discontinued = true;
                    //        product.Discontinued = discontinued;
                    //    }
                    //    else
                    //    {
                    //        discontinued = false;
                    //        product.Discontinued = discontinued;
                    //    }

                    //    Console.WriteLine("Enter Category Name: ");
                    //    var categoryName = Console.ReadLine().ToLower();
                    //    var categoryQuery = db.Categories.Where(c => c.CategoryName.Equals(categoryName));
                    //    var categoryID = 0;

                    //    foreach (var ca in categoryQuery)
                    //    {
                    //        categoryID = ca.CategoryId;
                    //    }

                    //    product.CategoryId = categoryID;

                    //    Console.WriteLine("Enter Supplier name: ");
                    //    var supplierName = Console.ReadLine();
                    //    var supplierQuery = db.Suppliers.Where(s => s.CompanyName.Equals(supplierName));
                    //    var supplierID = 0;

                    //    foreach (var s in supplierQuery)
                    //    {
                    //        supplierID = s.SupplierId;
                    //    }

                    //    product.SupplierId = supplierID;

                    //    var isProductValid = true;

                    //    if (db.Products.Any(p => p.ProductName.ToLower() == product.ProductName))
                    //    {
                    //        isProductValid = false;
                    //    }


                    //    if (isProductValid)
                    //    {
                    //        db.addProduct(product);
                    //        logger.Info($"Product {product.ProductName} added");
                    //    }
                    //    else if (!isProductValid)
                    //    {
                    //        logger.Error("Product already exists");
                    //    }

                    //}
                    //else if (choice == "2")
                    //{

                    //    logger.Info("Choice: Display all products");
                    //    var db = new NorthwindContext();

                    //    var productQuery = db.Products.OrderBy(p => p.ProductID);
                    //    logger.Info(productQuery.Count());
                    //    foreach (var p in productQuery)
                    //    {
                    //        Console.WriteLine($"{p.ProductName}");

                    //    }

                    //    Console.WriteLine("\n\nPress any key to return to menu");
                    //    Console.ReadLine();


                    //}
                    //else if (choice == "3")
                    //{

                    //    logger.Info("Choice: Display Active Products");
                    //    var db = new NorthwindContext();

                    //    Console.WriteLine("Active Products");
                    //    Console.WriteLine("-----------------------");

                    //    var ProductQuery = db.Products.Where(p => p.Discontinued == false);


                    //    foreach (var p in ProductQuery)
                    //    {

                    //        Console.WriteLine($"{p.ProductName}");
                    //    }

                    //    Console.WriteLine("\n\nPress any key to return to menu");
                    //    Console.ReadLine();


                    //}
                    //else if (choice == "4")
                    //{

                    //    logger.Info("Choice: Display Discontinued Products");
                    //    var db = new NorthwindContext();

                    //    Console.WriteLine("Discontinued Products");
                    //    Console.WriteLine("-----------------------");

                    //    var ProductQuery = db.Products.Where(p => p.Discontinued == true);


                    //    foreach (var p in ProductQuery)
                    //    {

                    //        Console.WriteLine($"{p.ProductName}");
                    //    }

                    //    Console.WriteLine("\n\nPress any key to return to menu");
                    //    Console.ReadLine();
                    //}
                    //else if (choice == "5")
                    //{

                    //    logger.Info("Choice: Search Products");
                    //    var db = new NorthwindContext();

                    //    Console.WriteLine("Enter product name: ");
                    //    string name = Console.ReadLine().ToLower();

                    //    logger.Info($"User search for {name.ToUpper()}");


                    //    var searchProduct = db.Products.Where(p => p.ProductName.Equals(name));

                    //    if (searchProduct.Any())
                    //    {

                    //        Console.WriteLine($"Search Results for {name.ToUpper()}");
                    //        foreach (var p in searchProduct)
                    //        {
                    //            Console.WriteLine($"Id: {p.ProductID}");
                    //            Console.WriteLine($"Quantity Per Unit: {p.QuantityPerUnit}");
                    //            Console.WriteLine($"Unit Price: {p.UnitPrice}");
                    //            Console.WriteLine($"Units In Stock: {p.UnitsInStock}");
                    //            Console.WriteLine($"Units On Order: {p.UnitsOnOrder}");
                    //            Console.WriteLine($"Reorder Level: {p.ReorderLevel}");
                    //            Console.WriteLine($"Discontinued: {p.Discontinued}");
                    //            Console.WriteLine("---------------------");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine($"There were {searchProduct.Count()} products that matched \"{name.ToUpper()}\"");
                    //    }
                    //    Console.WriteLine();
                    //    Console.WriteLine("Press any key to return to menu");
                    //    Console.ReadLine();
                    //}





                } while (choice.ToLower() != "q");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            logger.Info("Program ended");
        }



    }
}
