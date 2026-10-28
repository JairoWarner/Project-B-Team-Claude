
using Dapper;
using Microsoft.Data.Sqlite;
using System.Globalization;
using System.Linq;

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
            UserID = reservation.UserID,
            TableID = reservation.SelectedTableId,
            Time = reservation.ReservationTime.ToString("HH:mm"),
            Date = reservation.ReservationDate.ToString("yyyy-MM-dd"),
            People = reservation.NumberOfPeople
        });

        Console.WriteLine("Reservation successfully created!");
    }

    public List<ReservationModel> GetAll()
    {
        string sql = $"SELECT ID AS Id, UserID, TableID AS SelectedTableId, Date, Time, People AS NumberOfPeople FROM {Table} ORDER BY Date, Time";
        IEnumerable<ReservationRow> rows = _connection.Query<ReservationRow>(sql);

        return rows.Select(row => new ReservationModel
        {
            Id = row.Id,
            UserID = row.UserID,
            SelectedTableId = row.SelectedTableId,
            ReservationDate = DateTime.ParseExact(row.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture),
            ReservationTime = DateTime.ParseExact(row.Time, "HH:mm", CultureInfo.InvariantCulture),
            NumberOfPeople = row.NumberOfPeople
        }).ToList();
    }

    private sealed class ReservationRow
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int SelectedTableId { get; set; }
        public string Date { get; set; } = "";
        public string Time { get; set; } = "";
        public int NumberOfPeople { get; set; }
    }
}
