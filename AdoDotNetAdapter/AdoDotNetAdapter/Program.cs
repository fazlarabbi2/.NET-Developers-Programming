using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdoDotNetAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConnectionStringSQLServer"].ToString();
                //string ConString = @"data source = DESKTOP-GD2F47O\SQLEXPRESS; database = studentdb; integrated security = SSPI";

                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("select Name, Email, Mobile from student", connection);

                    //Using Data Table
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    //1. Open the connection
                    //2. Execute Command
                    //3. Retrive the Result
                    //4. Fill/Store the Retrive Result in the Datatable
                    //5. Close the connection

                    //Active and Open connection is not required
                    Console.WriteLine("Using Data Table");
                    //dt.Rows: Gets the collection of rows that belong to this table
                    //DataRow: Represents a row of data in a System.Data.DataTable.
                    foreach (DataRow row in dt.Rows)
                    {
                        Console.WriteLine(row["Name"] + ",  " + row["Email"] + ",  " + row["Mobile"]);
                        Console.WriteLine(row[0] + ",  " + row[1] + ",  " + row[2]);
                    }
                    Console.WriteLine("---------------");

                    //Using DataSet
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "student1"); //Index: 0

                    adapter = new SqlDataAdapter("select Name, Mobile from student", connection);
                    adapter.Fill(ds, "student2"); //Inex: 1
                    Console.WriteLine("Using Data Set");

                    //Tables: Gets the collection of tables contained in the System.Data.DataSet.
                    foreach (DataRow row in ds.Tables["student1"].Rows)
                    {
                        Console.WriteLine(row[0] + ",  " + row[1] + ",  " + row[2]);
                    }

                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        Console.WriteLine(row["Name"] + ", " + row["Mobile"]);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("OOPs, something went wrong.\n" + e);
            }

            Console.ReadKey();
        }
    }
}