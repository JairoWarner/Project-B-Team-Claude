using Project.Presentation;

public class ShowReservation
{
    public void ShowReservations(bool admin = false)
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
        List<ReservationModel> displayedReservations = new();

        Console.WriteLine("Reservations:");

        List<string[]> menuList = new();

        for (int i = 0; i < reservations.Count; i++)
        {
            if (!admin && reservations[i].UserID != currentAccount.Id)
                continue;

            menuList.Add(new string[]
            {
                $"User: {reservations[i].UserID}| " +
                $"Reservation {reservations[i].Id}: {reservations[i].ReservationDate:dd-MM-yyyy} at {reservations[i].ReservationTime:HH:mm}, " +
                $"Table {reservations[i].SelectedTableId}, {reservations[i].NumberOfPeople} people, Status: {reservations[i].Status}",
                "Cancel reservation",
                "Back"
            });

            displayedReservations.Add(reservations[i]);
        }

        if (displayedReservations.Count == 0)
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

            switch (resChoice.Item2)
            {
                case 1:
                    reservationLogic.CancelReservation(displayedReservations[resChoice.Item1 - 1].Id);
                    Console.WriteLine("Reservation canceled.");
                    Console.ReadLine();
                    return;
                case 2:
                    Style.Error("Not implemented yet");
                    break;
                case 3:
                    Style.Error("Not implemented yet");
                    break;
            }
        }
    }
}