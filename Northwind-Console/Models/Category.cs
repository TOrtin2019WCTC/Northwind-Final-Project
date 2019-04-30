using System;
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
    }
}
