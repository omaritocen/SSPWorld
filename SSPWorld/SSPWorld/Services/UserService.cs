using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSPWorld.Services
{
    public class UserService
    {
        public async Task<bool> Login(int id, string password)
        {
            return id == 6294 && password == "omar";
        }
    }
}
