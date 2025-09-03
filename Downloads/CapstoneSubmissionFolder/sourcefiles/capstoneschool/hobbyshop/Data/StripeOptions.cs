/**
 * Author: Joel McGillivray
 * 
 * Brief summary of page: 
 * This is the Stripe Options class that contains all the information to purchase items with stripe
 * for when a user purchases items
 */

namespace hobbyshop.Data;

public class StripeOptions
{
    /// <summary>
    /// The key that is required for the stripe api to work
    /// </summary>
    public string PublishableKey { get; set; }
    /// <summary>
    /// The secret key given to owners that are using the api
    /// </summary>
    public string SecretKey { get; set; }
}
