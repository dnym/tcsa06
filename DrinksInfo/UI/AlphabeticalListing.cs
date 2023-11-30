using DrinksInfo.DataAccess;
using DrinksInfo.DataAccess.Models;
using TCSAHelper.Console;

namespace DrinksInfo.UI;

internal static class AlphabeticalListing
{
    public static Screen Get(IDataAccess dataAccess)
    {

        Screen screen = new(header: (_, _) => "Alphabetical Listing", body: (_, _) => "Enter a letter or number: ", footer: (_, _) => "[Esc] Cancel");
        screen.AddAction(ConsoleKey.Escape, screen.ExitScreen);

        screen.SetPromptAction((letter) =>
        {
            if (letter.Length != 1)
            {
                Console.Beep();
                return;
            }
            List<ListDrink> drinks = dataAccess.GetDrinksByLetterAsync(letter[0]).Result;
            DrinksListing.Get(dataAccess, drinks, $"Alphabetical Listing: {letter}").Show();
            screen.ExitScreen();
        });

        return screen;
    }
}
