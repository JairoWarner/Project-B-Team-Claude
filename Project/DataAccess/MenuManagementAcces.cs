
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

        string sql = "SELECT * FROM Menu ORDER BY ID";

        return connection.Query<MenuModel>(sql).ToList();
    }

    // Get all food items currently in a menu
    public List<FoodModel> GetFoodByMenu(long menuId)
    {
        using var connection = new SqliteConnection(_connectionString);

        string sql = @"
            SELECT F.*
            FROM Food F
            INNER JOIN MenuFood MF ON F.ID = MF.FoodID
            WHERE MF.MenuID = @MenuID
            ORDER BY F.Category, F.FoodName";

        return connection.Query<FoodModel>(
            sql, new { MenuID = menuId }).ToList();
    }

    // Get food items that are not yet in the selected menu
    public List<FoodModel> GetFoodNotInMenu(long menuId)
    {
        using var connection = new SqliteConnection(_connectionString);

        string sql = @"
            SELECT F.*
            FROM Food F
            WHERE NOT EXISTS (
                SELECT 1
                FROM MenuFood MF
                WHERE MF.FoodID = F.ID
                AND MF.MenuID = @MenuID
            )
            ORDER BY F.Category, F.FoodName";

        return connection.Query<FoodModel>(
            sql, new { MenuID = menuId }).ToList();
    }

    // Add food to an existing menu
    public bool AddFoodToMenu(long menuId, long foodId)
    {
        using var connection = new SqliteConnection(_connectionString);

        string sql = @"
            INSERT INTO MenuFood (MenuID, FoodID)
            VALUES (@MenuID, @FoodID)";

        try
        {
            int result = connection.Execute(sql, new
            {
                MenuID = menuId,
                FoodID = foodId
            });

            return result > 0;
        }
        catch (SqliteException)
        {
            return false;
        }
    }

    // Remove food from an existing menu
    public bool RemoveFoodFromMenu(long menuId, long foodId)
    {
        using var connection = new SqliteConnection(_connectionString);

        string sql = @"
            DELETE FROM MenuFood
            WHERE MenuID = @MenuID
            AND FoodID = @FoodID";

        int result = connection.Execute(sql, new
        {
            MenuID = menuId,
            FoodID = foodId
        });

        return result > 0;
    }
}
