using Project.Presentation;

public class MenuManagement
{
    private readonly MenuManagementLogic _logic = new();

    public void Start()
    {
        // Admin authorization
        if (!_logic.IsAdmin())
        {
            Style.Error("Access denied. Admin privileges required.");
            Console.ReadKey(true);
            return;
        }

        string[] menu =
        {
            "Add Menu",
            "Remove Menu",
            "Go Back"
        };

        while (true)
        {
            Style.Setup();
            Style.Title("MENU MANAGEMENT");

            int choice = ArrowNavigation.PrintMenu(menu);

            switch (choice)
            {
                case 1:
                    AddMenu();
                    break;

                case 2:
                    RemoveMenu();
                    break;

                case 3:
                    return;
            }
        }
    }

    // Add menu
    private void AddMenu()
    {
        Style.Setup();
        Style.Title("ADD MENU");

        Console.Write("\nMenu name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Menu description: ");
        string description = Console.ReadLine() ?? "";

        List<FoodModel> foods = _logic.GetAllFood();

        Style.Title("\nAVAILABLE FOOD");

        foreach (FoodModel food in foods)
        {
            Console.WriteLine(
                $"{food.ID}. {food.FoodName} - €{food.Price:0.00}"
            );
        }

        Console.Write("\nEnter Food IDs separated by commas: ");

        List<long> foodIds;

        try
        {
            foodIds = (Console.ReadLine() ?? "")
                .Split(',')
                .Select(id => long.Parse(id.Trim()))
                .Distinct()
                .ToList();
        }
        catch
        {
            Style.Error("Invalid Food IDs.");
            Console.ReadKey(true);
            return;
        }

        bool result = _logic.AddMenu(name, description, foodIds);

        if (result)
            Style.Success("Menu successfully added!");
        else
            Style.Error("Could not add menu. Check your input.");

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey(true);
    }

    // Remove menu
    private void RemoveMenu()
    {
        Style.Setup();
        Style.Title("REMOVE MENU");

        List<MenuModel> menus = _logic.GetAllMenus();

        if (menus.Count == 0)
        {
            Style.Error("No menus available.");
            Console.ReadKey(true);
            return;
        }

        foreach (MenuModel menu in menus)
        {
            Console.WriteLine(
                $"{menu.ID}. {menu.Name} - {menu.Description}"
            );
        }

        Console.Write("\nEnter Menu ID: ");

        if (!long.TryParse(Console.ReadLine(), out long menuId))
        {
            Style.Error("Invalid Menu ID.");
            Console.ReadKey(true);
            return;
        }

        Console.Write("\nAre you sure you want to remove this menu? (Y/N): ");

        string confirmation = Console.ReadLine() ?? "";

        if (!confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
        {
            Style.Normal("Removal cancelled.");
            Console.ReadKey(true);
            return;
        }

        bool result = _logic.RemoveMenu(menuId);

        if (result)
            Style.Success("Menu successfully removed!");
        else
            Style.Error("Could not remove menu.");

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey(true);
    }
}