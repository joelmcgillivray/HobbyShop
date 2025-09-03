using System.ComponentModel.DataAnnotations;

/**
 * Author: Joel McGillivray
 * 
 * Brief summary of page: 
 * This is the Category class that contains all the variables for the table to be built in the db
 * for when an admin assigns a category to the item they create
 */

namespace hobbyshop.Data
{
    public class Category
    {
        /// <summary>
        /// Primary key for Category
        /// </summary>
        [Key]
        public int CategoryID { get; set; }
        /// <summary>
        /// The name of the category
        /// </summary>
        public string CategoryName { get; set; }
    }
}
