
public class OrderFood
{
    private readonly OrderLogic _orderLogic = new();
    private readonly MenuLogic _menuLogic = new();
    private readonly FoodLogic _foodLogic = new();

    public long? PlaceOrder(AccountModel account)
    {
        if (!AskPreOrder())
            return null;

        int age = OrderLogic.CalculateAge(account.DateOfBirth);

        ChooseItems(age);

        if (_orderLogic.IsEmpty)
            return null;

        ShowSummary();

        return _orderLogic.Save();
    }

    public bool AskPreOrder()
    {
        while (true)
        {
            Style.Setup();
            Style.Title("Do you want to pre-order food and drinks?");
            Style.Normal("Y: Yes");
            Style.Normal("N: No, continue without an order");

            string input = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (input == "Y")
                return true;

            if (input == "N")
                return false;

            Style.Error("Please enter Y or N");
            Pause();
        }
    }

    private void ChooseItems(int age)
    {
        List<MenuModel> menus = _menuLogic.GetMenu();

        while (true)
        {
            Style.Setup();
            Style.Title("Which menu do you want to order from?");

            for (int i = 0; i < menus.Count; i++)
            {
                Style.Normal($"{i + 1}: {menus[i].Name}");
            }

            Style.Normal($"{menus.Count + 1}: Finish order");
            Style.Normal("B: Back to reservation");

            string input = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (input == "B")
                return;

            if (!int.TryParse(input, out int choice) ||
                choice < 1 || choice > menus.Count + 1)
            {
                Style.Error("That option doesn't exist");
                Pause();
                continue;
            }

            if (choice == menus.Count + 1)
                return;

            OrderFromMenu(menus[choice - 1], age);
        }
    }

    private void OrderFromMenu(MenuModel menu, int age)
    {
        List<FoodModel> foods = _foodLogic.GetMenu((int)menu.ID);

        while (true)
        {
            Style.Setup();
            ShowMenu(menu, foods);

            Style.Normal("");
            Style.Normal("Enter an item ID to order or view information.");
            Style.Normal("B: Back to menu selection");

            string input = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (input == "B")
                return;

            FoodModel? food = null;

            if (long.TryParse(input, out long id))
            {
                food = foods.FirstOrDefault(item => item.ID == id);
            }

            if (food is null)
            {
                Style.Error("That item doesn't exist");
                Pause();
                continue;
            }

            ChooseAction(food, age);
        }
    }

    private void ChooseAction(FoodModel food, int age)
    {
        while (true)
        {
            Style.Setup();
            Style.Title(food.FoodName);

            Style.Normal("1: Order");
            Style.Normal("2: Information");
            Style.Normal("3: Back");

            string input = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (input == "1")
            {
                if (food.IsAlcoholic == 1 && age < 18)
                {
                    Style.Error(
                        "Alcoholic drinks can only be ordered by customers of 18 years or older."
                    );

                    Pause();
                    return;
                }

                int quantity = AskQuantity();

                if (quantity == 0)
                    return;

                if (_orderLogic.AddItem(food, age, quantity))
                {
                    Style.Success(
                        $"{quantity} x {food.FoodName} added to your order."
                    );
                }
                else
                {
                    Style.Error("Could not add item to order.");
                }

                Pause();
                return;
            }

            if (input == "2")
            {
                ShowInformation(food);
                Pause();
                continue;
            }

            if (input == "3")
                return;

            Style.Error("That option doesn't exist");
            Pause();
        }
    }

    private int AskQuantity()
    {
        while (true)
        {
            Style.Setup();
            Style.Title("ORDER QUANTITY");

            Console.Write("How many would you like to order? ");

            string input = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (input == "B")
                return 0;

            if (!int.TryParse(input, out int quantity))
            {
                Style.Error("Please enter a valid number.");
                Pause();
                continue;
            }

            if (quantity < 1)
            {
                Style.Error("Order amount must be at least 1.");
                Pause();
                continue;
            }

            return quantity;
        }
    }

    private void ShowMenu(MenuModel menu, List<FoodModel> foods)
    {
        Style.Title(menu.Name);

        string lastCategory = "";

        foreach (FoodModel food in foods)
        {
            if (food.Category != lastCategory)
            {
                Style.Title($"-- {food.Category}");
                lastCategory = food.Category;
            }

            string alcoholic = food.IsAlcoholic == 1
                ? $" {Style.Rood}(Alcoholic){Style.Reset}"
                : "";

            Style.Normal(
                $"{food.ID}    {food.FoodName}{alcoholic} - {Style.Goud}€{food.Price:0.00}{Style.Reset}"
            );
        }
    }

    private void ShowInformation(FoodModel food)
    {
        Style.Setup();
        Style.Title(food.FoodName);

        Style.Normal($"Category: {food.Category}");
        Style.Normal($"Description: {food.Description}");
        Style.Normal($"Price: €{food.Price:0.00}");
    }

    private void ShowSummary()
    {
        Style.Setup();
        Style.Title("Order summary");

        foreach (OrderLine line in _orderLogic.GetLines())
        {
            Style.Normal(
                $"{line.Quantity} x {line.Food.FoodName} - " +
                $"{Style.Goud}€{line.Food.Price:0.00}{Style.Reset} each = " +
                $"{Style.Goud}€{line.LineTotal:0.00}{Style.Reset}"
            );
        }

        Style.Title(
            $"Total: {Style.Goud}€{_orderLogic.CalculateTotalPrice():0.00}{Style.Reset}"
        );

        Pause();
    }

    private static void Pause()
    {
        Style.Normal("Press any key to continue..");
        Console.ReadKey(true);
    }
}
