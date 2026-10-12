using Microsoft.Data.Sqlite;

using Dapper;
public class FoodAcces
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    private string Table = "Food";

    public FoodModel GetById(int id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @ID";
        return _connection.QueryFirstOrDefault<FoodModel>(sql, new { ID = id });
    }
}