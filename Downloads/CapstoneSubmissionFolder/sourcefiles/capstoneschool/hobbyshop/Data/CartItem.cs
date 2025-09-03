using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
 * Author: Joel McGillivray
 * 
 * Brief summary of page: 
 * This is the CartItem class that contains all the variables for the table to be built in the db
 * for when a user adds an item to their cart this is the detail of that item
 */

namespace hobbyshop.Data
{
	public class CartItem
	{
        /// <summary>
        /// The primary key of the CartItem
        /// </summary>
        [Key]
        public int CartItemID { get; set; }
        /// <summary>
        /// The cart id that this cart item is assocaited with
        /// </summary>
        [ForeignKey("Cart")]
        public int CartID { get; set; }
        /// <summary>
        /// The Item ID that is in the cart
        /// </summary>
        [ForeignKey("Item")]
        public int ItemID { get; set; }
        /// <summary>
        /// The relationship between the cartItems and the cart class
        /// </summary>
        public Cart Cart { get; set; }
        /// <summary>
        /// The relationship between the cartItems and item class
        /// </summary>
        public Item Item { get; set; }
        /// <summary>
        /// The number of that item that is in the cart
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Price of the item that is in the cart
        /// </summary>
        public decimal ItemPrice { get; set; }
    }
}

