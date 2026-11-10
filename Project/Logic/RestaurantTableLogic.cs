public class RestaurantTableLogic
{
    private RestaurantTableAccess _access = new();

    public List<RestaurantTableModel> GetAllTables()
    {
        return _access.GetAllTables();
    }

    public List<RestaurantTableModel> GetAvailableTables(DateTime moment, int numberOfPeople)
    {
        return _access.GetTablesAvailableAt(moment, numberOfPeople);
    }
}