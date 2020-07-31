
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
namespace APPSYNCSNK
{
    public class InitMethods
    {
        private snkdbEntities db = new snkdbEntities();
        //protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        //{
        //    return new JsonResult()
        //    {
        //        Data = data,
        //        ContentType = contentType,
        //        ContentEncoding = contentEncoding,
        //        JsonRequestBehavior = behavior,
        //        MaxJsonLength = Int32.MaxValue
        //    };
        //}

        public List<Dictionary<string, object>> GetTableRows(DataTable data)
        {
            var lstRows = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictRow = null;

            foreach (DataRow row in data.Rows)
            {
                dictRow = new Dictionary<string, object>();
                foreach (DataColumn column in data.Columns)
                {
                    dictRow.Add(column.ColumnName, row[column]);
                }
                lstRows.Add(dictRow);
            }
            return lstRows;
        }


        public DataTable ReturnDataTableNonConstraints(StringBuilder sql)
        {
            using (var cmd = db.Database.Connection.CreateCommand())
            {
                db.Database.Connection.Open();
                cmd.CommandText = sql.ToString();
                using (var reader = cmd.ExecuteReader())
                {
                    DataTable DTSchema = reader.GetSchemaTable();
                    DataTable DT = new DataTable();
                    if (DTSchema != null)
                        if (DTSchema.Rows.Count > 0)
                            for (int i = 0; i < DTSchema.Rows.Count; i++)
                            {
                                //Create new column for each row in schema table
                                //Set properties that are causing errors and add it to our datatable
                                //Rows in schema table are filled with information of columns in our actual table
                                DataColumn Col = new DataColumn(DTSchema.Rows[i]["ColumnName"].ToString(), (Type)DTSchema.Rows[i]["DataType"]);
                                Col.AllowDBNull = true;
                                Col.Unique = false;
                                Col.AutoIncrement = false;
                                DT.Columns.Add(Col);
                            }

                    while (reader.Read())
                    {
                        //Read data and fill it to our datatable
                        DataRow Row = DT.NewRow();
                        for (int i = 0; i < DT.Columns.Count; i++)
                        {
                            Row[i] = reader[i];
                        }
                        DT.Rows.Add(Row);
                    }
                    db.Database.Connection.Close();
                    return DT;
                }
            }
        }

     

        /// using for export to excel in server side
   
        public string Between(string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }

        /// Get string value after [first] symbol.
        public string Before(string value, string a)
        {
            int posA = value.IndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            return value.Substring(0, posA);
        }

        /// Get string value after [last] symbol.
        public string After(string value, string a)
        {
            int posA = value.LastIndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= value.Length)
            {
                return "";
            }
            return value.Substring(adjustedPosA);
        }

        /// auto insert 00000 
        public string CreateId(int id)
        {
            if (id.ToString().Length < 2)
            {
                return string.Format("00{0}", id);
            }

            if (id.ToString().Length < 3)
            {
                return string.Format("0{0}", id);
            }

            if (id.ToString().Length < 4)
            {
                return string.Format("{0}", id);
            }

            return string.Empty;
        }
        public string autoPono(int id)
        {
            if (id.ToString().Length < 2)
            {
                return string.Format("000000000{0}", id);
            }

            if ((id.ToString().Length < 3) || (id.ToString().Length == 2))
            {
                return string.Format("00000000{0}", id);
            }

            if ((id.ToString().Length < 4) || (id.ToString().Length == 3))
            {
                return string.Format("0000000{0}", id);
            }

            if ((id.ToString().Length < 5) || (id.ToString().Length == 4))
            {
                return string.Format("000000{0}", id);
            }
            if ((id.ToString().Length < 6) || (id.ToString().Length == 5))
            {
                return string.Format("00000{0}", id);
            }
            if ((id.ToString().Length < 7) || (id.ToString().Length == 6))
            {
                return string.Format("0000{0}", id);
            }
            if ((id.ToString().Length < 8) || (id.ToString().Length == 7))
            {
                return string.Format("000{0}", id);
            }
            if ((id.ToString().Length < 9) || (id.ToString().Length == 8))
            {
                return string.Format("00{0}", id);
            }
            if ((id.ToString().Length < 10) || (id.ToString().Length == 9))
            {
                return string.Format("0{0}", id);
            }

            if ((id.ToString().Length < 11) || (id.ToString().Length == 10))
            {
                return string.Format("{0}", id);
            }
            return string.Empty;
        }
        public string CreateMDPO(int id)
        {
            if (id.ToString().Length < 2)
            {
                db.Database.Connection.Close();

                return string.Format("000000000{0}", id);
            }

            if ((id.ToString().Length < 3) || (id.ToString().Length == 2))
            {
                db.Database.Connection.Close();

                return string.Format("00000000{0}", id);
            }

            if ((id.ToString().Length < 4) || (id.ToString().Length == 3))
            {
                db.Database.Connection.Close();

                return string.Format("0000000{0}", id);
            }

            if ((id.ToString().Length < 5) || (id.ToString().Length == 4))
            {
                db.Database.Connection.Close();

                return string.Format("000000{0}", id);
            }
            if ((id.ToString().Length < 6) || (id.ToString().Length == 5))
            {
                db.Database.Connection.Close();

                return string.Format("00000{0}", id);
            }
            if ((id.ToString().Length < 7) || (id.ToString().Length == 6))
            {
                return string.Format("00000{0}", id);
            }
            if ((id.ToString().Length < 8) || (id.ToString().Length == 7))
            {
                return string.Format("0000{0}", id);
            }
            if ((id.ToString().Length < 9) || (id.ToString().Length == 8))
            {
                return string.Format("000{0}", id);
            }
            if ((id.ToString().Length < 10) || (id.ToString().Length == 9))
            {
                return string.Format("00{0}", id);
            }

            if ((id.ToString().Length < 11) || (id.ToString().Length == 10))
            {
                return string.Format("0{0}", id);
            }
            if ((id.ToString().Length < 12) || (id.ToString().Length == 11))
            {
                return string.Format("{0}", id);
            }
            return string.Empty;
        }
        /// Conver datatable to list<> (generic type)
        public List<T> ConvertDataTableToList<T>(StringBuilder sql)
        {
            DataTable dt = ReturnDataTableNonConstraints(sql);
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        //pro.SetValue(obj, dr[column.ColumnName], null);
                        pro.SetValue(obj, dr[column.ColumnName] == DBNull.Value ? string.Empty :
      dr[column.ColumnName].ToString(), null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}