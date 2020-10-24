using System.Collections.Generic;
using System.Threading.Tasks;
using ZooWebApi.Models;

namespace ZooWebApi.Services
{
    public interface IDb
    {
        public Task<List<User>> GetUsersFromDb();
        public Task<List<Animal>> GetAnimalsFromDb();
        public Task<List<Animal>> UpdateAnimal(Animal animal);
        public Task<List<Animal>> InsertAnimal(Animal animal);
    }
}
