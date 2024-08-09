namespace TaskManagerAPI_2.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
