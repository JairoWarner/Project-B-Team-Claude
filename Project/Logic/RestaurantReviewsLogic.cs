using System.Security.Cryptography;
using Project.DataAccess;
using Project.DataModels;

namespace Project.Logic;

public class RestaurantReviewsLogic
{
    ReviewAccess _reviewAccess =  new ReviewAccess();
    
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

    public bool ValidateReviewText(string reviewText)
    {
        if (string.IsNullOrEmpty(reviewText))
        {
            return false;
        }
        
        return true;
    }

    public void CreateReview(double rating, string reviewText)
    {
        AccountModel? currentAccount = AccountsLogic.CurrentAccount;
        DateTime dateTimeNow = DateTime.Now;
        
        var review = new ReviewModel(0, currentAccount.Id, rating, reviewText, dateTimeNow);
        _reviewAccess.AddReview(review);
    }

    public double CalculateAverageRating()
    {
        List<int> allRatings = new();
        allRatings = _reviewAccess.GetAllRating();
        
        return Math.Round(allRatings.Average(), 1);
    }

    public List<ReviewModel> GetAllReviews()
    {
        List<ReviewModel> reviews = _reviewAccess.GetAllReviews();
        return reviews;
    }

    public string GetNameById(Int64 id)
    {
        AccountAccess accountAccess = new AccountAccess();
        string name = accountAccess.GetNameByID(id);

        return name;
    }
}