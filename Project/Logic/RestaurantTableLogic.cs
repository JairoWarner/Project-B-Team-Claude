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

    public List<int> GetFreeBarSeats(DateTime moment)
    {
        return _access.GetFreeBarSeats(moment);
    }

    public List<int> FindBarSeats(DateTime moment, int numberOfPeople)
    {
        List<int> freeSeats = GetFreeBarSeats(moment);

        foreach (var startSeat in freeSeats)
        {
            List<int> group = [];

            for (int i = 0; i < numberOfPeople; i++)
            {
                int seat = startSeat + i;

                if (freeSeats.Contains(seat))
                {
                    group.Add(seat);
                }
            }
            if (group.Count == numberOfPeople)
            {
                return group;
            }
            
        }
        return [];
    }
}