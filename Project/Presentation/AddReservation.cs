using System;

public class AddReservation
{
    private readonly ReservationLogic _logic = new();

    public int NumberOfPeople { get; private set; } = 1;
    public int NumberOfChildren { get; private set; }

    public void start()
    {
        AccountModel? acc = AccountsLogic.CurrentAccount;
        if (acc is null)
        {
            Console.WriteLine("A logged-in account is required to make a reservation.");
            Console.ReadLine();
            return;
        }

        OrderFood orderFood = new();
        DateTime? reservationDate = AskDateAndTime();
        if (reservationDate is null)
        {
            return;
        }

        if (!AskPeople() || !AskChildren())
        {
            return;
        }

        orderFood.PlaceOrder(acc);

        try
        {
            int selectedTableId = AskTable(reservationDate.Value);
            _logic.AddReservation(new ReservationModel
            {
                UserID = acc.Id,
                NumberOfPeople = NumberOfPeople,
                NumberOfChildren = NumberOfChildren,
                ReservationDate = reservationDate.Value.Date,
                ReservationTime = reservationDate.Value,
                SelectedTableId = selectedTableId
            });

            Console.WriteLine("Reservation added successfully!");
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine(exception.Message);
        }

        Console.ReadLine();
    }

    public bool AskPeople()
    {
        while (true)
        {
            Console.WriteLine("How many people are in your reservation?(MIN 1, MAX 6)(including children) (Enter M to return to the main menu)");
            string? input = Console.ReadLine();

            if (input?.ToUpper() == "M")
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                NumberOfPeople = 1;
                return true;
            }

            if (!int.TryParse(input, out int numberOfPeople))
            {
                Console.WriteLine("Invalid number of people");
                continue;
            }

            if (ReservationLogic.ValidateNumberOfPeople(numberOfPeople))
            {
                NumberOfPeople = numberOfPeople;
                Console.Clear();
                return true;
            }

            Console.WriteLine("Number of people must be between 1 and 6");
        }
    }

    public bool AskChildren()
    {
        while (true)
        {
            Console.WriteLine($"How many children are in your reservation?(MIN 0, MAX {NumberOfPeople - 1}) (Enter M to return to the main menu)");
            string? input = Console.ReadLine();

            if (input?.ToUpper() == "M")
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                NumberOfChildren = 0;
            }
            else if (!int.TryParse(input, out int numberOfChildren))
            {
                Console.WriteLine("Invalid number of children");
                continue;
            }
            else
            {
                NumberOfChildren = numberOfChildren;
            }

            if (ReservationLogic.ValidateNumberOfChildren(NumberOfChildren, NumberOfPeople))
            {
                Console.Clear();
                return true;
            }

            Console.WriteLine("Number of children must be at least 0 and lower than the total number of people");
        }
    }

    public DateTime? AskDateAndTime()
    {
        DateTime reservationDate;
        while (true)
        {
            Console.WriteLine("What date would you like to make the reservation for? (dd-mm-yyyy) (Enter M to return to the main menu)");
            string? input = Console.ReadLine();

            if (input?.ToUpper() == "M")
            {
                return null;
            }

            if (!DateTime.TryParseExact(input, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out reservationDate))
            {
                Console.WriteLine("Invalid date format. Please enter the date in the format dd-mm-yyyy:");
                continue;
            }

            DateTime currentTime = DateTime.Now;
            ReservationDateTimeValidationResult dateValidation = ReservationLogic.ValidateReservationDate(reservationDate, currentTime);
            if (dateValidation != ReservationDateTimeValidationResult.Valid)
            {
                Console.WriteLine(GetValidationMessage(dateValidation));
                continue;
            }

            Console.Clear();
            break;
        }

        DateTime reservationTime;
        while (true)
        {
            Console.WriteLine("What time would you like to make the reservation for? (hh:mm) (Enter M to return to the main menu)");
            string? input = Console.ReadLine();

            if (input?.ToUpper() == "M")
            {
                return null;
            }

            if (DateTime.TryParseExact(input, "HH:mm", null, System.Globalization.DateTimeStyles.None, out reservationTime))
            {
                DateTime reservationDateTime = reservationDate.Date.Add(reservationTime.TimeOfDay);
                DateTime currentTime = DateTime.Now;
                ReservationDateTimeValidationResult validation = ReservationLogic.ValidateReservationDateTime(reservationDateTime, currentTime);
                if (validation != ReservationDateTimeValidationResult.Valid)
                {
                    Console.WriteLine(GetValidationMessage(validation));
                    continue;
                }

                if (ReservationLogic.NeedsShortNoticeConfirmation(reservationDateTime, currentTime) && !ConfirmShortNoticeReservation())
                {
                    return null;
                }

                Console.Clear();
                return reservationDateTime;
            }

            Console.WriteLine("Invalid time format. Please enter the time in the format hh:mm:");
        }
    }

    public int AskTable(DateTime reservationDate)
    {
        int selectedTableId = _logic.GetAvailableTableId(reservationDate, NumberOfPeople);

        Console.WriteLine($"Table {selectedTableId} has been assigned to your reservation.");
        Console.ReadLine();
        Console.Clear();
        return selectedTableId;
    }

    private static string GetValidationMessage(ReservationDateTimeValidationResult validation)
    {
        if (validation == ReservationDateTimeValidationResult.BeforeCurrentDate)
        {
            return "Cannot put in a reservation before current date";
        }

        if (validation == ReservationDateTimeValidationResult.BeforeCurrentTime)
        {
            return "Cannot put in a reservation before current time";
        }

        if (validation == ReservationDateTimeValidationResult.AfterOneYear)
        {
            return "Cannot put in a reservation after 1 year";
        }

        return "Invalid reservation date or time";
    }

    private static bool ConfirmShortNoticeReservation()
    {
        Console.WriteLine("You have put in a reservation less than 48 before the reservation");
        Console.WriteLine("if you want to cancel the reservation after confirming, a fine of 5 dollars has to be paid");

        while (true)
        {
            Console.WriteLine("Confirm reservation Y/N");
            string? response = Console.ReadLine();
            if (response?.ToUpper() == "Y")
            {
                return true;
            }

            if (response?.ToUpper() == "N")
            {
                return false;
            }
        }
    }
}
