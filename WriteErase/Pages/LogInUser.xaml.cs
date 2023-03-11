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
using System.Windows.Threading;

namespace WriteErase
{
    /// <summary>
    /// Логика взаимодействия для LogInUser.xaml
    /// </summary>
    public partial class LogInUser : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
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
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += new EventHandler(Timer_Trick);
        }
        public LogInUser()
        {
            InitializeComponent();
            tbnumber.Focus();
            if (che == 0)
            {
                spcode.Visibility = Visibility.Collapsed;
            }
            else
            {
                spcode.Visibility = Visibility.Visible;
            }
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += new EventHandler(Timer_Trick);
        }

        private void Timer_Trick(object sender, EventArgs e) 
        {
           tbnumber.IsEnabled = true;
           tbnumber.Focus();
        }

        private void tbnumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                tbpassword.IsEnabled = true;
                tbpassword.Focus();
                timer.Stop();
            }
        }

        private void tbpassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (che != 1)
                {
                    LogInUs();
                }
                else
                {
                    //вывести капчу
                    capch();
                    tbcode.IsEnabled = true;
                    tbcode.Focus();
                }
                
            }
        }

        public void LogInUs() //метод авторизации
        {
            //User user = Base.WE.User.FirstOrDefault(z => z.UserLogin == tbnumber.Text);
            //User user1 = Base.WE.User.FirstOrDefault(z => z.UserPassword == tbpassword.Password);

            User user = Base.WE.User.FirstOrDefault(z => z.UserLogin == tbnumber.Text&& z.UserPassword == tbpassword.Password);
            if (user == null)
            {
                if (che == 1) 
                {
                    MessageBox.Show("Не удалось войти! Система входа заблокирована на 10 секунд!");
                    tbnumber.IsEnabled = false;
                    tbnumber.Text = "";
                    tbpassword.Password = "";
                    tbcode.Text = "";
                    tbcode.IsEnabled = false;
                    timer.Start();
                }
                else 
                {
                    MessageBox.Show("Не удалось войти! Повторите вход!");
                    tbnumber.Text = "";
                    tbpassword.Password = "";
                    che = 1;
                    FrameC.frameM.Navigate(new LogInUser(che));
                }
            }
            else
            { 
                    switch (user.UserRole)
                    {
                        case 2:
                            MessageBox.Show("Здравствуйте, администратор " + user.UserName);
                            FrameC.frameM.Navigate(new ShowProduct(user));  // переход в меню администратора
                            break;
                        case 3:
                            MessageBox.Show("Здравствуйте, менеджер " + user.UserName);
                        FrameC.frameM.Navigate(new ShowProduct(user));   // переход в личный кабинет
                        break;
                        case 1:
                            MessageBox.Show("Здравствуйте, клиент " + user.UserName);
                        FrameC.frameM.Navigate(new ShowProduct(user));   // переход в личный кабинет
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
                    btlogin.IsEnabled = false;
                }
            }
            else
            {
                btlogin.IsEnabled = false;
            }
        }

        public void capch()  //метод для вызова капчи
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
            tbcode.Text = "";
        }

        private void tbcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbcode.Text != "")
            {
               btlogin.IsEnabled = true; 
            }
            else
            {
                btlogin.IsEnabled = false;
            }
        }

        private void tbcode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (WindowCapcha.c == tbcode.Text)
                {
                    LogInUs();
                }
                else 
                {
                    MessageBox.Show("Не верный код! Система входа заблокирована на 10 секунд!");
                    tbnumber.IsEnabled = false;
                    tbnumber.Text = "";
                    tbpassword.Password = "";
                    tbcode.Text = "";
                    tbcode.IsEnabled = false;
                    timer.Start();
                }
            }
        }

        private void btguest_Click(object sender, RoutedEventArgs e)
        {
            FrameC.frameM.Navigate(new ShowProduct());
        }
    }
}
