using DrinksInfo.DataAccess.Models;

namespace DrinksInfo.DataAccess;

public interface IDataAccess
{
    public Task<List<Category>> GetCategoriesAsync();
    public Task<List<ListDrink>> GetDrinksByCategoryAsync(Category category);
}
