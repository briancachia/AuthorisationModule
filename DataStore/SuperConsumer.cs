using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore
{
    /// <summary>
    /// Inherit this class to enable changes in the database from inherited classes. Provides a scaffolding for the basic functions to interact with database.
    /// </summary>
    /// <typeparam name="T">Entity type from dbmodels in CommonTier Project.</typeparam>
    public abstract class SuperConsumer<T>
    {
        /// <summary>
        /// mainDbContext Singleton to be used by inherited classes.
        /// </summary>
        private AuthenticationContext mainDbContext = null;

        /// <summary>
        /// mainDbContext Singleton to be used by inherited classes.
        /// </summary>
        protected AuthenticationContext MainDbContext
        {
            get
            {
                if(mainDbContext == null)
                {
                    mainDbContext = new AuthenticationContext();
                }
                return mainDbContext;
            }
        }

        ///// <summary>
        ///// Get all rows
        ///// </summary>
        public abstract List<T> GetRows();

        /// <summary>
        /// Get rows by status
        /// </summary>
        public abstract List<T> GetRows(string status);

        /// <summary>
        /// Get row by ID
        /// </summary>
        public abstract T GetRowByID(int ID);

        /// <summary>
        /// Add row
        /// </summary>
        /// abstract public List GetRowByID(int ID);
        public abstract int AddRow(T row);

        /// <summary>
        /// Update row
        /// </summary>
        /// abstract public List GetRowByID(int ID);
        public abstract int UpdateRow(T row);
    }
}
