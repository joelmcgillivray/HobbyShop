using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

/**
 * Author: Joel McGillivray
 * 
 * Brief summary of page: 
 * This is the db context that will contain all the identity scaffolded items
 */

namespace hobbyshop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        /// <summary>
        /// This sets up the identity in its own db context so not to mix and match with other input data
        /// </summary>
        /// <param name="options">options as per project setup</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    }
}
