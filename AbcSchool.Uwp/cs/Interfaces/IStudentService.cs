using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ABCSchool.Models;

namespace ABCSchool.Uwp.Interfaces
{
    public interface IStudentService<T> where T : IEntity
    {
        Task<List<T>> GetAllAsync(string accessToken = null, bool forceRefresh = false);
        Task<T> GetByIdAsync(int id, string accessToken = null, bool forceRefresh = false);
        Task<bool> PostAsync(T item);
        Task<HttpResponseMessage> PostAsJsonAsync(T item);
        Task<bool> PutAsync(T item);
        Task<bool> PutAsJsonAsync(T item);
        Task<bool> DeleteAsync(int studentModelId);
        void AddAuthorizationHeader(string token);
    }
}
