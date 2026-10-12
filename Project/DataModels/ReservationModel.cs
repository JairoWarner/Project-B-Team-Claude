public class ReservationModel
{
	public int Id { get; set; }
	public int AccountId { get; set; }
	public int NumberOfPeople { get; set; } = 1;
	public List<ReservationChildModel> Children { get; set; } = [];
	public int NumberOfChildren { get; set; } = 0;
	public DateOnly ReservationDate { get; set; }
	public TimeOnly ReservationTime { get; set; }
	public List<int> SelectedTableIds { get; set; } = [];

	public ReservationModel()
	{
	}

	public ReservationModel(int accountId, int numberOfPeople, List<ReservationChildModel> children, DateOnly reservationDate, TimeOnly reservationTime, List<int> selectedTableIds)
	{
		AccountId = accountId;
		NumberOfPeople = numberOfPeople;
		Children = children;
		ReservationDate = reservationDate;
		ReservationTime = reservationTime;
		SelectedTableIds = selectedTableIds;
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
