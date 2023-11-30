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

    public Task<List<ListDrink>> GetDrinksByCategoryAsync(Category category)
    {
        var drinks = new List<ListDrink>
        {
            new(15346, "155 Belmont"),
            new(14029, "57 Chevy with a White License Plate"),
            new(178318, "747 Drink"),
            new(16108, "9 1/2 Weeks"),
            new(16943, "A Gilligan's Island"),
            new(17005, "A True Amaretto Sour"),
            new(14560, "A.D.M. (After Dinner Mint)"),
            new(17222, "A1"),
            new(17223, "Abbey Martini"),
            new(14107, "Absolut Summertime"),
            new(17224, "Absolutely Fabulous"),
            new(16134, "Absolutly Screwed Up"),
            new(17225, "Ace"),
            new(17226, "Adam & Eve"),
            new(17227, "Addington"),
            new(17228, "Addison"),
            new(14272, "Addison Special"),
            new(17229, "Adios Amigos Cocktail"),
            new(12560, "Afterglow"),
            new(12562, "Alice Cocktail"),
            new(178321, "Amaretto fizz"),
            new(178325, "Aperol Spritz"),
            new(178353, "Apple Highball"),
            new(12564, "Apple Karate"),
            new(16311, "Applejack"),
            new(178319, "Aquamarine"),
            new(14584, "Arizona Stingers"),
            new(17074, "Arizona Twister"),
            new(17066, "Army special"),
            new(178337, "Autumn Garibaldi"),
            new(17180, "Aviation"),
            new(17267, "Bahama Mama"),
            new(178320, "Banana Cream Pi"),
            new(178317, "Bee's Knees"),
            new(17254, "Bijou"),
            new(17268, "Blue Hurricane"),
            new(178336, "Blueberry Mojito"),
            new(17242, "Bombay Cassis"),
            new(12572, "Bora Bora"),
            new(17251, "Boulevardier"),
            new(178331, "Bounty Hunter"),
            new(17825, "Brigadier"),
            new(178311, "Broadside"),
            new(178310, "Brooklyn"),
            new(178356, "Butterfly Effect"),
            new(178329, "Captain Kidd's Punch"),
            new(17174, "Cherry Electric Lemonade"),
            new(178369, "Cocktail Horse’s Neck"),
            new(17830, "Corn n Oil"),
            new(17250, "Corpse Reviver"),
            new(17196, "Cosmopolitan"),
            new(14133, "Cosmopolitan Martini"),
            new(14608, "Cream Soda"),
            new(17177, "Dark Caipirinha"),
            new(178334, "Death in the Afternoon"),
            new(17181, "Dirty Martini"),
            new(11005, "Dry Martini"),
            new(17182, "Duchamp's Punch"),
            new(178346, "Elderflower Caipirinha"),
            new(17246, "Empellón Cocina's Fat-Washed Mezcal"),
            new(17212, "Espresso Martini"),
            new(178309, "Espresso Rumtini"),
            new(178344, "Figgy Thyme"),
            new(16485, "Flaming Lamborghini"),
            new(17213, "French Martini"),
            new(17248, "French Negroni"),
            new(178352, "Frosé"),
            new(178328, "Funk and Soul"),
            new(12758, "Gagliardo"),
            new(178340, "Garibaldi Negroni"),
            new(17255, "Gimlet"),
            new(178342, "Gin and Soda"),
            new(178314, "Gin Basil Smash"),
            new(178366, "Gin Lemon"),
            new(17230, "Gin Rickey"),
            new(178365, "Gin Tonic"),
            new(17252, "Greyhound"),
            new(178316, "Honey Bee"),
            new(178345, "Hot Toddy"),
            new(17239, "Hunter's Moon"),
            new(12706, "Imperial Cocktail"),
            new(16987, "Irish Curdling Cow"),
            new(16178, "Jitterbug"),
            new(178359, "Kiwi Martini"),
            new(178335, "Lazy Coconut Paloma"),
            new(14366, "Lemon Drop"),
            new(178360, "Lemon Elderflower Spritzer"),
            new(15224, "Malibu Twister"),
            new(178358, "Mango Mojito"),
            new(11008, "Manhattan"),
            new(17256, "Martinez 2"),
            new(11720, "Martinez Cocktail"),
            new(11728, "Martini"),
            new(17188, "Mary Pickford"),
            new(178370, "Mauresque"),
            new(13936, "Miami Vice"),
            new(178343, "Michelada"),
            new(14842, "Midnight Mint"),
            new(11000, "Mojito"),
            new(15841, "Mojito Extra")
        };
        return Task.FromResult(drinks);
    }

    public Task<Drink?> GetDrinkByIdAsync(int id)
    {
        Drink? drink = null;
        if (id > 0)
        {
            drink = new(11007, "Margarita", null,
            new Tag[]
            {
                new("IBA"),
                new("ContemporaryClassic"),
            }, new("Ordinary Drink"), new("Contemporary Classics"),
            new("Alcoholic"), new("Cocktail glass"),
            new Ingredient[]
            {
                new("Tequila"),
                new("Triple sec"),
                new("Lime juice"),
                new("Salt")
            },
            new string[]
            {
                "1 1/2 oz ",
                "1/2 oz ",
                "1 oz "
            },
            "Rub the rim of the glass with the lime slice to make the salt stick to it. Take care to moisten only the outer rim and sprinkle the salt on it. The salt should present to the lips of the imbiber and never mix into the cocktail. Shake the other ingredients with ice, then carefully pour into the glass.");
        }
        return Task.FromResult(drink);
    }

    public Task<List<ListDrink>> SearchDrinksAsync(string searchTerm)
    {
        var drinks = GetDrinksByCategoryAsync(new("")).Result;
        return Task.FromResult(drinks.Where(d => d.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList());
    }
}
