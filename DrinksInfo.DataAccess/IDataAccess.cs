using DrinksInfo.DataAccess.Models;

namespace DrinksInfo.DataAccess;

public interface IDataAccess
{
    public Task<List<Category>> GetCategoriesAsync();
    public Task<List<ListDrink>> GetDrinksByCategoryAsync(Category category);
    public Task<Drink?> GetDrinkByIdAsync(int id);
    public Task<List<ListDrink>> SearchDrinksAsync(string searchTerm);
    public Task<List<ListDrink>> GetDrinksByLetterAsync(char initial);
    public Task<List<Glass>> GetGlassesAsync();
    public Task<List<ListDrink>> GetDrinksByGlassAsync(Glass glass);
    public Task<List<Ingredient>> GetIngredientsAsync();
    public Task<List<ListDrink>> GetDrinksByIngredientAsync(Ingredient ingredient);
    public Task<List<AlcoholType>> GetAlcoholTypesAsync();
    public Task<List<ListDrink>> GetDrinksByAlcoholTypeAsync(AlcoholType alcoholType);
}
