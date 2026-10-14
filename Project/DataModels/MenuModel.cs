public class MenuModel
{
    public Int64 ID {get;set;}
    public string Name {get;set;}
    public string Description {get;set;}

    public MenuModel(Int64 id, string name, string description)
    {
        ID = id;
        Name = name;
        Description = description;
    }
}
