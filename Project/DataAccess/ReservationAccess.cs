using Dapper;
using Microsoft.Data.Sqlite;

public class ReservationAccess
{
    private readonly SqliteConnection _connection = new("Data Source=DataSources/project.db");
    private const string Table = "Reservations";

    public ReservationAccess()
    {
        string sql = $"CREATE TABLE IF NOT EXISTS {Table} (id INTEGER PRIMARY KEY AUTOINCREMENT, accountid INTEGER NOT NULL, numberofpeople INTEGER NOT NULL, reservationdate TEXT NOT NULL, reservationtime TEXT NOT NULL, selectedtableids TEXT NOT NULL)";
        _connection.Execute(sql);
    }

    public void Write(ReservationModel reservation)
    {
        string sql = $"INSERT INTO {Table} (accountid, numberofpeople, reservationdate, reservationtime, selectedtableids) VALUES (@AccountId, @NumberOfPeople, @ReservationDate, @ReservationTime, @SelectedTableIds)";
        _connection.Execute(sql, reservation);
    }

    public ReservationModel? GetById(long id)
    {
        string sql = $"SELECT * FROM {Table} WHERE id = @Id";
        return _connection.QueryFirstOrDefault<ReservationModel>(sql, new { Id = id });
    }

    public void Update(ReservationModel reservation)
    {
        string sql = $"UPDATE {Table} SET accountid = @AccountId, numberofpeople = @NumberOfPeople, reservationdate = @ReservationDate, reservationtime = @ReservationTime, selectedtableids = @SelectedTableIds WHERE id = @Id";
        _connection.Execute(sql, reservation);
    }

    public void Delete(ReservationModel reservation)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = reservation.Id });
    }
}