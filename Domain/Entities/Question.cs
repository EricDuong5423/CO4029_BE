using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CO4029_BE.Domain.Entities;

[Table("question")]
public class Question: BaseModel
{
    [PrimaryKey("id", false)]
    public string? ID { get; set; }
    [Column("content")]
    public string? Content { get; set; }
    [Column("email")]
    public string? Email { get; set; }
    [Column("name")]
    public string? Name { get; set; }
    [Column("create_date")]
    public DateTime? CreateDate { get; set; }
    [Column("category_id")]
    public string? CategoryID { get; set; }
    [Column("user_id")]
    public string? UserID { get; set; }
}