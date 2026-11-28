using Project.DataModels;
using Project.Logic;

namespace Project.Presentation;

public static class RestaurantReviews
{
    private static string FullStar = "\u2605";
    private static string HalfStar = "\u2BE8";
    private static string EmptyStar = "\u2606";
    public static double Rating { get; set; }
    public static string ReviewText { get; set; }
    public static RestaurantReviewsLogic _logic = new RestaurantReviewsLogic();

    public static void ReviewStart()
    {
        Console.Clear();
        double rating = _logic.CalculateAverageRating();

        string[] ReviewPage =
        [
            "Leave a review?",
            "Go back"
        ];

        int choice = ArrowNavigation.PrintMenu(ReviewPage, () =>
        {
            Console.WriteLine($"Restaurant rating: {rating}/5\n");
            Style.Normal($"{Style.LightGrey}──────────────────────────────────────\n");
            PrintAllReviews();
        });

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
            TextReview();

            break;
        }

        _logic.CreateReview(Rating, ReviewText);
        ReviewStart();
    }

    public static void PrintRating(int rating)
    {
        RestaurantReviewsLogic restaurantReviewsLogic = new RestaurantReviewsLogic();

        Rating = restaurantReviewsLogic.CalculateRating(rating);
        Style.Normal($"{Rating}/5");
    }

    public static void TextReview()
    {
        string[] YN = ["Yes", "No"];

        while (true)
        {
            int choice = ArrowNavigation.SidewaysArrowNavigation(YN, () =>
            {
                Style.Normal("Would you like to specify your review?");
                Style.Normal($"{Style.LightGrey}──────────────────────────────────────\n");
            });

            switch (choice)
            {
                case 1:
                    Style.Normal("Write your review:\n");
                    WriteText();
                    break;
                case 2:
                    Style.Normal("No text was added!");
                    Thread.Sleep(2000);
                    Style.WaitingAnimation();
                    break;
            }

            break;
        }
    }

    public static void WriteText()
    {
        ReviewText = Console.ReadLine();

        while (true)
        {
            if (!_logic.ValidateReviewText(ReviewText))
                Style.Error("Invalid review text!");
            else
                break;
        }
    }

    public static void PrintAllReviews()
    {
        List<ReviewModel> reviews = _logic.GetAllReviews();

        foreach (ReviewModel review in reviews)
        {
            string name = _logic.GetNameById(review.AccountId);
            Style.Normal($"{name} On: {review.CreatedAt}\n\nRating: {review.Rating}/5\n\n{review.ReviewText}\n");
            Style.Normal($"{Style.LightGrey}──────────────────────────────────────\n");
        }
    }
}