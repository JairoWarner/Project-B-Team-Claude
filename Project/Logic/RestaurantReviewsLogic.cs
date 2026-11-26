namespace Project.Logic;

public class RestaurantReviewsLogic
{
    public double CalculateRating(int rating)
    {
        double rat = rating switch
        {
            1 => 0,
            2 => 0.5,
            3 => 1,
            4 => 1.5,
            5 => 2,
            6 => 2.5,
            7 => 3,
            8 => 3.5,
            9 => 4,
            10 => 4.5,
            11 => 5
        };
        
        return rat;
    }
}