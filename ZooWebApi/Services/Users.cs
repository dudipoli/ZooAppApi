using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooWebApi.Models;

namespace ZooWebApi.Services
{
    public class Users : IUsers
    {
        public User GetUserByName(IEnumerable<User> users, string userName)
        {
            User user = users.FirstOrDefault(u => u.UserName == userName);

            return user;
        }
    }
}
