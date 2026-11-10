public class OrderModel
{
    public List<FoodModel> Items { get; set; } = new();
}

public record OrderLine(FoodModel Food, int Quantity)
{
    public double LineTotal => Food.Price * Quantity;
}