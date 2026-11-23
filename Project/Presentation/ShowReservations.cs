using Project.Presentation;

public class ShowReservation
{
    public void ShowReservations()
    {
        AccountModel? currentAccount = AccountsLogic.CurrentAccount;
        if (currentAccount is null)
        {
            Console.WriteLine("A logged-in account is required to show reservations.");
            Console.ReadLine();
            return;
        }

        ReservationLogic reservationLogic = new();
        List<ReservationModel> reservations = reservationLogic.GetReservations();
        int displayedReservations = 0;

        Console.WriteLine("Reservations:");

        List<string[]> menuList = new();

        for (int i = 0; i < reservations.Count; i++)
        {
            if (reservations[i].UserID != currentAccount.Id)
                continue;

            menuList.Add(new string[]
            {
                $"User: {reservations[i].UserID}| " +
                $"Reservation {reservations[i].Id}: {reservations[i].ReservationDate:dd-MM-yyyy} at {reservations[i].ReservationTime:HH:mm}, " +
                $"Table {reservations[i].SelectedTableId}, {reservations[i].NumberOfPeople} people",
                "Edit reservation",
                "Back"
            });

            displayedReservations++;
        }

        if (displayedReservations == 0)
            Console.WriteLine("No reservations found.");

        menuList.Add(new string[] { "Go back" });

        string[][] menu = menuList.ToArray();
        (int, int) resChoice = (0, 0);

        while (true)
        {
            resChoice = ArrowNavigation.PrintJaggedMenu(menu);

            if (resChoice.Item1 == menu.Length)
            {
                return;
            }

            switch (resChoice)
            {
                case (1, 1):
                    break;
                case (2, 1):
                    break;
                case (3, 1):
                    break;
                case (4, 1):
                    break;
                case (5, 1):
                    break;
            }
        }
    }
}