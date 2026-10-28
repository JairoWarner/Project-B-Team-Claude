class ShowReservation
{
    public void ShowReservations()
    {
        ReservationLogic reservationLogic = new();
        List<ReservationModel> reservations = reservationLogic.GetReservations();

        if (reservations.Count == 0)
        {
            Console.WriteLine("No reservations found.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Reservations:");
        foreach (ReservationModel reservation in reservations)
        {
            Console.WriteLine();
            Console.WriteLine(
                $"User: {reservation.UserID}| " +
                $"Reservation {reservation.Id}: {reservation.ReservationDate:dd-MM-yyyy} at {reservation.ReservationTime:HH:mm}, " +
                $"Table {reservation.SelectedTableId}, {reservation.NumberOfPeople} people");
        }

        Console.ReadLine();
    }
}