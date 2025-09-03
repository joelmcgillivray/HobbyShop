using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

/**
 * Author: Joel McGillivray
 * 
 * Brief summary of page: 
 * This is the Cart class that contains all the variables for the table to be built in the db
 * for when a user gets a cart assigned to them this will stay with them.
 */

namespace hobbyshop.Data
{
    public class Cart
    {
        /// <summary>
        /// Primary key for the Cart
        /// </summary>
        [Key]
        public int CartID { get; set; }
        
        /// <summary>
        /// The user id that the cart is associated with
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Whether the carts deleted or not - out of scope for project
        /// </summary>
        public bool isDeleted { get; set; } = false;
        /// <summary>
        /// The collection of cart items 
        /// </summary>
        public ICollection<CartItem> CartItems { get; set; }
    }


}

