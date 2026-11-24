using Project.Presentation;

public class MenuManagement
{
    private readonly MenuManagementLogic _logic = new();

    public void Start()
    {
        if (!_logic.IsAdmin())
        {
            Style.Error("Access denied. Admin privileges required.");
            Console.ReadKey(true);
            return;
        }

        string[] options =
        [
            "Add to menu",
            "Remove from menu",
            "Add food to store",
            "Go back"
        ];

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
                    AddFoodToStore();
                    break;

                case 4:
                    return;
            }
        }
    }

    private long? SelectMenu()
    {
        List<MenuModel> menus = _logic.GetAllMenus();

        while (true)
        {
            Style.Setup();
            Style.Title("SELECT MENU");

            for (int i = 0; i < menus.Count; i++)
            {
                Style.Normal($"{i + 1}. {menus[i].Name}");
            }

            Style.Normal("0. Go back");

            Console.Write("\nChoose menu: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Style.Error("Invalid choice.");
                Console.ReadKey(true);
                continue;
            }

            if (choice == 0)
                return null;

            if (choice < 1 || choice > menus.Count)
            {
                Style.Error("Menu does not exist.");
                Console.ReadKey(true);
                continue;
            }

            return menus[choice - 1].ID;
        }
    }

    private void AddFood()
    {
        long? selectedMenu = SelectMenu();

        if (!selectedMenu.HasValue)
            return;

        long menuId = selectedMenu.Value;

        while (true)
        {
            Style.Setup();
            Style.Title("ADD EXISTING FOOD TO MENU");

            List<FoodModel> foods =
                _logic.GetFoodNotInMenu(menuId);

            if (foods.Count == 0)
            {
                Style.Normal("All products are already in this menu.");
                Console.ReadKey(true);
                return;
            }

            foreach (FoodModel food in foods)
            {
                Style.Normal(
                    $"{food.ID}. {food.FoodName} | {food.Category} | €{food.Price:0.00}");
            }

            Console.Write("\nEnter Food ID (0 to go back): ");

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
                Style.Success("Item successfully added to menu!");
            else
                Style.Error("Could not add item.");

            Console.ReadKey(true);
        }
    }

    private void RemoveFood()
    {
        long? selectedMenu = SelectMenu();

        if (!selectedMenu.HasValue)
            return;

        long menuId = selectedMenu.Value;

        while (true)
        {
            Style.Setup();
            Style.Title("REMOVE FOOD FROM MENU");

            List<FoodModel> foods =
                _logic.GetFoodByMenu(menuId);

            if (foods.Count == 0)
            {
                Style.Normal("This menu contains no products.");
                Console.ReadKey(true);
                return;
            }

            foreach (FoodModel food in foods)
            {
                Style.Normal(
                    $"{food.ID}. {food.FoodName} | {food.Category} | €{food.Price:0.00}");
            }

            Console.Write("\nEnter Food ID to remove (0 to go back): ");

            if (!long.TryParse(Console.ReadLine(), out long foodId))
            {
                Style.Error("Invalid Food ID.");
                Console.ReadKey(true);
                continue;
            }

            if (foodId == 0)
                return;

            if (!foods.Any(f => f.ID == foodId))
            {
                Style.Error("This item is not in the selected menu.");
                Console.ReadKey(true);
                continue;
            }

            Console.Write("Are you sure? (Y/N): ");

            string confirmation = Console.ReadLine() ?? "";

            if (!confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                Style.Normal("Removal cancelled.");
                Console.ReadKey(true);
                continue;
            }

            bool success = _logic.RemoveFoodFromMenu(menuId, foodId);

            if (success)
                Style.Success("Item removed from menu.");
            else
                Style.Error("Could not remove item.");

            Console.ReadKey(true);
        }
    }

    private void AddFoodToStore()
    {
        Style.Setup();
        Style.Title("ADD FOOD TO STORE");

        Console.Write("Food name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Price (e.g. 3.50): ");

        if (!double.TryParse(
            Console.ReadLine(),
            System.Globalization.NumberStyles.Number,
            System.Globalization.CultureInfo.InvariantCulture,
            out double price))
        {
            Style.Error("Invalid price.");
            Console.ReadKey(true);
            return;
        }

        Console.Write("Description: ");
        string description = Console.ReadLine() ?? "";

        Console.Write("Category: ");
        string category = Console.ReadLine() ?? "";

        Console.Write("Is this item alcoholic? (Y/N): ");

        bool isAlcoholic = (Console.ReadLine() ?? "")
            .Equals("Y", StringComparison.OrdinalIgnoreCase);

        long? menuId = SelectMenuForNewFood(out bool cancelled);

        if (cancelled)
            return;

        bool success = _logic.AddFoodToStore(
            name,
            price,
            description,
            category,
            isAlcoholic,
            menuId,
            out string message
        );

        if (success)
            Style.Success(message);
        else
            Style.Error(message);

        Console.ReadKey(true);
    }

    private long? SelectMenuForNewFood(out bool cancelled)
    {
        cancelled = false;

        List<MenuModel> menus = _logic.GetAllMenus();

        while (true)
        {
            Style.Setup();
            Style.Title("ADD PRODUCT TO MENU?");

            for (int i = 0; i < menus.Count; i++)
            {
                Style.Normal($"{i + 1}. {menus[i].Name}");
            }

            Style.Normal($"{menus.Count + 1}. Don't add to a menu");
            Style.Normal("0. Cancel");

            Console.Write("\nChoose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Style.Error("Invalid choice.");
                Console.ReadKey(true);
                continue;
            }

            if (choice == 0)
            {
                cancelled = true;
                return null;
            }

            if (choice == menus.Count + 1)
                return null;

            if (choice < 1 || choice > menus.Count)
            {
                Style.Error("Menu does not exist.");
                Console.ReadKey(true);
                continue;
            }

            return menus[choice - 1].ID;
        }
    }
}
