using Dapper;
using Microsoft.Data.Sqlite;
 
public class OrderAccess
{
    private readonly SqliteConnection _connection = new("Data Source=DataSources/project.db");
 
    public long Write(List<OrderLine> lines, double totalPrice)
    {
        _connection.Open();
        try
        {
            using SqliteTransaction transaction = _connection.BeginTransaction();
 
            long orderId = _connection.ExecuteScalar<long>(
                "INSERT INTO Orders (Price) VALUES (@Price); SELECT last_insert_rowid();",
                new { Price = totalPrice },
                transaction);
 
            foreach (OrderLine line in lines)
            {
                _connection.Execute(
                    "INSERT INTO OrderedFood (OrderID, FoodID, Quantity) VALUES (@OrderID, @FoodID, @Quantity)",
                    new { OrderID = orderId, FoodID = line.Food.ID, Quantity = line.Quantity },
                    transaction);
            }
 
            transaction.Commit();
            return orderId;
        }
        finally
        {
            _connection.Close();
        }
    }
}