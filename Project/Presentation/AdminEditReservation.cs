using Project.Presentation;

public class AdminEditReservation
{
    private readonly ReservationLogic _logic = new();

    public void EditReservation()
    {
        List<ReservationModel> reservations = _logic.GetReservations();
        if (reservations.Count == 0)
        {
            Console.WriteLine("No reservations found.");
            Console.ReadLine();
            return;
        }

        string[] menu = reservations
            .Select(reservation =>
                $"Reservation {reservation.Id}: {reservation.ReservationDate:dd-MM-yyyy} at " +
                $"{reservation.ReservationTime:HH:mm}, Table {reservation.SelectedTableId}, " +
                $"{reservation.NumberOfPeople} people")
            .Append("Back")
            .ToArray();

        int choice = ArrowNavigation.PrintMenu(menu);
        if (choice == menu.Length)
        {
            return;
        }

        ReservationModel reservationToEdit = reservations[choice - 1];
        string[] fieldsToEdit = { "Date and time", "Number of people", "Table", "Back" };
        int fieldChoice = ArrowNavigation.PrintMenu(fieldsToEdit);
        AddReservation reservationInput = new();

        switch (fieldChoice)
        {
            case 1:
                DateTime? reservationTime = reservationInput.AskDateAndTime();
                if (reservationTime is null)
                    return;

                reservationToEdit.ReservationDate = reservationTime.Value.Date;
                reservationToEdit.ReservationTime = reservationTime.Value;
                break;

            case 2:
                if (!reservationInput.AskPeople())
                    return;

                reservationToEdit.NumberOfPeople = reservationInput.NumberOfPeople;
                break;

            case 3:
                List<int>? selectedTableIds = reservationInput.AskTable(
                    reservationToEdit.ReservationTime,
                    reservationToEdit.NumberOfPeople,
                    reservationToEdit.Id);
                if (selectedTableIds is null || selectedTableIds.Count == 0)
                    return;

                reservationToEdit.SelectedTableId = selectedTableIds[0];
                break;

            case 4:
                return;
        }

        SaveReservation(reservationToEdit);
    }

    private void SaveReservation(ReservationModel reservation)
    {
        try
        {
            _logic.UpdateReservation(reservation);
            Console.WriteLine("Reservation updated successfully.");
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine(exception.Message);
        }

        Console.ReadLine();
    }
}