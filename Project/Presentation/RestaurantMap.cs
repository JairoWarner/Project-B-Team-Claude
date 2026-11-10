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

        Console.WriteLine("+--------------------------------------------------+");
        Console.WriteLine("|                      KITCHEN                     |");
        Console.WriteLine("|--------------------------------------------------|");
        Console.WriteLine("|                                                  |");

        // 2 persoons tafels
        Console.Write("|  ");
        DrawTable(1, reservedIds, userIds);
        Console.Write("  ");
        DrawTable(2, reservedIds, userIds);
        Console.Write("  ");
        DrawTable(3, reservedIds, userIds);
        Console.Write("  ");
        DrawTable(4, reservedIds, userIds);
        Console.Write("  ");
        DrawTable(5, reservedIds, userIds);
        Console.Write("  ");
        DrawTable(6, reservedIds, userIds);
        Console.Write("  ");
        DrawTable(7, reservedIds, userIds);
        Console.Write("  ");
        DrawTable(8, reservedIds, userIds);
        Console.WriteLine("  |");

        Console.WriteLine("|                                                  |");
        // 4 persoons tafels
        Console.Write("|     ");
        DrawTable(9, reservedIds, userIds);
        Console.Write("     ");
        DrawTable(10, reservedIds, userIds);
        Console.Write("     ");
        DrawTable(11, reservedIds, userIds);
        Console.Write("     ");
        DrawTable(12, reservedIds, userIds);
        Console.Write("     ");
        DrawTable(13, reservedIds, userIds);
        Console.WriteLine("     |");

        Console.WriteLine("|                                                  |");

        // 6/8 persoons tafels
        Console.Write("|         ");
        DrawTable(14, reservedIds, userIds);
        Console.Write("          ");
        DrawTable(15, reservedIds, userIds);
        Console.Write("          ");
        DrawTable(16, reservedIds, userIds);
        Console.WriteLine("         |");

        Console.WriteLine("|                                                  |");

        Console.WriteLine("|                                        +---------|");
        Console.WriteLine("|                                        | TOILET  |");
        Console.WriteLine("+-----[ENTRANCE]-------------------------+---------+");


        Console.WriteLine();
        Console.WriteLine("Legend:");
        Console.WriteLine($"{Style.Groen}[  ]{Style.Reset} Free");
        Console.WriteLine($"{Style.Rood}[  ]{Style.Reset} Reserved");
        Console.WriteLine($"{Style.Goud}[  ]{Style.Reset} Your table");
        Console.WriteLine("\nPress any key to go back...");
        Console.ReadKey();
    }

    private static string GetColor(int tableId, List<int> reservedIds, List<int> userIds)
    {
        string color;

        if (userIds.Contains(tableId))
        {
            color = Style.Goud;
        }
        else if (reservedIds.Contains(tableId))
        {
            color = Style.Rood;
        }
        else
        {
            color = Style.Groen;
        }
        return color;
    }

    private static void DrawTable(int tableId, List<int> reservedIds, List<int> userIds)
    {
        string color = GetColor(tableId, reservedIds, userIds);
        Console.Write($"{color}[{tableId,2}]{Style.Reset}");
    }
}