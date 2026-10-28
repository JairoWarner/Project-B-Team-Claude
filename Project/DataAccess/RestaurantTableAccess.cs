using Dapper;
using Microsoft.Data.Sqlite;

public class RestaurantTableAccess
{
    private readonly SqliteConnection _connection = new("Data Source=DataSources/project.db");
    private const string Table = "RestaurantTable";

    public List<RestaurantTableModel> GetTablesForDate(DateTime reservationDate, int numberOfPeople)
    {
        string sql = $"""SELECT ID AS TableId, Seats, Available FROM {Table} WHERE Available = 1 AND Seats >= @NumberOfPeople AND NOT EXISTS ( SELECT 1 FROM Reservation WHERE Reservation.TableID = {Table}.ID AND Reservation.Date = @Date) ORDER BY ID""";

        return _connection.Query<RestaurantTableModel>(sql, new
        {
            Date = reservationDate.ToString("yyyy-MM-dd"),
            NumberOfPeople = numberOfPeople
        }).ToList();
    }
}
