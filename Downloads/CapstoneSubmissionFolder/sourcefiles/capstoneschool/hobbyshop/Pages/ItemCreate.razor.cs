using hobbyshop.Data;
using Microsoft.EntityFrameworkCore;

/**
* Author: Joel McGillivray
*
* Brief summary of page:
* This page is the backend code to the ItemCreate page. This controls all the behaviour of every button, search
* creation, updating and archiving of the Items that are created by the admin.
*/

namespace hobbyshop.Pages
{
    public partial class ItemCreate
    {
        /// <summary>
        /// Boolean to show the creation of a new item
        /// </summary>
        public bool showCreate { get; set; }
        /// <summary>
        /// Boolean to show the editing of one item that is selected for edit
        /// </summary>
        public bool editItem { get; set; }
        /// <summary>
        /// The database context that houses all the information
        /// </summary>
        private DataContext _context;
        /// <summary>
        /// The currently created new item
        /// </summary>
        public Item newItem { get; set; }
        /// <summary>
        /// Current Item being edited
        /// </summary>
        public int EditingItemId { get; set; }
        /// <summary>
        /// Item that is being updated
        /// </summary>
        public Item? ItemToUpdate { get; set; }
        /// <summary>
        /// List of all items in the inventory
        /// </summary>
        public List<Item>? InventoryItems{ get; set; }
        /// <summary>
        /// List of all possible categories
        /// </summary>
        private List<Category>categories { get; set; } = new List<Category>();
        /// <summary>
        /// List of all possible conditions
        /// </summary>
        private List<Condition>condition{ get; set; } = new List<Condition>();
        /// <summary>
        /// List of all possible tags
        /// </summary>
        private List<Tag>tags{ get; set; } = new List<Tag>();
        /// <summary>
        /// This is for filtering on historical inventory items
        /// </summary>
        private string selectedHistoricalFilter { get; set; } = "false";
        /// <summary>
        /// This is to allow for easier searching by the admins
        /// </summary>
        public string searchTerm { get; set; } = string.Empty;
        /// <summary>
        /// Current page that a user is on
        /// </summary>
        int currentPage = 1;
        /// <summary>
        /// Total number of items per page
        /// </summary>
        int pageSize = 10;
        /// <summary>
        /// Total count of the items
        /// </summary>
        int totalCount = 0;
        /// <summary>
        /// Boolean value to determine if the page is the first page
        /// </summary>
        bool IsFirstPage => currentPage == 1;
        /// <summary>
        /// Boolean value to determine if the page is the last page
        /// </summary>
        bool IsLastPage => currentPage >= TotalPages;
        /// <summary>
        /// Getting the total amount of pages that should be displayed for the UI, if 0 then always 1
        /// </summary>
        int TotalPages => totalCount > 0 ? (int)Math.Ceiling((double)totalCount / pageSize) : 1;
        /// <summary>
        /// Shows whether an attempt was made to save with invalid data
        /// </summary>
        private bool saveAttempted = false;


        /// <summary>
        /// The initial setup of the page. Setting showCreate boolean to false. Creating the context
        /// loading in the categories, conditions, and tags, then showing all the items with the default 
        /// historical filter and page 1.
        /// </summary>
        /// <returns>No return</returns>
        protected override async Task OnInitializedAsync()
        {
            showCreate = false;
            _context = DataContextFactory.CreateDbContext();
            categories = await _context.Category.ToListAsync();
            condition = await _context.Condition.ToListAsync();
            tags = await _context.Tags.ToListAsync();
            await ShowItems(selectedHistoricalFilter, 1);
        }

        /// <summary>
        /// Whether to show the creation of an item form or not, if its true then we create a new item
        /// </summary>
        public void createForm()
        {
            showCreate = true;
            newItem = new Item();
        }

        /// <summary>
        /// If we cancel the creation of a new item we set showCreate to false to show the admin 
        /// what should be on the page
        /// </summary>
        public void CancelCreateItem()
        {
            showCreate = false;
        }

        /// <summary>
        /// Creating a new item as long as its not null we are setting the historical value to false (as 
        /// its a new item) and filling in all the values from the create form and saving. We then ensure we're showing
        /// the proper active items only and then making showCreate false
        /// </summary>
        /// <returns>No return</returns>
        public async Task createNewItem()
        {
            saveAttempted = true;
            if (CanSaveItem)
            {
                _context ??= await DataContextFactory.CreateDbContextAsync();

                if (newItem is not null)
                {
                    newItem.Historical = false;

                    await _context.Items.AddAsync(newItem);
                    await _context?.SaveChangesAsync();
                }
                await ShowItems(selectedHistoricalFilter, currentPage);
                showCreate = false;
            }
        }

        /// <summary>
        /// This method will ensure that the item can actually be created. There are 4 fields which need to be validated.
        /// These are the most important to ensure that an Item isn't created with no data at all. 
        /// </summary>
        public bool CanSaveItem
        {
            get
            {
                bool isItemNameValid = !string.IsNullOrWhiteSpace(newItem?.ItemName);
                bool isDescriptionValid = !string.IsNullOrWhiteSpace(newItem?.Description);

                bool isPriceValid = false;
                bool isStockValid = false;

                if (newItem?.Price.HasValue == true)
                {
                    isPriceValid = double.TryParse(newItem.Price.ToString(), out double _);
                }

                if (newItem?.Stock.HasValue == true)
                {
                    isStockValid = int.TryParse(newItem.Stock.ToString(), out int _);
                }

                    return isItemNameValid && isDescriptionValid && isPriceValid && isStockValid;
            }
        }

        /// <summary>
        /// Showing the items based on the selected historical value (active/inactive) and showing all the items 
        /// of page 1 (landing page)
        /// </summary>
        /// <param name="selectedHistoricalFilter">Historical value can be true or false, true is inactive and false
        /// is currently active items, or showing all the items if selected</param>
        /// <param name="pageNumber">The page number that a user should be on upon reloads (1) </param>
        /// <returns>No return</returns>
        public async Task ShowItems(string selectedHistoricalFilter, int pageNumber = 1)
        {
            using var context = await DataContextFactory.CreateDbContextAsync();

            if (context is not null)
            {
                var activeItems = context.Items.AsQueryable();
                if (selectedHistoricalFilter == "All")
                {
                    activeItems = activeItems.Where(item => item.Historical.HasValue);
                }
                else
                {
                    bool filterValue = bool.Parse(selectedHistoricalFilter);
                    activeItems = activeItems.Where(item => item.Historical == filterValue);
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    activeItems = activeItems.Where(item => item.ItemName.Contains(searchTerm));
                }

                totalCount = await activeItems.CountAsync();

                // Applying pagination
                InventoryItems = await activeItems
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(item => item.ItemID)
                    .ToListAsync();

                // Set the current page (1)
                currentPage = Math.Min(pageNumber, TotalPages);
                StateHasChanged(); 
            }
        }

        /// <summary>
        /// Setting the category name based on the category ID
        /// </summary>
        /// <param name="categoryID">The unique value of the category</param>
        /// <returns>If there is a value return it, is there isn't return no category</returns>
        private string GetCategoryName(int? categoryID)
        {
            if (categoryID.HasValue)
            {
                var category = categories.FirstOrDefault(c => c.CategoryID == categoryID.Value);
                return category?.CategoryName ?? "Unknown Category";
            }

            return "No Category";
        }

        /// <summary>
        /// Setting the condition name based on the condition ID
        /// </summary>
        /// <param name="conditionID">The unique value of the condition</param>
        /// <returns>If there is a value return it, is there isn't return no condition</returns>
        private string GetConditionName(int? conditionID)
        {
            if (conditionID.HasValue)
            {
                var conditionObj = condition.FirstOrDefault(c => c.ConditionID == conditionID.Value);
                return conditionObj?.ConditionName ?? "Unknown Condition";
            }

            return "No Condition";
        }

        /// <summary>
        /// Setting the tag name based on the tag ID
        /// </summary>
        /// <param name="tagID">The unique value of the tag</param>
        /// <returns>If there is a value return it, is there isn't return no tag</returns>
        private string GetTagName(int? TagID)
        {
            if (TagID.HasValue)
            {
                var tagObj = tags.FirstOrDefault(c => c.TagID == TagID.Value);
                return tagObj?.TagName ?? "Unknown Tag";
            }

            return "No Tag";
        }

        
        /// <summary>
        /// Updating the specific item that is selected by admin, which populates 
        /// the Item form to be used and updated
        /// </summary>
        /// <param name="itemToUpdate">The item that is selected to be updated by admin</param>
        /// <returns>No return</returns>
        public async Task ItemUpdateForm(Item itemToUpdate)
        {
            using var context = await DataContextFactory.CreateDbContextAsync();

            ItemToUpdate = context.Items.FirstOrDefault(x => x.ItemID == itemToUpdate.ItemID);
            // For the display to change
            editItem = true;
            // Item ID that we're currently editing
            EditingItemId = itemToUpdate.ItemID; 
        }

        /// <summary>
        /// Updating the specific item that has been selected
        /// </summary>
        /// <returns>No return</returns>
        public async Task UpdateItem()
        {
            using var context = await DataContextFactory.CreateDbContextAsync();

            if (context is not null)
            {
                if (ItemToUpdate is not null) context.Items.Update(ItemToUpdate);
                await context.SaveChangesAsync();
            }

            await ShowItems(selectedHistoricalFilter, currentPage);
            // Returns the screen back to all items
            editItem = false;
        }

        /// <summary>
        /// Soft Deleting the item -- Making the item historical in the system.
        /// </summary>
        /// <param name="itemToUpdate">The item that is being updated</param>
        /// <returns>No return</returns>
        public async Task UpdateHistoricalFlag(Item itemToUpdate)
        {
            using var context = await DataContextFactory.CreateDbContextAsync();

            if (itemToUpdate is not null && _context is not null)
            {
                // Toggle the Historical flag
                itemToUpdate.Historical = !itemToUpdate.Historical.GetValueOrDefault();

                // Update the item in the context
                context.Entry(itemToUpdate).Property(x => x.Historical).IsModified = true;
                await context.SaveChangesAsync();

            }

            await ShowItems(selectedHistoricalFilter, currentPage);
        }

        /// <summary>
        /// Getting all the items that match the admin entered search term
        /// </summary>
        /// <param name="searchTerm">Value entered by the admin</param>
        /// <returns>The items that resulted from the query</returns>
        public async Task<List<Item>> GetItems(string searchTerm = "")
        {
            using var context = await DataContextFactory.CreateDbContextAsync();

            IQueryable<Item> itemQuery = context.Items;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                itemQuery = itemQuery.Where(item => item.ItemName.Contains(searchTerm));
            }

            return await itemQuery.ToListAsync();
        }

        /// <summary>
        /// Gets the items based on the search term and updates the UI
        /// </summary>
        /// <returns>No return</returns>
        public async Task ApplySearch()
        {
            InventoryItems = await GetItems(searchTerm);
            StateHasChanged();
        }

        /// <summary>
        /// Method for next page and updates with showItems method
        /// </summary>
        /// <returns>No return</returns>
        public async Task NextPage()
        {
            if (currentPage < TotalPages)
            {
                await ShowItems(selectedHistoricalFilter, currentPage + 1);
            }
        }

        /// <summary>
        /// Method for previous page and updates with showItems method
        /// </summary>
        /// <returns>No return</returns>
        async Task PreviousPage()
        {
            if (currentPage > 1)
            {
                await ShowItems(selectedHistoricalFilter, currentPage - 1);
            }
        }

        /// <summary>
        /// Resets the page to its initial state, as if it was just loaded.
        /// </summary>
        /// <returns>No return</returns>
        public async Task ResetPage()
        {
            selectedHistoricalFilter = "false";
            searchTerm = string.Empty;
            currentPage = 1;

            showCreate = false;
            editItem = false;

            await ShowItems(selectedHistoricalFilter, 1);
        }
    }
}
