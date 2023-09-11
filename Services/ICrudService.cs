using System.Collections.Generic;

namespace ECommerce_Api2.Services
{
    public interface ICrudService<T> where T : class
    {
        public T create(T item);
        public T update(int id ,T item);
        public int delete(T item); 
        public T GetById(int id);
        public List<T> GetAll();
    }
}
