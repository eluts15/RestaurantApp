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


    public void Dispose()
    {
      Cuisine.DeleteAll();
      Restaurant.DeleteAll();
    }
  }
}
