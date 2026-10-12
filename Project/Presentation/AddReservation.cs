using System.Globalization;
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
        DateOnly reservationDate = DateOnly.ParseExact(Console.ReadLine() ?? DateTime.Today.ToString("dd-MM-yyyy"),"dd-MM-yyyy", CultureInfo.InvariantCulture);

        Console.WriteLine("What time would you like to make the reservation for? (hh:mm)");
        TimeOnly reservationTime = TimeOnly.Parse(Console.ReadLine() ?? "12:00");

        Console.WriteLine("What table(s) would you like to reserve?");
        string selectedTableInput = Console.ReadLine() ?? "";
        List<int> selectedTableIds = new List<int>();
        string[] tableIds = selectedTableInput.Split(',');
        foreach (string tableId in tableIds)
        {
            if (int.TryParse(tableId.Trim(), out int id))
            {
                selectedTableIds.Add(id);
            }
        }

        ReservationModel reservation = new ReservationModel()
        {
            NumberOfPeople = numberOfPeople,
            NumberOfChildren = numberOfChildren,
            Children = children,
            ReservationDate = reservationDate,
            ReservationTime = reservationTime,
            SelectedTableIds = selectedTableIds
        };

        _logic.AddReservation(reservation);
        return reservation;
    }
}