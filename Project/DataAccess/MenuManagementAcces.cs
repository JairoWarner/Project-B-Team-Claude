
using Microsoft.Data.Sqlite;
using Dapper;

public class MenuManagementAcces
{
    private readonly string _connectionString =
        "Data Source=DataSources/project.db";

    // Get all existing menus
    public List<MenuModel> GetAllMenus()
    {
        using var connection = new SqliteConnection(_connectionString);

        string sql = "SELECT * FROM Menu";

        return connection.Query<MenuModel>(sql).ToList();
    }

    // Get all available food
    public List<FoodModel> GetAllFood()
    {
        using var connection = new SqliteConnection(_connectionString);

        string sql = "SELECT * FROM Food";

        return connection.Query<FoodModel>(sql).ToList();
    }

    // Add a new menu with selected food
    public bool AddMenu(MenuModel menu, List<long> foodIds)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            string sql = @"
                INSERT INTO Menu (Name, Description)
                VALUES (@Name, @Description);

                SELECT last_insert_rowid();";

            long menuId = connection.ExecuteScalar<long>(
                sql, menu, transaction);

            foreach (long foodId in foodIds)
            {
                string foodSql = @"
                    INSERT INTO MenuFood (MenuID, FoodID)
                    VALUES (@MenuID, @FoodID)";

                connection.Execute(
                    foodSql,
                    new { MenuID = menuId, FoodID = foodId },
                    transaction);
            }

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }

    // Remove an existing menu
    public bool RemoveMenu(long menuId)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            string deleteFoodSql = @"
                DELETE FROM MenuFood
                WHERE MenuID = @MenuID";

            connection.Execute(
                deleteFoodSql,
                new { MenuID = menuId },
                transaction);

            string deleteMenuSql = @"
                DELETE FROM Menu
                WHERE ID = @MenuID";

            int result = connection.Execute(
                deleteMenuSql,
                new { MenuID = menuId },
                transaction);

            transaction.Commit();

            return result > 0;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }
}
