using System;
using System.Data;
using System.Linq;
using keepr.Models;
using Dapper;
using System.Collections.Generic;

namespace keepr.Repositories
{
  public class VaultRepository
  {
    private readonly IDbConnection _db;
    public VaultRepository(IDbConnection db)
    {
      _db = db;
    }

    // //get all public keeps for home view
    // public IEnumerable<Keep> GetALL()
    // {
    //   return _db.Query<Keep>("SELECT * FROM keeps WHERE isPrivate = 'false'");
    // }

    //get all public/private keeps by userId for dashboard
    public IEnumerable<Vault> GetById(string userId)
    {
      return _db.Query<Vault>("SELECT * FROM vaults WHERE userId = @UserId", new { userId });
    }

    public Vault CreateVault(Vault vault)
    {
      try
      {
        int id = _db.ExecuteScalar<int>(@"
                INSERT INTO vaults (name, description, id, userId)
                    VALUES (@Name, @Description, @Id, @UserId);
                    SELECT LAST_INSERT_ID();
                ", vault);
        vault.Id = id;
        return vault;
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return null;
      }
    }

    public bool Delete(int id)
    {
      int success = _db.Execute("DELETE FROM vaults WHERE id = @id", new { id });
      return success > 0;
    }

  }
}