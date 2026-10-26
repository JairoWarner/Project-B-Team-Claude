public class AddReservation
{
    private readonly ReservationLogic _logic = new();

    public int NumberOfPeople { get; private set; } = 1;
    public int NumberOfChildren { get; private set; }

    public void start()
    {
        AskPeople();
        AskChildren();
        DateTime reservationDate = AskDateAndTime();
        int selectedTableId = AskTable();

        _logic.AddReservation(new ReservationModel
        {
            NumberOfPeople = NumberOfPeople,
            NumberOfChildren = NumberOfChildren,
            ReservationDate = reservationDate.Date,
            ReservationTime = reservationDate,
            SelectedTableId = selectedTableId
        });

        Console.WriteLine("Reservation added successfully!");
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

    public void AskPeople()
    {
        while (true)
        {
            Console.WriteLine("How many people are in your reservation?(MIN 0, MAX 6)(including children)");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                NumberOfPeople = 1;
                return;
            }

            if (!int.TryParse(input, out int numberOfPeople))
            {
                Console.WriteLine("Invalid number of people");
                continue;
            }

            if (numberOfPeople >= 0)
            {
                NumberOfPeople = numberOfPeople;
                return;
            }

            Console.WriteLine("Number of people cannot be lower than 0");
        }
    }

    public void AskChildren()
    {
        while (true)
        {
            Console.WriteLine("How many children are in your reservation?(MIN 0, MAX 5)");
            string? input = Console.ReadLine();

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
                return;
            }

            Console.WriteLine("Number of children cannot be higher than total number of people");
        }
    }

    public DateTime AskDateAndTime()
    {
        Console.WriteLine("What date would you like to make the reservation for? (dd-mm-yyyy)");
        DateTime reservationDate;
        while (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out reservationDate))
        {
            Console.WriteLine("Invalid date format. Please enter the date in the format dd-mm-yyyy:");
        }
        

        Console.WriteLine("What time would you like to make the reservation for? (hh:mm)");
        DateTime reservationTime;
        while (!DateTime.TryParseExact(Console.ReadLine(), "HH:mm", null, System.Globalization.DateTimeStyles.None, out reservationTime))
        {
            Console.WriteLine("Invalid time format. Please enter the time in the format hh:mm:");
        }

        return reservationDate.Date.Add(reservationTime.TimeOfDay);
    }

    public int AskTable()
    {
        Console.WriteLine("What table would you like to reserve?");
        int selectedTableId;
        while (!int.TryParse(Console.ReadLine(), out selectedTableId) || selectedTableId <= 0)
        {
            Console.WriteLine("Invalid table ID. Please enter a valid table ID:");
        }

        return selectedTableId;
    }
}