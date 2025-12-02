using Supabase.Postgrest;
using Supabase.Postgrest.Interfaces;
using Supabase.Postgrest.Models;

namespace AgenticAR.Infrastructure.Repository
{
    public interface IRepository<T> where T : BaseModel, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(Guid id, T entity);
        Task DeleteAsync(Guid id);
    }

    public class Repository<T> : IRepository<T> where T : BaseModel, new()
    {
        protected readonly Supabase.Client Client;
        protected readonly IPostgrestTable<T> Table;

        public Repository(Supabase.Client client)
        {
            Client = client;
            Table = Client.Postgrest.Table<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var response = await Table.Get();
            return response.Models;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var model = await Table
                .Filter("id", Constants.Operator.Equals, id.ToString())
                .Get();

            return model.Models.FirstOrDefault();
        }

        public async Task<T> CreateAsync(T entity)
        {
            var response = await Table.Insert(entity);
            return response.Model!;
        }

        public async Task UpdateAsync(Guid id, T entity)
        {
            await Table
                .Filter("id", Constants.Operator.Equals, id.ToString())
                .Update(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await Table
                .Filter("id", Constants.Operator.Equals, id.ToString())
                .Delete();
        }
    }
}