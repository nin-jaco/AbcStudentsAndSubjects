using System.Collections.Generic;
using System.Threading.Tasks;
using ABCSchool.Models;

namespace ABCSchool.Uwp.Interfaces
{
    public interface IStudentsSubjectsService<T> where T : IEntity
    {
        Task<List<T>> GetAllAsync(string accessToken = null, bool forceRefresh = false);
        Task<T> GetByIdAsync(int id, string accessToken = null, bool forceRefresh = false);
        Task<bool> PostAsync(T item);
        Task<T> PostAsJsonAsync(T item);
        Task<bool> PutAsync(T item);
        Task<bool> PutAsJsonAsync(T item);
        Task<bool> DeleteAsync();
        void AddAuthorizationHeader(string token);
        Task<List<Subject>> GetByStudentIdAsync(int id, string accessToken = null, bool forceRefresh = false);

    }
}
