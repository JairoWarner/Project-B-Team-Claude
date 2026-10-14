using System.Runtime.ConstrainedExecution;

public class FoodMenu
{
    public void ChooseMenu()
    {
        MenuLogic menuLogic = new();
        int countMenu = menuLogic.GetMenu().Count;
        List<MenuModel> menus = menuLogic.GetMenu();
        FoodLogic logic = new();
        string menuChoice = "";
        while (menuChoice != "4")
        {
            Style.Setup();
            Style.Title("Which menu do you want to see?");
            Style.Normal("1: Adult menu");
            Style.Normal("2: Kids menu");
            Style.Normal("3: Drinks menu");
            Style.Normal("4: Go back..");
            menuChoice = Console.ReadLine();

            switch (menuChoice)
            {
                case "1":
                    ShowMenu(logic.GetMenu(1));
                    Style.Normal("Press any key to continue..");
                    Console.ReadKey();
                    break;

                case "2":
                    ShowMenu(logic.GetMenu(2));
                    Style.Normal("Press any key to continue..");
                    Console.ReadKey();
                    break;

                case "3":
                    ShowMenu(logic.GetMenu(3));
                    Style.Normal("Press any key to continue..");
                    Console.ReadKey();
                    break;

                case "4":
                    break;

                default:
                    Console.WriteLine("That option isn't available");
                    break;
            }
        }
    }

    public void ShowMenu(List<FoodModel> menuList)
    {
        string lastCategory = "";
        Style.Setup();
        foreach (var product in menuList)
        {
            if (product.Category != lastCategory)
            {
                Style.Title($"-- {product.Category}");
                lastCategory = product.Category;
            }

            if (product.IsAlcoholic == 1)
            {
                Style.Normal($"{product.ID}    {product.FoodName} {Style.Rood}(Alcoholic){Style.Reset}, {product.Description} - {Style.Goud}€{product.Price:0.00}{Style.Reset}");
            }
            else
            {
                Style.Normal($"{product.ID}    {product.FoodName}, {product.Description} - {Style.Goud}€{product.Price:0.00}{Style.Reset}");
            }
        }
    }
}