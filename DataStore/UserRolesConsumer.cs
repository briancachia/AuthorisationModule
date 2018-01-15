using CommonTier.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore
{
    /// <summary>
    /// UserRoles data accessor class
    /// </summary>
    public class UserRolesConsumer : SuperConsumer<UserRole>
    {
        /// <summary>
        /// Add row to database for user detail.
        /// </summary>
        /// <param name="row">user detail instance of db model</param>
        /// <returns>id</returns>
        public override int AddRow(UserRole row)
        {
            //add row to context
            MainDbContext.UserRoles.Add(row);

            //save changes and return resulting id
            return this.MainDbContext.SaveChanges();
        }

        /// <summary>
        /// Return row by id. May return null if id was not found.
        /// </summary>
        /// <param name="ID">Id of row</param>
        /// <returns></returns>
        public override UserRole GetRowByID(int ID)
        {
            //return the row by the id given. Only a single row is expected so return first or default
            return MainDbContext.UserRoles.Where(x => x.ID == ID).FirstOrDefault();
        }

        /// <summary>
        /// Get all rows in table. May be slow, use with caution.
        /// </summary>
        /// <returns>list of the resulting rows from database</returns>
        public override List<UserRole> GetRows()
        {
            //Return all rows from the table.
            return MainDbContext.UserRoles.ToList<UserRole>();
        }

        /// <summary>
        /// Get all rows in table that match the status provided. May be slow, use with caution.
        /// </summary>
        /// <returns>list of the resulting rows from database</returns>
        /// <param name="status">Status of the rows needed.</param>
        public override List<UserRole> GetRows(string status)
        {
            //Fetch data from database using the status specified
            return MainDbContext.UserRoles.Where(x => x.Status == status).ToList();
        }

        /// <summary>
        /// Update a specific row. To use this method make sure that you load an entity from the database.
        /// </summary>
        /// <param name="row">User Detail instance to be updated in the database.</param>
        /// <returns>Resulting ID</returns>
        public override int UpdateRow(UserRole row)
        {
            //Modify the entity state prior to saving the changes.
            this.MainDbContext.Entry(row).State = System.Data.Entity.EntityState.Modified;

            //Save the final changes into the database.
            return this.MainDbContext.SaveChanges();
        }


        /// <summary>
        /// Get a user role (if exists) by user and role id
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <param name="userId">User ID not username</param>
        /// <returns>UserRole instance representing row</returns>
        public UserRole GetRow(int roleId, int userId)
        {
            return this.MainDbContext.UserRoles.Where(x => x.RoleId == roleId && x.UserId == userId).FirstOrDefault();
        }
    }
}
