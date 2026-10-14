public class AddReservation
{
    private readonly ReservationLogic _logic = new();

    public void start()
    {
        ReservationAdd();
    }

    public ReservationModel ReservationAdd()
    {
        Console.WriteLine("How many people are in your reservation?(including children)");
        int numberOfPeople = int.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine("How many children are in your reservation?");
        int numberOfChildren = int.Parse(Console.ReadLine() ?? "0");

        List<ReservationChildModel> children = [];
        if (numberOfChildren > 0)
        {
            for (int i = 0; i < numberOfChildren; i++)
            {
                Console.WriteLine($"What is the age of child {i + 1}?");
                int age = int.Parse(Console.ReadLine() ?? "0");
                string ageCategory = ReservationLogic.SetChildCategory(age);
                children.Add(new ReservationChildModel { Age = age, AgeCategory = ageCategory });
            }
        }

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

        Console.WriteLine("What table would you like to reserve?");
        int selectedTableId;

        while (!int.TryParse(Console.ReadLine(), out selectedTableId) || selectedTableId <= 0)
        {
            Console.WriteLine("Invalid table ID. Please enter a valid table ID:");
        }

        ReservationModel reservation = new ReservationModel()
        {
            NumberOfPeople = numberOfPeople,
            NumberOfChildren = numberOfChildren,
            Children = children,
            ReservationDate = reservationDate,
            ReservationTime = reservationTime,
            SelectedTableId = selectedTableId
        };

        _logic.AddReservation(reservation);
        Console.WriteLine("Reservation added successfully!");
        return reservation;
    }
}