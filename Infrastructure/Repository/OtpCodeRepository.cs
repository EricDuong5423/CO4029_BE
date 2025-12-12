using CO4029_BE.Domain.Entities;

namespace AgenticAR.Infrastructure.Repository
{
    public interface IOtpCodeRepository : IRepository<OtpCode>
    {
        
    }

    public class OtpCodeRepository: Repository<OtpCode>, IOtpCodeRepository
    {
        public OtpCodeRepository(Supabase.Client client) : base(client)
        {
            
        }
    }
}