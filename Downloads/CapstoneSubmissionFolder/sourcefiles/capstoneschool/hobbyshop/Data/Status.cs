using System.ComponentModel.DataAnnotations;

/**
 * Author: Joel McGillivray
 * 
 * Brief summary of page: 
 * This is the Status class that contains all the variables for the table to be built in the db
 * for when an admin assigns a status to the item
 */

namespace hobbyshop.Data
{
    public class Status
    {
        /// <summary>
        /// The Primary key of the status 
        /// </summary>
        [Key]
        public int StatusID { get; set; }
        /// <summary>
        /// The name of the status
        /// </summary>
        public string StatusName { get; set; }
    }
}
