using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooWebApi.Models;

namespace ZooWebApi.Services
{
    public interface IUsers
    {
        public User GetUserByName(IEnumerable<User> users, string userName);
    }
}
