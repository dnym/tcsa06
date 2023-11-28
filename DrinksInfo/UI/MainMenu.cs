using ConsoleTableExt;
using DrinksInfo.DataAccess;
using TCSAHelper.Console;

namespace DrinksInfo.UI;

internal static class MainMenu
{
    public static Screen Get(IDataAccess dataAccess)
    {
        string menuContents = ConsoleTableBuilder
            .From(dataAccess.GetCategoriesAsync().Result.ConvertAll(c => c.Name))
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
        });
        screen.AddAction(ConsoleKey.UpArrow, () => menu!.SelectedIndex--);
        screen.AddAction(ConsoleKey.DownArrow, () => menu!.SelectedIndex++);
        screen.AddAction(ConsoleKey.PageUp, () => menu!.SelectedIndex -= 5);
        screen.AddAction(ConsoleKey.PageDown, () => menu!.SelectedIndex += 5);
        screen.AddAction(ConsoleKey.Home, () => menu!.SelectedIndex = 0);
        screen.AddAction(ConsoleKey.End, () => menu!.SelectedIndex = 10);

        screen.AddAction(ConsoleKey.RightArrow, () => Console.WriteLine($"{menuContents.Split("\n")[1 + (2 * menu!.SelectedIndex)]}"));

        return screen;
    }
}
