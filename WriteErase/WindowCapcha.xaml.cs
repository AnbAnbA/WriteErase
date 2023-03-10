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

namespace WriteErase
{
    /// <summary>
    /// Логика взаимодействия для WindowCapcha.xaml
    /// </summary>
    public partial class WindowCapcha : Window
    {
        public WindowCapcha()
        {
            InitializeComponent();
            Random random = new Random();
            string cac = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string[] ca = new string[4];

            for (int i = 0; i < 4; i++)
            {
                //if (random.Next(1, 3) == 1)
                //{
                    ca[i] = Convert.ToString(cac[random.Next(cac.Length)]);
                    
                //}
                //else
                //{
                //    //capcha[i] = (char)(random.Next('a', 'z')) ;
                   
                //}

            }


            TextBlock te = new TextBlock()
            {

                Text = Convert.ToString(ca[0].ToString()),
                Margin = new Thickness(20),
                Padding = new Thickness(40),
                FontSize = random.Next(13, 18),
                FontStyle = FontStyles.Italic,
            };
            can1.Children.Add(te);


            TextBlock te1 = new TextBlock()
            {

                Text = Convert.ToString(ca[1].ToString()),
                Margin = new Thickness(20),
                Padding = new Thickness(40),
                FontSize = random.Next(13, 18),
                FontStyle = FontStyles.Italic,
                FontWeight = FontWeights.Bold,
            };
            can2.Children.Add(te1);

            TextBlock te2 = new TextBlock()
            {

                Text = Convert.ToString(ca[2].ToString()),
                Margin = new Thickness(20),
                Padding = new Thickness(40),
                FontSize = random.Next(13, 18),
                FontWeight = FontWeights.Bold,
            };
            can3.Children.Add(te2);

            TextBlock te3 = new TextBlock()
            {

                Text = Convert.ToString(ca[3].ToString()),
                Margin = new Thickness(20),
                Padding = new Thickness(40),
                FontSize = random.Next(13, 18),
                FontStyle = FontStyles.Italic,
                FontWeight = FontWeights.Bold,
            };
            can4.Children.Add(te3);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
