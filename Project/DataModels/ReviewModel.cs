namespace Project.DataModels;

public class ReviewModel
{
    public Int64 Id { get; set; }
    public Int64 AccountId { get; set; }
    public double Rating { get; set; }
    public string ReviewText { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public ReviewModel()
    {
    }
    
    public ReviewModel(Int64 id, Int64 accountId, double rating, string reviewText, DateTime createdAt)
    {
        Id = id;
        AccountId = accountId;
        Rating = rating;
        ReviewText = reviewText;
        CreatedAt = createdAt;
    }
}