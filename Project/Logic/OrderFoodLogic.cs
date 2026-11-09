public class OrderLogic
{
    private OrderModel _order = new();
    private OrderAccess _access = new();

    public bool AddItem(FoodModel food, int age)
    {
        if (food.IsAlcoholic == 1 && age < 18)
        {
            return false;
        }

        _order.Items.Add(food);
        return true;
    }

    public static int CalculateAge(string dateOfBirth)
    {
        DateTime birthdate = DateTime.ParseExact(dateOfBirth, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        int age = DateTime.Today.Year - birthdate.Year;

        if (birthdate > DateTime.Today.AddYears(-age))
        {
            age--;
        }

        return age;
    }

    public bool IsEmpty => _order.Items.Count == 0;

    public List<OrderLine> GetLines()
    {
        return _order.Items
            .GroupBy(item => item.ID)
            .Select(group => new OrderLine(group.First(), group.Count()))
            .ToList();
    }

    public long Save()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("An empty order cannot be saved.");
        }

        return _access.Write(GetLines(), CalculateTotalPrice());
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