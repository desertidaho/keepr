using System;
using System.Data;
using System.Linq;
using keepr.Models;
using Dapper;
using System.Collections.Generic;

namespace keepr.Repositories
{
  public class KeepRepository
  {
    private readonly IDbConnection _db;
    public KeepRepository(IDbConnection db)
    {
      _db = db;
    }

    //get all public keeps for home view
    public IEnumerable<Keep> GetALL()
    {
      return _db.Query<Keep>("SELECT * FROM keeps WHERE isPrivate = 'false'");
    }

    //get all public/private keeps by userId for dashboard
    public IEnumerable<Keep> GetById(string userId)
    {
      return _db.Query<Keep>("SELECT * FROM keeps WHERE userId = @UserId", new { userId });
    }

    public Keep CreateKeep(Keep keep)
    {
      try
      {
        int id = _db.ExecuteScalar<int>(@"
                INSERT INTO keeps (name, description, img, isPrivate, id, keeps, shares, userId, views)
                    VALUES (@Name, @Description, @Img, @IsPrivate, @Id, @Keeps, @Shares, @UserId, @Views);
                    SELECT LAST_INSERT_ID();
                ", keep);
        keep.Id = id;
        return keep;
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return null;
      }
    }

    public Keep EditKeep(int id, Keep editedKeep)
    {
      try
      {
        string query = @"
                UPDATE keeps SET
                    name = @Name,
                    description = @Description,
                    userId = @UserId,
                    img = @Img,
                    isPrivate = @IsPrivate,
                    views = @Views,
                    shares = @Shares,
                    keeps = @Keeps,
                    id = @Id    
                WHERE id = @Id;
                SELECT * FROM keeps WHERE id = @Id;
                ";
        return _db.QueryFirstOrDefault<Keep>(query, editedKeep);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return null;
      }
    }

    public bool Delete(int id)
    {
      int success = _db.Execute("DELETE FROM keeps WHERE id = @id", new { id });
      return success > 0;
    }

  }
}