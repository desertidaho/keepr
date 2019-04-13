using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
  public class Keep
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public string Img { get; set; }
    public bool IsPrivate { get; set; }
    public string Views { get; set; }
    public string Shares { get; set; }
    public string Keeps { get; set; }

  }
}