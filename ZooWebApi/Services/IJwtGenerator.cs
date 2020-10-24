using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooWebApi.Entities;
using ZooWebApi.Models;

namespace ZooWebApi.Services
{
    public interface IJwtGenerator
    {
       public string GenerateJWTToken(UserCredentials userInfo);
    }
}
