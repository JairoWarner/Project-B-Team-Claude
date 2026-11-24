using Project.DataModels;

public class MenuManagementLogic
{
    private readonly MenuManagementAcces _access = new();

    // Check admin permissions
    public bool IsAdmin()
    {
        return AccountsLogic.CurrentAccount?.Role == CustomerRole.Admin;
    }

    // Get all menus
    public List<MenuModel> GetAllMenus()
    {
        return _access.GetAllMenus();
    }

    // Get all food
    public List<FoodModel> GetAllFood()
    {
        return _access.GetAllFood();
    }

    // Add menu
    public bool AddMenu(
        string name,
        string description,
        List<long> foodIds)
    {
        if (!IsAdmin())
            return false;

        if (string.IsNullOrWhiteSpace(name))
            return false;

        if (string.IsNullOrWhiteSpace(description))
            return false;

        if (foodIds == null || foodIds.Count == 0)
            return false;

        // Check duplicate menu names
        if (_access.GetAllMenus().Any(m =>
            m.Name.Equals(
                name.Trim(),
                StringComparison.OrdinalIgnoreCase)))
        {
            return false;
        }

        // Check if food IDs exist
        List<long> existingFoodIds = _access.GetAllFood()
            .Select(f => f.ID)
            .ToList();

        if (foodIds.Any(id => !existingFoodIds.Contains(id)))
            return false;

        MenuModel menu = new MenuModel(
            0,
            name.Trim(),
            description.Trim()
        );

        return _access.AddMenu(menu, foodIds.Distinct().ToList());
    }

    // Remove menu
    public bool RemoveMenu(long menuId)
    {
        if (!IsAdmin())
            return false;

        if (menuId <= 0)
            return false;

        bool menuExists = _access.GetAllMenus()
            .Any(m => m.ID == menuId);

        if (!menuExists)
            return false;

        return _access.RemoveMenu(menuId);
    }
}