public class RestaurantTableLogic
{
    private RestaurantTableAccess _access = new();

    public List<RestaurantTableModel> GetAllTables()
    {
        return _access.GetAllTables();
    }
}