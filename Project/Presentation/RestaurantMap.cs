public static class RestaurantMap
{
    private static RestaurantTableLogic _tableLogic = new();
    private static ReservationLogic _reservationLogic = new();
    public static void Start()
    {
        Style.Setup();
        Style.Title("Restaurant Map");

        DateTime moment = new DateTime(2026, 11, 11, 21, 0, 0);

        List<RestaurantTableModel> tables = _tableLogic.GetAllTables();
        List<int> reservedIds = _reservationLogic.GetReservedTableIds(moment);
        List<int> userIds = _reservationLogic.GetUserTable(moment, (int)AccountsLogic.CurrentAccount!.Id);

        foreach (var table in tables)
        {
            string color;

            if (userIds.Contains(table.TableId))
            {
                color = Style.Goud;
            }
            else if (reservedIds.Contains(table.TableId))
            {
                color = Style.Rood;
            }
            else
            {
                color = Style.Groen;
            }

            Console.WriteLine($"{color}Tafel {table.TableId} ({table.Seats} pers.){Style.Reset}");
        }

        Console.WriteLine("\nPress any key to go back...");
        Console.ReadKey();
    }
}