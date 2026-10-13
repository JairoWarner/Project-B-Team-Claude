public class FoodLogic
{
    public List<FoodModel> GetMenu(int id)
    {
        FoodAcces acces = new();
        return acces.GetMenu(id);
    }
}