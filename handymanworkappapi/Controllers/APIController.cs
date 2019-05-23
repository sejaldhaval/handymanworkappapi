using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Text;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using SendGrid;

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
                            value = value + "," + "'" + item.GetType().GetProperty(b).GetValue(item) + "'";
                            break;
                        case "time":
                            value = value + "," + "'" + item.GetType().GetProperty(b).GetValue(item) + "'";
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
                        case "time":
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
                        case "time":
                            foreach (PropertyInfo property in properties)
                            {
                                if (property.Name == s.Split(':')[1])
                                {
                                    TimeSpan? BoolValue = ((reader[property.Name]) == DBNull.Value) ? (TimeSpan?)null : (TimeSpan)(reader[property.Name]);
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
    public class Crud
    {
        public ResultResponse ListAll(object item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = "",
                data = null
            };
            try
            {
                ADOHelper dbHelp = new ADOHelper();

                Type objectType = item.GetType();
                ConstructorInfo objectConstructor = objectType.GetConstructor(Type.EmptyTypes);
                object magicClassObject = objectConstructor.Invoke(new object[] { });
                MethodInfo colmunsmethod = objectType.GetMethod("columns");
                string columns = (string)colmunsmethod.Invoke(magicClassObject, new object[] { });

                MethodInfo readerFieldsmethod = objectType.GetMethod("readerFields");
                string[] readerFields = (string[])readerFieldsmethod.Invoke(magicClassObject, new object[] { });

                MethodInfo lookupFieldsmethod = objectType.GetMethod("lookupFields");
                string[] lookupFields = (string[])lookupFieldsmethod.Invoke(magicClassObject, new object[] { });

                string SQLQuery = "SELECT " + columns + " FROM " + objectType.Name.ToString();

                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<object> self = new List<object>();
                            while (reader.Read())
                            {
                                if (objectType.Name.ToString() == "Employee")
                                {
                                    object obj = null;
                                    Employee instance = new Employee();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "EmployeeSchedule")
                                {
                                    object obj = null;
                                    EmployeeSchedule instance = new EmployeeSchedule();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "EmployeeScheduleWeek")
                                {
                                    object obj = null;
                                    EmployeeScheduleWeek instance = new EmployeeScheduleWeek();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "InventoryItem")
                                {
                                    object obj = null;
                                    InventoryItem instance = new InventoryItem();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "InventoryType")
                                {
                                    object obj = null;
                                    InventoryType instance = new InventoryType();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "Location")
                                {
                                    object obj = null;
                                    Location instance = new Location();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "MaintenanceIssueStatus")
                                {
                                    object obj = null;
                                    MaintenanceIssueStatus instance = new MaintenanceIssueStatus();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }

                                if (objectType.Name.ToString() == "MaintenancePriority")
                                {
                                    object obj = null;
                                    MaintenancePriority instance = new MaintenancePriority();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "MaintenanceService")
                                {
                                    object obj = null;
                                    MaintenanceService instance = new MaintenanceService();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "MaintenanceServiceImages")
                                {
                                    object obj = null;
                                    MaintenanceServiceImages instance = new MaintenanceServiceImages();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "MaintenanceServiceStatus")
                                {
                                    object obj = null;
                                    MaintenanceServiceStatus instance = new MaintenanceServiceStatus();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "MenuOptions")
                                {
                                    object obj = null;
                                    MenuOptions instance = new MenuOptions();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "PurchaseOrder")
                                {
                                    object obj = null;
                                    PurchaseOrder instance = new PurchaseOrder();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }

                                if (objectType.Name.ToString() == "PurchaseOrderStatus")
                                {
                                    object obj = null;
                                    PurchaseOrderStatus instance = new PurchaseOrderStatus();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }

                                if (objectType.Name.ToString() == "Room")
                                {
                                    object obj = null;
                                    Room instance = new Room();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "UserRoles")
                                {
                                    object obj = null;
                                    UserRoles instance = new UserRoles();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "UserRolesMenuOptionsMapping")
                                {
                                    object obj = null;
                                    UserRolesMenuOptionsMapping instance = new UserRolesMenuOptionsMapping();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "Vendor")
                                {
                                    object obj = null;
                                    Vendor instance = new Vendor();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
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
        public ResultResponse Get(object item, int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = "",
                data = null
            };

            ADOHelper dbHelp = new ADOHelper();

            Type objectType = item.GetType();
            ConstructorInfo objectConstructor = objectType.GetConstructor(Type.EmptyTypes);
            object magicClassObject = objectConstructor.Invoke(new object[] { });
            MethodInfo colmunsmethod = objectType.GetMethod("columns");
            string columns = (string)colmunsmethod.Invoke(magicClassObject, new object[] { });

            MethodInfo readerFieldsmethod = objectType.GetMethod("readerFields");
            string[] readerFields = (string[])readerFieldsmethod.Invoke(magicClassObject, new object[] { });

            MethodInfo lookupFieldsmethod = objectType.GetMethod("lookupFields");
            string[] lookupFields = (string[])lookupFieldsmethod.Invoke(magicClassObject, new object[] { });

            string SQLQuery = "SELECT " + columns + " FROM " + objectType.Name.ToString() + " WHERE Id=" + id;

            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            object obj = null;
                            while (reader.Read())
                            {
                                if (objectType.Name.ToString() == "Employee")
                                {
                                    Employee instance = new Employee();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }
                                if (objectType.Name.ToString() == "EmployeeSchedule")
                                {
                                    EmployeeSchedule instance = new EmployeeSchedule();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }
                                if (objectType.Name.ToString() == "EmployeeScheduleWeek")
                                {
                                    EmployeeScheduleWeek instance = new EmployeeScheduleWeek();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }
                                if (objectType.Name.ToString() == "InventoryItem")
                                {
                                    InventoryItem instance = new InventoryItem();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }
                                if (objectType.Name.ToString() == "InventoryType")
                                {
                                    InventoryType instance = new InventoryType();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }
                                if (objectType.Name.ToString() == "Location")
                                {
                                    Location instance = new Location();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }
                                if (objectType.Name.ToString() == "MaintenanceIssueStatus")
                                {
                                    MaintenanceIssueStatus instance = new MaintenanceIssueStatus();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }

                                if (objectType.Name.ToString() == "MaintenancePriority")
                                {
                                    MaintenancePriority instance = new MaintenancePriority();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }
                                if (objectType.Name.ToString() == "MaintenanceService")
                                {
                                    MaintenanceService instance = new MaintenanceService();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }
                                if (objectType.Name.ToString() == "MaintenanceServiceImages")
                                {
                                    MaintenanceServiceImages instance = new MaintenanceServiceImages();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }
                                if (objectType.Name.ToString() == "MaintenanceServiceStatus")
                                {
                                    MaintenanceServiceStatus instance = new MaintenanceServiceStatus();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }
                                if (objectType.Name.ToString() == "MenuOptions")
                                {
                                    MenuOptions instance = new MenuOptions();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }
                                if (objectType.Name.ToString() == "PurchaseOrder")
                                {
                                    PurchaseOrder instance = new PurchaseOrder();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }

                                if (objectType.Name.ToString() == "PurchaseOrderStatus")
                                {
                                    PurchaseOrderStatus instance = new PurchaseOrderStatus();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }

                                if (objectType.Name.ToString() == "Room")
                                {
                                    Room instance = new Room();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }
                                if (objectType.Name.ToString() == "UserRoles")
                                {
                                    UserRoles instance = new UserRoles();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }
                                if (objectType.Name.ToString() == "UserRolesMenuOptionsMapping")
                                {
                                    UserRolesMenuOptionsMapping instance = new UserRolesMenuOptionsMapping();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }
                                if (objectType.Name.ToString() == "Vendor")
                                {
                                    Vendor instance = new Vendor();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                }
                            }
                            result.data = obj;
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
        public ResultResponse ListFiltered(object item, string whereString)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = "",
                data = null
            };
            ADOHelper dbHelp = new ADOHelper();

            Type objectType = item.GetType();
            ConstructorInfo objectConstructor = objectType.GetConstructor(Type.EmptyTypes);
            object magicClassObject = objectConstructor.Invoke(new object[] { });
            MethodInfo colmunsmethod = objectType.GetMethod("columns");
            string columns = (string)colmunsmethod.Invoke(magicClassObject, new object[] { });

            MethodInfo readerFieldsmethod = objectType.GetMethod("readerFields");
            string[] readerFields = (string[])readerFieldsmethod.Invoke(magicClassObject, new object[] { });

            MethodInfo lookupFieldsmethod = objectType.GetMethod("lookupFields");
            string[] lookupFields = (string[])lookupFieldsmethod.Invoke(magicClassObject, new object[] { });

            string SQLQuery = "SELECT " + columns + " FROM " + objectType.Name.ToString() + " WHERE " + whereString;


            try
            {
                using (DbConnection conn = dbHelp.getDataConnection())
                {
                    using (DbCommand cmd = dbHelp.getDataCommand(conn, SQLQuery))
                    {
                        using (DbDataReader reader = dbHelp.getDataReader(cmd))
                        {
                            List<object> self = new List<object>();
                            while (reader.Read())
                            {
                                if (objectType.Name.ToString() == "Employee")
                                {
                                    object obj = null;
                                    Employee instance = new Employee();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "EmployeeSchedule")
                                {
                                    object obj = null;
                                    EmployeeSchedule instance = new EmployeeSchedule();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "EmployeeScheduleWeek")
                                {
                                    object obj = null;
                                    EmployeeScheduleWeek instance = new EmployeeScheduleWeek();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "InventoryItem")
                                {
                                    object obj = null;
                                    InventoryItem instance = new InventoryItem();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "InventoryType")
                                {
                                    object obj = null;
                                    InventoryType instance = new InventoryType();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "Location")
                                {
                                    object obj = null;
                                    Location instance = new Location();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "MaintenanceIssueStatus")
                                {
                                    object obj = null;
                                    MaintenanceIssueStatus instance = new MaintenanceIssueStatus();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }

                                if (objectType.Name.ToString() == "MaintenancePriority")
                                {
                                    object obj = null;
                                    MaintenancePriority instance = new MaintenancePriority();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "MaintenanceService")
                                {
                                    object obj = null;
                                    MaintenanceService instance = new MaintenanceService();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "MaintenanceServiceImages")
                                {
                                    object obj = null;
                                    MaintenanceServiceImages instance = new MaintenanceServiceImages();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "MaintenanceServiceStatus")
                                {
                                    object obj = null;
                                    MaintenanceServiceStatus instance = new MaintenanceServiceStatus();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "MenuOptions")
                                {
                                    object obj = null;
                                    MenuOptions instance = new MenuOptions();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "PurchaseOrder")
                                {
                                    object obj = null;
                                    PurchaseOrder instance = new PurchaseOrder();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }

                                if (objectType.Name.ToString() == "PurchaseOrderStatus")
                                {
                                    object obj = null;
                                    PurchaseOrderStatus instance = new PurchaseOrderStatus();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }

                                if (objectType.Name.ToString() == "Room")
                                {
                                    object obj = null;
                                    Room instance = new Room();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "UserRoles")
                                {
                                    object obj = null;
                                    UserRoles instance = new UserRoles();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "UserRolesMenuOptionsMapping")
                                {
                                    object obj = null;
                                    UserRolesMenuOptionsMapping instance = new UserRolesMenuOptionsMapping();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
                                if (objectType.Name.ToString() == "Vendor")
                                {
                                    object obj = null;
                                    Vendor instance = new Vendor();
                                    obj = dbHelp.ReaderLoop(instance, reader, readerFields);
                                    obj = dbHelp.LookupLoop(instance, lookupFields);
                                    self.Add(obj);
                                }
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
        public ResultResponse Create(object item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = "",
                data = null
            };

            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string columns = "";
            string values = "";

            Type objectType = item.GetType();
            ConstructorInfo objectConstructor = objectType.GetConstructor(Type.EmptyTypes);
            object magicClassObject = objectConstructor.Invoke(new object[] { });

            MethodInfo fieldsmethod = objectType.GetMethod("fields");
            string[] fields = (string[])fieldsmethod.Invoke(magicClassObject, new object[] { });

            bool statusOfCreate = dbHelp.InsertStatement(ref columns, ref values, fields, item, ref ErrorMessage);
            if (statusOfCreate)
            {
                string insertQuery = "INSERT INTO " + objectType.Name.ToString() + " " + columns + " VALUES " + values;
                sqlList.Add(insertQuery);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorStatus = false;
                    result.errorMessage = "Success";
                    result.data = Get(item, dbHelp.callExecuteQueryIdent("SELECT MAX(Id) as identCount FROM " + objectType.Name.ToString())).data;
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
        public ResultResponse Update([FromBody]object item)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = "",
                data = null
            };

            ADOHelper dbHelp = new ADOHelper();
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            string cloumnsWithValues = "";

            Type objectType = item.GetType();
            ConstructorInfo objectConstructor = objectType.GetConstructor(Type.EmptyTypes);
            object magicClassObject = objectConstructor.Invoke(new object[] { });

            MethodInfo fieldsmethod = objectType.GetMethod("fields");
            string[] fields = (string[])fieldsmethod.Invoke(magicClassObject, new object[] { });

            bool statusOfUpdate = dbHelp.UpdateStatement(ref cloumnsWithValues, fields, item, ref ErrorMessage);
            if (statusOfUpdate)
            {
                string insertQuery = "UPDATE " + objectType.Name.ToString() + " SET " + cloumnsWithValues + " WHERE Id=" + item.GetType().GetProperty("Id").GetValue(item);
                sqlList.Add(insertQuery);

                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);

                if (result.errorStatus)
                {
                    result.errorStatus = false;
                    result.errorMessage = "Success";
                    result.data = true;
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
        public ResultResponse Delete(object item, int id)
        {
            ResultResponse result = new ResultResponse
            {
                errorStatus = false,
                errorMessage = "",
                data = null
            };
            result.errorStatus = false;
            result.errorMessage = "";

            ADOHelper dbHelp = new ADOHelper();
            string sqlStr = "";
            List<string> sqlList = new List<string>();
            Exception ErrorMessage = null;
            try
            {
                sqlStr = "DELETE FROM " + item.GetType().Name.ToString() + " WHERE Id = " + id;
                sqlList.Add(sqlStr);
                result.errorStatus = dbHelp.callInsertUpdate(sqlList, ref ErrorMessage);
                if (result.errorStatus)
                {
                    result.errorStatus = false;
                    result.errorMessage = "Success";
                    result.data = true;
                }
                else
                {
                    result.errorStatus = true;
                    result.errorMessage = ErrorMessage.Message;
                }
            }
            catch (Exception Ex)
            {
                result.errorStatus = true;
                result.errorMessage = Ex.Message;
            }
            return result;
        }
    }

    public class Employee
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
        public static string columns()
        {
            return "Id,FirstName,LastName,NickName,Mobile,Email,Password,Active,RoleId,LocationId,DefaultMenuId,CreatedOnUtc,UpdatedOnUtc";
        }
        public static string[] fields()
        {
            string[] f = { "string:FirstName", "string:LastName", "string:NickName", "string:Mobile", "string:Email", "string:Password", "bool:Active", "int:RoleId", "int:LocationId", "int:DefaultMenuId" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "string:FirstName", "string:LastName", "string:NickName", "string:Mobile", "string:Email", "string:Password", "bool:Active", "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc", "int:RoleId", "int:LocationId", "int:DefaultMenuId" };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { "RoleId:RoleName:UserRoleName", "LocationId:LocationName:LocationName", "DefaultMenuId:DefaultMenuName:MenuOptionName", "DefaultMenuId:DefaultMenuComponent:MenuOptionComponent" };
            return f;
        }
    }
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new Employee());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new Employee(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new Employee(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]Employee item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]Employee item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new Employee(), id);
        }
    }

    public class InventoryItem
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
        public static string[] fields()
        {
            string[] f = { "string:Name", "string:Description", "int:InventoryTypeId", "int:Price", "bool:Active" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "string:Name", "string:Description", "int:InventoryTypeId", "int:Price", "bool:Active", "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc" };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { "InventoryTypeId:InventoryTypeName:InventoryTypeName", };
            return f;
        }
        public static string columns()
        {
            return "Id,Name,Description,InventoryTypeId,Price,Active,CreatedOnUtc,UpdatedOnUtc";
        }
    }
    [RoutePrefix("api/inventoryitem")]
    public class InventoryItemController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new InventoryItem());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new InventoryItem(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new InventoryItem(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]InventoryItem item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]InventoryItem item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new InventoryItem(), id);
        }
    }

    public class InventoryType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public static string[] fields()
        {
            string[] f = { "string:Name", "bool:Active" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "string:Name", "bool:Active", "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc", };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { };
            return f;
        }
        public static string columns()
        {
            return "Id,Name,Active,CreatedOnUtc,UpdatedOnUtc";
        }
    }
    [RoutePrefix("api/inventorytype")]
    public class InventoryTypeController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new InventoryType());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new InventoryType(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new InventoryType(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]InventoryType item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]InventoryType item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new InventoryType(), id);
        }
    }

    public class Location
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
        public static string[] fields()
        {
            string[] f = { "string:Name", "string:Street1", "string:Street2", "string:City", "string:State", "string:Country", "string:Zipcode", "string:Phone", "string:Email" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "string:Name", "string:Street1", "string:Street2", "string:City", "string:State", "string:Country", "string:Zipcode", "string:Phone", "string:Email", "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc" };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { };
            return f;
        }
        public static string columns()
        {
            return "Id,Name,Street1,Street2,City,State,Country,Zipcode,Phone,Email,CreatedOnUtc,UpdatedOnUtc";
        }
    }
    [RoutePrefix("api/location")]
    public class LocationController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new Location());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new Location(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new Location(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]Location item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]Location item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new Location(), id);
        }
    }

    public class MaintenanceIssueStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public static string[] fields()
        {
            string[] f = { "string:Name" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "string:Name", "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc", };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { };
            return f;
        }
        public static string columns()
        {
            return "Id,Name,CreatedOnUtc,UpdatedOnUtc";
        }
    }
    [RoutePrefix("api/maintenanceissuestatus")]
    public class MaintenanceIssueStatusController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new MaintenanceIssueStatus());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new MaintenanceIssueStatus(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new MaintenanceIssueStatus(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]MaintenanceIssueStatus item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]MaintenanceIssueStatus item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new MaintenanceIssueStatus(), id);
        }
    }

    public class MaintenancePriority
    {
        public int Id { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string Name { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public static string[] fields()
        {
            string[] f = { "string:Name" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "string:Name", "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc", };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { };
            return f;
        }
        public static string columns()
        {
            return "Id,CreatedOnUtc,UpdatedOnUtc,Name";
        }
    }
    [RoutePrefix("api/maintenancepriority")]
    public class MaintenancePriorityController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new MaintenancePriority());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new MaintenancePriority(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new MaintenancePriority(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]MaintenancePriority item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]MaintenancePriority item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new MaintenancePriority(), id);
        }
    }

    public class MaintenanceService
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
        public static string[] fields()
        {
            string[] f = { "int:LocationId", "int:AssignedEmployeeId", "bool:Deleted", "int:MaintenanceIssueStatusId", "int:MaintenancePriorityId", "int:DaysToFinish", "int:RoomId", "string:Comment", "string:Description" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "int:LocationId", "int:AssignedEmployeeId", "bool:Deleted", "int:MaintenanceIssueStatusId", "int:MaintenancePriorityId", "int:DaysToFinish", "string:Comment", "string:Description", "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc", "int:RoomId", };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { "LocationId:LocationName:LocationName", "AssignedEmployeeId:AssignedEmployeeName:EmployeeName", "MaintenanceIssueStatusId:MaintenanceIssueStatusName:MaintenanceIssueStatusName", "MaintenancePriorityId:MaintenancePriorityName:MaintenancePriorityName", "RoomId:RoomName:RoomName", };
            return f;
        }
        public static string columns()
        {
            return "Id,LocationId,AssignedEmployeeId,Deleted,MaintenanceIssueStatusId,MaintenancePriorityId,DaysToFinish,RoomId,Comment,Description,CreatedOnUtc,UpdatedOnUtc";
        }
    }
    [RoutePrefix("api/maintenanceservice")]
    public class MaintenanceServiceController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new MaintenanceService());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new MaintenanceService(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new MaintenanceService(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]MaintenanceService item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]MaintenanceService item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new MaintenanceService(), id);
        }
    }

    public class MaintenanceServiceImages
    {
        public int Id { get; set; }
        public int MaintenanceServiceId { get; set; }
        public string Image { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public static string[] fields()
        {
            string[] f = { "int:MaintenanceServiceId", "varbinarymax:Image" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "int:MaintenanceServiceId", "varbinarymax:Image", };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { };
            return f;
        }
        public static string columns()
        {
            return "Id,MaintenanceServiceId,Image";
        }
    }
    [RoutePrefix("api/maintenanceserviceimages")]
    public class MaintenanceServiceImagesController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new MaintenanceServiceImages());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new MaintenanceServiceImages(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new MaintenanceServiceImages(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]MaintenanceServiceImages item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]MaintenanceServiceImages item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new MaintenanceServiceImages(), id);
        }
    }

    public class MaintenanceServiceStatus
    {
        public int Id { get; set; }
        public int MaintenanceServiceId { get; set; }
        public string Comment { get; set; }
        public int CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public static string[] fields()
        {
            string[] f = { "int:MaintenanceServiceId", "string:Comment", "int:CreatedById" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "int:MaintenanceServiceId", "string:Comment", "int:CreatedById", "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc", };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { "CreatedById:CreatedByName:CreatedByName", };
            return f;
        }
        public static string columns()
        {
            return "Id,MaintenanceServiceId,Comment,CreatedById,CreatedOnUtc,UpdatedOnUtc";
        }
    }
    [RoutePrefix("api/MaintenanceServiceStatus")]
    public class MaintenanceServiceStatusController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new MaintenanceServiceStatus());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new MaintenanceServiceStatus(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new MaintenanceServiceStatus(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]MaintenanceServiceStatus item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]MaintenanceServiceStatus item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new MaintenanceServiceStatus(), id);
        }
    }

    public class MenuOptions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Component { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public static string[] fields()
        {
            string[] f = { "string:Name", "string:Component" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "string:Name", "string:Component", "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc", };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { };
            return f;
        }
        public static string columns()
        {
            return "Id,Name,Component,CreatedOnUtc,UpdatedOnUtc";
        }
    }
    [RoutePrefix("api/menuoptions")]
    public class MenuOptionsController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new MenuOptions());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new MenuOptions(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new MenuOptions(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]MenuOptions item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]MenuOptions item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new MenuOptions(), id);
        }
    }

    public class PurchaseOrder
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
        public static string[] fields()
        {
            string[] f = { "int:ItemId", "int:VendorId", "bool:Active", "int:Quantity", "int:PurchaseOrderStatus" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "int:ItemId", "int:VendorId", "bool:Active", "int:Quantity", "int:PurchaseOrderStatus", "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc", };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { "ItemId:ItemName:InventoryItemName", "VendorId:VendorName:VendorName", };
            return f;
        }
        public static string columns()
        {
            return "Id,ItemId,VendorId,Active,CreatedOnUtc,UpdatedOnUtc,Quantity,PurchaseOrderStatus";
        }
    }
    [RoutePrefix("api/purchaseorder")]
    public class PurchaseOrderController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new PurchaseOrder());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new PurchaseOrder(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new PurchaseOrder(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]PurchaseOrder item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]PurchaseOrder item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new PurchaseOrder(), id);
        }
    }

    public class PurchaseOrderStatus
    {
        public int Id { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string Name { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public static string[] fields()
        {
            string[] f = { "string:Name" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "string:Name", "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc", };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { };
            return f;
        }
        public static string columns()
        {
            return "Id,Name,CreatedOnUtc,UpdatedOnUtc";
        }
    }
    [RoutePrefix("api/purchaseorderstatus")]
    public class PurchaseOrderStatusController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new PurchaseOrderStatus());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new PurchaseOrderStatus(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new PurchaseOrderStatus(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]PurchaseOrderStatus item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]PurchaseOrderStatus item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new PurchaseOrderStatus(), id);
        }
    }

    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public static string[] fields()
        {
            string[] f = { "string:Name", "int:LocationId" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "string:Name", "int:LocationId", "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc", };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { "LocationId:LocationName:LocationName", };
            return f;
        }
        public static string columns()
        {
            return "Id,Name,LocationId,CreatedOnUtc,UpdatedOnUtc";
        }
    }
    [RoutePrefix("api/room")]
    public class RoomController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new Room());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new Room(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new Room(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]Room item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]Room item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new Room(), id);
        }
    }

    public class UserRoles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public static string[] fields()
        {
            string[] f = { "string:Name" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "string:Name", "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc", };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { };
            return f;
        }
        public static string columns()
        {
            return "Id,Name,CreatedOnUtc,UpdatedOnUtc";
        }
    }
    [RoutePrefix("api/userroles")]
    public class UserRolesController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new UserRoles());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new UserRoles(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new UserRoles(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]UserRoles item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]UserRoles item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new UserRoles(), id);
        }
    }

    public class UserRolesMenuOptionsMapping
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
        public static string[] fields()
        {
            string[] f = { "int:UserRoleId", "int:MenuOptionId", "bool:DeleteAccess", "bool:CreateAccess", "bool:UpdateAccess", "bool:UpdateAccess", "bool:ReadAccess" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "int:UserRoleId", "int:MenuOptionId", "bool:DeleteAccess", "bool:CreateAccess", "bool:UpdateAccess", "bool:UpdateAccess", "bool:ReadAccess", "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc", };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { "UserRoleId:UserRoleName:UserRoleName", "MenuOptionId:MenuOptionName:MenuOptionName", };
            return f;
        }
        public static string columns()
        {
            return "Id,UserRoleId,CreatedOnUtc,UpdatedOnUtc,MenuOptionId,DeleteAccess,CreateAccess,UpdateAccess,ReadAccess";
        }
    }
    [RoutePrefix("api/userrolesmenuoptionsmapping")]
    public class UserRolesMenuOptionsMappingController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new UserRolesMenuOptionsMapping());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new UserRolesMenuOptionsMapping(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new UserRolesMenuOptionsMapping(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]UserRolesMenuOptionsMapping item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]UserRolesMenuOptionsMapping item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new UserRolesMenuOptionsMapping(), id);
        }
    }

    public class Vendor
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
        public static string[] fields()
        {
            string[] f = { "string:Name", "string:Phone", "string:Email", "string:Street1", "string:Street2", "string:City", "string:State", "string:Zipcode", "string:Country", "bool:Active" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "string:Name", "string:Phone", "string:Email", "string:Street1", "string:Street2", "string:City", "string:State", "string:Zipcode", "string:Country", "bool:Active", "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc", };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { };
            return f;
        }
        public static string columns()
        {
            return "Id,Name,Phone,Email,Street1,Street2,City,State,Zipcode,Country,Active,CreatedOnUtc,UpdatedOnUtc";
        }
    }

    [RoutePrefix("api/vendor")]
    public class VendorController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new Vendor());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new Vendor(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new Vendor(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]Vendor item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]Vendor item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new Vendor(), id);
        }
    }

    public class EmployeeSchedule
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public static string[] fields()
        {
            string[] f = { "int:EmployeeId", "DateTime:StartDate", "DateTime:EndDate" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "int:EmployeeId", "DateTime:StartDate", "DateTime:EndDate", "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc", };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { "EmployeeId:EmployeeName:EmployeeName" };
            return f;
        }
        public static string columns()
        {
            return "Id,EmployeeId,StartDate,EndDate,CreatedOnUtc,UpdatedOnUtc";
        }
    }
    [RoutePrefix("api/employeeschedule")]
    public class EmployeeScheduleController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new EmployeeSchedule());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new EmployeeSchedule(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new EmployeeSchedule(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]EmployeeSchedule item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]EmployeeSchedule item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new EmployeeSchedule(), id);
        }
    }

    public class EmployeeScheduleWeek
    {
        public int Id { get; set; }
        public int EmployeeScheduleId { get; set; }
        public TimeSpan? MondayIn1 { get; set; }
        public TimeSpan? MondayOut1 { get; set; }
        public TimeSpan? MondayIn2 { get; set; }
        public TimeSpan? MondayOut2 { get; set; }
        public TimeSpan? TuesdayIn1 { get; set; }
        public TimeSpan? TuesdayOut1 { get; set; }
        public TimeSpan? TuesdayIn2 { get; set; }
        public TimeSpan? TuesdayOut2 { get; set; }
        public TimeSpan? WednesdayIn1 { get; set; }
        public TimeSpan? WednesdayOut1 { get; set; }
        public TimeSpan? WednesdayIn2 { get; set; }
        public TimeSpan? WednesdayOut2 { get; set; }
        public TimeSpan? ThursdayIn1 { get; set; }
        public TimeSpan? ThursdayOut1 { get; set; }
        public TimeSpan? ThursdayIn2 { get; set; }
        public TimeSpan? ThursdayOut2 { get; set; }
        public TimeSpan? FridayIn1 { get; set; }
        public TimeSpan? FridayOut1 { get; set; }
        public TimeSpan? FridayIn2 { get; set; }
        public TimeSpan? FridayOut2 { get; set; }
        public TimeSpan? SaturdayIn1 { get; set; }
        public TimeSpan? SaturdayOut1 { get; set; }
        public TimeSpan? SaturdayIn2 { get; set; }
        public TimeSpan? SaturdayOut2 { get; set; }
        public TimeSpan? SundayIn1 { get; set; }
        public TimeSpan? SundayOut1 { get; set; }
        public TimeSpan? SundayIn2 { get; set; }
        public TimeSpan? SundayOut2 { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public string errorMessage { get; set; }
        public bool errorStatus { get; set; }
        public static string[] fields()
        {
            string[] f = { "int:EmployeeScheduleId",
                "time:MondayIn1", "time:MondayOut1", "time:MondayIn2", "time:MondayOut2",
                "time:TuesdayIn1", "time:TuesdayOut1", "time:TuesdayIn2", "time:TuesdayOut2",
                "time:WednesdayIn1", "time:WednesdayOut1", "time:WednesdayIn2", "time:WednesdayOut2",
                "time:ThursdayIn1", "time:ThursdayOut1", "time:ThursdayIn2", "time:ThursdayOut2",
                "time:FridayIn1", "time:FridayOut1", "time:FridayIn2", "time:FridayOut2",
                "time:SaturdayIn1", "time:SaturdayOut1", "time:SaturdayIn2", "time:SaturdayOut2",
                "time:SundayIn1", "time:SundayOut1", "time:SundayIn2", "time:SundayOut2" };
            return f;
        }
        public static string[] readerFields()
        {
            string[] f = { "int:Id", "int:EmployeeScheduleId",
                "time:MondayIn1", "time:MondayOut1", "time:MondayIn2", "time:MondayOut2",
                "time:TuesdayIn1", "time:TuesdayOut1", "time:TuesdayIn2", "time:TuesdayOut2",
                "time:WednesdayIn1", "time:WednesdayOut1", "time:WednesdayIn2", "time:WednesdayOut2",
                "time:ThursdayIn1", "time:ThursdayOut1", "time:ThursdayIn2", "time:ThursdayOut2",
                "time:FridayIn1", "time:FridayOut1", "time:FridayIn2", "time:FridayOut2",
                "time:SaturdayIn1", "time:SaturdayOut1", "time:SaturdayIn2", "time:SaturdayOut2",
                "time:SundayIn1", "time:SundayOut1", "time:SundayIn2", "time:SundayOut2",
                "DateTime:CreatedOnUtc", "DateTime:UpdatedOnUtc", };
            return f;
        }
        public static string[] lookupFields()
        {
            string[] f = { };
            return f;
        }
        public static string columns()
        {
            return "Id,EmployeeScheduleId,MondayIn1,MondayOut1,MondayIn2,MondayOut2,TuesdayIn1,TuesdayOut1,TuesdayIn2,TuesdayOut2,WednesdayIn1,WednesdayOut1,WednesdayIn2,WednesdayOut2,ThursdayIn1,ThursdayOut1,ThursdayIn2,ThursdayOut2,FridayIn1,FridayOut1,FridayIn2,FridayOut2,SaturdayIn1,SaturdayOut1,SaturdayIn2,SaturdayOut2,SundayIn1,SundayOut1,SundayIn2,SundayOut2,CreatedOnUtc,UpdatedOnUtc";
        }
    }
    [RoutePrefix("api/employeescheduleweek")]
    public class EmployeeScheduleWeekController : ApiController
    {
        [HttpGet]
        [Route("listAll")]
        public ResultResponse ListAll()
        {
            return (new Crud()).ListAll(new EmployeeScheduleWeek());
        }
        [HttpGet]
        [Route("get/{id}")]
        public ResultResponse Get(int id)
        {
            return (new Crud()).Get(new EmployeeScheduleWeek(), id);
        }
        [HttpGet]
        [Route("listFiltered/{whereString}")]
        public ResultResponse ListFiltered(string whereString)
        {
            return (new Crud()).ListFiltered(new EmployeeScheduleWeek(), whereString);
        }
        [HttpPost]
        [Route("create")]
        public ResultResponse Create([FromBody]EmployeeScheduleWeek item)
        {
            return (new Crud()).Create(item);
        }
        [HttpPost]
        [Route("update")]
        public ResultResponse Update([FromBody]EmployeeScheduleWeek item)
        {
            return (new Crud()).Update(item);
        }
        [HttpGet]
        [Route("delete/{id}")]
        public ResultResponse Delete(int id)
        {
            return (new Crud()).Delete(new EmployeeScheduleWeek(), id);
        }
    }

    public class Email
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
    [RoutePrefix("api/sendemail")]
    public class SendEmailController : ApiController
    {
        [HttpPost]
        [Route("send")]
        [AllowAnonymous]
        public ResultResponse Send([FromBody]Email item)
        {
            ResultResponse result = new ResultResponse();
            try
            {
                //var apiKey = Environment.GetEnvironmentVariable("SendGridKey");
                //var client = new SendGridClient(apiKey);
                //var from = new EmailAddress("sejaldhaval@gmail.com", "User");
                //var subject = "Dhyanu Contact us";
                //var to = new EmailAddress(item.EmailAddress, "User");
                //var plainTextContent = item.Message;
                //var htmlContent = "<strong>" + item.Message + "</strong>";
                //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                //var response = await client.SendEmailAsync(msg);

                MailMessage message = new MailMessage();
                message.From = new MailAddress("sejaldhaval@gmail.com");
                message.To.Add(new MailAddress(item.EmailAddress));
                message.Subject = "Dhyanu Contact us";
                message.IsBodyHtml = true;
                message.Body = item.Message;
                SmtpClient client = new SmtpClient(strMailServer);
                System.Net.NetworkCredential ncCredentials = new System.Net.NetworkCredential(strMailUserName, strMailPassword);
                client.Credentials = ncCredentials;
                client.Port = int.Parse(strMailPort);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
        public static string strMailServer
        {
            get { return ConfigurationManager.AppSettings["SMTPServer"]; }
        }
        public static string strMailPassword
        {
            get { return ConfigurationManager.AppSettings["SMTPPassword"]; }
        }

        public static string strMailUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["SMTPUserName"];
            }
        }
        public static string strMailPort
        {
            get
            {
                return ConfigurationManager.AppSettings["SMTPPort"];
            }
        }
    }
}
