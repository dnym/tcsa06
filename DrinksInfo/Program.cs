using DrinksInfo.DataAccess;
using DrinksInfo.UI;

namespace DrinksInfo;

internal static class Program
{
    static void Main()
    {
        IDataAccess dataAccess = new Mock();
        MainMenu.Get(dataAccess).Show();
    }
}
