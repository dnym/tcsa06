using DrinksInfo.DataAccess.Models;

namespace DrinksInfo.DataAccess;

public class Mock : IDataAccess
{
    public Task<List<Category>> GetCategoriesAsync()
    {
        var categories = new List<Category>
        {
            new("Beer"),
            new("Cocktail"),
            new("Cocoa"),
            new("Coffee / Tea"),
            new("Homemade Liqueur"),
            new("Ordinary Drink"),
            new("Punch / Party Drink"),
            new("Shake"),
            new("Shot"),
            new("Soft Drink"),
            new("Other / Unknown")
        };
        return Task.FromResult(categories);
    }
}
