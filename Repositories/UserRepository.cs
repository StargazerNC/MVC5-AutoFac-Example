using System;
using System.Linq;
using Contracts;
using Entities;

namespace Repository
{
    public class UserRepository : IRepository
    {
        public string GetVersionRepo(int id)
        {
            User user = new User();
            return "Version is 1.0.0.0";
        }
    }
}
