using System.Runtime.ConstrainedExecution;
using Project.Presentation;

public class FoodMenu
{
    public void ChooseMenu()
    {
        MenuLogic menuLogic = new();
        int countMenu = menuLogic.GetMenu().Count;
        List<MenuModel> menus = menuLogic.GetMenu();
        FoodLogic foodLogic = new();

        List<string> MenuList = [];

        foreach (MenuModel i in menus)
        {
            MenuList.Add(i.Name.ToString());
        }

        MenuList.Add("Go back..");
        string[] Menu = MenuList.ToArray();

        while (true)
        {
            Style.Title("Which menu do you want to see?");
            int choice = ArrowNavigation.PrintMenu(Menu);

            if (choice == countMenu + 1)
                return;

            ShowMenu(foodLogic.GetMenu(choice));

            Style.Normal("Press any key to continue..");
            Console.ReadKey(true);
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
                Style.Normal(
                    $"{product.ID}    {product.FoodName} {Style.Rood}(Alcoholic){Style.Reset}, {product.Description} - {Style.Goud}€{product.Price:0.00}{Style.Reset}");
            }
            else
            {
                Style.Normal(
                    $"{product.ID}    {product.FoodName}, {product.Description} - {Style.Goud}€{product.Price:0.00}{Style.Reset}");
            }
        }
    }
}