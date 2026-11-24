public class AdminAddRes
{
    private readonly ReservationLogic _logic = new();
    private const int WalkInAccountId = 1;
    public void AddReservation(bool walkIn = false)
    {
        AccountModel? admin = AccountsLogic.CurrentAccount;
        if (admin is null)
        {
            Console.WriteLine("A logged-in admin account is required to add a reservation.");
            Console.ReadLine();
            return;
        }

        AddReservation reservationInput = new();
        DateTime reservationTime;

        if (walkIn)
        {
            reservationTime = DateTime.Now;

            if (!reservationInput.AskPeople())
            {
                return;
            }
        }
        else
        {
            DateTime? chosenTime = reservationInput.AskDateAndTime();
            if (chosenTime is null)
            {
                return;
            }
            reservationTime = chosenTime.Value;

            if (!reservationInput.AskPeople() || !reservationInput.AskChildren())
            {
                return;
            }
        }

        List<int>? selectedTableIds = reservationInput.AskTable(reservationTime);
        if (selectedTableIds is null)
        {
            return;
        }

        try
        {
            string status;
            if (walkIn)
            {
                status = "on-going";
            }
            else
            {
                status = "Pending";
            }
            foreach (int tableId in selectedTableIds)
            {
                ReservationModel reservation = new()
                {
                    UserID = WalkInAccountId,
                    SelectedTableId = tableId,
                    ReservationDate = reservationTime.Date,
                    ReservationTime = reservationTime,
                    NumberOfPeople = reservationInput.NumberOfPeople,
                    NumberOfChildren = reservationInput.NumberOfChildren,
                    Status = status
                };

                _logic.AddReservation(reservation);
            }

            Console.WriteLine("Reservation added successfully.");
            Console.ReadLine();
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine(exception.Message);
            Console.ReadLine();
        }
    }
}