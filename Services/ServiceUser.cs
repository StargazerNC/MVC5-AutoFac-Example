using System;
using System.Linq;
using Contracts;
using DTOs;
using Entities;

namespace Services
{
    public class ServiceUser : IService
    {
        private readonly IRepository mUserRepo = null;

        public ServiceUser(IRepository repo)
        {
            mUserRepo = repo;
        }

        public string GetVersion()
        {
            User user = new User();
            UserDTO userDTO = new UserDTO();

            user.Name = userDTO.Name;

            return mUserRepo.GetVersionRepo(32);   
        }
    }
}
