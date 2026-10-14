using System.Runtime.ConstrainedExecution;

public class FoodMenu
{
    public void ChooseMenu()
    {
        FoodLogic logic = new();
        string menuChoice = "";
        while (menuChoice != "4")
        {
            Print.Setup();
            Print.Title("Which menu do you want to see?");
            Print.Normal("1: Adult menu");
            Print.Normal("2: Kids menu");
            Print.Normal("3: Drinks menu");
            Print.Normal("4: Go back..");
            menuChoice = Console.ReadLine();

            switch (menuChoice)
            {
                case "1":
                    ShowMenu(logic.GetMenu(1));
                    Print.Normal("Press any key to continue..");
                    Console.ReadKey();
                    break;

                case "2":
                    ShowMenu(logic.GetMenu(2));
                    Print.Normal("Press any key to continue..");
                    Console.ReadKey();
                    break;

                case "3":
                    ShowMenu(logic.GetMenu(3));
                    Print.Normal("Press any key to continue..");
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