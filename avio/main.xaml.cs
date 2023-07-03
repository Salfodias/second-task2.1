using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
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
using System.Windows.Controls.Primitives;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace avio
{
   
    public partial class main : Window
    {
        Class1 auth;

        Fill im;
        public main(Class1 log2)
        {

            InitializeComponent();
            auth = log2;
            load_info();
            
            im = new Fill();
            im.image = new List<string>();
            im.author1 = new List<string>();
            im.disc1 = new List<string>();
            load_comment_info();
            DataContext = im;
        }
        
        
        private void load_comment_info()
        {
            
            var Fill_comments = JsonConvert.DeserializeObject<Fill_comments>(File.ReadAllText(@"C:\Users\Andrey\source\repos\avio\comments.json"));
            ObservableCollection<Comments_json> jsdata = new ObservableCollection<Comments_json>();
            foreach (var item2 in Fill_comments.data)
            {
                jsdata.Add(item2);
                
                im.image.Add(item2.image);
                im.author1.Add(item2.author);
                im.disc1.Add(item2.disc);
             
            }
         
        }

        private void load_info()
        {
            
            string login = auth.log;
            string pass = auth.pas;
           
            object temp2 = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["avio"].ConnectionString;
            con.Open();
            SqlDataReader myReader = null;
            SqlCommand cmd = new SqlCommand("SELECT name_user, surname, patronomic\r\nFROM   dbo.users\r\nWHERE(user_login = @L) AND(user_pass = @P)", con);
            cmd.Parameters.Add("@L", login);
            cmd.Parameters.Add("@P", pass);
            myReader = cmd.ExecuteReader();
            while(myReader.Read())
            {
                greeting.Content = myReader[1].ToString()+ " " + myReader[0].ToString() + " " + myReader[2].ToString();

            }
           
            SqlConnection con2 = new SqlConnection();
            string sql2 = "SELECT bio\r\nFROM   dbo.users\r\nWHERE (user_login = @L) AND (user_pass = @P)";
            con2.ConnectionString = ConfigurationManager.ConnectionStrings["avio"].ConnectionString;
            con2.Open();
            SqlCommand cmd2 = new SqlCommand(sql2, con2);

            cmd2.Parameters.Add("@L", login);
            cmd2.Parameters.Add("@P", pass);

            temp2 = cmd2.ExecuteScalar();
            bio.Text = temp2.ToString();
            con2.Close();

        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            

            settings set = new settings(ref auth);
            set.Show();
            this.Close();
        }

        private void ststus_Click(object sender, RoutedEventArgs e)
        {
            string login = auth.log;
            string pass = auth.pas;
            SqlConnection con = new SqlConnection();
            string sql = "Update users set bio = @B WHERE (user_login = @L) AND (user_pass = @P)";
            con.ConnectionString = ConfigurationManager.ConnectionStrings["avio"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
                                   
            cmd.Parameters.Add("@P", pass);
            cmd.Parameters.Add("@L", login);
            cmd.Parameters.Add("@B", bio.Text);

            int a = cmd.ExecuteNonQuery();
            con.Close();
            if (a == 1)
            {
                MessageBox.Show("Статус изменен");

            }
            else { MessageBox.Show("Ошибка"); }
           

        }

        private void tickets_Click(object sender, RoutedEventArgs e)
        {
            tickets win = new tickets(auth, this);
            win.Show();
            Hide();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            string login = auth.log;
            string pass = auth.pas;
            SqlConnection con = new SqlConnection();
            string sql = "Update users set status_login = 0 where (user_login = @L) AND (user_pass = @P)";
            con.ConnectionString = ConfigurationManager.ConnectionStrings["avio"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@L", login);
            cmd.Parameters.Add("@P", pass);

            int a = cmd.ExecuteNonQuery();
            con.Close();
            if (a == 1)
            {
                MainWindow aq = new MainWindow();
                aq.Show();
                this.Close();
            }
            else { MessageBox.Show("Ошибка"); }
            
        }

        private void more_info_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            int index = Convert.ToInt32(btn.Tag);
            more_info more =  new more_info(this, im, index);
            more.Show();
            this.Hide();
            
        }
    }
}
