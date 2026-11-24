
using Project.DataModels;

public class MenuManagementLogic
{
    private readonly MenuManagementAcces _access = new();

    public bool IsAdmin()
    {
        return AccountsLogic.CurrentAccount?.Role == CustomerRole.Admin;
    }

    public List<MenuModel> GetAllMenus()
    {
        return _access.GetAllMenus();
    }

    public List<FoodModel> GetFoodByMenu(long menuId)
    {
        return _access.GetFoodByMenu(menuId);
    }

    public List<FoodModel> GetFoodNotInMenu(long menuId)
    {
        return _access.GetFoodNotInMenu(menuId);
    }

    public bool AddFoodToMenu(long menuId, long foodId)
    {
        if (!IsAdmin())
            return false;

        bool menuExists = _access.GetAllMenus()
            .Any(m => m.ID == menuId);

        if (!menuExists)
            return false;

        bool foodAvailable = _access.GetFoodNotInMenu(menuId)
            .Any(f => f.ID == foodId);

        if (!foodAvailable)
            return false;

        return _access.AddFoodToMenu(menuId, foodId);
    }

    public bool AddFoodToStore(
        string name,
        double price,
        string description,
        string category,
        bool isAlcoholic,
        long? menuId,
        out string message)
    {
        if (!IsAdmin())
        {
            message = "Admin privileges required.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            message = "Food name cannot be empty.";
            return false;
        }

        if (price < 0)
        {
            message = "Price cannot be negative.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(category))
        {
            message = "Category cannot be empty.";
            return false;
        }

        if (menuId.HasValue &&
            !_access.GetAllMenus().Any(m => m.ID == menuId.Value))
        {
            message = "Selected menu does not exist.";
            return false;
        }

        FoodModel food = new FoodModel(
            0,
            name.Trim(),
            price,
            description.Trim(),
            category.Trim(),
            isAlcoholic ? 1 : 0
        );

        bool success = _access.AddFoodToStore(food, menuId);

        message = success
            ? "Food successfully added to store!"
            : "Could not add food to store.";

        return success;
    }

    public bool RemoveFoodFromMenu(long menuId, long foodId)
    {
        if (!IsAdmin())
            return false;

        bool menuExists = _access.GetAllMenus()
            .Any(m => m.ID == menuId);

        if (!menuExists)
            return false;

        bool foodExists = _access.GetFoodByMenu(menuId)
            .Any(f => f.ID == foodId);

        if (!foodExists)
            return false;

        return _access.RemoveFoodFromMenu(menuId, foodId);
    }
}
