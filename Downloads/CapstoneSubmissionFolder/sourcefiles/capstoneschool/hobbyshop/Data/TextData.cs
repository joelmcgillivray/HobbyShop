/**
 * Author: Joel McGillivray
 * 
 * Brief summary of page: 
 * This is the TextData class that contains all the variables for the table to be built in the db
 * for when an admin is updating the Newsworthy box, and when a guest or user sees the newsworthy text box
 */

namespace hobbyshop.Data
{
    public class TextData
    {
        /// <summary>
        /// The ID of the content
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The content that will be stored
        /// </summary>
        public string Content { get; set; }
    }
}
