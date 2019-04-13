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

    public IEnumerable<Keep> GetALL()
    {
      return _db.Query<Keep>("SELECT * FROM keep");
    }

    public Keep GetById(int Id)
    {
      return _db.QueryFirstOrDefault<Keep>("SELECT * FROM keeps WHERE id = @Id", new { Id });
    }

    public Keep CreateKeep(Keep keep)
    {
      try
      {
        int id = _db.ExecuteScalar<int>(@"
                INSERT INTO keeps (name, description, img, isPrivate,  )
                    VALUES (@Name, @Description, @Img, @IsPrivate);
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

    public bool Delete(int id)
    {
      int success = _db.Execute("DELETE FROM keeps WHERE id = @id", new { id });
      return success > 0;
    }

  }
}