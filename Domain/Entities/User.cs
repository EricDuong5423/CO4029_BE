using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CO4029_BE.Domain.Entities;
[Table("user")]
public class User: BaseModel
{
    [PrimaryKey("id", false)]
    public Guid id { get; set; }
    [Column("name")]
    public string? name { get; set; }
    [Column("email")]
    public string? email { get; set; }
    [Column("phone")]
    public string? phone { get; set; }
    [Column("birthday")]
    public DateTime? birthday { get; set; }

}