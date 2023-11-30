using ConsoleTableExt;
using DrinksInfo.DataAccess;
using DrinksInfo.DataAccess.Models;
using TCSAHelper.Console;

namespace DrinksInfo.UI;

internal static class MainMenu
{
    public static Screen Get(IDataAccess dataAccess)
    {
        List<Category> categories = dataAccess.GetCategoriesAsync().Result;

        string menuContents = ConsoleTableBuilder
            .From(categories.ConvertAll(c => c.Name))
            .WithTitle("CATEGORIES")
            .WithFormat(ConsoleTableBuilderFormat.Alternative)
            .Export().ToString();
        SelectionMenu? menu = null;
        int previousUsableHeight = -1;

        Screen screen = new(body: (_, usableHeight) =>
        {
            if (usableHeight != previousUsableHeight)
            {
                menu = new(menuContents, itemCount: 11, indexToLine: (i) => 1 + (2 * i), leftIndicator: ">>", rightIndicator: "<<", startSelectedIndex: menu?.SelectedIndex ?? 0, maxHeight: usableHeight);
            }
            previousUsableHeight = usableHeight;

            return menu!.Show();
        }, footer: (_, _) => @"List Navigation: [Up][Dn] [PgUp][PgDn] [Home][End]
Select/Back: [->][<-]
        [S]earch        [A]lphabetical
Filter: [G]lass         [I]ngredient     Al[c]ohol");
        screen.AddAction(ConsoleKey.UpArrow, () => menu!.SelectedIndex--);
        screen.AddAction(ConsoleKey.DownArrow, () => menu!.SelectedIndex++);
        screen.AddAction(ConsoleKey.PageUp, () => menu!.SelectedIndex -= 5);
        screen.AddAction(ConsoleKey.PageDown, () => menu!.SelectedIndex += 5);
        screen.AddAction(ConsoleKey.Home, () => menu!.SelectedIndex = 0);
        screen.AddAction(ConsoleKey.End, () => menu!.SelectedIndex = categories.Count - 1);

        screen.AddAction(ConsoleKey.RightArrow, () =>
        {
            Category category = categories[menu!.SelectedIndex];
            List<ListDrink> drinks = dataAccess.GetDrinksByCategoryAsync(category).Result;
            DrinksListing.Get(dataAccess, drinks, category.Name).Show();
        });
        screen.AddAction(ConsoleKey.S, () => SearchScreen.Get(dataAccess).Show());
        screen.AddAction(ConsoleKey.A, () => AlphabeticalListing.Get(dataAccess).Show());
        screen.AddAction(ConsoleKey.G, () => FilteredListing.GetGlasses(dataAccess).Show());
        screen.AddAction(ConsoleKey.I, () => FilteredListing.GetIngredients(dataAccess).Show());
        screen.AddAction(ConsoleKey.C, () => FilteredListing.GetAlcoholTypes(dataAccess).Show());

        return screen;
    }
}
