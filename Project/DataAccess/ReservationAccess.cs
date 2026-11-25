
using Dapper;
using Microsoft.Data.Sqlite;
using System.Linq;

public class ReservationAccess
{
    private readonly SqliteConnection _connection = new("Data Source=DataSources/project.db");
    private const string Table = "Reservation";

    // Creates the reservations table if it does not already exist.
    public ReservationAccess()
    {
        string sql = $"CREATE TABLE IF NOT EXISTS {Table} (ID INTEGER PRIMARY KEY AUTOINCREMENT, UserID INTEGER NOT NULL, TableID INTEGER NOT NULL, OrderID INTEGER, Time TEXT NOT NULL, Date TEXT NOT NULL, People INTEGER NOT NULL, Status TEXT NOT NULL DEFAULT 'Pending')";
        _connection.Execute(sql);

        int statusColumnCount = _connection.ExecuteScalar<int>(
            $"SELECT COUNT(*) FROM pragma_table_info('{Table}') WHERE name = 'Status'");
        if (statusColumnCount == 0)
        {
            _connection.Execute($"ALTER TABLE {Table} ADD COLUMN Status TEXT NOT NULL DEFAULT 'Pending'");
        }
    }

    
    // Saves a reservation if its table has no reservation within two hours; throws if there is a conflict.
    public void Write(ReservationModel reservation)
    {
        string sql = $"""
            INSERT INTO {Table} (UserID, TableID, Time, Date, People, Status)
            SELECT @UserID, @TableID, @Time, @Date, @People, @Status
            WHERE NOT EXISTS (
                SELECT 1
                FROM {Table}
                WHERE TableID = @TableID
                    AND LOWER(COALESCE(Status, '')) != 'canceled'
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
            People = reservation.NumberOfPeople,
            Status = reservation.Status
        });

        if (rowsInserted == 0)
        {
            throw new InvalidOperationException("During the selected time there is an ongoing reservation");
        }
    }

    // Retrieves all reservations ordered by date and time.
    public List<ReservationModel> GetAll()
    {
        string sql = $"""
            SELECT ID AS Id,
                UserID,
                TableID AS SelectedTableId,
                Date AS ReservationDate,
                Date || ' ' || Time AS ReservationTime,
                People AS NumberOfPeople,
                Status
            FROM {Table}
            ORDER BY Date, Time
            """;

        return _connection.Query<ReservationModel>(sql).ToList();
    }

    public List<int> GetReservedTableIds(DateTime moment)
    {
        string sql = $@"SELECT TableID FROM {Table}
                       WHERE Date || ' ' || Time > @EarliestTime
                       AND Date || ' ' || Time < @LatestTime
                       AND LOWER(COALESCE(Status, '')) != 'canceled'";
        
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
                       AND LOWER(COALESCE(Status, '')) != 'canceled'
                       AND UserID = @UserID";

        return _connection.Query<int>(sql, new
        {
            EarliestTime = moment.AddHours(-2).ToString("yyyy-MM-dd HH:mm"),
            LatestTime = moment.AddHours(2).ToString("yyyy-MM-dd HH:mm"),
            UserID = userId
        }).ToList();
    }

    public void ChangeToCanceled(int reservationId)
    {
        string sql = $@"UPDATE {Table} SET Status = 'Canceled' WHERE ID = @ReservationId";
        _connection.Execute(sql, new { ReservationId = reservationId });
    }

    public void Update(ReservationModel reservation)
    {
        string sql = $"""
            UPDATE {Table}
            SET TableID = @TableID,
                Time = @Time,
                Date = @Date,
                People = @People
            WHERE ID = @Id
                AND NOT EXISTS (
                    SELECT 1
                    FROM {Table} AS Existing
                    WHERE Existing.TableID = @TableID
                        AND Existing.ID != @Id
                        AND LOWER(COALESCE(Existing.Status, '')) != 'canceled'
                        AND Existing.Date || ' ' || Existing.Time > @EarliestAllowedTime
                        AND Existing.Date || ' ' || Existing.Time < @LatestAllowedTime
                )
            """;

        int rowsUpdated = _connection.Execute(sql, new
        {
            reservation.Id,
            TableID = reservation.SelectedTableId,
            Time = reservation.ReservationTime.ToString("HH:mm"),
            Date = reservation.ReservationDate.ToString("yyyy-MM-dd"),
            People = reservation.NumberOfPeople,
            EarliestAllowedTime = reservation.ReservationTime.AddHours(-2).ToString("yyyy-MM-dd HH:mm"),
            LatestAllowedTime = reservation.ReservationTime.AddHours(2).ToString("yyyy-MM-dd HH:mm")
        });

        if (rowsUpdated == 0)
        {
            throw new InvalidOperationException("Reservation not found or the selected table is no longer available.");
        }
    }
}
