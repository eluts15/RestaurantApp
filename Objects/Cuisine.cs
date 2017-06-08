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

    public static Cuisine FindCuisines(int searchId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines WHERE id = @searchId;", conn);
      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@searchId";
      restaurantIdParameter.Value = searchId.ToString();
      cmd.Parameters.Add(restaurantIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int cuisinesId = 0;
      string cuisinesType = "";

      while(rdr.Read())
      {
        cuisinesId = rdr.GetInt32(0);
        cuisinesType =  rdr.GetString(1);
      }
      Cuisine newCuisine = new Cuisine(cuisinesType, cuisinesId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return newCuisine;
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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO cuisines (type) OUTPUT INSERTED.id VALUES (@Type);", conn);

      SqlParameter typeParam = new SqlParameter();
      typeParam.ParameterName = "@Type";
      typeParam.Value = this.GetType();

      cmd.Parameters.Add(typeParam);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
     {
       this._id = rdr.GetInt32(0);
     }
     if (rdr != null)
     {
       rdr.Close();
     }
     if (conn != null)
     {
       conn.Close();
     }
    }

    public void Update(string newType)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE cuisines SET type = @NewType OUTPUT INSERTED.type WHERE id = @CuisineId;", conn);

      SqlParameter newTypeParameter = new SqlParameter();
      newTypeParameter.ParameterName = "@NewType";
      newTypeParameter.Value = newType;
      cmd.Parameters.Add(newTypeParameter);

      SqlParameter newIdParameter = new SqlParameter();
      newIdParameter.ParameterName = "@CuisineId";
      newIdParameter.Value = this.GetId();
      cmd.Parameters.Add(newIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._type = rdr.GetString(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM cuisines WHERE id = @CuisineId; DELETE FROM restaurants WHERE cuisine_id = @CuisineId;", conn);

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@CuisineId";
      cuisineIdParameter.Value = this.GetId();

      cmd.Parameters.Add(cuisineIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
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
