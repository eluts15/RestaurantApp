using Xunit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

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
      Cuisine cuisineOne = new Cuisine("Spanish");
      Cuisine cuisineTwo = new Cuisine("Spanish");

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


    public void Dispose()
    {
      Cuisine.DeleteAll();
      Restaurant.DeleteAll();
    }
  }
}
