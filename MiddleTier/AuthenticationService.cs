using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStore;
using CommonTier.DBModels;
using CommonTier.Security;
using System.Transactions;

namespace MiddleTier
{
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// Singleton for Role Consumption
        /// </summary>
        SysRoleConsumer roleCons = null;
        SysRoleConsumer RoleCons
        {
            get
            {
                if (roleCons == null)
                    roleCons = new SysRoleConsumer();
                return roleCons;
            }
        }

        /// <summary>
        /// Singleton for UserRole Consumption
        /// </summary>
        UserRolesConsumer userRoleCons = null;
        UserRolesConsumer UserRoleCons
        {
            get
            {
                if (userRoleCons == null)
                    userRoleCons = new UserRolesConsumer();
                return userRoleCons;
            }
        }

        /// <summary>
        /// Singleton for User Consumption
        /// </summary>
        UserDetailsConsumer userCons = null;
        UserDetailsConsumer UserCons
        {
            get
            {
                if (userCons == null)
                    userCons = new UserDetailsConsumer();
                return userCons;
            }
        }

        /// <summary>
        /// Singleton for the SecurityManager
        /// </summary>
        SecurityManager securityManager = null;
        SecurityManager SecurityManager
        {
            get
            {
                if (securityManager == null)
                    securityManager = new SecurityManager();
                return securityManager;
            }
        }

        /// <summary>
        /// Check if user exists and return instance if exists.
        /// </summary>
        /// <param name="username">username of user to check</param>
        /// <returns></returns>
        public UserDetail PreCheckUser(string username)
        {
            UserDetail user = UserCons.GetRow(username);

            if (user == null)
                throw new ArgumentException(string.Format("User with username {0} was not found.", username));

            return user;
        }

        /// <summary>
        /// Allocate a role to the provided user
        /// </summary>
        /// <param name="roleId">Role</param>
        /// <param name="username">User</param>
        public void AllocateRole(int roleId, string username)
        {
            UserDetail user = PreCheckUser(username);

            if (this.HasRole(username, roleId))
                throw new ArgumentException("User already has role selected.");

            UserRoleCons.AddRow(new UserRole()
            {
                RoleId = roleId,
                UserId = user.ID
            });
        }

        /// <summary>
        /// Allocate a role to a provided user by user id
        /// </summary>
        /// <param name="roleId">Role Id</param>
        /// <param name="userId">User Id</param>
        public void AllocateRole(int roleId, int userId)
        {
            UserRole ur = new UserRole()
            {
                RoleId = roleId,
                UserId = userId
            };

            UserRoleCons.AddRow(ur);
        }

        /// <summary>
        /// Check if user has a role
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <param name="roleId">Id of Role</param>
        /// <returns></returns>
        public bool HasRole(string username, int roleId)
        {
            UserDetail user = UserCons.GetRow(username);

            if (user == null)
                throw new ArgumentException(string.Format("User with username {0} was not found.", username));

            return this.UserRoleCons.GetRow(roleId, user.ID) != null;

        }

        /// <summary>
        /// Register a new user in the database.
        /// </summary>
        /// <param name="name">Name of user</param>
        /// <param name="surname">Surname of user</param>
        /// <param name="email">Email of user</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password in plain text.</param>
        /// <returns>id of user created</returns>
        public int RegisterUserDetails(string name, string surname, string email, string username, string password)
        {

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(surname) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Please provide all information.");

            if (UserCons.GetRow(username) != null)
                throw new ArgumentException("Username provided already exists.");

            UserDetail user = new UserDetail()
            {
                Name = name,
                Surname = surname,
                Email = email,
                Username = username
            };

            user.Password = this.SecurityManager.GeneratePassword(user.Username, password);

            return UserCons.AddRow(user);
        }

        /// <summary>
        /// Register a new user in the database.
        /// </summary>
        /// <param name="name">Name of user</param>
        /// <param name="surname">Surname of user</param>
        /// <param name="email">Email of user</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password in plain text.</param>
        /// <param name="roleId">Id of role to allocate to user</param>
        public void RegisterNewUser(string name, string surname, string email, string username, string password, int roleId)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    int userId = RegisterUserDetails(name, surname, email, username, password);
                    AllocateRole(roleId, username);
                    ts.Complete();
                }
                catch(Exception ex)
                {
                    ts.Dispose();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Basic login functionality. Hash provided details and compare with one in database.
        /// </summary>
        /// <param name="username">username in form</param>
        /// <param name="password">password in form</param>
        /// <returns>true if logged in</returns>
        public bool Login(string username, string password)
        {
            UserDetail ud = PreCheckUser(username);

            if(ud.LoginAttempts > 1)
            {
                throw new Exception("User has been locked. Contact your administrator to unlock the account.");
            }

            string providedPass = SecurityManager.GeneratePassword(ud.Username, password);

            if (ud.Password == providedPass)
                return true;

            ud.LoginAttempts++;
            UserCons.UpdateRow(ud);

            return false;
        }
    }
}
