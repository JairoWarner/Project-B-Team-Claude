
using Dapper;
using Microsoft.Data.Sqlite;
using System.Globalization;
using System.Linq;

public class ReservationAccess
{
    private readonly SqliteConnection _connection = new("Data Source=DataSources/project.db");
    private const string Table = "Reservation";

    // Creates the reservations table if it does not already exist.
    public ReservationAccess()
    {
        string sql = $"CREATE TABLE IF NOT EXISTS {Table} (ID INTEGER PRIMARY KEY AUTOINCREMENT, UserID INTEGER NOT NULL, TableID INTEGER NOT NULL, OrderID INTEGER, Time TEXT NOT NULL, Date TEXT NOT NULL, People INTEGER NOT NULL)";
        _connection.Execute(sql);
    }

    
    // Saves a reservation if its table has no reservation within two hours; throws if there is a conflict.
    public void Write(ReservationModel reservation)
    {
        string sql = $"""
            INSERT INTO {Table} (UserID, TableID, Time, Date, People)
            SELECT @UserID, @TableID, @Time, @Date, @People
            WHERE NOT EXISTS (
                SELECT 1
                FROM {Table}
                WHERE TableID = @TableID
                    AND Date || ' ' || Time > @EarliestAllowedTime
                    AND Date || ' ' || Time < @LatestAllowedTime
            )
            """;
        int rowsInserted = _connection.Execute(sql, new
        {
            UserID = reservation.UserID,
            TableID = reservation.SelectedTableId,
            Time = reservation.ReservationTime.ToString("HH:mm"),
            Date = reservation.ReservationDate.ToString("yyyy-MM-dd"),
            EarliestAllowedTime = reservation.ReservationTime.AddHours(-2).ToString("yyyy-MM-dd HH:mm"),
            LatestAllowedTime = reservation.ReservationTime.AddHours(2).ToString("yyyy-MM-dd HH:mm"),
            People = reservation.NumberOfPeople
        });

        if (rowsInserted == 0)
        {
            throw new InvalidOperationException("During the selected time there is an ongoing reservation");
        }
    }

    // Retrieves all reservations ordered by date and time.
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

    public List<int> GetReservedTableIds(DateTime moment)
    {
        string sql = $@"SELECT TableID FROM {Table}
                       WHERE Date || ' ' || Time > @EarliestTime
                       AND Date || ' ' || Time < @LatestTime";
        
        return _connection.Query<int>(sql, new
        {
            EarliestTime = moment.AddHours(-2).ToString("yyyy-MM-dd HH:mm"),
            LatestTime = moment.AddHours(2).ToString("yyyy-MM-dd HH:mm")
        }).ToList();
    }

    public List<int> GetUserTable(DateTime moment, int userId)
    {
        string sql = $@"SELECT TableID FROM {Table}
                       WHERE Date || ' ' || Time > @EarliestTime
                       AND Date || ' ' || Time < @LatestTime
                       AND UserID = @UserID";

        return _connection.Query<int>(sql, new
        {
            EarliestTime = moment.AddHours(-2).ToString("yyyy-MM-dd HH:mm"),
            LatestTime = moment.AddHours(2).ToString("yyyy-MM-dd HH:mm"),
            UserID = userId
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
