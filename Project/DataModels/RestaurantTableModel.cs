public class RestaurantTableModel
{
    public int TableId { get; set; }
    public bool Available { get; set; }

    public int Seats { get; set; }

    public RestaurantTableModel()
    {
    }

    public RestaurantTableModel(int tableId, int seats, bool available)
    {
        TableId = tableId;
        
        Available = available;

        Seats = seats;
    }
}
