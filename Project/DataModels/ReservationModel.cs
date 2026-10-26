public class ReservationModel
{
	public int Id { get; set; }
	public int UserID { get; set; }
	public int NumberOfPeople { get; set; } = 1;
	public int NumberOfChildren { get; set; } = 0;
	public DateTime ReservationDate { get; set; }
	public DateTime ReservationTime { get; set; }
	public int SelectedTableId { get; set; }

	public ReservationModel()
	{
	}

    public ReservationModel(int UserID, int numberOfPeople, int numberOfChildren, DateTime reservationDate, DateTime reservationTime, int selectedTableId)
    {
        this.UserID = UserID;
        NumberOfPeople = numberOfPeople;
        NumberOfChildren = numberOfChildren;
        ReservationDate = reservationDate;
        ReservationTime = reservationTime;
        SelectedTableId = selectedTableId;
    }
}
