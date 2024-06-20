using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.ComponentModel;
using ME.Entities.Database;
using ME.Entities.Interfaces.Services;
using System.Text.RegularExpressions;

namespace ME.Application.Services.AdoDotNets
{
    public class FileStreamGenerator : IFileStreamGenerator
    {
        public virtual System.Data.Common.DbConnection? cnn { get; set; }

        public virtual string? ConnectionString { get; set; }

        public FileStreamGenerator(string conString)
        {
            this.ConnectionString = conString;
        }

        public FileStreamGenerator(System.Data.Common.DbConnection dbConnection)
        {
            this.cnn = dbConnection;
        }

        public FileStreamGenerator()
        {

        }

        public virtual void Open()
        {
            if (cnn == null)
                cnn = new System.Data.SqlClient.SqlConnection(ConnectionString);

            cnn.Open();
        }

        public virtual void Generate(Entities.Generals.FileStreamGeneratorArguments arguments)
        {
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));

            if (string.IsNullOrEmpty(arguments.FileGroupName))
                throw new ArgumentNullException(nameof(arguments.FileGroupName));

            if (string.IsNullOrEmpty(arguments.FileStreamPath))
                throw new ArgumentNullException(nameof(arguments.FileStreamPath));


            if (string.IsNullOrEmpty(arguments.TableName))
                throw new ArgumentNullException(nameof(arguments.TableName));

            if (string.IsNullOrEmpty(arguments.ColumnName))
                throw new ArgumentNullException(nameof(arguments.ColumnName));


            if (string.IsNullOrEmpty(arguments.DatabaseName))
                throw new ArgumentNullException(nameof(arguments.DatabaseName));


            if (string.IsNullOrEmpty(arguments.FolderName))
                throw new ArgumentNullException(nameof(arguments.FolderName));


            if (string.IsNullOrEmpty(arguments.PrimarykeyName))
                throw new ArgumentNullException(nameof(arguments.PrimarykeyName));


            if (cnn == null)
                throw new InvalidOperationException("First you need to call the open method.!(Communication has not been established)");


            string ConstraintKeyName = GetConstraintPrimaryKeyColumn(arguments.TableName, arguments.PrimarykeyName);

            if (string.IsNullOrEmpty(ConstraintKeyName) || string.IsNullOrWhiteSpace(ConstraintKeyName))
                throw new NullReferenceException(@$"CONSTRAINT name can not be null.(Can not find CONSTRAINT name of '{arguments.PrimarykeyName}') from '{arguments.TableName}' table");


            //https://www.sqlshack.com/manage-filestream-filegroups-of-sql-databases/
            //https://stackoverflow.com/questions/1100260/multiline-string-literal-in-c-sharp

            string GeneratorQuery = @$"
                                    USE [master]

--Create file group to file stream                                    

                                    ALTER DATABASE [{arguments.DatabaseName}]
                                    ADD FILEGROUP [{arguments.FileGroupName}] CONTAINS FILESTREAM;
                                    USE [master]

--Add file stream to database
                                   
                                    ALTER DATABASE [{arguments.DatabaseName}] 
                                    ADD FILE 
                                    (
                                        NAME = N'{arguments.DatabaseName}FileStream',
                                        FILENAME = N'{arguments.FileStreamPath}\{arguments.FolderName}',
                                        MAXSIZE = UNLIMITED
                                    )

                                    TO FILEGROUP [{arguments.FileGroupName}];
                                   
                                    
                                    use [{arguments.DatabaseName}]

                                    ALTER TABLE [dbo].[{arguments.TableName}]
                                        DROP CONSTRAINT {ConstraintKeyName};   

                                     Alter Table [dbo].[{arguments.TableName}]
                                    		DROP COLUMN [{arguments.ColumnName}];                                    

                                    Alter Table [dbo].[{arguments.TableName}]
                                    	add [{arguments.ColumnName}] varbinary(max) filestream not null

                                    Alter Table [dbo].[{arguments.TableName}]
                                    	 [{arguments.PrimarykeyName}] uniqueidentifier not null rowguidcol unique default newid()
                                        ";


            SqlCommand cmd1 = new SqlCommand(GeneratorQuery, (SqlConnection)this.cnn);

            int FileGroupEffectQuery = cmd1.ExecuteNonQuery();

        }

        /// <summary>
        /// https://stackoverflow.com/questions/41549549/sql-server-change-primary-key-data-type
        /// https://stackoverflow.com/questions/3930338/sql-server-get-table-primary-key-using-sql-query
        /// SQL Server: Get table primary key using sql query [duplicate]
        /// SQL Server Change Primary Key Data Type
        /// </summary>
        private string? GetConstraintPrimaryKeyColumn(string TableName, string ColumnName)
        {
            string IfColumnName = string.Empty;

            if (!string.IsNullOrEmpty(ColumnName))
                IfColumnName = @$"AND COLUMN_NAME = '{ColumnName}'";

            string q = @$"
SELECT *
FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + QUOTENAME(CONSTRAINT_NAME)), 'IsPrimaryKey') = 1
AND TABLE_NAME = '{TableName}' {IfColumnName}";

            if (this.cnn == null)
                throw new NullReferenceException();

            using (SqlCommand cmd1 = new SqlCommand(q, (SqlConnection)this.cnn))
            {
                using (SqlDataReader dataReader = cmd1.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        if (dataReader.Read())
                        {
                            return dataReader.GetValue("CONSTRAINT_NAME")?.ToString();
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    else
                    {
                        return string.Empty;
                    }
                }

            }
        }


        public virtual void Close()
        {

            cnn.Close();
        }

        public void Dispose()
        {
            cnn?.Dispose();
        }

        #region ADO .Net connection string

        /// <summary>
        /// If Application name leave empty default value is .NET sqlClient Data Provider
        /// </summary>
        /// <param name="EFStringConnection"></param>
        /// <returns></returns>
        public static string CreateADONetStringConnection(string EFStringConnection)
        {
            return null;
            /*
                        if (EFStringConnection.ToLower().StartsWith("metadata=")) // ADO.Net Unsopport metadata 
                        {
                            //https://stackoverflow.com/questions/34635822/how-do-i-solve-keyword-not-supported-metadata

                            var efBuilder = new EntityConnectionStringBuilder(EFStringConnection); // Convert string connection to ADO.Net
                            EFStringConnection = efBuilder.ProviderConnectionString; // Get String Connection
                        }
                        return EFStringConnection;*/
        }

        /// <summary>
        /// If Application name leave empty default value is .NET sqlClient Data Provider
        /// </summary>
        /// <param name="address"></param>
        /// <param name="dbName"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="applictionName"></param>
        /// <returns></returns>
        public static SqlConnectionStringBuilder CreateSqlConnectionStringBuilder(string address, string dbName, string username, string password, string applictionName)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException("Server address is empty");

            // Database name can be empty
            /*  if (dbName.IsNull())
                  throw new ArgumentNullException("Database name is empty");*/
            /*
                        if (username.IsNull())
                            throw new ArgumentNullException("Username is empty");

                        if (password.IsNull())
                            throw new ArgumentNullException("Password is empty");*/

            SqlConnectionStringBuilder sqlConnection = new SqlConnectionStringBuilder()
            {
                DataSource = address, // Server name  
                //InitialCatalog = dbName.IsNull() ? "master" : dbName,  //Database  //https://stackoverflow.com/questions/28007550/is-initial-catalog-required-using-sqlconnectionstringbuilder-in-c-sql-server
                //UserID = username,         //Username  
                //Password = password,    //Password
                //ApplicationName = applictionName
            };

            if (!string.IsNullOrEmpty(dbName))
                sqlConnection.InitialCatalog = dbName;


            if (!string.IsNullOrEmpty(username))
                sqlConnection.UserID = username;

            if (!string.IsNullOrEmpty(password))
                sqlConnection.Password = password;

            if (!string.IsNullOrEmpty(applictionName))
                sqlConnection.ApplicationName = applictionName;

            return sqlConnection;
        }

        /// <summary>
        /// If Application name leave empty default value is .NET sqlClient Data Provider
        /// </summary>
        /// <param name="address"></param>
        /// <param name="dbName"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="ApplictionName"></param>
        /// <returns></returns>
        public static string CreateADONetStringConnection(string address, string dbName, string username, string password, string ApplictionName)
            => CreateSqlConnectionStringBuilder(address, dbName, username, password, ApplictionName).ConnectionString;


        public string ConvertEFConnectionString(string cs)
        {
            string address = GetDataSource(cs);
            string dbName = GetDatabaseName(cs);
            string UserId = GetUserId(cs);
            string Pass = GetPassword(cs);

            return CreateADONetStringConnection(address, dbName, UserId, Pass, null);
        }



        #region Connections string

        /// <summary>
        /// Get User Id from connection string (SQL Server)
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public static string GetUserId(string ConnectionString)
            => GetConnectionStringProperty(ConnectionString, "User ID");

        /// <summary>
        /// Get Password from connection string (SQL Server)
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public static string GetPassword(string ConnectionString)
            => GetConnectionStringProperty(ConnectionString, "Password");


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public static string GetDatabaseName(string ConnectionString)
        {

            string Catalog = GetConnectionStringProperty(ConnectionString, "Catalog");
            if (!string.IsNullOrEmpty(Catalog))
                return Catalog;
            string Database = GetConnectionStringProperty(ConnectionString, "Database");

            if (!string.IsNullOrEmpty(Database))
                return Database;


            return null;
        }

        public static string GetDataSource(string ConnectionString)
        {
            //The name or network address of the instance of SQL Server to which to connect. The port number can be specified after the server name:
            //server=tcp:servername, portnumber
            string ServerAddress = GetConnectionStringProperty(ConnectionString, "Data Source");
            if (!string.IsNullOrEmpty(ServerAddress))
                return ServerAddress;

            //When specifying a local instance, always use (local). To force a protocol, add one of the following prefixes:
            //np:(local), tcp:(local), lpc:(local)

            ServerAddress = GetConnectionStringProperty(ConnectionString, "Server");

            if (!string.IsNullOrEmpty(ServerAddress))
                return ServerAddress;

            //Beginning in .NET Framework 4.5, you can also connect to a LocalDB database as follows:
            //server=(localdb)\\myInstance

            ServerAddress = GetConnectionStringProperty(ConnectionString, "Addr");

            if (!string.IsNullOrEmpty(ServerAddress))
                return ServerAddress;

            //Please read below link
            //https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring?view=dotnet-plat-ext-7.0
            ServerAddress = GetConnectionStringProperty(ConnectionString, "Network Address");

            if (!string.IsNullOrEmpty(ServerAddress))
                return ServerAddress;


            return null;
        }

        /// <summary>
        /// try find CREATE DATABASE and read [MyDatabase]
        /// 
        /// 
        ///         string text = "CREATE DATABASE [MyDatabase]";
        ///        string pattern = @"(?<=CREATE\s+DATABASE\s+)\s*\[(?<dbName>[^\]]+)\]";
        ///
        ///        Match match = Regex.Match(text, pattern);
        ///
        ///        if (match.Success)
        ///        {
        ///            Console.WriteLine(match.Groups["dbName"].Value);
        ///        }
        /// </summary>
        /// <param name="SqlScript"></param>
        /// <returns>MyDatabase</returns>
        public static string GetDatabaseNameFromCreationScript(string SqlScript)
        {
            //ChatGDP
            // Empty space \s+
            //string pattern = @"(?<=CREATE DATABASE \[)[^\]]+(?=\])";              // space sensitive "]"
            //string pattern = @"(?<=CREATE\s+DATABASE\s+)\s*\[([^\]]+)\]";         // not space sensitive but include [ ]"

            string pattern = @"(?<=CREATE\s+DATABASE\s+)\s*\[(?<dbName>[^\]]+)\]";  // not include [ ] and not space sensitive "

            Match match = Regex.Match(SqlScript, pattern);

            if (match.Success)
            {
                return match.Groups["dbName"].Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <param name="propertyName">Send like this 
        /// Failover Partner => Partner
        /// Initial Catalog  => Catalog
        /// Provider => Provider 
        /// </param>
        /// <returns></returns>
        public static string GetConnectionStringProperty(string ConnectionString, string propertyName)
        {
            /// Difference between Initial Catalog and Database keyword in connection string
            /// https://stackoverflow.com/questions/12238548/difference-between-initial-catalog-and-database-keyword-in-connection-string
            /// The only difference is the name.
            /// These can be used interchangeably.
            /// https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnectionstringbuilder.initialcatalog?view=dotnet-plat-ext-7.0&redirectedfrom=MSDN#System_Data_SqlClient_SqlConnectionStringBuilder_InitialCatalog
            /// This property corresponds to the "Initial Catalog" and "database" keys within the connection string.

            #region If has two part
            // like 
            // Data Source | Integrated Security | Network Library | Initial Catalog
            //  ignore empty space between word 

            string pn = "";
            StringBuilder stringBuilder = new StringBuilder();
            if (propertyName.Split(' ') != null && propertyName.Split(' ').Length > 1)
            {
                var ps = propertyName.Split(' ');

                for (int i = 0; i < ps.Length; i++)//Data\s*Source\s*=\s*([^;]+)
                    if (!string.IsNullOrEmpty(ps[i]))
                        stringBuilder.Append((i > 0 ? @"\s*" : "") + ps[i]);


                pn = stringBuilder.ToString() + @"\s*";
            }
            else
            {
                pn = propertyName + @"\s*";
            }


            #endregion


            string PatternCatalog = $@"{pn}=\s*([^;]+)";
            //string PatternCatalogDatabase = @"Database\s*=\s*([^;]+)";

            Match match = Regex.Match(ConnectionString, PatternCatalog, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            /*
                        Match match2 = Regex.Match(ConnectionString, PatternCatalogDatabase);

                        if (match2.Success)
                        {
                            return match2.Groups[1].Value;
                        }*/


            return null;
        }



        #endregion

        #endregion
    }


}
