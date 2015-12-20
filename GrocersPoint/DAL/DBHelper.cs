using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DAL
{
   public class DBHelper
    {
        private SqlDataAdapter myAdapter;
        private SqlConnection conn;

        /// <constructor>
        /// Initialise Connection
        /// </constructor>
        public DBHelper()
        {
            myAdapter = new SqlDataAdapter();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings
					["dbConnectionString"].ConnectionString);
        }

        /// <method>
        /// Open Database Connection if Closed or Broken
        /// </method>
        public SqlConnection openConnection()
        {
            if (conn.State == ConnectionState.Closed || conn.State == 
						ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }
        private void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
                SqlConnection.ClearPool(conn);
                conn.Close();
            }
            
        }

        /// <method>
        /// Select Query
        /// </method>
        public DataSet executeSelectQuery(String procName, SqlParameter[] sqlParameter)
        {

            SqlCommand myCommand = new SqlCommand(procName, openConnection());

            DataSet ds = new DataSet();
            try
            {

                myCommand.Parameters.AddRange(sqlParameter);
                myCommand.CommandType = CommandType.StoredProcedure;
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(ds);
                
            }
            catch (SqlException e)
            {
               
                return null;
            }
            finally
            {
                
                myCommand.Dispose();
                CloseConnection();
            }
            return ds;
        }

        public DataSet executeSelectQuery(String procName)
        {

            SqlDataAdapter myAdapter = new SqlDataAdapter(procName, openConnection());

            DataSet ds = new DataSet();
            try
            {
                myAdapter.Fill(ds);

            }
            catch (SqlException e)
            {

                return null;
            }
            finally
            {
                myAdapter.Dispose();
                CloseConnection();
            }
            return ds;
        }
        /// <method>
        /// Insert Query
        /// </method>
        public int InsertQuery(String procName, SqlParameter[] sqlParameter)
        {
            int res=0;
            try
            {
                SqlCommand myCommand = new SqlCommand(procName, openConnection());
                myCommand.Parameters.AddRange(sqlParameter);
                myCommand.CommandType = CommandType.StoredProcedure;
                res = myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {

                return 0;
            }
            finally
            {
                CloseConnection();
            }

            return res ;
        }

        /// <method>
        /// Update Query
        /// </method>
        public int executeUpdateQuery(String procName, SqlParameter[] sqlParameter)
        {
            int res = 0;
            try
            {
                SqlCommand myCommand = new SqlCommand(procName, openConnection());
                myCommand.Parameters.AddRange(sqlParameter);
                myCommand.CommandType = CommandType.StoredProcedure;
                res = myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {

                return res;
            }
            finally
            {
                CloseConnection();
            }

            return res;
        }
    }
}
