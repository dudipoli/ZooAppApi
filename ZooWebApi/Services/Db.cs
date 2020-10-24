using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZooWebApi.Models;

namespace ZooWebApi.Services
{
    public class Db : IDb
    {
        private readonly IConfiguration _config;

        public Db(IConfiguration config)
        {
            _config = config;
        }

        private async Task<DataStore> GetDataFromFile()
        {
            string ds = await File.ReadAllTextAsync(_config["StoredData:Data"]);
            return JsonConvert.DeserializeObject<DataStore>(ds);
        }

        private async Task<bool> UpdateDataInFile(List<Animal> animals)
        {
            bool result;

            try
            {
                DataStore dsObj = await GetDataFromFile();
                dsObj.Animals = animals;
                string ds = JsonConvert.SerializeObject(dsObj);
                await File.WriteAllTextAsync(_config["StoredData:Data"], ds);
                result = true;
            }
            catch
            {
                throw new IOException();
            }


            return result;
        }

        public async Task<List<User>> GetUsersFromDb()
        {
            DataStore dsObj = await GetDataFromFile();
            return dsObj.Users;
        }

        public async Task<List<Animal>> GetAnimalsFromDb()
        {
            DataStore dsObj = await GetDataFromFile();
            return dsObj.Animals;
        }

        public async Task<List<Animal>> UpdateAnimal(Animal animal)
        {
            List<Animal> animals = await GetAnimalsFromDb();
            animals.Where(a => a.ID == animal.ID).Select(a => 
            { 
                a.Name = animal.Name;
                a.Quantity = animal.Quantity;
                return a; 
            }).ToList();
            await UpdateDataInFile(animals);

            return animals;
        }

        public async Task<List<Animal>> InsertAnimal(Animal animal)
        {
            List<Animal> animals = await GetAnimalsFromDb();
            animals.Add(animal);
            await UpdateDataInFile(animals);

            return animals;
        }
    }
}
