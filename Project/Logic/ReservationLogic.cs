using System;
using System.Collections.Generic;

public enum ReservationDateTimeValidationResult
{
    Valid,
    BeforeCurrentDate,
    BeforeCurrentTime,
    AfterOneYear
}

public class ReservationLogic
{
    private ReservationAccess _access = new();
    private readonly RestaurantTableAccess _tableAccess = new();

    public int LimitPeople(int numberOfPeople)
    {
        return 0;
    }

    public static bool ValidateNumberOfPeople(int numberOfPeople)
    {
        return numberOfPeople >= 1 && numberOfPeople <= 6;
    }

    public static bool ValidateNumberOfChildren(int numberOfChildren, int numberOfPeople)
    {
        return ValidateNumberOfPeople(numberOfPeople) && numberOfChildren >= 0 && numberOfChildren < numberOfPeople;
    }

    public void AddReservation(ReservationModel reservation)
    {
        if (!ValidateNumberOfPeople(reservation.NumberOfPeople))
        {
            throw new InvalidOperationException("Number of people must be between 1 and 6");
        }

        if (!ValidateNumberOfChildren(reservation.NumberOfChildren, reservation.NumberOfPeople))
        {
            throw new InvalidOperationException("Number of children must be at least 0 and lower than the total number of people");
        }

        _access.Write(reservation);
    }

    public int GetAvailableTableId(DateTime reservationDate, int numberOfPeople)
    {
        List<RestaurantTableModel> availableTables = _tableAccess.GetTablesAvailableAt(reservationDate, numberOfPeople);

        if (availableTables.Count == 0)
        {
            bool aTableFitsTheParty = _tableAccess.GetTablesForDate(reservationDate, numberOfPeople).Count > 0;

            if (aTableFitsTheParty && !_tableAccess.HasAvailabilityDuringDay(reservationDate, numberOfPeople))
            {
                throw new InvalidOperationException("Table is booked full for the day, please select another table");
            }
            else if (_tableAccess.HasOngoingReservation(reservationDate, numberOfPeople))
            {
                throw new InvalidOperationException("During the selected time there is an ongoing reservation");
            }
            else
            {
                throw new InvalidOperationException("Table is already booked at this time, please select a different time");
            }
        }

        return availableTables[0].TableId;
    }

    public static ReservationDateTimeValidationResult ValidateReservationDate(DateTime reservationDate, DateTime currentTime)
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

    public static ReservationDateTimeValidationResult ValidateReservationDateTime(DateTime reservationDateTime, DateTime currentTime)
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

    public static bool NeedsShortNoticeConfirmation(DateTime reservationDateTime, DateTime currentTime)
    {
        TimeSpan timeUntilReservation = reservationDateTime - currentTime;
        return timeUntilReservation >= TimeSpan.Zero && timeUntilReservation < TimeSpan.FromHours(48);
    }

    public List<ReservationModel> GetReservations()
    {
        return _access.GetAll();
    }
}
