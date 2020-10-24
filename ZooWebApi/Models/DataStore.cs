using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooWebApi.Models
{
    public class DataStore
    {
        public List<User> Users { get; set; }
        public List<Animal> Animals { get; set; }
    }
}
