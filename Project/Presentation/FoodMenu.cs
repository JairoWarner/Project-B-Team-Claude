using System.Runtime.ConstrainedExecution;

public class FoodMenu
{
    public void ChooseMenu()
    {
        FoodLogic logic = new();
        string menuChoice = "";
        while (menuChoice != "4")
        {
            Console.WriteLine();
            Console.WriteLine("Which menu do you want to look at?");
            Console.WriteLine("1: Adult menu");
            Console.WriteLine("2: Kids menu");
            Console.WriteLine("3: Drinks menu");
            Console.WriteLine("4: Go back..");
            menuChoice = Console.ReadLine();

            switch (menuChoice)
            {
                case "1":
                    ShowMenu(logic.GetMenu(1));
                    break;

                case "2":
                    ShowMenu(logic.GetMenu(2));
                    break;

                case "3":
                    ShowMenu(logic.GetMenu(3));
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
        foreach (var product in menuList)
        {
            if (product.Category != lastCategory)
            {
                Console.WriteLine();
                Console.WriteLine($"-- {product.Category}");
                lastCategory = product.Category;
            }

            if (product.IsAlcoholic == 1)
            {
                Console.WriteLine($"{product.ID}    {product.FoodName} (Alcoholic), {product.Description} - €{product.Price:0.00}");
            }
            else
            {
                Console.WriteLine($"{product.ID}    {product.FoodName}, {product.Description} - €{product.Price:0.00}");
            }
        }
    }
}