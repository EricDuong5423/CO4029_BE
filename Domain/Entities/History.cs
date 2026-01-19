using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CO4029_BE.Domain.Entities;

[Table("history")]
public class History: BaseModel
{
    [PrimaryKey("id", false)]
    public string? id { get; set; }
    [Column("header")]
    public string? header { get; set; }
    [Column("create_date")]
    public DateTime? create_date { get; set; }
    [Column("user_id")]
    public string? user_id { get; set; }
}