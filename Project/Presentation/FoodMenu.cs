using System.Runtime.ConstrainedExecution;

public class FoodMenu
{
    public void ChooseMenu()
    {
        MenuLogic menuLogic = new();
        int countMenu = menuLogic.GetMenu().Count;
        List<MenuModel> menus = menuLogic.GetMenu();
        FoodLogic foodLogic = new();
        int menuChoice = 0;
        while (menuChoice != countMenu + 1)
        {
            Style.Setup();
            Style.Title("Which menu do you want to see?");
            for (int i = 1; i < countMenu + 1; i++)
            {
                Style.Normal($"{i}: {menus[i - 1].Name}");
            }
            Style.Normal($"{countMenu + 1}: Go back..");
            if (int.TryParse(Console.ReadLine(), out menuChoice))
            {


                if (menuChoice > countMenu + 1)
                {
                    Style.Error("That option doesn't exist");
                    Style.Normal("Press any key to continue..");
                    Console.ReadKey();
                    continue;
                }

                ShowMenu(foodLogic.GetMenu(menuChoice));

                if (menuChoice != countMenu + 1)
                {
                    Style.Normal("Press any key to continue..");
                    Console.ReadKey();
                }
            }
            else
            {
                Style.Error("Not a number!");
                Style.Normal("Press any key to continue..");
                Console.ReadKey();
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