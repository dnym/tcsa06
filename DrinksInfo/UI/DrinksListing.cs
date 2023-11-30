using ConsoleTableExt;
using DrinksInfo.DataAccess;
using DrinksInfo.DataAccess.Models;
using TCSAHelper.Console;

namespace DrinksInfo.UI;

internal static class DrinksListing
{
    internal static Screen Get(IDataAccess dataAccess, List<ListDrink> drinks, string header)
    {
        string menuContents = ConsoleTableBuilder
            .From(drinks.ConvertAll(d => d.Name))
            .WithFormat(ConsoleTableBuilderFormat.Alternative)
            .Export().ToString();
        SelectionMenu? menu = null;
        int previousUsableHeight = -1;

        var screen = new Screen(header: (_, _) => header, body: (_, usableHeight) =>
        {
            if (drinks.Count == 0)
            {
                return "No drinks found.";
            }
            else
            {
                if (usableHeight != previousUsableHeight)
                {
                    menu = new(menuContents, drinks.Count, indexToLine: (i) => 1 + (2 * i), leftIndicator: ">>", rightIndicator: "<<", startSelectedIndex: menu?.SelectedIndex ?? 0, maxHeight: usableHeight);
                }
                previousUsableHeight = usableHeight;

                return menu!.Show();
            }
        }, footer: (_, _) => "Select/Back: [->][<-]");
        screen.AddAction(ConsoleKey.UpArrow, () => menu!.SelectedIndex--);
        screen.AddAction(ConsoleKey.DownArrow, () => menu!.SelectedIndex++);
        screen.AddAction(ConsoleKey.PageUp, () => menu!.SelectedIndex -= 5);
        screen.AddAction(ConsoleKey.PageDown, () => menu!.SelectedIndex += 5);
        screen.AddAction(ConsoleKey.Home, () => menu!.SelectedIndex = 0);
        screen.AddAction(ConsoleKey.End, () => menu!.SelectedIndex = drinks.Count - 1);

        screen.AddAction(ConsoleKey.LeftArrow, screen.ExitScreen);
        screen.AddAction(ConsoleKey.RightArrow, () => DrinkInformation.Get(dataAccess, drinks[menu!.SelectedIndex]).Show());

        return screen;
    }
}
