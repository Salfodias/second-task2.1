using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Xml.Linq;

namespace avio
{
    /// <summary>
    /// Логика взаимодействия для reg.xaml
    /// </summary>
    public partial class reg : Window
    {
        public reg()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text.Length > 50 || surname.Text.Length > 50 || patronomic.Text.Length > 50 || log.Text.Length > 50 || pass.Text.Length > 50)
            {
                MessageBox.Show("Введено слишком много символов");
            }
            else 
            {
                if (name.Text == "" || surname.Text == "" || log.Text == "" || pass.Text == "")
                {
                    MessageBox.Show("Заполните обязательные поля для регистрации!");
                }
                else
                {
                    SqlConnection con = new SqlConnection();
                    string sql = "insert into users (name_user, surname, patronomic, user_login, user_pass) values (@N, @S, @P, @L, @Pas)";
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["avio"].ConnectionString;
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sql, con);
                    
                    cmd.Parameters.Add("@N", name.Text);
                    cmd.Parameters.Add("@S", surname.Text);
                    cmd.Parameters.Add("@P", patronomic.Text);
                    cmd.Parameters.Add("@L", log.Text);
                    cmd.Parameters.Add("@Pas", pass.Text);
                    

                    int a = cmd.ExecuteNonQuery();
                    con.Close();
                    if (a == 1)
                    {
                        MessageBox.Show("пользователь добавлен");

                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                    }
                    else { MessageBox.Show("Ошибка"); }
                }
            }
        }
    }
}
