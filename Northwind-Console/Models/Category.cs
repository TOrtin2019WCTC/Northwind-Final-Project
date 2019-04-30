using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NLog;

namespace NorthwindConsole.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Must provide a category name")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Must provide description")]
        public string Description { get; set; }

        public virtual List<Product> Product { get; set; }


        public static void addCategories(Logger logger)
        {

            logger.Info("Choice: Add Category");
            var db = new NorthwindContext();

            Category category = new Category();

            Console.WriteLine("Enter Category Name: ");
            category.CategoryName = Console.ReadLine();

            bool validName;

            if (category.CategoryName.Contains(" "))
            {
                logger.Error("Name cannot contain spaces");
                validName = false;
            }
            else if (db.Categories.Any(c => c.CategoryName.ToLower().Equals(category.CategoryName.ToLower())))
            {
                validName = false;
                logger.Error($"\"{category.CategoryName}\" already exists in database");
            }
            else
            {
                validName = true;
            }

            if (validName)
            {
                Console.WriteLine("Enter Description: ");
                category.Description = Console.ReadLine();

                if (category.Description != null)
                {
                    db.addCategory(category);
                    logger.Info($"{category.CategoryName} Added");
                }
                else
                {
                    logger.Error("Description cannot be null");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();


        }

        public static void displayAllCategories(Logger logger)
        {
            logger.Info("Choice: Display All Categories");
            Console.WriteLine();
            var db = new NorthwindContext();

            var categories = db.Categories.OrderBy(c => c.CategoryId);

            foreach (var item in categories)
            {
                Console.WriteLine($"Name: {item.CategoryName}");
                Console.WriteLine($"Description: {item.Description}");
                Console.WriteLine("-------------------------");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }

        public static void displayCategoriesDiscontinued(Logger logger)
        {

            logger.Info("Choice: Display non-discontinued products by category");
            Console.WriteLine();
            var db = new NorthwindContext();

            var categories = (from p in db.Products
                              join c in db.Categories
                              on p.CategoryId equals c.CategoryId
                              where p.Discontinued == false
                              orderby c.CategoryId

                              select new
                              {
                                  c.CategoryId,
                                  c.CategoryName,
                                  p.ProductName
                              }).ToList();



            foreach (var item in categories)
            {

                Console.WriteLine($"{item.CategoryName}, {item.ProductName}");

            }

            logger.Info(categories.Count());

            Console.WriteLine();
            Console.WriteLine("Press any key to return to menu");
            Console.ReadLine();
        }

        public static Category InputCategory(NorthwindContext db)
        {
            Category category = new Category();

            Console.WriteLine("Enter Category Name: ");
            category.CategoryName = Console.ReadLine();

            Console.WriteLine("Enter description: ");
            category.Description = Console.ReadLine();

            if (category.CategoryName != null && category.Description != null)
            {
                return category;
            }
            else
            {
                Console.WriteLine("name and description cannot be empty");
            }

            return null;


        }

        public static Category GetCategory(NorthwindContext db)
        {
            var categories = db.Categories.OrderBy(c => c.CategoryId);

            foreach (Category c in categories)
            {
                Console.WriteLine($"ID: {c.CategoryId}) Name: {c.CategoryName}");
            }

            if (int.TryParse(Console.ReadLine(), out int CategoryId))
            {
                Category category = db.Categories.FirstOrDefault(c => c.CategoryId == CategoryId);
                if (category != null)
                {
                    return category;
                }

            }
            Console.WriteLine("Invalid category id");
            return null;
        }
    }
}
