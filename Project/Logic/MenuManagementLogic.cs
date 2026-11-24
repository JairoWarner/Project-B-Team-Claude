
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

    // Get food currently in a menu
    public List<FoodModel> GetFoodByMenu(long menuId)
    {
        return _access.GetFoodByMenu(menuId);
    }

    // Get food not yet in a menu
    public List<FoodModel> GetFoodNotInMenu(long menuId)
    {
        return _access.GetFoodNotInMenu(menuId);
    }

    // Add food to menu
    public bool AddFoodToMenu(long menuId, long foodId)
    {
        if (!IsAdmin())
            return false;

        if (!_access.GetAllMenus().Any(m => m.ID == menuId))
            return false;

        bool foodExists = _access.GetFoodNotInMenu(menuId)
            .Any(f => f.ID == foodId);

        if (!foodExists)
            return false;

        return _access.AddFoodToMenu(menuId, foodId);
    }

    // Remove food from menu
    public bool RemoveFoodFromMenu(long menuId, long foodId)
    {
        if (!IsAdmin())
            return false;

        if (!_access.GetAllMenus().Any(m => m.ID == menuId))
            return false;

        bool foodExists = _access.GetFoodByMenu(menuId)
            .Any(f => f.ID == foodId);

        if (!foodExists)
            return false;

        return _access.RemoveFoodFromMenu(menuId, foodId);
    }
}
