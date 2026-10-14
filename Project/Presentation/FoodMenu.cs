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
            Print.Setup();
            Print.Title("Which menu do you want to see?");
            for (int i = 1; i < countMenu + 1; i++)
            {
                Print.Normal($"{i}: {menus[i - 1].Name}");
            }
            Print.Normal($"{countMenu + 1}: Go back..");
            if (int.TryParse(Console.ReadLine(), out menuChoice))
            {


                if (menuChoice > countMenu + 1)
                {
                    Print.Error("That option doesn't exist");
                    Print.Normal("Press any key to continue..");
                    Console.ReadKey();
                    continue;
                }

                ShowMenu(foodLogic.GetMenu(menuChoice));
                
                if (menuChoice != countMenu + 1)
                {
                    Print.Normal("Press any key to continue..");
                    Console.ReadKey();
                }
            }
            else
            {
                Print.Error("Not a number!");
                Print.Normal("Press any key to continue..");
                Console.ReadKey();
            }
        }
    }

    public void ShowMenu(List<FoodModel> menuList)
    {
        string lastCategory = "";
        Print.Setup();
        foreach (var product in menuList)
        {
            if (product.Category != lastCategory)
            {
                Print.Title($"-- {product.Category}");
                lastCategory = product.Category;
            }

            if (product.IsAlcoholic == 1)
            {
                Print.Normal($"{product.ID}    {product.FoodName} {Print.Rood}(Alcoholic){Print.Reset}, {product.Description} - {Print.Goud}€{product.Price:0.00}{Print.Reset}");
            }
            else
            {
                Print.Normal($"{product.ID}    {product.FoodName}, {product.Description} - {Print.Goud}€{product.Price:0.00}{Print.Reset}");
            }
        }
    }
}