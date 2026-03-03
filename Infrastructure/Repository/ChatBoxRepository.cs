using CO4029_BE.Domain.Entities;

namespace AgenticAR.Infrastructure.Repository
{
    public interface IChatBoxRepository : IRepository<Chatbox>
    {
        Task<IEnumerable<Chatbox>> GetChatboxesByHistoryId(string? historyId);
    }
    public class ChatBoxRepository: Repository<Chatbox>, IChatBoxRepository
    {
        public ChatBoxRepository(Supabase.Client client) : base(client)
        {
            
        }

        public async Task<IEnumerable<Chatbox>> GetChatboxesByHistoryId(string? historyId)
        {
            var reponse = await Table.Where(x => x.history_id == historyId)
                                                           .Get();
            return reponse.Models;
        }
    }
}