public class MenuLogic
{
    public List<MenuModel> GetMenu()
    {
        MenuAcces acces = new();
        return acces.GetMenu();
    }
}