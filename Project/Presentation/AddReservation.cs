public class AddReservation
{
    private readonly ReservationLogic _logic = new();
    private readonly RestaurantTableAccess _tableAccess = new();

    public int NumberOfPeople { get; private set; } = 1;
    public int NumberOfChildren { get; private set; }

    public void start()
    {
        if (!AskPeople() || !AskChildren())
        {
            return;
        }

        DateTime? reservationDate = AskDateAndTime();
        if (reservationDate is null)
        {
            return;
        }

        int selectedTableId = AskTable(reservationDate.Value);
        AccountModel? currentAccount = AccountsLogic.CurrentAccount;
        if (currentAccount is null)
        {
            throw new InvalidOperationException("A logged-in account is required to make a reservation.");
        }

        _logic.AddReservation(new ReservationModel
        {
            UserID = currentAccount.Id,
            NumberOfPeople = NumberOfPeople,
            NumberOfChildren = NumberOfChildren,
            ReservationDate = reservationDate.Value.Date,
            ReservationTime = reservationDate.Value,
            SelectedTableId = selectedTableId
        });

        Console.WriteLine("Reservation added successfully!");
        Console.ReadLine();
    }

    // public ReservationModel ReservationAdd()
    // {
    //     Console.WriteLine("How many people are in your reservation?(including children)");
    //     int numberOfPeople = int.Parse(Console.ReadLine() ?? "1");

    //     Console.WriteLine("How many children are in your reservation?");
    //     int numberOfChildren = int.Parse(Console.ReadLine() ?? "0");

    //     Console.WriteLine("What date would you like to make the reservation for? (dd-mm-yyyy)");
    //     DateTime reservationDate;
    //     while (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out reservationDate))
    //     {
    //         Console.WriteLine("Invalid date format. Please enter the date in the format dd-mm-yyyy:");
    //     }
        

    //     Console.WriteLine("What time would you like to make the reservation for? (hh:mm)");
    //     DateTime reservationTime;
    //     while (!DateTime.TryParseExact(Console.ReadLine(), "HH:mm", null, System.Globalization.DateTimeStyles.None, out reservationTime))
    //     {
    //         Console.WriteLine("Invalid time format. Please enter the time in the format hh:mm:");
    //     }

    //     Console.WriteLine("What table would you like to reserve?");
    //     int selectedTableId;

    //     while (!int.TryParse(Console.ReadLine(), out selectedTableId) || selectedTableId <= 0)
    //     {
    //         Console.WriteLine("Invalid table ID. Please enter a valid table ID:");
    //     }

    //     ReservationModel reservation = new ReservationModel()
    //     {
    //         NumberOfPeople = numberOfPeople,
    //         NumberOfChildren = numberOfChildren,
    //         ReservationDate = reservationDate,
    //         ReservationTime = reservationTime,
    //         SelectedTableId = selectedTableId
    //     };

    //     _logic.AddReservation(reservation);
    //     Console.WriteLine("Reservation added successfully!");
    //     return reservation;
    // }

    public bool AskPeople()
    {
        while (true)
        {
            Console.WriteLine("How many people are in your reservation?(MIN 0, MAX 6)(including children) (Enter M to return to the main menu)");
            string? input = Console.ReadLine();

            if (input?.ToUpper() == "M")
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                NumberOfPeople = 1;
                return true;
            }

            if (!int.TryParse(input, out int numberOfPeople))
            {
                Console.WriteLine("Invalid number of people");
                continue;
            }

            if (numberOfPeople >= 0)
            {
                NumberOfPeople = numberOfPeople;
                Console.Clear();
                return true;
            }

            Console.WriteLine("Number of people cannot be lower than 0");
            Console.Clear();
        }
    }

    public bool AskChildren()
    {
        while (true)
        {
            Console.WriteLine("How many children are in your reservation?(MIN 0, MAX 5) (Enter M to return to the main menu)");
            string? input = Console.ReadLine();

            if (input?.ToUpper() == "M")
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                NumberOfChildren = 0;
            }
            else if (!int.TryParse(input, out int numberOfChildren))
            {
                Console.WriteLine("Invalid number of children");
                continue;
            }
            else
            {
                NumberOfChildren = numberOfChildren;
            }

            if (NumberOfChildren < NumberOfPeople)
            {
                Console.Clear();
                return true;
            }

            Console.WriteLine("Number of children cannot be higher than total number of people");
        }
    }

    public DateTime? AskDateAndTime()
    {
        DateTime reservationDate;
        while (true)
        {
            Console.WriteLine("What date would you like to make the reservation for? (dd-mm-yyyy) (Enter M to return to the main menu)");
            string? input = Console.ReadLine();

            if (input?.ToUpper() == "M")
            {
                return null;
            }

            if (!DateTime.TryParseExact(input, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out reservationDate))
            {
                Console.WriteLine("Invalid date format. Please enter the date in the format dd-mm-yyyy:");
                continue;
            }

            if (reservationDate.Date <= DateTime.Today)
            {
                Console.WriteLine("Reservations can only be made for a date after today.");
                continue;
            }

            if (_tableAccess.GetTablesForDate(reservationDate, NumberOfPeople).Count == 0)
            {
                Console.WriteLine("That date is fully booked. Please choose another date.");
                continue;
            }
            Console.Clear();

            break;
        }
        

        DateTime reservationTime;
        while (true)
        {
            Console.WriteLine("What time would you like to make the reservation for? (hh:mm) (Enter M to return to the main menu)");
            string? input = Console.ReadLine();

            if (input?.ToUpper() == "M")
            {
                return null;
            }

            if (DateTime.TryParseExact(input, "HH:mm", null, System.Globalization.DateTimeStyles.None, out reservationTime))
            {
                break;
            }

            Console.WriteLine("Invalid time format. Please enter the time in the format hh:mm:");
        }
        Console.Clear();

        return reservationDate.Date.Add(reservationTime.TimeOfDay);
    }

    public int AskTable(DateTime reservationDate)
    {
        List<RestaurantTableModel> tables = _tableAccess.GetTablesForDate(reservationDate, NumberOfPeople);
        RestaurantTableModel? table = tables.FirstOrDefault();
        if (table is null)
        {
            throw new InvalidOperationException("No available restaurant tables.");
        }

        Console.WriteLine($"Table {table.TableId} has been assigned to your reservation.");
        Console.Clear();
        return table.TableId;
    }
}