using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace avio
{
    public partial class settings : Window
    {
        Class1 auth2;
        public settings(ref Class1 auth)
        {
            InitializeComponent();
            auth2 = auth;
            load_info();
        }
        private void load_info()
        {
           
            
            string login = auth2.log;
            string pass = auth2.pas;

            password.Text = pass;
            object temp2 = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["avio"].ConnectionString;
            con.Open();
            SqlDataReader myReader = null;
            SqlCommand cmd = new SqlCommand("SELECT name_user, surname, patronomic\r\nFROM   dbo.users\r\nWHERE(user_login = @L) AND(user_pass = @P)", con);
            cmd.Parameters.Add("@L", login);
            cmd.Parameters.Add("@P", pass);
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                name.Text = myReader[0].ToString();
                surname.Text = myReader[1].ToString();
                patronomic.Text = myReader[2].ToString();
            }
        }   
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //back to main menu
            
            main M = new main( auth2);
            M.Show();
            this.Close();

        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            string login = auth2.log;
            string pass = auth2.pas;
            SqlConnection con = new SqlConnection();
            string sql = "Update users set name_user = @N, surname = @S, patronomic = @Pa, user_pass = @Pass WHERE (user_login = @Lo) AND (user_pass = @Ps)";
            con.ConnectionString = ConfigurationManager.ConnectionStrings["avio"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@N", name.Text);
            cmd.Parameters.Add("@S", surname.Text);
            cmd.Parameters.Add("@Pa", patronomic.Text);
            cmd.Parameters.Add("@Pass", password.Text);
            cmd.Parameters.Add("@Lo", login);
            cmd.Parameters.Add("@Ps", pass);

            int a = cmd.ExecuteNonQuery();
            con.Close();
            if (a == 1)
            {
                MessageBox.Show("Изменения сохранены");

            }
            else { MessageBox.Show("Ошибка"); }
        }
        
    }
}
