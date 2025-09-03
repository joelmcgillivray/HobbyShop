using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

/**
 * Author: Joel McGillivray
 * 
 * Brief summary of page: 
 * This is the Order class that contains all the variables for the table to be built in the db
 * for when a user purchases one or more items and is successful it creates an order
 */

namespace hobbyshop.Data
{
    public class Order
    {
        /// <summary>
        /// Order ID key
        /// </summary>
        [Key]
        public int OrderID { get; set; }
        /// <summary>
        /// The user id that the order is connected to
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// The status ID of the order, this status comes from Status class
        /// </summary>
        [ForeignKey("Status")]
        public int StatusID { get; set; }
        /// <summary>
        /// When new order is made it takes on the date time of when it happens
        /// </summary>
        public DateTime Date { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// If the order gets deleted, out of scope for project
        /// </summary>
        public bool IsDeleted { get; set; } = false;
        /// <summary>
        /// Communication so EF Core knows that the Status class is related
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// Communication so EF Core knows that the Order Details class is related
        /// </summary>
        public List<OrderDetails> OrderDetails { get; set; }

    }

}
