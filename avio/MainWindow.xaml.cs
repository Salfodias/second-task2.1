using avio.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;


namespace avio
{
    
    public partial class MainWindow : Window
    {
        //App stat;
        public Class1 class1;
        public MainWindow(Class1 class1)
        {
            InitializeComponent();

            log.Text = class1.log;
            pass.Text = class1.pas;
            //stat = app;
            
        }
        public MainWindow()
        {
            InitializeComponent();
            //stat = app;
            
        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //enter in main window

            Class1 log2 = new Class1();
            log2.log = log.Text;
            log2.pas = pass.Text;


            if (log.Text == "" || pass.Text == "")
            {
                MessageBox.Show("введите логин и пароль!");
            }
            else 
            {
                if (log.Text.Length > 50 || pass.Text.Length > 50)
                {
                    MessageBox.Show("В логине или пароле введено слишком много символов!");
                }
                else
                {
                    var Currentuser = AppData.db.users.FirstOrDefault(u => u.user_login == log.Text && u.user_pass == pass.Text);
                    if (Currentuser != null)
                    {
                        SqlConnection con = new SqlConnection();
                        string sql = "Update users set status_login = 1 where (user_login = @L) AND (user_pass = @P)";
                        con.ConnectionString = ConfigurationManager.ConnectionStrings["avio"].ConnectionString;
                        con.Open();
                        SqlCommand cmd = new SqlCommand(sql, con);

                        cmd.Parameters.Add("@L", log.Text);
                        cmd.Parameters.Add("@P", pass.Text);

                        int a = cmd.ExecuteNonQuery();
                        con.Close();
                        if (a == 1)
                        {
                            main Main_Window = new main( log2);
                            Main_Window.Show();
                            this.Close();
                        }
                        else { MessageBox.Show("Ошибка"); }
                       
                    }
                    else
                    {
                        MessageBox.Show("Некорректный логин или пароль!");
                    }
                }
            }
        }
        
        private void reg_Click(object sender, RoutedEventArgs e)
        {
            //new user
            reg reg = new reg();
            reg.Show();
            this.Close();
        }
    }
}
