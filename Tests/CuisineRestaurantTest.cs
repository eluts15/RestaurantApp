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

    [Fact]
    public void Test_Update_UpdatesCuisineInDatabase()
    {
      //Arrange
      string testType = "Not freedom:(";
      Cuisine testCuisines = new Cuisine(testType);
      testCuisines.Save();
      string newType = "FREEEEDOOOM";

      //Act
      testCuisines.Update(newType);
      string result = testCuisines.GetType();

      //Assert
      Assert.Equal(newType, result);
    }

    [Fact]
    public void Test_Restaurant_Update_UpdatesRestaurantinDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Papa's Burp n' Slurp", 1, "Papa's Village", 5);
      testRestaurant.Save();
      string newName = "Papa's Burp n' Slurp II";
      string newCity = "Papa's Town";
      int newRating = 500;

      //Act
      testRestaurant.Update(newName, newCity, newRating);
      string nameResult = testRestaurant.GetName();
      string cityResult = testRestaurant.GetCity();
      int ratingResult = testRestaurant.GetRating();

      //Assert
      Assert.Equal(newName, nameResult);
      Assert.Equal(newCity, cityResult);
      Assert.Equal(newRating, ratingResult);
    }

    [Fact]
    public void Test_Cuisine_Delete_DeletesCategoryFromDatabase()
    {
      //Arrange
      string type1 = "freedom";
      Cuisine testCuisine1 = new Cuisine(type1);
      testCuisine1.Save();

      string type2 = "FRREEEEEDOOMMMM";
      Cuisine testCuisine2 = new Cuisine(type2);
      testCuisine2.Save();

      Restaurant testRestaurant1 = new Restaurant("Papa's Burp n' Slurp II", testCuisine1.GetId(), "Papa's Town", 5);
      testRestaurant1.Save();
      Restaurant testRestaurant2 = new Restaurant("Big Papa's Burp n' Slurp II", testCuisine2.GetId(), "Big Papa's Town", 5);
      testRestaurant2.Save();

      //Act
      testCuisine1.Delete();
      List<Cuisine> resultCuisines = Cuisine.GetAll();
      List<Cuisine> testCuisineList = new List<Cuisine> {testCuisine2};

      List<Restaurant> resultRestaurants = Restaurant.GetAll();
      List<Restaurant> testRestaurantList = new List<Restaurant> {testRestaurant2};

      //Assert
      Assert.Equal(testCuisineList, resultCuisines);
      Assert.Equal(testRestaurantList, resultRestaurants);
    }

    [Fact]
    public void Test_Restaurant_Delete_DeleteRestaurantFromDatabase()
    {
      //Arrange
      Restaurant testRestaurant1 = new Restaurant("Papa's Burp n' Slurp II", 1, "Papa's Town", 5);
      testRestaurant1.Save();
      Restaurant testRestaurant2 = new Restaurant("Big Papa's Burp n' Slurp II", 1, "Big Papa's Town", 5);
      testRestaurant2.Save();

      //Act
      testRestaurant1.Delete();
      List<Restaurant> resultRestaurants = Restaurant.GetAll();
      List<Restaurant> testRestaurantList = new List<Restaurant> {testRestaurant2};

      //Assert
      Assert.Equal(resultRestaurants, testRestaurantList);
    }

    public void Dispose()
    {
      Cuisine.DeleteAll();
      Restaurant.DeleteAll();
    }
  }
}
