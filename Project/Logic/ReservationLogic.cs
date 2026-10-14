

//This class is not static so later on we can use inheritance and interfaces
public class ReservationLogic
{

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    private ReservationAccess _access = new();

    public static string SetChildCategory(int age)
    {
        if (age < 4)
        {
            return "Child is too young";
        }
        else if (age <= 12)
        {
            return "4-12";
        }
        else
        {
            return "13-17";
        }
    }

    public void AddReservation(ReservationModel reservation)
    {
        

        // Title Case - to do

        _access.Write(reservation);
    }

}