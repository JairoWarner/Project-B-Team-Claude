using Dapper;
using Microsoft.Data.Sqlite;
using Project.DataModels;

namespace Project.DataAccess;

public class ReviewAccess
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    private string Table = "Reviews";

    public void AddReview(ReviewModel review)
    {
        string sql = $"INSERT INTO {Table} (accountID, rating, reviewText, createdAt) VALUES (@AccountId, @Rating, @ReviewText, @CreatedAt)";
        _connection.Execute(sql, review);
    }
}