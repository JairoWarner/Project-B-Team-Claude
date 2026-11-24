
public class MenuManagement
{
    private readonly MenuManagementLogic _logic = new();

    public void Start()
    {
        // Check admin permissions
        if (!_logic.IsAdmin())
        {
            Style.Error("Access denied. Admin privileges required.");
            Console.ReadKey(true);
            return;
        }

        string[] options =
        {
            "Add Food / Drink to Menu",
            "Remove Food / Drink from Menu",
            "Go Back"
        };

        while (true)
        {
            Style.Setup();
            Style.Title("MENU MANAGEMENT");

            int choice = ArrowNavigation.PrintMenu(options);

            switch (choice)
            {
                case 1:
                    AddFood();
                    break;

                case 2:
                    RemoveFood();
                    break;

                case 3:
                    return;
            }
        }
    }

    // Select an existing menu
    private long SelectMenu()
    {
        Style.Setup();
        Style.Title("SELECT MENU");

        List<MenuModel> menus = _logic.GetAllMenus();

        if (menus.Count == 0)
        {
            Style.Error("No menus available.");
            Console.ReadKey(true);
            return 0;
        }

        foreach (MenuModel menu in menus)
        {
            Style.Normal($"{menu.ID}. {menu.Name}");
        }

        Console.Write("\nEnter Menu ID: ");

        if (!long.TryParse(Console.ReadLine(), out long menuId))
        {
            Style.Error("Invalid Menu ID.");
            Console.ReadKey(true);
            return 0;
        }

        if (!menus.Any(m => m.ID == menuId))
        {
            Style.Error("Menu does not exist.");
            Console.ReadKey(true);
            return 0;
        }

        return menuId;
    }

    // Add food or drinks to a menu
    private void AddFood()
    {
        long menuId = SelectMenu();

        if (menuId == 0)
            return;

        while (true)
        {
            Style.Setup();
            Style.Title("ADD FOOD / DRINK");

            List<FoodModel> foods =
                _logic.GetFoodNotInMenu(menuId);

            if (foods.Count == 0)
            {
                Style.Normal("All available food items are already in this menu.");
                Console.ReadKey(true);
                return;
            }

            foreach (FoodModel food in foods)
            {
                Style.Normal(
                    $"{food.ID}. {food.FoodName} | {food.Category} | €{food.Price:0.00}");
            }

            Console.Write("\nEnter Food ID to add (0 to finish): ");

            if (!long.TryParse(Console.ReadLine(), out long foodId))
            {
                Style.Error("Invalid Food ID.");
                Console.ReadKey(true);
                continue;
            }

            if (foodId == 0)
                return;

            bool success = _logic.AddFoodToMenu(menuId, foodId);

            if (success)
                Style.Success("Food successfully added to menu!");
            else
                Style.Error("Could not add food to menu.");

            Console.ReadKey(true);
        }
    }

    // Remove food or drinks from a menu
    private void RemoveFood()
    {
        long menuId = SelectMenu();

        if (menuId == 0)
            return;

        while (true)
        {
            Style.Setup();
            Style.Title("REMOVE FOOD / DRINK");

            List<FoodModel> foods =
                _logic.GetFoodByMenu(menuId);

            if (foods.Count == 0)
            {
                Style.Normal("This menu contains no food items.");
                Console.ReadKey(true);
                return;
            }

            foreach (FoodModel food in foods)
            {
                Style.Normal(
                    $"{food.ID}. {food.FoodName} | {food.Category} | €{food.Price:0.00}");
            }

            Console.Write("\nEnter Food ID to remove (0 to finish): ");

            if (!long.TryParse(Console.ReadLine(), out long foodId))
            {
                Style.Error("Invalid Food ID.");
                Console.ReadKey(true);
                continue;
            }

            if (foodId == 0)
                return;

            bool exists = foods.Any(f => f.ID == foodId);

            if (!exists)
            {
                Style.Error("This food item is not in the selected menu.");
                Console.ReadKey(true);
                continue;
            }

            Console.Write("Are you sure? (Y/N): ");

            string confirmation = Console.ReadLine() ?? "";

            if (!confirmation.Equals(
                "Y", StringComparison.OrdinalIgnoreCase))
            {
                Style.Normal("Removal cancelled.");
                Console.ReadKey(true);
                continue;
            }

            bool success = _logic.RemoveFoodFromMenu(menuId, foodId);

            if (success)
                Style.Success("Food successfully removed from menu!");
            else
                Style.Error("Could not remove food from menu.");

            Console.ReadKey(true);
        }
    }
}
