using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Text;
using Thinktecture.IdentityModel.Client;

namespace handymanworkappapi.Controllers
{
    public class ResultResponse
    {
        public bool errorStatus { get; set; }
        public string errorMessage { get; set; }
        public object data { get; set; }
    }
    public class LookupName
    {
        public string CreatedByName(string id)
        {
            string thisName = "";
            EmployeeController thisTable = new EmployeeController();
            object thisDetails = thisTable.Get(Int16.Parse(id)).data;
            if (!string.IsNullOrEmpty(thisDetails.GetType().GetProperty("FirstName").GetValue(thisDetails).ToString()))
            {
                thisName = thisDetails.GetType().GetProperty("FirstName").GetValue(thisDetails).ToString();
            }
            return thisName;
        }
        public string MenuOptionName(string id)
        {
            string thisName = "";
            MenuOptionsController thisTable = new MenuOptionsController();
            object thisDetails = thisTable.Get(Int16.Parse(id)).data;
            if (!string.IsNullOrEmpty(thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString()))
            {
                thisName = thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString();
            }
            return thisName;
        }
        public string LocationName(string id)
        {
            string thisName = "";
            LocationController thisTable = new LocationController();
            object thisDetails = thisTable.Get(Int16.Parse(id)).data;
            if (!string.IsNullOrEmpty(thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString()))
            {
                thisName = thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString();
            }
            return thisName;
        }
        public string EmployeeName(string id)
        {
            string thisName = "";
            EmployeeController thisTable = new EmployeeController();
            object thisDetails = thisTable.Get(Int16.Parse(id)).data;
            if (!string.IsNullOrEmpty(thisDetails.GetType().GetProperty("NickName").GetValue(thisDetails).ToString()))
            {
                thisName = thisDetails.GetType().GetProperty("NickName").GetValue(thisDetails).ToString();
            }
            return thisName;
        }
        public string MaintenanceIssueStatusName(string id)
        {
            string thisName = "";
            MaintenanceIssueStatusController thisTable = new MaintenanceIssueStatusController();
            object thisDetails = thisTable.Get(Int16.Parse(id)).data;
            if (!string.IsNullOrEmpty(thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString()))
            {
                thisName = thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString();
            }
            return thisName;
        }
        public string MaintenancePriorityName(string id)
        {
            string thisName = "";
            MaintenancePriorityController thisTable = new MaintenancePriorityController();
            object thisDetails = thisTable.Get(Int16.Parse(id)).data;
            if (!string.IsNullOrEmpty(thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString()))
            {
                thisName = thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString();
            }
            return thisName;
        }
        public string MenuOptionComponent(string id)
        {
            string thisName = "";
            MenuOptionsController thisTable = new MenuOptionsController();
            object thisDetails = thisTable.Get(Int16.Parse(id)).data;
            if (!string.IsNullOrEmpty(thisDetails.GetType().GetProperty("Component").GetValue(thisDetails).ToString()))
            {
                thisName = thisDetails.GetType().GetProperty("Component").GetValue(thisDetails).ToString();
            }
            return thisName;
        }
        public string RoomName(string id)
        {
            string thisName = "";
            RoomController thisTable = new RoomController();
            object thisDetails = thisTable.Get(Int16.Parse(id)).data;
            if (!string.IsNullOrEmpty(thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString()))
            {
                thisName = thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString();
            }
            return thisName;
        }
        public string InventoryTypeName(string id)
        {
            string thisName = "";
            InventoryTypeController thisTable = new InventoryTypeController();
            object thisDetails = thisTable.Get(Int16.Parse(id)).data;
            if (!string.IsNullOrEmpty(thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString()))
            {
                thisName = thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString();
            }
            return thisName;
        }
        public string InventoryItemName(string id)
        {
            string thisName = "";
            InventoryItemController thisTable = new InventoryItemController();
            object thisDetails = thisTable.Get(Int16.Parse(id)).data;
            if (!string.IsNullOrEmpty(thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString()))
            {
                thisName = thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString();
            }
            return thisName;
        }
        public string VendorName(string id)
        {
            string thisName = "";
            VendorController thisTable = new VendorController();
            object thisDetails = thisTable.Get(Int16.Parse(id)).data;
            if (!string.IsNullOrEmpty(thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString()))
            {
                thisName = thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString();
            }
            return thisName;
        }
        public string UserRoleName(string id)
        {
            string thisName = "";
            UserRolesController thisTable = new UserRolesController();
            object thisDetails = thisTable.Get(Int16.Parse(id)).data;
            if (!string.IsNullOrEmpty(thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString()))
            {
                thisName = thisDetails.GetType().GetProperty("Name").GetValue(thisDetails).ToString();
            }
            return thisName;
        }
    }
    public class ADOHelper
    {
        public string appName = "handymanworkapp";
        public DbConnection getDataConnection()
        {
            string connectionStringName = "";
            DbConnection conn = null;
            connectionStringName = ConfigurationManager.ConnectionStrings[appName].ToString();
            conn = new SqlConnection(connectionStringName);
            return conn;
        }

        public DbCommand getDataCommand(DbConnection conn, string SQLText, CommandType SQLCommandType = CommandType.Text)
        {
            DbCommand cmd = null;
            conn.Open();
            cmd = new SqlCommand(SQLText, (SqlConnection)conn);
            cmd.CommandType = SQLCommandType;
            cmd.CommandTimeout = 600;        // Default is 30 but we can try this to see if it helps (0)means indefinite
            if (SQLCommandType == CommandType.StoredProcedure)
                SqlCommandBuilder.DeriveParameters((SqlCommand)cmd);
            return cmd;
        }

        public DbDataReader getDataReader(DbCommand cmd)
        {
            DbDataReader rdr = null;
            rdr = (SqlDataReader)cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return rdr;
        }

        public List<Object[]> callSP(string spName, params object[] arg)
        {
            List<Object[]> returnVal = new List<Object[]>();

            using (DbConnection conn = getDataConnection())              // Connect to Database
            {
                try
                {
                    using (DbCommand cmd = getDataCommand(conn, spName, CommandType.StoredProcedure))  // Prepare SP Command
                    {
                        int i;
                        int arrayLen = 0;
                        if (arg != null)
                            arrayLen = arg.Length;
                        for (i = 0; i < arrayLen; i++)
                        {
                            cmd.Parameters[i + 1].Value = arg[i];
                        }
                        using (DbDataReader rdr = getDataReader(cmd))      // Read the returned resultset
                        {
                            int fieldCount = rdr.FieldCount;
                            while (rdr.Read())
                            {
                                Object[] oneRow = new object[fieldCount];
                                rdr.GetValues(oneRow);
                                returnVal.Add(oneRow);
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message + " - " + spName);
                }
            }
            return returnVal;
        }

        public bool callInsertUpdate(List<string> transList, ref Exception ErrorMessage)
        {
            bool SQLStatus = true;

            using (DbConnection conn = getDataConnection())              // Connect to Database
            {
                string SQLText = "";
                try
                {
                    using (DbCommand cmd = getDataCommand(conn, ""))   // Prepare SQL Command
                    {
                        DbTransaction transaction;
                        transaction = conn.BeginTransaction();
                        cmd.Transaction = transaction;
                        try
                        {
                            foreach (string spCall in transList)
                            {
                                SQLText = spCall;
                                cmd.CommandText = spCall;
                                cmd.CommandType = CommandType.Text;
                                cmd.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                        catch (Exception Ex)
                        {
                            Console.WriteLine("Rollback: " + Ex.Message + " - " + SQLText);
                            ErrorMessage = Ex;
                            try
                            {
                                transaction.Rollback();
                            }
                            catch (Exception Ex2)
                            {
                                Console.WriteLine(Ex2.Message + " - " + SQLText);
                            }
                            SQLStatus = false;
                        }
                    }
                }
                catch (Exception Ex3)
                {
                    Console.WriteLine(Ex3.Message + " - " + SQLText);
                    ErrorMessage = Ex3;
                    SQLStatus = false;
                }
            }
            return SQLStatus;
        }

        public int callExecuteQueryCount(string SQLText)
        {
            int count = 0;

            using (DbConnection conn = getDataConnection())              // Connect to Database
            {
                try
                {
                    using (DbCommand cmd = getDataCommand(conn, SQLText))  // Prepare SP Command
                    {
                        count = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message + " - " + SQLText);
                }
            }
            return count;
        }

        public int callExecuteQueryIdent(string SQLText)
        {
            int count = 0;

            using (DbConnection conn = getDataConnection())              // Connect to Database
            {
                try
                {
                    using (DbCommand cmd = getDataCommand(conn, SQLText))  // Prepare SP Command
                    {
                        using (DbDataReader rdr = getDataReader(cmd))      // Read the returned resultset
                        {
                            rdr.Read();
                            count = (int)rdr["identCount"];
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message + " - " + SQLText);
                }
            }
            return count;
        }

        public void errorLog(string error, string desc, Exception e = null, string logType = "None")
        {
            string exception;
            if (e != null)
                exception = e.Message;
            else
                exception = "";

            callSP("logging", "sp_insertLog", new object[6] { appName, error, desc, exception, logType, "N" });
            Console.WriteLine(exception + " - " + desc);
        }

        public void userLog(int userId, string location, string source, string process, string message, string eventclass, int eventpriority)
        {
            callSP("activity", "sp_insertUserLog", new object[7] { userId, location, source, process, message, eventclass, eventpriority });
        }

        public bool InsertStatement(ref string insert, ref string value, string[] fields, object item, ref Exception ErrorMessage)
        {
            bool result = true;
            try
            {
                foreach (string s in fields)
                {
                    insert = insert + "," + s.Split(':')[1];
                    var a = s.Split(':')[0];
                    var b = s.Split(':')[1];
                    switch (a)
                    {
                        case "int":
                            value = value + "," + item.GetType().GetProperty(b).GetValue(item);
                            break;
                        case "bool":
                            if (item.GetType().GetProperty(b).GetValue(item).ToString() == "False")
                            {
                                value = value + "," + 0;
                            }
                            if (item.GetType().GetProperty(b).GetValue(item).ToString() == "True")
                            {
                                value = value + "," + 1;
                            }
                            break;
                        case "string":
                            value = value + "," + "'" + item.GetType().GetProperty(b).GetValue(item) + "'";
                            break;
                        case "DateTime":
                            value = value + "," + item.GetType().GetProperty(b).GetValue(item);
                            break;
                        case "varbinarymax":
                            value = value + "," + "CONVERT(varbinary(max),'" + item.GetType().GetProperty(b).GetValue(item) + "')";
                            break;
                        default:
                            value = value + "," + item.GetType().GetProperty(b).GetValue(item);
                            break;
                    }
                }

                insert = insert + ",CreatedOnUtc,UpdatedOnUtc";
                value = value + ",'" + DateTime.Now + "','" + DateTime.Now + "'";
                insert = "(" + insert.Substring(1) + ")";
                value = "(" + value.Substring(1) + ")";
            }
            catch (Exception ex)
            {
                result = false;
                ErrorMessage = ex;
            }

            return result;
        }

        public bool UpdateStatement(ref string value, string[] fields, object item, ref Exception ErrorMessage)
        {
            bool result = true;
            try
            {
                foreach (string s in fields)
                {
                    switch (s.Split(':')[0])
                    {
                        case "int":
                            value = value + "," + s.Split(':')[1] + "=" + item.GetType().GetProperty(s.Split(':')[1]).GetValue(item);
                            break;
                        case "bool":
                            if (item.GetType().GetProperty(s.Split(':')[1]).GetValue(item).ToString() == "False")
                            {
                                value = value + "," + s.Split(':')[1] + "=" + 0;
                            }
                            if (item.GetType().GetProperty(s.Split(':')[1]).GetValue(item).ToString() == "True")
                            {
                                value = value + "," + s.Split(':')[1] + "=" + 1;
                            }
                            break;
                        case "string":
                            value = value + "," + s.Split(':')[1] + "='" + item.GetType().GetProperty(s.Split(':')[1]).GetValue(item) + "'";
                            break;
                        case "DateTime":
                            value = value + "," + s.Split(':')[1] + "='" + item.GetType().GetProperty(s.Split(':')[1]).GetValue(item) + "'";
                            break;
                        case "varbinarymax":
                            value = value + "," + "CONVERT(varbinary(max),'" + item.GetType().GetProperty(s.Split(':')[1]).GetValue(item) + "')";
                            break;
                        default:
                            value = value + "," + s.Split(':')[1] + "=" + item.GetType().GetProperty(s.Split(':')[1]).GetValue(item);
                            break;
                    }
                }
                value = value + ",UpdatedOnUtc='" + DateTime.Now + "'";
                value = value.Substring(1);
            }
            catch (Exception ex)
            {
                result = false;
                ErrorMessage = ex;
            }
            return result;
        }

        public object ReaderLoop(object item, DbDataReader reader, string[] fields)
        {
            try
            {
                Type type = item.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (string s in fields)
                {
                    switch (s.Split(':')[0])
                    {
                        case "int":
                            foreach (PropertyInfo property in properties)
                            {
                                if (property.Name == s.Split(':')[1])
                                {
                                    int IntValue = ((reader[property.Name]) == DBNull.Value) ? 0 : (int)(reader[property.Name]);
                                    item.GetType().GetProperty(property.Name).SetValue(item, IntValue);
                                }
                            }

                            break;
                        case "bool":
                            foreach (PropertyInfo property in properties)
                            {
                                if (property.Name == s.Split(':')[1])
                                {
                                    bool BoolValue = ((reader[property.Name]) == DBNull.Value) ? false : (bool)(reader[property.Name]);
                                    item.GetType().GetProperty(property.Name).SetValue(item, BoolValue);
                                }
                            }

                            break;
                        case "string":

                            foreach (PropertyInfo property in properties)
                            {
                                if (property.Name == s.Split(':')[1])
                                {
                                    string BoolValue = reader[property.Name].ToString().Trim();
                                    item.GetType().GetProperty(property.Name).SetValue(item, BoolValue);
                                }
                            }

                            break;
                        case "DateTime":

                            foreach (PropertyInfo property in properties)
                            {
                                if (property.Name == s.Split(':')[1])
                                {
                                    DateTime? BoolValue = ((reader[property.Name]) == DBNull.Value) ? (DateTime?)null : (DateTime)(reader[property.Name]);
                                    item.GetType().GetProperty(property.Name).SetValue(item, BoolValue);
                                }
                            }
                            break;
                        case "varbinarymax":
                            foreach (PropertyInfo property in properties)
                            {
                                if (property.Name == s.Split(':')[1])
                                {
                                    byte[] BoolValue = ((reader[property.Name]) == DBNull.Value) ? null : (byte[])(reader[property.Name]);
                                    item.GetType().GetProperty(property.Name).SetValue(item, Encoding.ASCII.GetString(BoolValue));
                                }
                            }

                            break;
                        default:
                            foreach (PropertyInfo property in properties)
                            {
                                if (property.Name == s.Split(':')[1])
                                {
                                    string BoolValue = reader[property.Name].ToString().Trim();
                                    item.GetType().GetProperty(property.Name).SetValue(item, BoolValue);
                                }
                            }

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                item.GetType().GetProperty("errorStatus").SetValue(item, false);
                item.GetType().GetProperty("errorMessage").SetValue(item, ex.Message);
            }
            return item;
        }

        public object LookupLoop(object item, string[] fields)
        {
            try
            {
                LookupName lookup = new LookupName();
                Type type = item.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (string field in fields)
                {
                    string parameterId = "";
                    foreach (PropertyInfo property in properties)
                    {
                        string Id = field.Split(':')[0];
                        string Name = field.Split(':')[1];
                        string methodName = field.Split(':')[2];

                        if (property.Name == Id)
                        {
                            string tempParameterId = item.GetType().GetProperty(property.Name).GetValue(item).ToString();
                            parameterId = tempParameterId;
                        }
                        if (property.Name == Name)
                        {
                            Type lookupType = lookup.GetType();
                            ConstructorInfo magicConstructor = lookupType.GetConstructor(Type.EmptyTypes);
                            object magicClassObject = magicConstructor.Invoke(new object[] { });
                            MethodInfo method = lookupType.GetMethod(methodName);
                            string retValue = (string)method.Invoke(magicClassObject, new object[] { parameterId });
                            item.GetType().GetProperty(property.Name).SetValue(item, retValue);
                            parameterId = "";
                        }
                    }
                }
                item.GetType().GetProperty("errorStatus").SetValue(item, true);
                item.GetType().GetProperty("errorMessage").SetValue(item, "Success");
            }
            catch (Exception ex)
            {
                item.GetType().GetProperty("errorStatus").SetValue(item, false);
                item.GetType().GetProperty("errorMessage").SetValue(item, ex.Message);
            }
            return item;
        }
    }

    public class _Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int DefaultMenuId { get; set; }
        public string DefaultMenuName { get; set; }
        public string DefaultMenuComponent { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public string[] Fields = {
     "string:FirstName",
     "string:LastName",
     "string:NickName",
     "string:Mobile",
     "string:Email",
     "string:Password",
     "bool:Active",
     "int:RoleId",
     "int:LocationId",
     "int:DefaultMenuId"
     };
        public string[] ReaderFields = {
     "int:Id",
     "string:FirstName",
     "string:LastName",
     "string:NickName",
     "string:Mobile",
     "string:Email",
     "string:Password",
     "bool:Active",
     "DateTime:CreatedOnUtc",
     "DateTime:UpdatedOnUtc",
     "int:RoleId",
     "int:LocationId",
     "int:DefaultMenuId"
     };
        public string[] LookupReaderFields = {
     "RoleId:RoleName:UserRoleName",
     "LocationId:LocationName:LocationName",
     "DefaultMenuId:DefaultMenuName:MenuOptionName",
     "DefaultMenuId:DefaultMenuComponent:MenuOptionComponent"
     };
        public string TableColumnsBuilder()
        {
            return "Id,FirstName,LastName,NickName,Mobile,Email,Password,Active,CreatedOnUtc,UpdatedOnUtc,RoleId,LocationId,DefaultMenuId";
        }
        public string SqlScript()
        {
            return "USE [handymanworkapp]" +
              " GO" +
              " SET ANSI_NULLS ON" +
              " GO" +
              " SET QUOTED_IDENTIFIER ON" +
              " GO" +
              " CREATE TABLE[dbo].[Employee](" +
              "   [Id][int] IDENTITY(1, 1) NOT NULL," +
              "   [FirstName] [nvarchar] (50) NOT NULL," +
              "   [LastName] [nvarchar] (50) NOT NULL," +
              "   [NickName] [nvarchar] (50) NULL," +
              "   [Mobile] [nvarchar] (50) NULL," +
              "   [Email] [nvarchar] (50) NULL," +
              "   [Password] [nvarchar] (50) NULL," +
              "   [Active] [bit] NOT NULL," +
              "   [RoleId] [int] NOT NULL," +
              "   [LocationId] [int] NOT NULL," +
              "   [DefaultMenuId] [int] NOT NULL," +
              "   [CreatedOnUtc] [DATETIME] NOT NULL," +
              "   [UpdatedOnUtc] [DATETIME] NOT NULL," +
              "  CONSTRAINT[PK_Employee] PRIMARY KEY CLUSTERED" +
              " (" +
              "    [Id] ASC" +
              " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
              " ) ON[PRIMARY]" +
              " GO";
        }
    }
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();
                _Employee sqlQueryHelp = new _Employee();

                string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder()) + " FROM Employee";

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_Employee> self = new List<_Employee>();
                            while (reader.Read())
                            {
                                _Employee _instance = new _Employee();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Employee)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _Employee sqlQueryHelp = new _Employee();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM Employee WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _Employee _instance = new _Employee();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Employee)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("ValidateUser")]
        public bool ValidateUser([FromBody]_Employee item)
        {
            bool userValidated = false;

            ADOHelper dbHelp = new ADOHelper();
            _Employee sqlQueryHelp = new _Employee();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM Employee WHERE Email='" + item.Email + "' AND Password='" + item.Password + "'";


            using (DbConnection conn = dbHelp.getDataConnection())
            {
                using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                {
                    using (DbDataReader reader = dbHelp.getDataReader(cmd))
                    {
                        while (reader.Read())
                        {
                            userValidated = true;
                        }
                    }
                }
            }

            return userValidated;
        }

        [HttpPost]
        [Route("GetUser")]
        public ResultResponse GetUser([FromBody]_Employee item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _Employee sqlQueryHelp = new _Employee();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM Employee WHERE Email='" + item.Email + "' AND Password='" + item.Password + "'";


            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _Employee _instance = new _Employee();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Employee)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }

        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _Employee sqlQueryHelp = new _Employee();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM Employee WHERE " + whereString;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_Employee> self = new List<_Employee>();

                            while (reader.Read())
                            {
                                _Employee _instance = new _Employee();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Employee)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public ResultResponse Create([FromBody]_Employee item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _Employee _instance = new _Employee();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, _instance.Fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO Employee " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                    _instance.Id = dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM Employee");
                    result.data = Get(_instance.Id);
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]_Employee item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            _Employee _instance = new _Employee();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, _instance.Fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = ""; // "UPDATE Employee SET " + cloumnsWithValues + " WHERE Id=" + id;
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = false;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            result.errorStatus = false;
            result.errorMessage = "";

            ADOHelper dbHelp = new ADOHelper();
            _Employee sqlQueryHelp = new _Employee();

            string SQLQuery = "DELETE FROM Employee WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _Employee _instance = new _Employee();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Employee)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }

    public class _InventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int InventoryTypeId { get; set; }
        public string InventoryTypeName { get; set; }
        public int Price { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public string[] Fields = {
     "string:Name",
     "string:Description",
     "int:InventoryTypeId",
     "int:Price",
     "bool:Active",
     };
        public string[] ReaderFields = {
     "int:Id",
     "string:Name",
     "string:Description",
     "int:InventoryTypeId",
     "int:Price",
     "bool:Active",
     "DateTime:CreatedOnUtc",
     "DateTime:UpdatedOnUtc",
     };
        public string[] LookupReaderFields = {
     "InventoryTypeId:InventoryTypeName:InventoryTypeName",
     };
        public string TableColumnsBuilder()
        {
            return "Id,Name,Description,InventoryTypeId,Price,Active,CreatedOnUtc,UpdatedOnUtc";
        }
        public string SqlScript()
        {
            return "USE [handymanworkapp]" +
            " GO" +
            " SET ANSI_NULLS ON" +
            " GO" +
            " SET QUOTED_IDENTIFIER ON" +
            " GO" +
            " CREATE TABLE[dbo].[InventoryItem](" +
            "     [Id][INT] IDENTITY(1, 1) NOT NULL," +
            "     [Name] [NVARCHAR] (50) NOT NULL," +
            "     [Description] [NVARCHAR] (100) NULL," +
            " 	[InventoryTypeId]  [INT] NOT NULL," +
            "     [Price] [INT]  NOT NULL," +
            "     [Active] [BIT] NOT NULL," +
            "     [CreatedOnUtc] [DATETIME] NOT NULL," +
            "     [UpdatedOnUtc] [DATETIME] NOT NULL," +
            "  CONSTRAINT[PK_InventoryItem] PRIMARY KEY CLUSTERED" +
            " (" +
            "    [Id] ASC" +
            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
            " ) ON[PRIMARY]" +
            " GO";
        }
    }
    [RoutePrefix("api/inventoryitem")]
    public class InventoryItemController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();
                _InventoryItem sqlQueryHelp = new _InventoryItem();
                string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder()) + " FROM InventoryItem";

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_InventoryItem> self = new List<_InventoryItem>();
                            while (reader.Read())
                            {
                                _InventoryItem _instance = new _InventoryItem();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_InventoryItem)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _InventoryItem sqlQueryHelp = new _InventoryItem();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM InventoryItem WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _InventoryItem _instance = new _InventoryItem();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_InventoryItem)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _InventoryItem sqlQueryHelp = new _InventoryItem();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM InventoryItem WHERE " + whereString;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_InventoryItem> self = new List<_InventoryItem>();

                            while (reader.Read())
                            {
                                _InventoryItem _instance = new _InventoryItem();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_InventoryItem)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]_InventoryItem item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _InventoryItem _instance = new _InventoryItem();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, _instance.Fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO InventoryItem " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                    _instance.Id = dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM InventoryItem");
                    result.data = Get(_instance.Id);
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]_InventoryItem item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            _InventoryItem _instance = new _InventoryItem();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, _instance.Fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = ""; // "UPDATE InventoryItem SET " + cloumnsWithValues + " WHERE Id=" + id;
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = false;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _InventoryItem sqlQueryHelp = new _InventoryItem();

            string SQLQuery = "DELETE FROM InventoryItem WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _InventoryItem _instance = new _InventoryItem();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_InventoryItem)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }

    public class _InventoryType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public string[] Fields = {
     "string:Name",
     "bool:Active",
     };
        public string[] ReaderFields = {
     "int:Id",
     "string:Name",
     "bool:Active",
     "DateTime:CreatedOnUtc",
     "DateTime:UpdatedOnUtc",
     };
        public string[] LookupReaderFields = {

     };
        public string TableColumnsBuilder()
        {
            return "Id,Name,Active,CreatedOnUtc,UpdatedOnUtc";
        }
        public string SqlScript()
        {
            return "USE [handymanworkapp]" +
            " GO" +
            " SET ANSI_NULLS ON" +
            " GO" +
            " SET QUOTED_IDENTIFIER ON" +
            " GO" +
            " CREATE TABLE[dbo].[InventoryType] (" +
            " 	[Id] [INT] IDENTITY(1,1) NOT NULL," +
            " 	[Name] [NVARCHAR] (50) NOT NULL," +
            " 	[Active] [BIT] NOT NULL," +
            " 	[CreatedOnUtc] [DATETIME] NOT NULL," +
            " 	[UpdatedOnUtc] [DATETIME] NOT NULL," +
            "  CONSTRAINT[PK_InventoryType] PRIMARY KEY CLUSTERED " +
            " (" +
            " 	[Id] ASC" +
            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
            " ) ON[PRIMARY]" +
            " GO";
        }
    }
    [RoutePrefix("api/inventorytype")]
    public class InventoryTypeController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();
                _InventoryType sqlQueryHelp = new _InventoryType();
                string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder()) + " FROM InventoryType";

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_InventoryType> self = new List<_InventoryType>();
                            while (reader.Read())
                            {
                                _InventoryType _instance = new _InventoryType();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_InventoryType)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _InventoryType sqlQueryHelp = new _InventoryType();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM InventoryType WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _InventoryType _instance = new _InventoryType();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_InventoryType)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _InventoryType sqlQueryHelp = new _InventoryType();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM InventoryType WHERE " + whereString;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_InventoryType> self = new List<_InventoryType>();

                            while (reader.Read())
                            {
                                _InventoryType _instance = new _InventoryType();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_InventoryType)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]_InventoryType item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _InventoryType _instance = new _InventoryType();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, _instance.Fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO InventoryType " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                    _instance.Id = dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM InventoryType");
                    result.data = Get(_instance.Id);
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]_InventoryType item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            _InventoryType _instance = new _InventoryType();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, _instance.Fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = ""; // "UPDATE InventoryType SET " + cloumnsWithValues + " WHERE Id=" + id;
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = false;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            result.errorStatus = false;
            result.errorMessage = "";

            ADOHelper dbHelp = new ADOHelper();
            _InventoryType sqlQueryHelp = new _InventoryType();

            string SQLQuery = "DELETE FROM InventoryType WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _InventoryType _instance = new _InventoryType();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_InventoryType)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }

    public class _Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public string[] Fields = {
     "string:Name",
     "string:Street1",
     "string:Street2",
     "string:City",
     "string:State",
     "string:Country",
     "string:Zipcode",
     "string:Phone",
     "string:Email",
     };
        public string[] ReaderFields = {
     "int:Id",
     "string:Name",
     "string:Street1",
     "string:Street2",
     "string:City",
     "string:State",
     "string:Country",
     "string:Zipcode",
     "string:Phone",
     "string:Email",
     "DateTime:CreatedOnUtc",
     "DateTime:UpdatedOnUtc"
     };
        public string[] LookupReaderFields = {

     };
        public string TableColumnsBuilder()
        {
            return "Id,Name,Street1,Street2,City,State,Country,Zipcode,Phone,Email,CreatedOnUtc,UpdatedOnUtc";
        }
        public string SqlScript()
        {
            return "USE [handymanworkapp]" +
              " GO" +
              " SET ANSI_NULLS ON" +
              " GO" +
              " SET QUOTED_IDENTIFIER ON" +
              " GO" +
              " CREATE TABLE[dbo].[Location] (" +
              " 	[Id] [INT] IDENTITY(1,1) NOT NULL," +
              " 	[Name] [NVARCHAR] (50) NOT NULL," +
              " 	[Street1] [NVARCHAR] (50) NOT NULL," +
              " 	[Street2] [NVARCHAR] (50) NULL," +
              " 	[City] [NVARCHAR] (50) NULL," +
              " 	[State] [NVARCHAR] (50) NULL," +
              " 	[Country] [NVARCHAR] (50) NULL," +
              " 	[Zipcode] [NVARCHAR] (50) NOT NULL," +
              " 	[Phone] [NVARCHAR] (50) NOT NULL," +
              " 	[Email] [NVARCHAR] (50) NOT NULL," +
              " 	[CreatedOnUtc]    [DATETIME]    NOT NULL," +
              " 	[UpdatedOnUtc]    [DATETIME]    NOT NULL," +
              "  CONSTRAINT[PK_Location] PRIMARY KEY CLUSTERED " +
              " (" +
              " 	[Id] ASC" +
              " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
              " ) ON[PRIMARY]" +
              " GO";
        }
    }
    [RoutePrefix("api/location")]
    public class LocationController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();
                _Location sqlQueryHelp = new _Location();
                string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder()) + " FROM Location";

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_Location> self = new List<_Location>();
                            while (reader.Read())
                            {
                                _Location _instance = new _Location();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Location)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _Location sqlQueryHelp = new _Location();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM Location WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _Location _instance = new _Location();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Location)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _Location sqlQueryHelp = new _Location();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM Location WHERE " + whereString;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_Location> self = new List<_Location>();

                            while (reader.Read())
                            {
                                _Location _instance = new _Location();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Location)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]_Location item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            _Location _instance = new _Location();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, _instance.Fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO Location " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                    _instance.Id = dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM Location");
                    result.data = Get(_instance.Id);
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]_Location item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            _Location _instance = new _Location();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, _instance.Fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = ""; // "UPDATE Location SET " + cloumnsWithValues + " WHERE Id=" + id;
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = false;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _Location sqlQueryHelp = new _Location();

            string SQLQuery = "DELETE FROM Location WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _Location _instance = new _Location();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Location)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }

    public class _MaintenanceIssueStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public string[] Fields = {
     "string:Name",
     };
        public string[] ReaderFields = {
     "int:Id",
     "string:Name",
     "DateTime:CreatedOnUtc",
     "DateTime:UpdatedOnUtc",
     };
        public string[] LookupReaderFields = {

     };
        public string TableColumnsBuilder()
        {
            return "Id,Name,CreatedOnUtc,UpdatedOnUtc";
        }
        public string SqlScript()
        {
            return "USE [handymanworkapp]" +
            " GO" +
            " SET ANSI_NULLS ON" +
            " GO" +
            " SET QUOTED_IDENTIFIER ON" +
            " GO" +
            " CREATE TABLE[dbo].[MaintenanceIssueStatus](" +
            " 	[Id] [INT] IDENTITY(1,1) NOT NULL," +
            " 	[Name] [NVARCHAR] (50) NOT NULL," +
            " 	[CreatedOnUtc]    [DATETIME]    NOT NULL," +
            " 	[UpdatedOnUtc] [DATETIME] NULL," +
            "  CONSTRAINT[PK_MaintenanceIssueStatus] PRIMARY KEY CLUSTERED " +
            " (" +
            " 	[Id] ASC" +
            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
            " ) ON[PRIMARY]" +
            " GO";
        }
    }
    [RoutePrefix("api/maintenanceissuestatus")]
    public class MaintenanceIssueStatusController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();
                _MaintenanceIssueStatus sqlQueryHelp = new _MaintenanceIssueStatus();
                string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder()) + " FROM MaintenanceIssueStatus";

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_MaintenanceIssueStatus> self = new List<_MaintenanceIssueStatus>();
                            while (reader.Read())
                            {
                                _MaintenanceIssueStatus _instance = new _MaintenanceIssueStatus();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenanceIssueStatus)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MaintenanceIssueStatus sqlQueryHelp = new _MaintenanceIssueStatus();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM MaintenanceIssueStatus WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _MaintenanceIssueStatus _instance = new _MaintenanceIssueStatus();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenanceIssueStatus)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MaintenanceIssueStatus sqlQueryHelp = new _MaintenanceIssueStatus();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM MaintenanceIssueStatus WHERE " + whereString;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_MaintenanceIssueStatus> self = new List<_MaintenanceIssueStatus>();

                            while (reader.Read())
                            {
                                _MaintenanceIssueStatus _instance = new _MaintenanceIssueStatus();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenanceIssueStatus)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]_MaintenanceIssueStatus item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _MaintenanceIssueStatus _instance = new _MaintenanceIssueStatus();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, _instance.Fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO MaintenanceIssueStatus " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                    _instance.Id = dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM MaintenanceIssueStatus");
                    result.data = Get(_instance.Id);
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]_MaintenanceIssueStatus item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _MaintenanceIssueStatus _instance = new _MaintenanceIssueStatus();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, _instance.Fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = ""; // "UPDATE MaintenanceIssueStatus SET " + cloumnsWithValues + " WHERE Id=" + id;
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = false;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MaintenanceIssueStatus sqlQueryHelp = new _MaintenanceIssueStatus();

            string SQLQuery = "DELETE FROM MaintenanceIssueStatus WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _MaintenanceIssueStatus _instance = new _MaintenanceIssueStatus();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenanceIssueStatus)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }

    public class _MaintenancePriority
    {
        public int Id { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string Name { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public string[] Fields = {
     "string:Name",
     };
        public string[] ReaderFields = {
     "int:Id",
     "string:Name",
     "DateTime:CreatedOnUtc",
     "DateTime:UpdatedOnUtc",
     };
        public string[] LookupReaderFields = {

     };
        public string TableColumnsBuilder()
        {
            return "Id,CreatedOnUtc,UpdatedOnUtc,Name";
        }
        public string SqlScript()
        {
            return "USE [handymanworkapp]" +
            " GO" +
            " SET ANSI_NULLS ON" +
            " GO" +
            " SET QUOTED_IDENTIFIER ON" +
            " GO" +
            " CREATE TABLE[dbo].[MaintenancePriority](" +
            "   [Id][INT] IDENTITY(1, 1) NOT NULL," +
            "   [Name][NVARCHAR](50) NOT NULL," +
            "   [CreatedOnUtc][DATETIME] NULL," +
            "   [UpdatedOnUtc][DATETIME] NULL," +
            " CONSTRAINT[PK_MaintenancePriority] PRIMARY KEY CLUSTERED" +
            " (" +
            "   [Id] ASC" +
            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
            " ) ON[PRIMARY]" +
            " GO";
        }
    }
    [RoutePrefix("api/maintenancepriority")]
    public class MaintenancePriorityController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();
                _MaintenancePriority sqlQueryHelp = new _MaintenancePriority();
                string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder()) + " FROM MaintenancePriority";

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_MaintenancePriority> self = new List<_MaintenancePriority>();
                            while (reader.Read())
                            {
                                _MaintenancePriority _instance = new _MaintenancePriority();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenancePriority)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MaintenancePriority sqlQueryHelp = new _MaintenancePriority();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM MaintenancePriority WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _MaintenancePriority _instance = new _MaintenancePriority();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenancePriority)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MaintenancePriority sqlQueryHelp = new _MaintenancePriority();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM MaintenancePriority WHERE " + whereString;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_MaintenancePriority> self = new List<_MaintenancePriority>();

                            while (reader.Read())
                            {
                                _MaintenancePriority _instance = new _MaintenancePriority();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenancePriority)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]_MaintenancePriority item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _MaintenancePriority _instance = new _MaintenancePriority();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, _instance.Fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO MaintenancePriority " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                    _instance.Id = dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM MaintenancePriority");
                    result.data = Get(_instance.Id);
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]_MaintenancePriority item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _MaintenancePriority _instance = new _MaintenancePriority();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, _instance.Fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = ""; // "UPDATE MaintenancePriority SET " + cloumnsWithValues + " WHERE Id=" + id;
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = false;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MaintenancePriority sqlQueryHelp = new _MaintenancePriority();

            string SQLQuery = "DELETE FROM MaintenancePriority WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _MaintenancePriority _instance = new _MaintenancePriority();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenancePriority)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }

    public class _MaintenanceService
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int AssignedEmployeeId { get; set; }
        public string AssignedEmployeeName { get; set; }
        public bool Deleted { get; set; }
        public int MaintenanceIssueStatusId { get; set; }
        public string MaintenanceIssueStatusName { get; set; }
        public int MaintenancePriorityId { get; set; }
        public string MaintenancePriorityName { get; set; }
        public int DaysToFinish { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string Comment { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public string[] Fields = {
     "int:LocationId",
     "int:AssignedEmployeeId",
     "bool:Deleted",
     "int:MaintenanceIssueStatusId",
     "int:MaintenancePriorityId",
     "int:DaysToFinish",
     "int:RoomId",
     "string:Comment",
     "string:Description"
     };
        public string[] ReaderFields = {
     "int:Id",
     "int:LocationId",
     "int:AssignedEmployeeId",
     "bool:Deleted",
     "int:MaintenanceIssueStatusId",
     "int:MaintenancePriorityId",
     "int:DaysToFinish",
     "string:Comment",
     "string:Description",
     "DateTime:CreatedOnUtc",
     "DateTime:UpdatedOnUtc",
     "int:RoomId",
     };
        public string[] LookupReaderFields = {
     "LocationId:LocationName:LocationName",
     "AssignedEmployeeId:AssignedEmployeeName:EmployeeName",
     "MaintenanceIssueStatusId:MaintenanceIssueStatusName:MaintenanceIssueStatusName",
     "MaintenancePriorityId:MaintenancePriorityName:MaintenancePriorityName",
     "RoomId:RoomName:RoomName",
     };
        public string TableColumnsBuilder()
        {
            return "Id,LocationId,AssignedEmployeeId,Deleted,MaintenanceIssueStatusId,MaintenancePriorityId,DaysToFinish,RoomId,Comment,Description,CreatedOnUtc,UpdatedOnUtc";
        }
        public string SqlScript()
        {
            return "USE [handymanworkapp]" +
            " GO" +
            " SET ANSI_NULLS ON" +
            " GO" +
            " SET QUOTED_IDENTIFIER ON" +
            " GO" +
            " CREATE TABLE[dbo].[MaintenanceService](" +
            "   [Id][INT] IDENTITY(1, 1) NOT NULL," +
            "   [LocationId][INT] NOT NULL," +
            "   [AssignedEmployeeId][INT] NOT NULL," +
            "   [Deleted][BIT] NOT NULL," +
            "   [MaintenanceIssueStatusId][INT] NOT NULL," +
            "   [MaintenancePriorityId][INT] NOT NULL," +
            "   [DaysToFinish][INT] NULL," +
            "   [RoomId][INT] NULL," +
            "   [Comment][NVARCHAR](200) NULL," +
            "   [Description][NVARCHAR](200) NULL," +
            "   [CreatedOnUtc][DATETIME] NOT NULL," +
            "   [UpdatedOnUtc][DATETIME] NOT NULL," +
            " CONSTRAINT[PK_MaintenanceService] PRIMARY KEY CLUSTERED" +
            " (" +
            "   [Id] ASC" +
            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
            " ) ON[PRIMARY]" +
            " GO";
        }
    }
    [RoutePrefix("api/maintenanceservice")]
    public class MaintenanceServiceController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();
                _MaintenanceService sqlQueryHelp = new _MaintenanceService();
                string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder()) + " FROM MaintenanceService";

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_MaintenanceService> self = new List<_MaintenanceService>();
                            while (reader.Read())
                            {
                                _MaintenanceService _instance = new _MaintenanceService();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenanceService)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MaintenanceService sqlQueryHelp = new _MaintenanceService();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM MaintenanceService WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _MaintenanceService _instance = new _MaintenanceService();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenanceService)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MaintenanceService sqlQueryHelp = new _MaintenanceService();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM MaintenanceService WHERE " + whereString;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_MaintenanceService> self = new List<_MaintenanceService>();

                            while (reader.Read())
                            {
                                _MaintenanceService _instance = new _MaintenanceService();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenanceService)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]_MaintenanceService item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _MaintenanceService _instance = new _MaintenanceService();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, _instance.Fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO MaintenanceService " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorStatus = false;
                    result.errorMessage = "Success";
                    _instance.Id = dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM MaintenanceService");
                    result.data = Get(_instance.Id).data;
                }
                else
                {
                    result.errorStatus = true;
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = true;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]_MaintenanceService item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _MaintenanceService _instance = new _MaintenanceService();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, _instance.Fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = "UPDATE MaintenanceService SET " + cloumnsWithValues + " WHERE Id=" + item.Id;
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorStatus = false;
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorStatus = true;
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = true;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            _MaintenanceService sqlQueryHelp = new _MaintenanceService();

            string SQLQuery = "DELETE FROM MaintenanceService WHERE Id=" + id;

            try
            {
                sqlList.Add(SQLQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorStatus = false;
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorStatus = true;
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }

    public class _MaintenanceServiceImages
    {
        public int Id { get; set; }
        public int MaintenanceServiceId { get; set; }
        public string Image { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public string[] Fields = {
     "int:MaintenanceServiceId",
     "varbinarymax:Image"
     };
        public string[] ReaderFields = {
     "int:Id",
     "int:MaintenanceServiceId",
     "varbinarymax:Image",
     };
        public string[] LookupReaderFields = {
     };
        public string TableColumnsBuilder()
        {
            return "Id,MaintenanceServiceId,Image";
        }
        public string SqlScript()
        {
            return "USE [handymanworkapp]" +
            " GO" +
            " SET ANSI_NULLS ON" +
            " GO" +
            " SET QUOTED_IDENTIFIER ON" +
            " GO" +
            " CREATE TABLE[dbo].[MaintenanceService](" +
           "  [Id][INT] IDENTITY(1, 1) NOT NULL," +
           " [MaintenanceServiceId] [INT] NOT NULL," +
           " [Image] [VARBINARY] (MAX) NOT NULL, " +
           "   [CreatedOnUtc][DATETIME] NOT NULL," +
           "   [UpdatedOnUtc][DATETIME] NOT NULL," +
            " CONSTRAINT[PK_MaintenanceServiceImages] PRIMARY KEY CLUSTERED" +
            " (" +
            "   [Id] ASC" +
            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
            " ) ON[PRIMARY]" +
            " GO";
        }
    }
    [RoutePrefix("api/maintenanceserviceimages")]
    public class MaintenanceServiceImagesController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();
                _MaintenanceServiceImages sqlQueryHelp = new _MaintenanceServiceImages();
                string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder()) + " FROM MaintenanceServiceImages";

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_MaintenanceServiceImages> self = new List<_MaintenanceServiceImages>();
                            while (reader.Read())
                            {
                                _MaintenanceServiceImages _instance = new _MaintenanceServiceImages();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenanceServiceImages)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MaintenanceServiceImages sqlQueryHelp = new _MaintenanceServiceImages();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM MaintenanceServiceImages WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _MaintenanceServiceImages _instance = new _MaintenanceServiceImages();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenanceServiceImages)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MaintenanceServiceImages sqlQueryHelp = new _MaintenanceServiceImages();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM MaintenanceServiceImages WHERE " + whereString;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_MaintenanceServiceImages> self = new List<_MaintenanceServiceImages>();

                            while (reader.Read())
                            {
                                _MaintenanceServiceImages _instance = new _MaintenanceServiceImages();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenanceServiceImages)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]_MaintenanceServiceImages item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _MaintenanceServiceImages _instance = new _MaintenanceServiceImages();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, _instance.Fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO MaintenanceServiceImages " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorStatus = false;
                    result.errorMessage = "Success";
                    _instance.Id = dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM MaintenanceServiceImages");
                    result.data = Get(_instance.Id).data;
                }
                else
                {
                    result.errorStatus = true;
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = true;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]_MaintenanceServiceImages item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _MaintenanceServiceImages _instance = new _MaintenanceServiceImages();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, _instance.Fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = "UPDATE MaintenanceServiceImages SET " + cloumnsWithValues + " WHERE Id=" + item.Id;
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorStatus = false;
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorStatus = true;
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = true;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MaintenanceServiceImages sqlQueryHelp = new _MaintenanceServiceImages();

            string SQLQuery = "DELETE FROM MaintenanceServiceImages WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _MaintenanceServiceImages _instance = new _MaintenanceServiceImages();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenanceServiceImages)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }


    public class _MaintenanceServiceStatus
    {
        public int Id { get; set; }
        public int MaintenanceServiceId { get; set; }
        public string Comment { get; set; }
        public int CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public string[] Fields = {
     "int:MaintenanceServiceId",
     "string:Comment",
            "int:CreatedById"
     };
        public string[] ReaderFields = {
     "int:Id",
     "int:MaintenanceServiceId",
     "string:Comment",
     "int:CreatedById",
     "DateTime:CreatedOnUtc",
     "DateTime:UpdatedOnUtc",
     };
        public string[] LookupReaderFields = {
            "CreatedById:CreatedByName:CreatedByName",
     };
        public string TableColumnsBuilder()
        {
            return "Id,MaintenanceServiceId,Comment,CreatedById,CreatedOnUtc,UpdatedOnUtc";
        }
        public string SqlScript()
        {
            return "USE [handymanworkapp]" +
            " GO" +
            " SET ANSI_NULLS ON" +
            " GO" +
            " SET QUOTED_IDENTIFIER ON" +
            " GO" +
            " CREATE TABLE[dbo].[MaintenanceService](" +
           "  [Id][INT] IDENTITY(1, 1) NOT NULL," +
           " [MaintenanceServiceId] [INT] NOT NULL," +
           " [Comment] [VARBINARY] (MAX) NOT NULL, " +
           " [CreatedById] [INT] NOT NULL," +
           "   [CreatedOnUtc][DATETIME] NOT NULL," +
           "   [UpdatedOnUtc][DATETIME] NOT NULL," +
            " CONSTRAINT[PK_MaintenanceServiceStatus] PRIMARY KEY CLUSTERED" +
            " (" +
            "   [Id] ASC" +
            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
            " ) ON[PRIMARY]" +
            " GO";
        }
    }
    [RoutePrefix("api/MaintenanceServiceStatus")]
    public class MaintenanceServiceStatusController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();
                _MaintenanceServiceStatus sqlQueryHelp = new _MaintenanceServiceStatus();
                string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder()) + " FROM MaintenanceServiceStatus";

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_MaintenanceServiceStatus> self = new List<_MaintenanceServiceStatus>();
                            while (reader.Read())
                            {
                                _MaintenanceServiceStatus _instance = new _MaintenanceServiceStatus();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenanceServiceStatus)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MaintenanceServiceStatus sqlQueryHelp = new _MaintenanceServiceStatus();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM MaintenanceServiceStatus WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _MaintenanceServiceStatus _instance = new _MaintenanceServiceStatus();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenanceServiceStatus)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MaintenanceServiceStatus sqlQueryHelp = new _MaintenanceServiceStatus();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM MaintenanceServiceStatus WHERE " + whereString;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_MaintenanceServiceStatus> self = new List<_MaintenanceServiceStatus>();

                            while (reader.Read())
                            {
                                _MaintenanceServiceStatus _instance = new _MaintenanceServiceStatus();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenanceServiceStatus)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]_MaintenanceServiceStatus item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _MaintenanceServiceStatus _instance = new _MaintenanceServiceStatus();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, _instance.Fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO MaintenanceServiceStatus " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorStatus = false;
                    result.errorMessage = "Success";
                    _instance.Id = dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM MaintenanceServiceStatus");
                    result.data = Get(_instance.Id).data;
                }
                else
                {
                    result.errorStatus = true;
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = true;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]_MaintenanceServiceStatus item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _MaintenanceServiceStatus _instance = new _MaintenanceServiceStatus();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, _instance.Fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = "UPDATE MaintenanceServiceStatus SET " + cloumnsWithValues + " WHERE Id=" + item.Id;
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorStatus = false;
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorStatus = true;
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = true;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MaintenanceServiceStatus sqlQueryHelp = new _MaintenanceServiceStatus();

            string SQLQuery = "DELETE FROM MaintenanceServiceStatus WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _MaintenanceServiceStatus _instance = new _MaintenanceServiceStatus();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MaintenanceServiceStatus)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }

    public class _MenuOptions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Component { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public string[] Fields = {
     "string:Name",
     "string:Component"
     };
        public string[] ReaderFields = {
     "int:Id",
     "string:Name",
     "string:Component",
     "DateTime:CreatedOnUtc",
     "DateTime:UpdatedOnUtc",
     };
        public string[] LookupReaderFields = {

     };
        public string TableColumnsBuilder()
        {
            return "Id,Name,Component,CreatedOnUtc,UpdatedOnUtc";
        }
        public string SqlScript()
        {
            return "USE [handymanworkapp]" +
            " GO" +
            " SET ANSI_NULLS ON" +
            " GO" +
            " SET QUOTED_IDENTIFIER ON" +
            " GO" +
            " CREATE TABLE [dbo].[MenuOptions](" +
            "   [Id] [INT] IDENTITY(1, 1) NOT NULL," +
            "   [Name] [NVARCHAR](50) NOT NULL," +
            "   [Component] [NVARCHAR](50) NULL," +
            "   [CreatedOnUtc] [DATETIME] NOT NULL," +
            "   [UpdatedOnUtc] [DATETIME] NOT NULL," +
            " CONSTRAINT[PK_MenuOptions] PRIMARY KEY CLUSTERED" +
            " (" +
            "   [Id] ASC" +
            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
            " ) ON[PRIMARY]" +
            " GO";
        }
    }
    [RoutePrefix("api/menuoptions")]
    public class MenuOptionsController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();
                _MenuOptions sqlQueryHelp = new _MenuOptions();
                string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder()) + " FROM MenuOptions";

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_MenuOptions> self = new List<_MenuOptions>();
                            while (reader.Read())
                            {
                                _MenuOptions _instance = new _MenuOptions();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MenuOptions)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MenuOptions sqlQueryHelp = new _MenuOptions();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM MenuOptions WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _MenuOptions _instance = new _MenuOptions();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MenuOptions)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MenuOptions sqlQueryHelp = new _MenuOptions();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM MenuOptions WHERE " + whereString;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_MenuOptions> self = new List<_MenuOptions>();

                            while (reader.Read())
                            {
                                _MenuOptions _instance = new _MenuOptions();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MenuOptions)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]_MenuOptions item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _MenuOptions _instance = new _MenuOptions();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, _instance.Fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO MenuOptions " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                    _instance.Id = dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM MenuOptions");
                    result.data = Get(_instance.Id);
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]_MenuOptions item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _MenuOptions _instance = new _MenuOptions();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, _instance.Fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = ""; // "UPDATE MenuOptions SET " + cloumnsWithValues + " WHERE Id=" + id;
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = false;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _MenuOptions sqlQueryHelp = new _MenuOptions();

            string SQLQuery = "DELETE FROM MenuOptions WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _MenuOptions _instance = new _MenuOptions();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_MenuOptions)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }

    public class _PurchaseOrder
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public int Quantity { get; set; }
        public int PurchaseOrderStatus { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public string[] Fields = {
     "int:ItemId",
     "int:VendorId",
     "bool:Active",
     "int:Quantity",
     "int:PurchaseOrderStatus"
     };
        public string[] ReaderFields = {
     "int:Id",
     "int:ItemId",
     "int:VendorId",
     "bool:Active",
     "int:Quantity",
     "int:PurchaseOrderStatus",
     "DateTime:CreatedOnUtc",
     "DateTime:UpdatedOnUtc",
     };
        public string[] LookupReaderFields = {
     "ItemId:ItemName:InventoryItemName",
     "VendorId:VendorName:VendorName",
     };
        public string TableColumnsBuilder()
        {
            return "Id,ItemId,VendorId,Active,CreatedOnUtc,UpdatedOnUtc,Quantity,PurchaseOrderStatus";
        }
        public string SqlScript()
        {
            return "USE [handymanworkapp]" +
            " GO" +
            " SET ANSI_NULLS ON" +
            " GO" +
            " SET QUOTED_IDENTIFIER ON" +
            " GO" +
            " CREATE TABLE[dbo].[PurchaseOrder](" +
            "   [Id][INT] IDENTITY(1, 1) NOT NULL," +
            "   [ItemId][INT] NOT NULL," +
            "   [VendorId][INT] NOT NULL," +
            "   [Active][BIT] NOT NULL," +
            "   [Quantity][INT] NOT NULL," +
            "   [PurchaseOrderStatus][INT] NOT NULL," +
            "   [CreatedOnUtc][DATETIME] NOT NULL," +
            "   [UpdatedOnUtc][DATETIME] NOT NULL," +
            " CONSTRAINT[PK_PurchaseOrder] PRIMARY KEY CLUSTERE" +
            " (" +
            "   [Id] ASC" +
            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
            " ) ON[PRIMARY]" +
            " GO";
        }
    }
    [RoutePrefix("api/purchaseorder")]
    public class PurchaseOrderController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();
                _PurchaseOrder sqlQueryHelp = new _PurchaseOrder();
                string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder()) + " FROM PurchaseOrder";

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_PurchaseOrder> self = new List<_PurchaseOrder>();
                            while (reader.Read())
                            {
                                _PurchaseOrder _instance = new _PurchaseOrder();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_PurchaseOrder)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _PurchaseOrder sqlQueryHelp = new _PurchaseOrder();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM PurchaseOrder WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _PurchaseOrder _instance = new _PurchaseOrder();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_PurchaseOrder)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _PurchaseOrder sqlQueryHelp = new _PurchaseOrder();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM PurchaseOrder WHERE " + whereString;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_PurchaseOrder> self = new List<_PurchaseOrder>();

                            while (reader.Read())
                            {
                                _PurchaseOrder _instance = new _PurchaseOrder();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_PurchaseOrder)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]_PurchaseOrder item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _PurchaseOrder _instance = new _PurchaseOrder();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, _instance.Fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO PurchaseOrder " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                    _instance.Id = dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM PurchaseOrder");
                    result.data = Get(_instance.Id);
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]_PurchaseOrder item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _PurchaseOrder _instance = new _PurchaseOrder();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, _instance.Fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = ""; // "UPDATE PurchaseOrder SET " + cloumnsWithValues + " WHERE Id=" + id;
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = false;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            result.errorStatus = false;
            result.errorMessage = "";

            ADOHelper dbHelp = new ADOHelper();
            _PurchaseOrder sqlQueryHelp = new _PurchaseOrder();

            string SQLQuery = "DELETE FROM PurchaseOrder WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _PurchaseOrder _instance = new _PurchaseOrder();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_PurchaseOrder)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }

    public class _PurchaseOrderStatus
    {
        public int Id { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string Name { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public string[] Fields = {
     "string:Name",
     };
        public string[] ReaderFields = {
     "int:Id",
     "string:Name",
     "DateTime:CreatedOnUtc",
     "DateTime:UpdatedOnUtc",
     };
        public string[] LookupReaderFields = {

     };
        public string TableColumnsBuilder()
        {
            return "Id,Name,CreatedOnUtc,UpdatedOnUtc";
        }
        public string SqlScript()
        {
            return "USE [handymanworkapp]" +
            " GO" +
            " SET ANSI_NULLS ON" +
            " GO" +
            " SET QUOTED_IDENTIFIER ON" +
            " GO" +
            "  CREATE TABLE[dbo].[PurchaseOrderStatus](" +
            "   [Id][INT] IDENTITY(1, 1) NOT NULL," +
            "   [Name] [NVARCHAR] (50) NOT NULL," +
            "   [CreatedOnUtc] [DATETIME] NOT NULL," +
            "   [UpdatedOnUtc] [DATETIME] NOT NULL," +
            " CONSTRAINT[PK_PurchaseOrderStatus] PRIMARY KEY CLUSTERED " +
            " (" +
            "   [Id] ASC" +
            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
            " ) ON[PRIMARY]" +
            " GO";
        }
    }
    [RoutePrefix("api/purchaseorderstatus")]
    public class PurchaseOrderStatusController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();
                _PurchaseOrderStatus sqlQueryHelp = new _PurchaseOrderStatus();
                string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder()) + " FROM PurchaseOrderStatus";

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_PurchaseOrderStatus> self = new List<_PurchaseOrderStatus>();
                            while (reader.Read())
                            {
                                _PurchaseOrderStatus _instance = new _PurchaseOrderStatus();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_PurchaseOrderStatus)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _PurchaseOrderStatus sqlQueryHelp = new _PurchaseOrderStatus();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM PurchaseOrderStatus WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _PurchaseOrderStatus _instance = new _PurchaseOrderStatus();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_PurchaseOrderStatus)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _PurchaseOrderStatus sqlQueryHelp = new _PurchaseOrderStatus();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM PurchaseOrderStatus WHERE " + whereString;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_PurchaseOrderStatus> self = new List<_PurchaseOrderStatus>();

                            while (reader.Read())
                            {
                                _PurchaseOrderStatus _instance = new _PurchaseOrderStatus();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_PurchaseOrderStatus)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]_PurchaseOrderStatus item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _PurchaseOrderStatus _instance = new _PurchaseOrderStatus();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, _instance.Fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO PurchaseOrderStatus " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                    _instance.Id = dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM PurchaseOrderStatus");
                    result.data = Get(_instance.Id);
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]_PurchaseOrderStatus item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _PurchaseOrderStatus _instance = new _PurchaseOrderStatus();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, _instance.Fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = ""; // "UPDATE PurchaseOrderStatus SET " + cloumnsWithValues + " WHERE Id=" + id;
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = false;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            result.errorStatus = false;
            result.errorMessage = "";

            ADOHelper dbHelp = new ADOHelper();
            _PurchaseOrderStatus sqlQueryHelp = new _PurchaseOrderStatus();

            string SQLQuery = "DELETE FROM PurchaseOrderStatus WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _PurchaseOrderStatus _instance = new _PurchaseOrderStatus();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_PurchaseOrderStatus)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }

    public class _Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public string[] Fields = {
     "string:Name",
     "int:LocationId"
     };
        public string[] ReaderFields = {
     "int:Id",
     "string:Name",
     "int:LocationId",
     "DateTime:CreatedOnUtc",
     "DateTime:UpdatedOnUtc",
     };
        public string[] LookupReaderFields = {
     "LocationId:LocationName:LocationName",
     };
        public string TableColumnsBuilder()
        {
            return "Id,Name,LocationId,CreatedOnUtc,UpdatedOnUtc";
        }
        public string SqlScript()
        {
            return "USE [handymanworkapp]" +
            " GO" +
            " SET ANSI_NULLS ON" +
            " GO" +
            " SET QUOTED_IDENTIFIER ON" +
            " GO" +
            " CREATE TABLE[dbo].[Room](" +
            "   [Id][INT] IDENTITY(1, 1) NOT NULL," +
            "   [Name][NVARCHAR](50) NOT NULL," +
            "   [LocationId][INT] NOT NULL," +
            "   [CreatedOnUtc][DATETIME] NOT NULL," +
            "   [UpdatedOnUtc][DATETIME] NOT NULL," +
            " CONSTRAINT[PK_Room] PRIMARY KEY CLUSTERED" +
            " (" +
            "   [Id] ASC" +
            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
            " ) ON[PRIMARY]" +
            " GO";
        }
    }
    [RoutePrefix("api/room")]
    public class RoomController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();
                _Room sqlQueryHelp = new _Room();
                string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder()) + " FROM Room";

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_Room> self = new List<_Room>();
                            while (reader.Read())
                            {
                                _Room _instance = new _Room();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Room)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            ADOHelper dbHelp = new ADOHelper();
            _Room sqlQueryHelp = new _Room();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM Room WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _Room _instance = new _Room();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Room)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            ADOHelper dbHelp = new ADOHelper();
            _Room sqlQueryHelp = new _Room();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM Room WHERE " + whereString;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_Room> self = new List<_Room>();

                            while (reader.Read())
                            {
                                _Room _instance = new _Room();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Room)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]_Room item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _Room _instance = new _Room();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, _instance.Fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO Room " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                    _instance.Id = dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM Room");
                    result.data = Get(_instance.Id);
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]_Room item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _Room _instance = new _Room();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, _instance.Fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = ""; // "UPDATE Room SET " + cloumnsWithValues + " WHERE Id=" + id;
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = false;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _Room sqlQueryHelp = new _Room();

            string SQLQuery = "DELETE FROM Room WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _Room _instance = new _Room();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Room)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }

    public class _UserRoles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public string[] Fields = {
     "string:Name",
     };
        public string[] ReaderFields = {
     "int:Id",
     "string:Name",
     "DateTime:CreatedOnUtc",
     "DateTime:UpdatedOnUtc",
     };
        public string[] LookupReaderFields = {

     };
        public string TableColumnsBuilder()
        {
            return "Id,Name,CreatedOnUtc,UpdatedOnUtc";
        }
        public string SqlScript()
        {
            return "USE [handymanworkapp]" +
            " GO" +
            " SET ANSI_NULLS ON" +
            " GO" +
            " SET QUOTED_IDENTIFIER ON" +
            " GO" +
            " CREATE TABLE[dbo].[UserRoles](" +
            "   [Id][INT] IDENTITY(1, 1) NOT NULL," +
            "   [Name][NVARCHAR](50) NOT NULL," +
            "   [CreatedOnUtc][DATETIME] NOT NULL," +
            "   [UpdatedOnUtc][DATETIME] NOT NULL," +
            " CONSTRAINT[PK_UserRoles] PRIMARY KEY CLUSTERED" +
            " (" +
            "   [Id] ASC" +
            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
            " ) ON[PRIMARY]" +
            " GO";
        }
    }
    [RoutePrefix("api/userroles")]
    public class UserRolesController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();
                _UserRoles sqlQueryHelp = new _UserRoles();
                string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder()) + " FROM UserRoles";

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_UserRoles> self = new List<_UserRoles>();
                            while (reader.Read())
                            {
                                _UserRoles _instance = new _UserRoles();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_UserRoles)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _UserRoles sqlQueryHelp = new _UserRoles();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM UserRoles WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _UserRoles _instance = new _UserRoles();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_UserRoles)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _UserRoles sqlQueryHelp = new _UserRoles();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM UserRoles WHERE " + whereString;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_UserRoles> self = new List<_UserRoles>();

                            while (reader.Read())
                            {
                                _UserRoles _instance = new _UserRoles();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_UserRoles)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]_UserRoles item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _UserRoles _instance = new _UserRoles();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, _instance.Fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO UserRoles " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                    _instance.Id = dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM UserRoles");
                    result.data = Get(_instance.Id);
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]_UserRoles item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _UserRoles _instance = new _UserRoles();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, _instance.Fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = ""; // "UPDATE UserRoles SET " + cloumnsWithValues + " WHERE Id=" + id;
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = false;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _UserRoles sqlQueryHelp = new _UserRoles();

            string SQLQuery = "DELETE FROM UserRoles WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _UserRoles _instance = new _UserRoles();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_UserRoles)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }

    public class _UserRolesMenuOptionsMapping
    {
        public int Id { get; set; }
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public int MenuOptionId { get; set; }
        public string MenuOptionName { get; set; }
        public bool DeleteAccess { get; set; }
        public bool CreateAccess { get; set; }
        public bool UpdateAccess { get; set; }
        public bool ReadAccess { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public string[] Fields = {
     "int:UserRoleId",
     "int:MenuOptionId",
     "bool:DeleteAccess",
     "bool:CreateAccess",
     "bool:UpdateAccess",
     "bool:UpdateAccess",
     "bool:ReadAccess",
     };
        public string[] ReaderFields = {
     "int:Id",
     "int:UserRoleId",
     "int:MenuOptionId",
     "bool:DeleteAccess",
     "bool:CreateAccess",
     "bool:UpdateAccess",
     "bool:UpdateAccess",
     "bool:ReadAccess",
     "DateTime:CreatedOnUtc",
     "DateTime:UpdatedOnUtc",
     };
        public string[] LookupReaderFields = {
     "UserRoleId:UserRoleName:UserRoleName",
     "MenuOptionId:MenuOptionName:MenuOptionName",
     };
        public string TableColumnsBuilder()
        {
            return "Id,UserRoleId,CreatedOnUtc,UpdatedOnUtc,MenuOptionId,DeleteAccess,CreateAccess,UpdateAccess,ReadAccess";
        }
        public string SqlScript()
        {
            return "USE [handymanworkapp]" +
            " GO" +
            " SET ANSI_NULLS ON" +
            " GO" +
            " SET QUOTED_IDENTIFIER ON" +
            " GO" +
            " CREATE TABLE[dbo].[UserRolesMenuOptionsMapping](" +
            "   [Id][INT] IDENTITY(1, 1) NOT NULL," +
            "   [UserRoleId][INT] NOT NULL," +
            "   [MenuOptionId][INT] NOT NULL," +
            "   [CreateAccess][BIT] NOT NULL," +
            "   [UpdateAccess][BIT] NOT NULL," +
            "   [ReadAccess][BIT] NOT NULL," +
            "   [DeleteAccess][BIT] NOT NULL," +
            "   [CreatedOnUtc][DATETIME] NOT NULL," +
            "   [UpdatedOnUtc][DATETIME] NOT NULL," +
            " CONSTRAINT[PK_UserRolesMenuOptionsMapping] PRIMARY KEY CLUSTERE" +
            " (" +
            "   [Id] ASC" +
            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
            " ) ON[PRIMARY]" +
            " GO";
        }
    }
    [RoutePrefix("api/userrolesmenuoptionsmapping")]
    public class UserRolesMenuOptionsMappingController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();
                _UserRolesMenuOptionsMapping sqlQueryHelp = new _UserRolesMenuOptionsMapping();
                string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder()) + " FROM UserRolesMenuOptionsMapping";

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_UserRolesMenuOptionsMapping> self = new List<_UserRolesMenuOptionsMapping>();
                            while (reader.Read())
                            {
                                _UserRolesMenuOptionsMapping _instance = new _UserRolesMenuOptionsMapping();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_UserRolesMenuOptionsMapping)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _UserRolesMenuOptionsMapping sqlQueryHelp = new _UserRolesMenuOptionsMapping();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM UserRolesMenuOptionsMapping WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _UserRolesMenuOptionsMapping _instance = new _UserRolesMenuOptionsMapping();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_UserRolesMenuOptionsMapping)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _UserRolesMenuOptionsMapping sqlQueryHelp = new _UserRolesMenuOptionsMapping();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM UserRolesMenuOptionsMapping WHERE " + whereString;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_UserRolesMenuOptionsMapping> self = new List<_UserRolesMenuOptionsMapping>();

                            while (reader.Read())
                            {
                                _UserRolesMenuOptionsMapping _instance = new _UserRolesMenuOptionsMapping();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_UserRolesMenuOptionsMapping)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]_UserRolesMenuOptionsMapping item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _UserRolesMenuOptionsMapping _instance = new _UserRolesMenuOptionsMapping();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, _instance.Fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO UserRolesMenuOptionsMapping " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorStatus = false;
                    result.errorMessage = "Success";
                    _instance.Id = dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM UserRolesMenuOptionsMapping");
                    result.data = Get(_instance.Id).data;
                }
                else
                {
                    result.errorStatus = true;
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = true;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]_UserRolesMenuOptionsMapping item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _UserRolesMenuOptionsMapping _instance = new _UserRolesMenuOptionsMapping();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, _instance.Fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = "UPDATE UserRolesMenuOptionsMapping SET " + cloumnsWithValues + " WHERE Id=" + item.Id;
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorStatus = false;
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorStatus = true;
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = true;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            _UserRolesMenuOptionsMapping sqlQueryHelp = new _UserRolesMenuOptionsMapping();

            string SQLQuery = "DELETE FROM UserRolesMenuOptionsMapping WHERE Id=" + id;

            try
            {
                sqlList.Add(SQLQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorStatus = false;
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorStatus = true;
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }

    public class _Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public string[] Fields = {
       "string:Name",
       "string:Phone",
       "string:Email",
       "string:Street1",
       "string:Street2",
       "string:City",
       "string:State",
       "string:Zipcode",
       "string:Country",
       "bool:Active",
     };
        public string[] ReaderFields = {
       "int:Id",
       "string:Name",
       "string:Phone",
       "string:Email",
       "string:Street1",
       "string:Street2",
       "string:City",
       "string:State",
       "string:Zipcode",
       "string:Country",
       "bool:Active",
       "DateTime:CreatedOnUtc",
       "DateTime:UpdatedOnUtc",
     };
        public string[] LookupReaderFields = {

     };
        public string TableColumnsBuilder()
        {
            return "Id,Name,Phone,Email,Street1,Street2,City,State,Zipcode,Country,Active,CreatedOnUtc,UpdatedOnUtc";
        }
        public string SqlScript()
        {
            return "USE [handymanworkapp]" +
            " GO" +
            " SET ANSI_NULLS ON" +
            " GO" +
            " SET QUOTED_IDENTIFIER ON" +
            " GO" +
            " CREATE TABLE[dbo].[Vendor](" +
            "   [Id][INT] IDENTITY(1, 1) NOT NULL," +
            "   [Name][NVARCHAR](50) NOT NULL," +
            "   [Phone][NVARCHAR](50) NULL," +
            "   [Email][NVARCHAR](50) NULL," +
            "   [Street1][NVARCHAR](50) NULL," +
            "   [Street2][NVARCHAR](50) NULL," +
            "   [City][NVARCHAR](50) NULL," +
            "   [State][NVARCHAR](50) NULL," +
            "   [Zipcode][NVARCHAR](50) NULL," +
            "   [Country][NVARCHAR](50) NULL," +
            "   [Active][BIT] NOT NULL," +
            "   [CreatedOnUtc][DATETIME] NOT NULL," +
            "   [UpdatedOnUtc][DATETIME] NOT NULL," +
            " CONSTRAINT[PK_Vendor] PRIMARY KEY CLUSTERED" +
            " (" +
            "   [Id] ASC" +
            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
            " ) ON[PRIMARY]" +
            " GO";
        }
    }
    [RoutePrefix("api/vendor")]
    public class VendorController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();
                _Vendor sqlQueryHelp = new _Vendor();
                string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder()) + " FROM Vendor";

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_Vendor> self = new List<_Vendor>();
                            while (reader.Read())
                            {
                                _Vendor _instance = new _Vendor();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Vendor)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _Vendor sqlQueryHelp = new _Vendor();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM Vendor WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _Vendor _instance = new _Vendor();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Vendor)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _Vendor sqlQueryHelp = new _Vendor();

            string SQLQuery = "SELECT " + (sqlQueryHelp.TableColumnsBuilder())
                         + " FROM Vendor WHERE " + whereString;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<_Vendor> self = new List<_Vendor>();

                            while (reader.Read())
                            {
                                _Vendor _instance = new _Vendor();
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Vendor)obj1;
                                self.Add(_instance);
                            }
                            result.data = self;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]_Vendor item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _Vendor _instance = new _Vendor();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, _instance.Fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO Vendor " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                    _instance.Id = dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM Vendor");
                    result.data = Get(_instance.Id);
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]_Vendor item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            _Vendor _instance = new _Vendor();
            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, _instance.Fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = ""; // "UPDATE Vendor SET " + cloumnsWithValues + " WHERE Id=" + id;
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorMessage = "Success";
                }
                else
                {
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            else
            {
                result.errorStatus = false;
                result.errorMessage = ErrorMessage.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = ""
            };

            ADOHelper dbHelp = new ADOHelper();
            _Vendor sqlQueryHelp = new _Vendor();

            string SQLQuery = "DELETE FROM Vendor WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            _Vendor _instance = new _Vendor();
                            while (reader.Read())
                            {
                                object obj1 = "";
                                obj1 = dbHelp.ReaderLoop(_instance, reader, _instance.ReaderFields);
                                obj1 = dbHelp.LookupLoop(_instance, _instance.LookupReaderFields);
                                _instance = (_Vendor)obj1;
                            }
                            result.data = _instance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.errorStatus = true;
                result.errorMessage = ex.Message;
            }
            return result;
        }
    }
}
