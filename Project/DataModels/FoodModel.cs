public class FoodModel
{
    public Int64 ID {get;set;}
    public string FoodName {get;set;}
    public double Price {get;set;}
    public string Description {get;set;}
    public string Ingredients {get;set;}

    public FoodModel(Int64 id, string foodName, double price, string description, string ingredients)
    {
        ID = id;
        FoodName = foodName;
        Price = price;
        Description = description;
        Ingredients = ingredients;
    }
}