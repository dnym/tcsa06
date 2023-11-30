using DrinksInfo.DataAccess;
using DrinksInfo.DataAccess.Models;
using TCSAHelper.Console;

namespace DrinksInfo.UI;

internal static class SearchScreen
{
    public static Screen Get(IDataAccess dataAccess)
    {
        Screen screen = new(header: (_, _) => "Search", body: (_, _) => "Enter a search term: ", footer: (_, _) => "[Enter] Search\t[Esc] Cancel");
        screen.AddAction(ConsoleKey.Escape, screen.ExitScreen);

        screen.SetPromptAction((searchTerm) =>
        {
            List<ListDrink> drinks = dataAccess.SearchDrinksAsync(searchTerm).Result;
            DrinksListing.Get(dataAccess, drinks, $"Search: {searchTerm}").Show();
            screen.ExitScreen();
        });

        return screen;
    }
}
