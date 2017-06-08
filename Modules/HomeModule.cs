using System;
using System.Collections.Generic;
using Nancy;

namespace CuisineProject
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };
      Post["/"] = _ => {
        Cuisine newCuisines = new Cuisine(Request.Form["cuisines"]);
        newCuisines.Save();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };
      Get["/cuisines/{id}"] = param => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Cuisine searchCuisines = Cuisine.FindCuisines(param.id);
        List<Restaurant> allCuisinesByType = Restaurant.GetAllByType(param.id);
        model.Add("cuisines", searchCuisines);
        model.Add("restaurants", allCuisinesByType);
        return View["view_restaurants.cshtml", model];
      };

      Post["/cuisines/{id}"] = param => {
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"], Request.Form["cuisine-id"], Request.Form["restaurant-city"], Request.Form["restaurant-rating"]);
        newRestaurant.Save();
        Dictionary<string, object> model = new Dictionary<string, object>();
        Cuisine searchCuisines = Cuisine.FindCuisines(param.id);
        List<Restaurant> allCuisinesByType = Restaurant.GetAllByType(param.id);
        model.Add("cuisines", searchCuisines);
        model.Add("restaurants", allCuisinesByType);
        return View["view_restaurants.cshtml", model];
      };

      Post["/delete"] = _ => {
        Cuisine.DeleteAll();
        Restaurant.DeleteAll();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };

      Get["cuisines/edit/{id}"] = param => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Cuisine searchCuisines = Cuisine.FindCuisines(param.id);
        List<Restaurant> allCuisinesByType = Restaurant.GetAllByType(param.id);
        model.Add("cuisines", searchCuisines);
        model.Add("restaurants", allCuisinesByType);
        return View["cuisines_edit.cshtml", model];
      };

      Patch["cuisines/edit/{id}"] = param => {
        Cuisine selectedCuisine = Cuisine.FindCuisines(param.id);
        selectedCuisine.Update(Request.Form["new-cuisine-name"]);
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };

      Get["cuisines/delete/{id}"] = param => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Cuisine searchCuisines = Cuisine.FindCuisines(param.id);
        List<Restaurant> allCuisinesByType = Restaurant.GetAllByType(param.id);
        model.Add("cuisines", searchCuisines);
        model.Add("restaurants", allCuisinesByType);
        return View["cuisines_delete.cshtml", model];
      };

      Delete["cuisines/delete/{id}"] = param => {
        Cuisine selectedCuisine = Cuisine.FindCuisines(param.id);
        selectedCuisine.Delete();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };

      Get["restaurant/delete/{id}"] = param => {
        Restaurant selectedRestaurant = Restaurant.Find(param.id);
        return View["restaurant_delete.cshtml", selectedRestaurant];
      };

      Delete["restaurant/delete/{id}"] = param => {
        Restaurant selectedRestaurant = Restaurant.Find(param.id);
        selectedRestaurant.Delete();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };
    }
  }
}
