public class FoodLogic
{
    public bool CheckAvailableById(int id)
    {
        FoodAcces Acces = new();
        if (Acces.GetById(id) != null)
        {
            return true;
        }
        return false;
    }
}