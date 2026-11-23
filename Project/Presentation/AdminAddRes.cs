public class AdminAddRes
{
    private readonly ReservationLogic _logic = new();

    public void AddWalkInReservation()
    {
        AccountModel? admin = AccountsLogic.CurrentAccount;
        if (admin is null)
        {
            Console.WriteLine("A logged-in admin account is required to add a walk-in reservation.");
            Console.ReadLine();
            return;
        }

        AddReservation reservationInput = new();
        if (!reservationInput.AskPeople())
        {
            return;
        }

        DateTime reservationTime = DateTime.Now;
        List<int>? selectedTableIds = reservationInput.AskTable(reservationTime);
        if (selectedTableIds is null)
        {
            return;
        }

        try
        {
            foreach (int tableId in selectedTableIds)
            {
                ReservationModel reservation = new()
                {
                    UserID = admin.Id,
                    SelectedTableId = tableId,
                    ReservationDate = reservationTime.Date,
                    ReservationTime = reservationTime,
                    NumberOfPeople = reservationInput.NumberOfPeople,
                    NumberOfChildren = reservationInput.NumberOfChildren,
                    Status = "on-going"
                };

                _logic.AddReservation(reservation);
            }

            Console.WriteLine("Walk-in reservation added successfully.");
            Console.ReadLine();
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine(exception.Message);
            Console.ReadLine();
        }
    }
}