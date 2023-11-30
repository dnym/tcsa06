using ConsoleTableExt;
using DrinksInfo.DataAccess;
using DrinksInfo.DataAccess.Models;
using TCSAHelper.Console;

namespace DrinksInfo.UI;

internal static class FilteredListing
{
    internal static Screen GetGlasses(IDataAccess dataAccess)
    {
        List<Glass> glasses = dataAccess.GetGlassesAsync().Result;
        return Get(glasses.ConvertAll(g => g.Name), "GLASSES",
            (i) => DrinksListing.Get(dataAccess,
            dataAccess.GetDrinksByGlassAsync(glasses[i]).Result, $"Filter by Glass: {glasses[i].Name}").Show());
    }

    internal static Screen GetIngredients(IDataAccess dataAccess)
    {
        List<Ingredient> ingredients = dataAccess.GetIngredientsAsync().Result;
        return Get(ingredients.ConvertAll(i => i.Name), "INGREDIENTS",
            (i) => DrinksListing.Get(dataAccess,
            dataAccess.GetDrinksByIngredientAsync(ingredients[i]).Result, $"Filter by Ingredient: {ingredients[i].Name}").Show());
    }

    internal static Screen GetAlcoholTypes(IDataAccess dataAccess)
    {
        List<AlcoholType> alcohols = dataAccess.GetAlcoholTypesAsync().Result;
        return Get(alcohols.ConvertAll(a => a.Name), "ALCOHOL",
            (i) => DrinksListing.Get(dataAccess,
            dataAccess.GetDrinksByAlcoholTypeAsync(alcohols[i]).Result, $"Filter by Alcohol: {alcohols[i].Name}").Show());
    }

    private static Screen Get(List<string> names, string header, Action<int> selectAction)
    {
        string menuContents = ConsoleTableBuilder
            .From(names)
            .WithTitle(header)
            .WithFormat(ConsoleTableBuilderFormat.Alternative)
            .Export().ToString();
        SelectionMenu? menu = null;
        int previousUsableHeight = -1;

        var screen = new Screen(body: (_, usableHeight) =>
        {
            if (usableHeight != previousUsableHeight)
            {
                menu = new(menuContents, names.Count, indexToLine: (i) => 1 + (2 * i), leftIndicator: ">>", rightIndicator: "<<", startSelectedIndex: menu?.SelectedIndex ?? 0, maxHeight: usableHeight);
            }
            previousUsableHeight = usableHeight;

            return menu!.Show();
        }, footer: (_, _) => @"List Navigation: [Up][Dn] [PgUp][PgDn] [Home][End]
Select/Back: [->][<-]");
        screen.AddAction(ConsoleKey.UpArrow, () => menu!.SelectedIndex--);
        screen.AddAction(ConsoleKey.DownArrow, () => menu!.SelectedIndex++);
        screen.AddAction(ConsoleKey.PageUp, () => menu!.SelectedIndex -= 5);
        screen.AddAction(ConsoleKey.PageDown, () => menu!.SelectedIndex += 5);
        screen.AddAction(ConsoleKey.Home, () => menu!.SelectedIndex = 0);
        screen.AddAction(ConsoleKey.End, () => menu!.SelectedIndex = names.Count - 1);

        screen.AddAction(ConsoleKey.LeftArrow, screen.ExitScreen);
        screen.AddAction(ConsoleKey.RightArrow, () => selectAction(menu!.SelectedIndex));

        return screen;
    }
}
