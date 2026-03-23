using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CO4029_BE.Domain.Entities;

[Table("answer")]
public class Answer: BaseModel
{
    [PrimaryKey("id", false)]
    public string? ID { get; set; }
    [Column("content")]
    public string? Content { get; set; }
    [Column("create_date")]
    public DateTime? CreateDate { get; set; }
    [Column("question_id")]
    public string? QuestionID { get; set; } 
    [Column("user_id")]
    public string? UserID { get; set; }
}