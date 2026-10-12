public class FoodMenu
{
    FoodLogic Logic = new();

    public bool Test()
    {
        Console.WriteLine("Write the id you want to look up");
        string choice = Console.ReadLine();
        if (int.TryParse(choice, out int id))
        {
            return Logic.CheckAvailableById(id);
        }
        else
        {
            Console.WriteLine("Invalid");
        }
        return false;
    }
}