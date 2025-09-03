using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
 * Author: Joel McGillivray
 * 
 * Brief summary of page: 
 * This is the Item class that contains all the variables for the table to be built in the db
 * for when an admin creates an item to be used and displayed on many screens as well as to carts, orders etc.
 */

namespace hobbyshop.Data
{
    public class Item
    {
        /// <summary>
        /// The Item ID key
        /// </summary>
        [Key]
        public int ItemID { get; set; }
        /// <summary>
        /// The name of the items set
        /// </summary>
        public string? SetName { get; set; }
        /// <summary>
        /// The actual name of the item
        /// </summary>
        public string? ItemName { get; set; }
        /// <summary>
        /// Setting the tagID of the item if applicable from the Tag class
        /// </summary>
        [ForeignKey("TagID")]
        public int? TagID { get; set; } 
        /// <summary>
        /// Description of the item
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Possible image to be loaded of the item
        /// </summary>
        public string? Image { get; set; }
        /// <summary>
        /// Price of the item
        /// </summary>
        public double? Price { get; set; }
        /// <summary>
        /// Category ID of the item from the Category class
        /// </summary>
        [ForeignKey("CategoryID")]
        public int? CategoryID { get; set; }   
        /// <summary>
        /// Stock value of the item
        /// </summary>
        public int? Stock { get; set; }
        /// <summary>
        /// Condition of the item if applicable
        /// </summary>
        public int? Condition { get; set; }
        /// <summary>
        /// Rather than hard deletions this will be used for soft deleting of data - default on item create is false
        /// </summary>
        [DefaultValue(false)]
        public bool? Historical { get; set; }
        /// <summary>
        /// Categoy class to ensure EF Core knows it can communicate with this class
        /// </summary>
        public Category Category { get; set; }
        /// <summary>
        /// Tag class to ensure EF Core knows it can communicate with this class
        /// </summary>
        public Tag Tag { get; set; }
        /// <summary>
        /// An item can be related to many Order Details, needs to communicate with this class
        /// </summary>
        public List<OrderDetails> OrderDetails { get; set; }
        /// <summary>
        /// An item can be related to many CartItem, needs to communicate with this class
        /// </summary>
        public List<CartItem> CartItem { get; set; }
    }
}
