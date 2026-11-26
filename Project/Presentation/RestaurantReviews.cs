using Project.Logic;

namespace Project.Presentation;

public static class RestaurantReviews
{
    private static string FullStar = "\u2605";
    private static string HalfStar = "\u2BE8";
    private static string EmptyStar = "\u2606";

    public static void ReviewStart()
    {
        Console.Clear();

        string[] ReviewPage =
        [
            "Leave a review?",
            "Go back"
        ];

        int choice = ArrowNavigation.PrintMenu(ReviewPage);

        switch (choice)
        {
            case 1:
                CreateReview();
                break;
            case 2:
                CustomerMenu.Start();
                break;
        }
    }

    public static void CreateReview()
    {
        Console.Clear();

        string[] ReviewPage =
        [
            $"{EmptyStar} {EmptyStar} {EmptyStar} {EmptyStar} {EmptyStar}",
            $"{HalfStar} {EmptyStar} {EmptyStar} {EmptyStar} {EmptyStar}",
            $"{FullStar} {EmptyStar} {EmptyStar} {EmptyStar} {EmptyStar}",
            $"{FullStar} {HalfStar} {EmptyStar} {EmptyStar} {EmptyStar}",
            $"{FullStar} {FullStar} {EmptyStar} {EmptyStar} {EmptyStar}",
            $"{FullStar} {FullStar} {HalfStar} {EmptyStar} {EmptyStar}",
            $"{FullStar} {FullStar} {FullStar} {EmptyStar} {EmptyStar}",
            $"{FullStar} {FullStar} {FullStar} {HalfStar} {EmptyStar}",
            $"{FullStar} {FullStar} {FullStar} {FullStar} {EmptyStar}",
            $"{FullStar} {FullStar} {FullStar} {FullStar} {HalfStar}",
            $"{FullStar} {FullStar} {FullStar} {FullStar} {FullStar}",
        ];

        while (true)
        {
            var choice = ArrowNavigation.SidewaysArrowNavigation(ReviewPage);
            
            PrintRating(choice);
            Console.ReadLine();
        }
    }

    public static void PrintRating(int rating)
    {
        RestaurantReviewsLogic restaurantReviewsLogic = new RestaurantReviewsLogic();
        
        double rat = restaurantReviewsLogic.CalculateRating(rating);
        Style.Normal($"{rat}/5");
    }
}