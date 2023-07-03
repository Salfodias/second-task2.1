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

namespace avio
{

    public partial class more_info : Window
    {
        //Class1 auth;
        main a;
        Fill b;
        int c;
       
        public more_info(main a2, Fill b2, int c2)
        {
            InitializeComponent();
            a = a2;
            b = b2;
            c = c2;
            basic();
            DataContext = b;
        }

        private void basic() 
        {
            b.a = b.image[c];
            b.b = b.author1[c];
            b.c = b.disc1[c];
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            a.Show();
            
        }
    }
}
