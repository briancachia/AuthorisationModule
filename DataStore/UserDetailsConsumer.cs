using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTier.DBModels;

namespace DataStore
{
    /// <summary>
    /// Main consumer for the User Details table in the database.
    /// </summary>
    public class UserDetailsConsumer : SuperConsumer<CommonTier.DBModels.UserDetail>
    {

        /// <summary>
        /// Add row to database for user detail.
        /// </summary>
        /// <param name="row">user detail instance of db model</param>
        /// <returns>id</returns>
        public override int AddRow(UserDetail row)
        {
            //add row to contexc
            MainDbContext.UserDetails.Add(row);
            
            //save changes and return resulting id
            return this.MainDbContext.SaveChanges();
        }

        /// <summary>
        /// Return row by id. May return null if id was not found.
        /// </summary>
        /// <param name="ID">Id of row</param>
        /// <returns></returns>
        public override UserDetail GetRowByID(int ID)
        {
            //return the row by the id given. Only a single row is expected so return first or default
            return MainDbContext.UserDetails.Where(x => x.ID == ID).FirstOrDefault();
        }

        /// <summary>
        /// Get all rows in table. May be slow, use with caution.
        /// </summary>
        /// <returns>list of the resulting rows from database</returns>
        public override List<UserDetail> GetRows()
        {
            //Return all rows from the table.
            return MainDbContext.UserDetails.ToList<UserDetail>();
        }

        /// <summary>
        /// Get all rows in table that match the status provided. May be slow, use with caution.
        /// </summary>
        /// <returns>list of the resulting rows from database</returns>
        /// <param name="status">Status of the rows needed.</param>
        public override List<UserDetail> GetRows(string status)
        {
            //Fetch data from database using the status specified
            return MainDbContext.UserDetails.Where(x => x.Status == status).ToList();
        }

        /// <summary>
        /// Update a specific row. To use this method make sure that you load an entity from the database.
        /// </summary>
        /// <param name="row">User Detail instance to be updated in the database.</param>
        /// <returns>Resulting ID</returns>
        public override int UpdateRow(UserDetail row)
        {
            //Modify the entity state prior to saving the changes.
            this.MainDbContext.Entry(row).State = System.Data.Entity.EntityState.Modified;
            
            //Save the final changes into the database.
            return this.MainDbContext.SaveChanges();
        }


        /// <summary>
        /// Get UserDetails by username
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <returns>Entity instance representing row from db</returns>
        public UserDetail GetRow(string username)
        {
            return this.MainDbContext.UserDetails.Where(x => x.Username == username).FirstOrDefault();
        }
    }
}
