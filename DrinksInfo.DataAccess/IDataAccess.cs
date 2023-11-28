using DrinksInfo.DataAccess.Models;

namespace DrinksInfo.DataAccess;

public interface IDataAccess
{
    public Task<List<Category>> GetCategoriesAsync();
}
