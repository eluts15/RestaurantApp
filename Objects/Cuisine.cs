using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace CuisineProject
{
  public class Cuisine
  {
    private int _id;
    private string _type;

    public Cuisine(string Type, int CuisineId = 0)
    {
      _id = CuisineId;
      _type = Type;


    }
    public int GetId()
    {
      return _id;
    }
    public string GetType()
    {
      return _type;
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool idEquality = (this.GetId() == newCuisine.GetId());
        bool typeEquality = (this.GetType() == newCuisine.GetType());

        return (idEquality && typeEquality);
      }
    }

    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisines = new List<Cuisine>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int cuisineId = rdr.GetInt32(0);
        string cuisineType = rdr.GetString(1);
        Cuisine newCuisine = new Cuisine(cuisineType, cuisineId);
        allCuisines.Add(newCuisine);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allCuisines;
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM cuisines;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
