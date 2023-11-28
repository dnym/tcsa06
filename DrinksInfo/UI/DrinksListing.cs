using ConsoleTableExt;
using DrinksInfo.DataAccess;
using DrinksInfo.DataAccess.Models;
using TCSAHelper.Console;

namespace DrinksInfo.UI;

internal static class DrinksListing
{
    internal static Screen Get(IDataAccess dataAccess, Category category)
    {
        List<ListDrink> drinks = dataAccess.GetDrinksByCategoryAsync(category).Result;

        string menuContents = ConsoleTableBuilder
            .From(drinks.ConvertAll(d => d.Name))
            .WithFormat(ConsoleTableBuilderFormat.Alternative)
            .Export().ToString();
        SelectionMenu? menu = null;
        int previousUsableHeight = -1;

        var screen = new Screen(header: (_, _) =>
        {
            return category.Name;
        }, body: (_, usableHeight) =>
        {
            if (usableHeight != previousUsableHeight)
            {
                menu = new(menuContents, drinks.Count, indexToLine: (i) => 1 + (2 * i), leftIndicator: ">>", rightIndicator: "<<", startSelectedIndex: menu?.SelectedIndex ?? 0, maxHeight: usableHeight);
            }
            previousUsableHeight = usableHeight;

            return menu!.Show();
        });
        screen.AddAction(ConsoleKey.UpArrow, () => menu!.SelectedIndex--);
        screen.AddAction(ConsoleKey.DownArrow, () => menu!.SelectedIndex++);
        screen.AddAction(ConsoleKey.PageUp, () => menu!.SelectedIndex -= 5);
        screen.AddAction(ConsoleKey.PageDown, () => menu!.SelectedIndex += 5);
        screen.AddAction(ConsoleKey.Home, () => menu!.SelectedIndex = 0);
        screen.AddAction(ConsoleKey.End, () => menu!.SelectedIndex = 10);

        screen.AddAction(ConsoleKey.LeftArrow, screen.ExitScreen);
        screen.AddAction(ConsoleKey.RightArrow, () => Console.WriteLine(drinks[menu!.SelectedIndex]));

        return screen;
    }
}
