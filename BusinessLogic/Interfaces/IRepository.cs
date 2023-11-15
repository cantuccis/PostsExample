using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IRepository<K, T>
    {
        T Get(K key);
        T Insert(T value);
        void Delete(K key);
        bool Exists(K key);
        void Update(K key, T value);
    }
}
