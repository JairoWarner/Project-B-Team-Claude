public class ReservationModel
{
	public int Id { get; set; }
	public int UserID { get; set; }
	public int NumberOfPeople { get; set; } = 1;
	public List<ReservationChildModel> Children { get; set; } = [];
	public int NumberOfChildren { get; set; } = 0;
	public DateTime ReservationDate { get; set; }
	public DateTime ReservationTime { get; set; }
	public int SelectedTableId { get; set; }

	public ReservationModel()
	{
	}

    public ReservationModel(int UserID, int numberOfPeople, List<ReservationChildModel> children, DateTime reservationDate, DateTime reservationTime, int selectedTableId)
    {
        this.UserID = UserID;
        NumberOfPeople = numberOfPeople;
        Children = children;
        ReservationDate = reservationDate;
        ReservationTime = reservationTime;
        SelectedTableId = selectedTableId;
    }
}

public class ReservationChildModel
{
	public int Age { get; set; }
	public string AgeCategory { get; set; } = string.Empty;

	public ReservationChildModel()
	{
	}

	public ReservationChildModel(int age, string ageCategory)
	{
		Age = age;
		AgeCategory = ageCategory;
	}
}