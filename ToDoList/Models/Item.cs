using System.Collections.Generic;

namespace ToDoList.Models {
  public class Item {
    public string Description { get; set; }
    public static List<Item> Instances { get; } = new List<Item> {};
    public Item(string description) {
      Description = description;
      Instances.Add(this);
    }

    public static void ClearAll() {
      Instances.Clear();
    }
  }
}