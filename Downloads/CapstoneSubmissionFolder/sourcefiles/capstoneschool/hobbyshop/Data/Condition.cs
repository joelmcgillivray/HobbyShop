using System.ComponentModel.DataAnnotations;

/**
 * Author: Joel McGillivray
 * 
 * Brief summary of page: 
 * This is the Condition class that contains all the variables for the table to be built in the db
 * for when an admin assigns a condition to the item they create
 */

namespace hobbyshop.Data
{
    public class Condition
    {
        /// <summary>
        /// Primary key for Condiion
        /// </summary>
        [Key]
        public int ConditionID { get; set; }
        /// <summary>
        /// The name of the condition
        /// </summary>
        public string ConditionName { get; set; }
    }
}
