using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CuisineProject
{
  public class Review
  {
    private int _id;
    private int _restaurantId;
    private string _name;
    private string _review;
    private int _rating;

    public Review(string newName, string newReview, int newRating, int newRestaurantId, int Id = 0)
    {
      _id = Id;
      _restaurantId = newRestaurantId;
      _name = newName;
      _review = newReview;
      _rating = newRating;
    }

    public string GetName()
    {
      return _name;
    }
    public int GetId()
    {
      return _id;
    }
    public int GetRestaurantId()
    {
      return _restaurantId;
    }
    public string GetReview()
    {
      return _review;
    }
    public int GetRating()
    {
      return _rating;
    }

    public override bool Equals(System.Object otherReview)
    {
      if (!(otherReview is Review))
      {
        return false;
      }
      else
      {
        Review newReview = (Review) otherReview;
        bool idEquality = (this.GetId() == newReview.GetId());
        bool nameEquality = (this.GetName() == newReview.GetName());
        bool reviewEquality = (this.GetReview() == newReview.GetReview());
        bool ratingEquality = (this.GetRating() == newReview.GetRating());
        bool restaurantIdEquality = (this.GetRestaurantId() == newReview.GetRestaurantId());


        return (idEquality && nameEquality && reviewEquality && ratingEquality && restaurantIdEquality);
      }
    }

    public static List<Review> GetAll()
    {
      List<Review> allReviews = new List<Review>();

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM reviews;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int reviewId = rdr.GetInt32(0);
        int reviewRestaurantId = rdr.GetInt32(1);
        string reviewName = rdr.GetString(2);
        string reviewReview = rdr.GetString(3);
        int reviewRating = rdr.GetInt32(4);

        Review newReview = new Review(reviewName, reviewReview, reviewRating, reviewRestaurantId, reviewId);
        allReviews.Add(newReview);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allReviews;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlParameter restaurantIdParameter = new SqlParameter("@RestaurantId", this.GetRestaurantId());
      SqlParameter nameParameter = new SqlParameter("@Name", this.GetName());
      SqlParameter reviewParameter = new SqlParameter("@Review", this.GetReview());
      SqlParameter ratingParameter = new SqlParameter("@Rating", this.GetRating());

      SqlCommand cmd = new SqlCommand("INSERT INTO reviews (restaurant_id, name, review, rating) OUTPUT INSERTED.id VALUES (@RestaurantId, @Name, @Review, @Rating);", conn);

      cmd.Parameters.Add(restaurantIdParameter);
      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(reviewParameter);
      cmd.Parameters.Add(ratingParameter);

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

    public static List<Review> GetAllByRestaurant(int typeId)
    {
      List<Review> getReviews = new List<Review>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlParameter restaurantIdParameter = new SqlParameter("@Id", typeId.ToString());

      SqlCommand cmd = new SqlCommand("SELECT * FROM reviews WHERE restaurant_id = @Id;", conn);

      cmd.Parameters.Add(restaurantIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int reviewId = rdr.GetInt32(0);
        int restaurantId = rdr.GetInt32(1);
        string reviewName = rdr.GetString(2);
        string reviewReview = rdr.GetString(3);
        int ratingId = rdr.GetInt32(4);

        Review newReview = new Review(reviewName, reviewReview, ratingId, restaurantId, reviewId);
        getReviews.Add(newReview);
      }

        if (rdr != null)
        {
         rdr.Close();
       }
       if (conn != null)
       {
           conn.Close();
       }
      return getReviews;
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM reviews;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
