using System;
using System.Collections.Generic;
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
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;

namespace avio
{
    
    public partial class tickets : Window
    {
        main a;
        Class1 log;
        public tickets(Class1 log2, main a2)
        {
            InitializeComponent();
            a = a2;
            log= log2;
            LoadInfo();
        }
       
        ObservableCollection<json> jsdata;
        private void LoadInfo()
        {
            var js = JsonConvert.DeserializeObject<jsFlights>(File.ReadAllText(@"C:\Users\Andrey\source\repos\avio\data.json"));
            jsdata = new ObservableCollection<json>();
            foreach (var item in js.data)
            { 
                jsdata.Add(item);
                
            }
            grid.ItemsSource = jsdata;
                
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //update info
            LoadInfo();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            a.Show();
            this.Close();
           
        }

        private void like_Click(object sender, RoutedEventArgs e)
        {
            liked_flights like = new liked_flights(log, a);
            like.Show();
            //Hide();
            this.Close();
        }
        private void Love_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var image = (Image)button.Content;
            var imOff = (ImageSource)FindResource("ImOFF");

            if (image.Source == imOff)
            {
                image.Source = (ImageSource)FindResource("ImON");
                button.IsEnabled = false;

                button.IsEnabled = true;
                dynamic row = grid.SelectedIndex;

                string id = jsdata[row].searchToken;

                string login = log.log;
                string pass = log.pas;
                SqlConnection con = new SqlConnection();
                string sql = "insert into tickets (id_user, id_flight) values((select user_id from users where (user_login = @L) AND (user_pass = @P)), @I)";
                con.ConnectionString = ConfigurationManager.ConnectionStrings["avio"].ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.Add("@L", login);
                cmd.Parameters.Add("@P", pass);
                cmd.Parameters.Add("@I", id);

                int a = cmd.ExecuteNonQuery();
                con.Close();
                if (a == 1)
                {

                }
                else { MessageBox.Show("Ошибка"); }
            }
            else
            {
                
                SqlConnection con2 = new SqlConnection();
                string sql2 = "DELETE FROM tickets WHERE id=(SELECT MAX(id) FROM tickets)";
                con2.ConnectionString = ConfigurationManager.ConnectionStrings["avio"].ConnectionString;
                con2.Open();
                SqlCommand cmd2 = new SqlCommand(sql2, con2);

                int a2 = cmd2.ExecuteNonQuery();
                con2.Close();
                if (a2 == 1)
                {

                }
                else { MessageBox.Show("Ошибка"); }
                image.Source = imOff;
            }
            

    }
        private void buy_Click(object sender, RoutedEventArgs e)
        { 
            
        }
    }
}
