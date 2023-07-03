using avio.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace avio
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Class1 cl = new Class1();
            var Currentuser = AppData.db.users.FirstOrDefault(u => u.status_login == "1");
            if (Currentuser != null)
            {
                SqlConnection con2 = new SqlConnection();
                string sql2 = "SELECT user_login, user_pass\r\nFROM   dbo.users\r\nWHERE (status_login = 1)";
                con2.ConnectionString = ConfigurationManager.ConnectionStrings["avio"].ConnectionString;
                con2.Open();

                SqlCommand cmd2 = new SqlCommand(sql2, con2);

                SqlDataReader myReader = null;
                myReader = cmd2.ExecuteReader();
                while (myReader.Read())
                {
                    cl.log = myReader[0].ToString();
                    cl.pas = myReader[1].ToString();
                   
                }
                con2.Close();
                MainWindow main = new MainWindow(cl);
                main.Show();

            }
            else 
            {
                MainWindow main = new MainWindow(cl);
                main.Show();
            }  

            
        }
    }
}
