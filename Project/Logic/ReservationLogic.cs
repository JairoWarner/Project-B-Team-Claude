

public enum ReservationDateTimeValidationResult
{
    Valid,
    BeforeCurrentDate,
    BeforeCurrentTime,
    AfterOneYear
}

//This class is not static so later on we can use inheritance and interfaces
public class ReservationLogic
{

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    private ReservationAccess _access = new();


    // Placeholder for limiting the party size; no limit is applied yet.
    public int LimitPeople(int numberOfPeople)
    {
        return 0;
    }


    // Saves a reservation through the data access layer.
    public void AddReservation(ReservationModel reservation)
    {
        // Title Case - to do

        _access.Write(reservation);
    }

    // Checks that the reservation date is not before today or more than one year away.
    public static ReservationDateTimeValidationResult ValidateReservationDate(
        DateTime reservationDate,
        DateTime currentTime)
    {
        if (reservationDate.Date < currentTime.Date)
        {
            return ReservationDateTimeValidationResult.BeforeCurrentDate;
        }

        if (reservationDate.Date > currentTime.AddYears(1).Date)
        {
            return ReservationDateTimeValidationResult.AfterOneYear;
        }

        return ReservationDateTimeValidationResult.Valid;
    }

    // Validates the date and time, including that today's reservation time is not in the past.
    public static ReservationDateTimeValidationResult ValidateReservationDateTime(
        DateTime reservationDateTime,
        DateTime currentTime)
    {
        ReservationDateTimeValidationResult dateResult = ValidateReservationDate(reservationDateTime, currentTime);
        if (dateResult != ReservationDateTimeValidationResult.Valid)
        {
            return dateResult;
        }

        if (reservationDateTime < currentTime)
        {
            return ReservationDateTimeValidationResult.BeforeCurrentTime;
        }

        if (reservationDateTime > currentTime.AddYears(1))
        {
            return ReservationDateTimeValidationResult.AfterOneYear;
        }

        return ReservationDateTimeValidationResult.Valid;
    }

    // Returns true when a future reservation is less than 48 hours away and needs confirmation.
    public static bool NeedsShortNoticeConfirmation(DateTime reservationDateTime, DateTime currentTime)
    {
        TimeSpan timeUntilReservation = reservationDateTime - currentTime;
        return timeUntilReservation >= TimeSpan.Zero && timeUntilReservation < TimeSpan.FromHours(48);
    }

    // Retrieves all saved reservations from the data access layer.
    public List<ReservationModel> GetReservations()
    {
        return _access.GetAll();
    }

}