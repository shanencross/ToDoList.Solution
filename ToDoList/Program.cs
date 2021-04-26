using System;
using ToDoList.Models;

namespace ToDoList 
{
  public class Program 
  {
    public static void Main() 
    {
      Console.WriteLine("Welcome to the To Do List!");

      bool runProgram = true;
      while (runProgram) 
      {
        Console.WriteLine("Would you like to add an item to your list or view your list? (Add/View)");
        string userInput = Console.ReadLine().Trim().ToLower();

        if (userInput == "add") 
        {
          Console.WriteLine("Please enter the description for the new item.");
          string description = Console.ReadLine();
          Item newItem = new Item(description);
          Console.Write("'" + newItem.Description + "' has been added to your list. ");
        } 
        else if (userInput == "view") 
        {
          if (Item.Instances.Count == 0) 
          {
            Console.WriteLine("No items added yet.");
          }
          
          for (int i = 0; i < Item.Instances.Count; i++) 
          {
            int num = i + 1;
            Item currentItem = Item.Instances[i];
           
            Console.WriteLine(num + ". " + currentItem.Description);
          }
        }
        else 
        {
          runProgram = false;
        }
      }
    }
  } 
}