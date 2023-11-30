using DrinksInfo.DataAccess;
using DrinksInfo.DataAccess.Models;
using System.Text;
using TCSAHelper.Console;

namespace DrinksInfo.UI;

internal static class DrinkInformation
{
    public static Screen Get(IDataAccess dataAccess, ListDrink listDrink)
    {
        string body;
        string linebrokenBody = "";

        Drink? drink = dataAccess.GetDrinkByIdAsync(listDrink.Id).Result;
        if (drink is null)
        {
            body = $"Drink with id {listDrink.Id} not found.";
        }
        else
        {
            StringBuilder sb = new();
            sb.Append("Name: ").AppendLine(drink.Name);
            if (drink.AlternateName is not null)
            {
                sb.Append("Alternate name: ").AppendLine(drink.AlternateName);
            }
            sb.Append("Tags: ").AppendJoin(", ", drink.Tags.Select(t => t.Name)).AppendLine();
            if (drink.Category is not null)
            {
                sb.Append("Category: ").AppendLine(drink.Category.Name);
            }
            if (drink.IBACategory is not null)
            {
                sb.Append("IBA category: ").AppendLine(drink.IBACategory);
            }
            if (drink.AlcoholType is not null)
            {
                if (drink.AlcoholType.Name == "Optional alcohol")
                {
                    sb.AppendLine("Alcoholic: optional");
                }
                else if (drink.AlcoholType.Name == "Non alcoholic")
                {
                    sb.AppendLine("Alcoholic: no");
                }
                else
                {
                    sb.AppendLine("Alcoholic: yes");
                }
            }
            if (drink.Glass is not null)
            {
                sb.AppendLine().Append("Glass: ").AppendLine(drink.Glass.Name);
            }
            if (drink.Ingredients.Length > 0)
            {
                sb.AppendLine().AppendLine("Ingredients:");
                for (int i = 0; i < drink.Ingredients.Length; i++)
                {
                    if (drink.Measures.Length > i)
                    {
                        sb.Append("- ").Append(drink.Ingredients[i].Name).Append(", ").AppendLine(drink.Measures[i]);
                    }
                    else
                    {
                        sb.Append("- ").AppendLine(drink.Ingredients[i].Name);
                    }
                }
            }
            for (int i = drink.Ingredients.Length; i < drink.Measures.Length; i++)
            {
                sb.Append("- ").AppendLine(drink.Measures[i]);
            }
            if (drink.Instructions is not null)
            {
                sb.AppendLine().AppendLine("Instructions:").AppendLine(drink.Instructions);
            }

            body = sb.ToString();
        }

        int previousUsableWidth = -1;

        Screen screen = new(body: (usableWidth, _) =>
        {
            if (usableWidth != previousUsableWidth)
            {
                linebrokenBody = TCSAHelper.General.Utils.BreakLines(body, usableWidth);
            }
            previousUsableWidth = usableWidth;

            return linebrokenBody;
        }, footer: (_, _) => "                  Back: [<-]");

        screen.AddAction(ConsoleKey.LeftArrow, screen.ExitScreen);

        return screen;
    }
}
