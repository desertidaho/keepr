using System;
using System.Data;
using System.Linq;
using keepr.Models;
using Dapper;
using System.Collections.Generic;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }


    //get all public/private keeps by userId for dashboard
    public IEnumerable<VaultKeeps> GetById(string userId)
    {
      return _db.Query<VaultKeeps>("SELECT * FROM vaultkeeps WHERE userId = @UserId", new { userId });
    }

    public VaultKeeps CreateVaultKeeps(VaultKeeps vaultkeeps)
    {
      try
      {
        int id = _db.ExecuteScalar<int>(@"
                INSERT INTO vaultkeeps (vaultId, keepId, userId)
                    VALUES (@VaultId, @KeepId, @UserId);
                    SELECT LAST_INSERT_ID();
                ", vaultkeeps);
        vaultkeeps.Id = id;
        return vaultkeeps;
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return null;
      }
    }

    public bool Delete(int id)
    {
      int success = _db.Execute("DELETE FROM vaultkeeps WHERE id = @id", new { id });
      return success > 0;
    }

  }
}