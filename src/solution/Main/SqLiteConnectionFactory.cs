using DatabaseRepositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;

namespace Main
{
    /// <summary>
    /// SqlListe Database Connection Factory to helps abstract string
    /// </summary>
    /// <seealso cref="Match3DAL.IConnectionFactory" />
    public class SqLiteConnectionFactory: IConnectionFactory
    {
        /// <summary>
        /// The relative SQL lite path
        /// </summary>
        public const string RelativeSqlLitePath = @"App_Data\QBDB.db";

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        public DbConnection CreateConnection()
        {
            var sqlConnectionString = this.CreateConnectionString();
            var result = new System.Data.SQLite.SQLiteConnection(sqlConnectionString);

            return result;
        }

        /// <summary>
        /// Creates the connection string.
        /// </summary>
        /// <returns></returns>
        public string CreateConnectionString() {
            var filePath = GetFilePath();
            var result = $"data source={filePath}";

            return result;
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <returns></returns>
        public string GetFilePath() {
            var assemblyName = System.Reflection.Assembly.GetEntryAssembly().Location;
            var sitePath = Path.GetDirectoryName(assemblyName);//  HttpContext.Current.Server.MapPath("~");
            var result = System.IO.Path.Combine(sitePath, RelativeSqlLitePath);
            return result;
        }
    }
}