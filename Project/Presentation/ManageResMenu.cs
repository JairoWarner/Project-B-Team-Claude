using Project.Presentation;

public class ManageResMenu
{
    public void ManageRes()
    {
        string[] menu = { "View / Cancel Reservations", "Add Walk-in Reservation", "Back" };

        while (true)
        {
            int menuChoice = ArrowNavigation.PrintMenu(menu);

            switch (menuChoice)
            {
                case 1:
                    ShowReservation showReservations = new();
                    showReservations.ShowReservations(true);
                    break;

                case 2:
                    AdminAddRes adminAddRes = new();
                    adminAddRes.AddReservation();
                    break;

                case 3:
                    return;
            }
        }
    }
}