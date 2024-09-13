namespace TaskManagment.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T>? GetByIdAsync(int Id);
        void AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(int Id);
    }
}