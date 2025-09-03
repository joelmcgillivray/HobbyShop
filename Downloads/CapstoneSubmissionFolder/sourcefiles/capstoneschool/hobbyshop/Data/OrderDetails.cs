using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
 * Author: Joel McGillivray
 * 
 * Brief summary of page: 
 * This is the OrderDetails class that contains all the variables for the table to be built in the db
 * for when a user wants to access the items they had in their orders, as well as for admins to view what was purchased
 */

namespace hobbyshop.Data
{
    public class OrderDetails
    {
        /// <summary>
        /// The Order Details ID Key
        /// </summary>
        [Key]
        public int OrderDetailsID { get; set; }
        /// <summary>
        /// The key of the Item
        /// </summary>
        [ForeignKey("Item")]
        public int ItemID { get; set; }
        /// <summary>
        /// The key of the Order
        /// </summary>
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        /// <summary>
        /// The quantity of the item
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// The tax amount
        /// </summary>
        public decimal TaxAmount { get; set; }
        /// <summary>
        /// The Price of the item in the order details
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Communication so EF Core knows that the Item class is related
        /// </summary>
        public Item Item { get; set; }
        /// <summary>
        /// Communication so EF Core knows that the Order class is related
        /// </summary>
        public Order Order { get; set; }
    }
}
