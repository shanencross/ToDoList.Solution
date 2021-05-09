using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Tests {
  [TestClass]
  public class ItemTests : IDisposable {
    public void Dispose() {
      Item.ClearAll();
    }

    public ItemTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=epicodus;port=3306;database=to_do_list_test;";
    }
    
  //   [TestMethod]
  //   public void ItemConstructor_CreatesInstanceOfItem_Item() {
  //     // Arrange and Act
  //     Item newItem = new Item("test");

  //     // Assert
  //     Assert.AreEqual(typeof(Item), newItem.GetType());
  //   }

  //   [TestMethod]
  //   public void GetDescription_ReturnsDescription_String() {
  //     // Arrange
  //     string description = "Walk the dog.";
      
  //     // Act
  //     Item newItem = new Item(description);
  //     string result = newItem.Description;
      
  //     // Asset
  //     Assert.AreEqual(description, result);
  //   }

  //   [TestMethod]
  //   public void SetDescription_SetsDescription_String() {
  //     // Arrange
  //     string description = "Walk the dog.";
  //     Item newItem = new Item(description);

  //     // Act
  //     string updatedDescription = "Do the dishes";
  //     newItem.Description = updatedDescription;
  //     string result = newItem.Description;
      
  //     // Assert
  //     Assert.AreEqual(updatedDescription, result);
  //   }
    
  //   [TestMethod]
  //   public void GetAll_ReturnsEmptyList_ItemList() 
  //   {
  //     // Arrange
  //     List<Item> newList = new List<Item> {};
      
  //     // Act
  //     List<Item> result = Item.GetAll();
      
  //     // Assert
  //     CollectionAssert.AreEqual(newList, result);
  //   }

  //   [TestMethod]
  //   public void GetAll_ReturnsItems_ItemList() 
  //   {
  //     // Arrange
  //     string description01 = "Walk the dog";
  //     string description02 = "Wash the dishes";
  //     Item newItem1 = new Item(description01);
  //     Item newItem2 = new Item(description02);
  //     List<Item> newList = new List<Item> { newItem1, newItem2 };

  //     // Act
  //     List<Item> result = Item.GetAll();

  //     // Assert
  //     CollectionAssert.AreEqual(newList, result);
  //   }
  //   [TestMethod]
  //   public void GetId_ItemsInstantiateWithAnIdAndGetterReturns_Int()
  //   {
  //     string description = "Walk the dog.";
  //     Item newItem = new Item(description);
  //     int result = newItem.Id;
  //     Assert.AreEqual(1,result);
  //   }

  //   [TestMethod]
  //   public void Find_ReturnsCorrectItem_Item()
  //   {
  //     string description01 = "Walk the dog";
  //     string description02 = "Wash the dishes";
  //     Item newItem1 = new Item(description01);
  //     Item newItem2 = new Item (description02);
  //     Item result = Item.Find(2);
  //     Assert.AreEqual(newItem2, result);
  //   }

    [TestMethod]
    public void GetAll_ReturnsEmptyListFromDatabase_ItemList()
    {
      //Arrange
      List<Item> newList = new List<Item> {};

      //Act
      List<Item> result = Item.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
    {
      //Arrange, Act
      Item firstItem = new Item("Mow the lawn");
      Item secondItem = new Item("Mow the lawn");

      //Assert
      Assert.AreEqual(firstItem, secondItem);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ItemList()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn");

      //Act
      testItem.Save();
      List<Item> result = Item.GetAll();
      List<Item> testList = new List<Item> { testItem };

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
  }
}
