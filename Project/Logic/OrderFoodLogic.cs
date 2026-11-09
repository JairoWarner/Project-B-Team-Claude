public class OrderLogic
{
    private OrderModel _order = new();

    public bool AddItem(FoodModel food, int age)
    {
        if (food.IsAlcoholic == 1 && age < 18)
        {
            return false;
        }

        _order.Items.Add(food);
        return true;
    }

    public double CalculateTotalPrice()
    {
        double total = 0;

        foreach (FoodModel item in _order.Items)
        {
            total += item.Price;
        }

        return total;
    }
}