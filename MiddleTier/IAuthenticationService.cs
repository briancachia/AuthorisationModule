using CommonTier.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleTier
{
    public interface IAuthenticationService
    {
        UserDetail PreCheckUser(string username);

        int RegisterUserDetails(string name, string surname, string email, string username, string password);

        void AllocateRole(int roleId, string username);

        void AllocateRole(int roleId, int userId);

        bool HasRole(string username, int roleId);

        void RegisterNewUser(string name, string surname, string email, string username, string password, int roleId);

        bool Login(string username, string password);
    }
}
