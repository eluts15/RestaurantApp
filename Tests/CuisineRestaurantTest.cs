using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CuisineProject
{
  public class CuisineRestaurantTest : IDisposable
  {
    public CuisineRestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=cuisine_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange
      int cuisineResult = Cuisine.GetAll().Count;
      int restaurantResult = Restaurant.GetAll().Count;

      //Assert
      Assert.Equal(0, cuisineResult + restaurantResult);
    }

    [Fact]
    public void Test_Cuisine_ReturnValuesEqualEachother()
    {
      //Arrange Act
      Cuisine cuisineOne = new Cuisine("Freedom");
      Cuisine cuisineTwo = new Cuisine("Freedom");

      //Assert
      Assert.Equal(cuisineOne, cuisineTwo);
    }

    [Fact]
    public void Test_Restaurant_ReturnValuesEqualEachother()
    {
      //Arrange Act
      Restaurant restaurantOne = new Restaurant("Papa's Burp n' Slurp", 1, "Papa's City", 5);
      Restaurant restaurantTwo = new Restaurant("Papa's Burp n' Slurp", 1, "Papa's City", 5);

      //Assert
      Assert.Equal(restaurantOne, restaurantTwo);
    }

    [Fact]
    public void Test_Cuisine_Save_SaveToDatabase()
    {
      //Arrange
      Cuisine french = new Cuisine("Frankenfurt");

      //Act
      french.Save();
      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{french};

      //Assert
      Assert.Equal(result, testList);
    }

    [Fact]
    public void Test_Restaurant_SaveToDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Papa's Burp n' Slurp II", 2, "Papa's Town", 5);

      //Act
      testRestaurant.Save();
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      Assert.Equal(testList, result);
    }


    public void Dispose()
    {
      Cuisine.DeleteAll();
      Restaurant.DeleteAll();
    }
  }
}
