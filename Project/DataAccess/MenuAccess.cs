using Microsoft.Data.Sqlite;

using Dapper;
using SQLitePCL;

public class MenuAcces
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    private string Table = "Menu";

    public List<MenuModel> GetMenu()
    {
        string sql = $"SELECT * FROM {Table}";
        return _connection.Query<MenuModel>(sql).ToList();
    }
}