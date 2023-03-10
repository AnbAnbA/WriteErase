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
        int che ;
        public LogInUser(int ch)
        {
            InitializeComponent();
            che = ch;
            tbnumber.Focus();
            if (che == 0)
            {
                spcode.Visibility = Visibility.Collapsed;
            }
            else 
            {
                spcode.Visibility= Visibility.Visible;
            }
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
                LogInUs();
            }
        }

        public void LogInUs() 
        {
            //User user = Base.WE.User.FirstOrDefault(z => z.UserLogin == tbnumber.Text);
            //User user1 = Base.WE.User.FirstOrDefault(z => z.UserPassword == tbpassword.Password);

            User user = Base.WE.User.FirstOrDefault(z => z.UserLogin == tbnumber.Text&& z.UserPassword == tbpassword.Password);
            if (user == null)
            {
                MessageBox.Show("Не удалось войти! Повторите вход!");
                tbnumber.Text = "";
                tbpassword.Password = "";
                che = 1;
                FrameC.frameM.Navigate(new LogInUser(che));
            }
            else
            { 
                    switch (user.UserRole)
                    {
                        case 2:
                            MessageBox.Show("Здравствуйте, администратор " + user.UserName);
                            //FrameC.frameM.Navigate(new Pages.MenuA(User));  // переход в меню администратора
                            break;
                        case 3:
                            MessageBox.Show("Здравствуйте, менеджер " + user.UserName);
                            //FrameC.frameM.Navigate(new Pages.PersonalArea(User));  // переход в личный кабинет
                            break;
                        case 1:
                            MessageBox.Show("Здравствуйте, клиент " + user.UserName);
                            //FrameC.frameM.Navigate(new Pages.PersonalArea(User));  // переход в личный кабинет
                            break;
                    }
            }

        }

        private void tbpassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (tbpassword.Password != "")
            {
                if (che != 1)
                {
                    btlogin.IsEnabled = true;
                }
                else 
                {
                    //вывести капчу
                    capch();
                    btlogin.IsEnabled = false;
                }
            }
            else
            {
                btlogin.IsEnabled = false;
            }
        }

        public void capch() 
        {
            WindowCapcha windowCapcha = new WindowCapcha();
            windowCapcha.ShowDialog();
        }

        private void tbnumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbnumber.Text != "")
            {
                tbpassword.IsEnabled = true;
            }
            else
            {
                tbpassword.IsEnabled = false;
            }
        }

        private void btlogin_Click(object sender, RoutedEventArgs e)
        {
            LogInUs();
        }

        private void btcancel_Click(object sender, RoutedEventArgs e)
        {
            tbnumber.Text = "";
            tbpassword.Password = "";
        }


    }
}
