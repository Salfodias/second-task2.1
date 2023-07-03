using avio.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;

namespace avio
{

    public partial class liked_flights : Window
    {
        Class1 log2;
        main m;
        public liked_flights(Class1 log, main m2)
        {
            InitializeComponent();
            log2 = log;
            m = m2;
            loadinfo();
        }
        ObservableCollection<json> jsdata;
        private void loadinfo()
        {
            var js = JsonConvert.DeserializeObject<jsFlights>(File.ReadAllText(@"C:\Users\Andrey\source\repos\avio\data.json"));
            jsdata = new ObservableCollection<json>();
            foreach (var item in js.data)
            {
                jsdata.Add(item);

            }

            //grid.ItemsSource = jsdata;




            /*var jsonStr = File.ReadAllText(System.IO.Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
    "data.json"));

            var parsed = JObject.Parse(jsonStr);

            var dIdList = parsed["data"]["dResult"]
                .Where(x => x.Value<String>("shape").Equals("round", StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Value<Int32>("dID"))
                .ToList();*/
            string login = log2.log;
            string pass = log2.pas;



            //object temp2 = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["avio"].ConnectionString;
            con.Open();
            SqlDataReader myReader = null;
            SqlCommand cmd = new SqlCommand("SELECT dbo.tickets.id_flight\r\nFROM   dbo.tickets INNER JOIN\r\n             dbo.users ON dbo.tickets.id_user = dbo.users.user_id\r\nWHERE (dbo.users.user_login = @L) AND (dbo.users.user_pass = @P)", con);
            cmd.Parameters.Add("@L", login);
            cmd.Parameters.Add("@P", pass);
            myReader = cmd.ExecuteReader();
            var tes = new List<object>();
            var tes2 = new List<string>();
            int i = 0;
            string tr = "";
            
            while( myReader.HasRows)
            {
                while (myReader.Read())
                {
                    tes2.Add(myReader.GetString(0));
                }
                myReader.NextResult();
            }
            var jsdataLike = new ObservableCollection<json>();
            foreach (var item in jsdata)
            {
                var ret = tes2.FirstOrDefault(p => p == item.searchToken);
                if (ret != null)
                {
                    jsdataLike.Add(item);
                }
            }
            /* SqlDataAdapter da = new SqlDataAdapter(cmd);
             DataTable dt = new DataTable("auau");
             da.Fill(dt);
             foreach (int item in myReader)
             {

             }*/

            //greeting.Content = myReader[1].ToString() + " " + myReader[0].ToString() + " " + myReader[2].ToString();

            datag.ItemsSource = jsdataLike;

        }
        private void back_Click(object sender, RoutedEventArgs e)
        {
            tickets ticket = new tickets(log2, m);
            ticket.Show();
            this.Close();

        }
    }
}
