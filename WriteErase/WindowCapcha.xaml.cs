using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public static string c ;
        public WindowCapcha()
        {
            InitializeComponent();
            Regex r1 = new Regex("[0-9]+");
            Regex r2 = new Regex("[A-z]+");
           
            metka: c = "";
            Random random = new Random();
            string cac = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string[] ca = new string[4];


            for (int i = 0; i < 4; i++)
            {
                ca[i] = Convert.ToString(cac[random.Next(cac.Length)]);
                c += ca[i];
            }

            bool re1 = r1.IsMatch(c);
            bool re2 = r2.IsMatch(c);


            if (re1) 
            {
                if (re2)
                {
                    goto met;
                }
                else 
                {
                    goto metka;
                }
            }
            else 
            {
                goto metka;
            }

            met:
            TextBlock te = new TextBlock()
            {

                Text = Convert.ToString(ca[0].ToString()),
                Margin = new Thickness(10),
                Padding = new Thickness(25),
                FontSize = random.Next(13, 18),
                FontStyle = FontStyles.Italic,
                TextDecorations = TextDecorations.Strikethrough
            };
            can1.Children.Add(te);


            TextBlock te1 = new TextBlock()
            {

                Text = Convert.ToString(ca[1].ToString()),
                Margin = new Thickness(10),
                Padding = new Thickness(25),
                FontSize = random.Next(13, 18),
                FontStyle = FontStyles.Italic,
                FontWeight = FontWeights.Bold,
                TextDecorations = TextDecorations.Strikethrough
            };
            can2.Children.Add(te1);

            TextBlock te2 = new TextBlock()
            {

                Text = Convert.ToString(ca[2].ToString()),
                Margin = new Thickness(10),
                Padding = new Thickness(25),
                FontSize = random.Next(13, 18),
                FontWeight = FontWeights.Bold,
                TextDecorations = TextDecorations.Strikethrough
            };
            can3.Children.Add(te2);

            TextBlock te3 = new TextBlock()
            {

                Text = Convert.ToString(ca[3].ToString()),
                Margin = new Thickness(10),
                Padding = new Thickness(25),
                FontSize = random.Next(13, 18),
                FontStyle = FontStyles.Italic,
                FontWeight = FontWeights.Bold,
                TextDecorations = TextDecorations.Strikethrough
            };
            can4.Children.Add(te3);


            Line l1 = new Line()
            {
                X1 = random.Next(225),
                Y1 = random.Next(125),
                Stroke = Brushes.Violet,
                StrokeThickness = random.Next(2, 7),
            };
            canvas.Children.Add(l1);
            Line l2 = new Line()
            {
                X1 = random.Next(225),
                Y1 = random.Next(125),
                Stroke = Brushes.SpringGreen,
                StrokeThickness = random.Next(2, 7),
            };
            canvas.Children.Add(l2);
            Line l3 = new Line()
            {
                X1 = random.Next(-325, 0),
                Y1 = random.Next(10, 70),
                Stroke = Brushes.SteelBlue,
                StrokeThickness = random.Next(2, 7),
            };
            canvas.Children.Add(l3);
            Line l4 = new Line()
            {
                X1 = random.Next(355, 399),
                Y1 = random.Next(40, 100),
                Stroke = Brushes.Tan,
                StrokeThickness = random.Next(2, 7),
            };
            canvas.Children.Add(l4);
            Line l5 = new Line()
            {
                X1 = random.Next(-225, 0),
                Y1 = random.Next(125),
                Stroke = Brushes.Tomato,
                Fill = Brushes.Tomato,
                StrokeThickness = random.Next(2, 7),
            };
            canvas.Children.Add(l5);


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
