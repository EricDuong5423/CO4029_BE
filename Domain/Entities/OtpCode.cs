using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CO4029_BE.Domain.Entities;
[Table("otp_codes")]
public class OtpCode : BaseModel
{
    [PrimaryKey("id", false)]
    public string? id { get; set; }
    [Column("otp")]
    public string? otp { get; set; }
    [Column("email")]
    public string? email { get; set; }
    [Column("expires_at")]
    public DateTime? expires_at { get; set; }

    [Column("is_verified")] 
    public bool? is_verified { get; set; } = false;
    [Column("created_at")]
    public DateTime? created_at { get; set; }
}
