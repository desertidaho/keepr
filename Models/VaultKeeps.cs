using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
  public class VaultKeeps
  {
    public int Id { get; set; }
    public int VaultId { get; set; }
    public int KeepId { get; set; }
    public string UserId { get; set; }

  }
}