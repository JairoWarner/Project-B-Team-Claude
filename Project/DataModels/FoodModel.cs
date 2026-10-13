public class FoodModel
{
    public Int64 ID {get;set;}
    public string FoodName {get;set;}
    public double Price {get;set;}
    public string Description {get;set;}
    public string Ingredients {get;set;}
    public string Category {get;set;}
    public Int64 IsAlcoholic {get;set;}


    public FoodModel(Int64 id, string foodName, double price, string description, string ingredients, string category, Int64 isAlcoholic)
    {
        ID = id;
        FoodName = foodName;
        Price = price;
        Description = description;
        Ingredients = ingredients;
        Category = category;
        IsAlcoholic = isAlcoholic;
    }
}