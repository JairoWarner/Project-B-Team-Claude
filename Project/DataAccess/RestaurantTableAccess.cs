using Dapper;
using Microsoft.Data.Sqlite;

public class RestaurantTableAccess
{
    private readonly SqliteConnection _connection = new("Data Source=DataSources/project.db");
    private const string Table = "RestaurantTable";

    public List<RestaurantTableModel> GetAllTables()
    {
        string sql = $"SELECT ID as TableId, Available, Seats FROM {Table} ORDER BY TableId";
        return _connection.Query<RestaurantTableModel>(sql).ToList();
    }

    public List<int> GetFreeBarSeats(DateTime moment, int? excludedReservationId = null)
    {
        string sql = $@"SELECT ID FROM {Table}
                    WHERE Type = 'Bar'
                    AND Available = 1
                    AND NOT EXISTS (
                        SELECT 1 FROM Reservation
                        WHERE Reservation.TableID = {Table}.ID
                        AND (@ExcludedReservationId IS NULL OR Reservation.ID != @ExcludedReservationId)
                        AND LOWER(COALESCE(Reservation.Status, '')) != 'canceled'
                        AND Reservation.Date || ' ' || Reservation.Time > @EarliestTime
                        AND Reservation.Date || ' ' || Reservation.Time < @LatestTime
                    )
                    ORDER BY ID";
        
        return _connection.Query<int>(sql, new
        {
            ExcludedReservationId = excludedReservationId,
            EarliestTime = moment.AddHours(-2).ToString("yyyy-MM-dd HH:mm"),
            LatestTime = moment.AddHours(2).ToString("yyyy-MM-dd HH:mm")
        }).ToList();
    }
    // Retrieves active tables with enough seats for the party.
    public List<RestaurantTableModel> GetTablesForDate(DateTime reservationDate, int numberOfPeople)
    {
        string sql = $"""SELECT ID AS TableId, Seats, Available FROM {Table} WHERE Available = 1 AND Seats >= @NumberOfPeople ORDER BY ID""";

        return _connection.Query<RestaurantTableModel>(sql, new
        {
            NumberOfPeople = numberOfPeople
        }).ToList();
    }

    // Retrieves active tables with enough seats and no reservation within two hours of the requested time.
    public List<RestaurantTableModel> GetTablesAvailableAt(DateTime reservationTime, int numberOfPeople, int? excludedReservationId = null)
    {
        string sql = $"""
            SELECT ID AS TableId, Seats, Available
            FROM {Table}
            WHERE Available = 1
                AND Seats >= @NumberOfPeople
                AND Type = 'Table'
                AND NOT EXISTS (
                    SELECT 1
                    FROM Reservation
                    WHERE Reservation.TableID = {Table}.ID
                        AND (@ExcludedReservationId IS NULL OR Reservation.ID != @ExcludedReservationId)
                        AND LOWER(COALESCE(Reservation.Status, '')) != 'canceled'
                        AND Reservation.Date || ' ' || Reservation.Time > @EarliestAllowedTime
                        AND Reservation.Date || ' ' || Reservation.Time < @LatestAllowedTime
                )
            ORDER BY ID
            """;

        return _connection.Query<RestaurantTableModel>(sql, new
        {
            NumberOfPeople = numberOfPeople,
            ExcludedReservationId = excludedReservationId,
            EarliestAllowedTime = reservationTime.AddHours(-2).ToString("yyyy-MM-dd HH:mm"),
            LatestAllowedTime = reservationTime.AddHours(2).ToString("yyyy-MM-dd HH:mm")
        }).ToList();
    }

    // Checks whether a suitable table has a reservation that started within the prior two hours.
    public bool HasOngoingReservation(DateTime reservationTime, int numberOfPeople)
    {
        string sql = $"""
            SELECT EXISTS (
                SELECT 1
                FROM Reservation
                INNER JOIN {Table} ON Reservation.TableID = {Table}.ID
                WHERE {Table}.Available = 1
                    AND {Table}.Seats >= @NumberOfPeople
                    AND LOWER(COALESCE(Reservation.Status, '')) != 'canceled'
                    AND Reservation.Date || ' ' || Reservation.Time >= @EarliestAllowedTime
                    AND Reservation.Date || ' ' || Reservation.Time <= @ReservationTime
            )
            """;

        return _connection.ExecuteScalar<bool>(sql, new
        {
            NumberOfPeople = numberOfPeople,
            EarliestAllowedTime = reservationTime.AddHours(-2).ToString("yyyy-MM-dd HH:mm"),
            ReservationTime = reservationTime.ToString("yyyy-MM-dd HH:mm")
        });
    }

    // Checks whether any suitable table has a two-hour reservation slot during the restaurant's opening hours.
    public bool HasAvailabilityDuringDay(DateTime reservationDate, int numberOfPeople)
    {
        List<RestaurantTableModel> tables = GetTablesForDate(reservationDate, numberOfPeople);
        if (tables.Count == 0)
        {
            return false;
        }

        string sql = "SELECT TableID, Time FROM Reservation WHERE Date = @Date AND LOWER(COALESCE(Status, '')) != 'canceled'";
        List<ReservationTimeRow> reservations = _connection.Query<ReservationTimeRow>(sql, new
        {
            Date = reservationDate.ToString("yyyy-MM-dd")
        }).ToList();

        TimeSpan openTime = new(12, 0, 0);
        TimeSpan closingTime = new(23, 0, 0);

        for (TimeSpan time = openTime; time <= closingTime - TimeSpan.FromHours(2); time += TimeSpan.FromMinutes(1))
        {
            DateTime candidate = reservationDate.Date.Add(time);
            if (tables.Any(table => !reservations.Any(reservation =>
                    reservation.TableId == table.TableId
                    && candidate > reservationDate.Date.Add(
                        TimeSpan.ParseExact(reservation.Time, "hh\\:mm", System.Globalization.CultureInfo.InvariantCulture)).AddHours(-2)
                    && candidate < reservationDate.Date.Add(
                        TimeSpan.ParseExact(reservation.Time, "hh\\:mm", System.Globalization.CultureInfo.InvariantCulture)).AddHours(2))))
            {
                return true;
            }
        }

        return false;
    }

    private sealed class ReservationTimeRow
    {
        public int TableId { get; set; }
        public string Time { get; set; } = "";
    }
}
