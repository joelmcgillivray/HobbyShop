using System.ComponentModel.DataAnnotations;

/**
 * Author: Joel McGillivray
 * 
 * Brief summary of page: 
 * This is the Tag class that contains all the variables for the table to be built in the db
 * for when an admin assigns a Tag to the item
 */

namespace hobbyshop.Data
{
    public class Tag
    {
        /// <summary>
        /// The Primary key of the tag class
        /// </summary>
        [Key]
        public int TagID { get; set; }
        /// <summary>
        /// The name of the tag
        /// </summary>
        public string TagName { get; set; }
    }

}
