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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WriteErase
{
    /// <summary>
    /// Логика взаимодействия для LogInUser.xaml
    /// </summary>
    public partial class LogInUser : Page
    {
        public LogInUser()
        {
            InitializeComponent();
            tbnumber.Focus();
        }

        private void tbnumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                tbpassword.IsEnabled = true;
                tbpassword.Focus();
            }
        }

        private void tbpassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                User user = Base.WE.User.FirstOrDefault(z => z.UserLogin == tbnumber.Text);
                User user1 = Base.WE.User.FirstOrDefault(z => z.UserPassword == tbpassword.Password);

            }
        }
    }
}
