using Microsoft.Data.Sqlite;

using Dapper;
public class FoodAcces
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    private string Table = "Food";

    public List<FoodModel> GetMenu(int id)
    {
        string sql = $"SELECT F.* FROM Food F JOIN MenuFood M ON F.ID = M.FoodID WHERE M.MenuID = @ID";
        return _connection.Query<FoodModel>(sql, new { ID = id }).ToList();
    }
}