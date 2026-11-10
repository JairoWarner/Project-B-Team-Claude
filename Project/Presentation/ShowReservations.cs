class ShowReservation
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
        foreach (ReservationModel reservation in reservations)
        {
            if (reservation.UserID != currentAccount.Id)
            {
                continue;
            }

            Console.WriteLine();
            Console.WriteLine(
                $"User: {reservation.UserID}| " +
                $"Reservation {reservation.Id}: {reservation.ReservationDate:dd-MM-yyyy} at {reservation.ReservationTime:HH:mm}, " +
                $"Table {reservation.SelectedTableId}, {reservation.NumberOfPeople} people");
            displayedReservations++;
        }

        if (displayedReservations == 0)
        {
            Console.WriteLine("No reservations found.");
        }

        Console.ReadLine();
    }
}