
using Dapper;
using Microsoft.Data.Sqlite;

public class MenuManagementAcces
{
    private readonly string _connectionString =
        "Data Source=DataSources/project.db";

    public List<MenuModel> GetAllMenus()
    {
        using var connection = new SqliteConnection(_connectionString);

        const string sql = "SELECT * FROM Menu ORDER BY ID";

        return connection.Query<MenuModel>(sql).ToList();
    }

    public List<FoodModel> GetFoodByMenu(long menuId)
    {
        using var connection = new SqliteConnection(_connectionString);

        const string sql = @"
            SELECT F.*
            FROM Food F
            INNER JOIN MenuFood MF ON F.ID = MF.FoodID
            WHERE MF.MenuID = @MenuID
            ORDER BY F.Category, F.FoodName";

        return connection.Query<FoodModel>(
            sql,
            new { MenuID = menuId }
        ).ToList();
    }

    public List<FoodModel> GetFoodNotInMenu(long menuId)
    {
        using var connection = new SqliteConnection(_connectionString);

        const string sql = @"
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
            sql,
            new { MenuID = menuId }
        ).ToList();
    }

    public bool AddFoodToMenu(long menuId, long foodId)
    {
        using var connection = new SqliteConnection(_connectionString);

        const string sql = @"
            INSERT INTO MenuFood (MenuID, FoodID)
            VALUES (@MenuID, @FoodID)";

        try
        {
            return connection.Execute(sql, new
            {
                MenuID = menuId,
                FoodID = foodId
            }) > 0;
        }
        catch (SqliteException)
        {
            return false;
        }
    }

    public bool AddFoodToStore(FoodModel food, long? menuId)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            const string insertFoodSql = @"
                INSERT INTO Food
                (FoodName, Price, Description, Category, IsAlcoholic)
                VALUES
                (@FoodName, @Price, @Description, @Category, @IsAlcoholic)";

            connection.Execute(insertFoodSql, new
            {
                food.FoodName,
                food.Price,
                food.Description,
                food.Category,
                food.IsAlcoholic
            }, transaction);

            long newFoodId = connection.ExecuteScalar<long>(
                "SELECT last_insert_rowid();",
                transaction: transaction
            );

            if (menuId.HasValue)
            {
                const string insertMenuFoodSql = @"
                    INSERT INTO MenuFood (MenuID, FoodID)
                    VALUES (@MenuID, @FoodID)";

                connection.Execute(insertMenuFoodSql, new
                {
                    MenuID = menuId.Value,
                    FoodID = newFoodId
                }, transaction);
            }

            transaction.Commit();
            return true;
        }
        catch (SqliteException)
        {
            transaction.Rollback();
            return false;
        }
    }

    public bool RemoveFoodFromMenu(long menuId, long foodId)
    {
        using var connection = new SqliteConnection(_connectionString);

        const string sql = @"
            DELETE FROM MenuFood
            WHERE MenuID = @MenuID
            AND FoodID = @FoodID";

        return connection.Execute(sql, new
        {
            MenuID = menuId,
            FoodID = foodId
        }) > 0;
    }
}
