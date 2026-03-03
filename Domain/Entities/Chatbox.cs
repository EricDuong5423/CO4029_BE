using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CO4029_BE.Domain.Entities;

[Table("chatbox")]
public class Chatbox: BaseModel
{
    [PrimaryKey("id", false)] 
    public string? id { get; set; }
    [Column("content")]
    public string? content { get; set; }
    [Column("contact_time")]
    public DateTime? contact_time { get; set; }
    [Column("contact_person")]
    public string? contact_person { get; set; }
    [Column("history_id")]
    public string? history_id { get; set; }
}