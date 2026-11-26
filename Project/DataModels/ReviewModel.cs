namespace Project.DataModels;

public class ReviewModel
{
    public Int64 Id { get; set; }
    public Int64 AccountId { get; set; }
    public Int64 Rating { get; set; }
    public string ReviewText { get; set; }
    public DateTime CreatedAt { get; set; }

    public ReviewModel(Int64 id, Int64 accountId, Int64 rating, string reviewText, DateTime createdAt)
    {
        Id = id;
        AccountId = accountId;
        Rating = rating;
        ReviewText = reviewText;
        CreatedAt = createdAt;
    }
}