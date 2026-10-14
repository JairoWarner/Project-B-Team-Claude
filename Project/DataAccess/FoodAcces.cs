using Microsoft.Data.Sqlite;

using Dapper;
public class FoodAcces
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    private string Table = "Food";

    public List<FoodModel> GetMenu(int id)
    {
        // select f.* betekent alles van food selecteren 
        // join betekent dat menufood op food id word gejoint zodat je het menu id per item ziet
        string sql = $"SELECT F.* FROM {Table} F JOIN MenuFood M ON F.ID = M.FoodID WHERE M.MenuID = @ID ORDER BY F.ID";
        return _connection.Query<FoodModel>(sql, new { ID = id }).ToList();
    }
}