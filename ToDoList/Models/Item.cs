using System.Collections.Generic;

namespace ToDoList.Models 
{
  public class Item 
  {
    public string Description { get; set; }
    public int Id { get; }
    public static List<Item> Instances { get; } = new List<Item> {};
    public Item(string description) 
    {
      Description = description;
      Instances.Add(this);
      Id = Instances.Count;
    }

    public static void ClearAll() 
    {
      Instances.Clear();
    }

    public static Item Find(int searchId)
    {
      return Instances[searchId-1];
    }
  }
}