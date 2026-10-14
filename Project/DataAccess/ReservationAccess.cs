
using Dapper;
using Microsoft.Data.Sqlite;
using System.Globalization;

public class ReservationAccess
{
    private readonly SqliteConnection _connection = new("Data Source=DataSources/project.db");
    private const string Table = "Reservation";

    public ReservationAccess()
    {
        string sql = $"CREATE TABLE IF NOT EXISTS {Table} (ID INTEGER PRIMARY KEY AUTOINCREMENT, UserID INTEGER NOT NULL, TableID INTEGER NOT NULL, OrderID INTEGER, Time TEXT NOT NULL, Date TEXT NOT NULL, People INTEGER NOT NULL)";
        _connection.Execute(sql);
    }

    
    public void Write(ReservationModel reservation)
    {
        string sql = $"INSERT INTO {Table} (UserID, TableID, Time, Date, People) VALUES (@UserID, @TableID, @Time, @Date, @People)";
        _connection.Execute(sql, new
        {
            UserID = 1,
            TableID = reservation.SelectedTableId,
            Time = reservation.ReservationTime.ToString("HH:mm"),
            Date = reservation.ReservationDate.ToString("yyyy-MM-dd"),
            People = reservation.NumberOfPeople
        });

        Console.WriteLine("Reservation successfully created!");
    }
}
