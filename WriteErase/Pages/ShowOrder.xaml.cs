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
    /// Логика взаимодействия для ShowOrder.xaml
    /// </summary>
    public partial class ShowOrder : Page
    {
        User user;
        public ShowOrder()
        {
            InitializeComponent();
            createFile();
        }


        /// <summary>
        /// второй конструктор 
        /// </summary>
        /// <param name="user"></param>
        public ShowOrder(User user)
        {
            InitializeComponent();
            this.user = user;
            createFile();
        }



        /// <summary>
        /// метод для заполнения листа и выпадающих списоков
        /// </summary>
        public void createFile() 
        {
            lvListOrders.ItemsSource = Base.WE.Order.ToList();
            cbSort.SelectedIndex = 0;
            cbFilt.SelectedIndex = 0;
        }
        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            FrameC.frameM.Navigate(new ShowProduct(user));
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filt();
        }


        /// <summary>
        /// метод фильтрации и сортировки
        /// </summary>
        private void Filt()
        {
            List<Order> orders = Base.WE.Order.ToList();
            if (cbFilt.SelectedIndex > 0) // Если фильрация выбрана
            {
                switch (cbFilt.SelectedIndex)
                {
                    case 1:
                        orders = orders.Where(x => x.DiscountProcent > 0 && x.DiscountProcent < 10).ToList();
                        break;
                    case 2:
                        orders = orders.Where(x => x.DiscountProcent >= 10 && x.DiscountProcent < 15).ToList();
                        break;
                    case 3:
                        orders = orders.Where(x => x.DiscountProcent >= 15).ToList();
                        break;
                }
            }
            if (cbSort.SelectedIndex > 0) // Если выбрана сортировка
            {
                switch (cbSort.SelectedIndex)
                {
                    case 1:
                        orders = orders.OrderBy(x => x.Summa).ToList();
                        break;
                    case 2:
                        orders = orders.OrderByDescending(x => x.Summa).ToList();
                        break;
                }
            }
            lvListOrders.ItemsSource = orders;
            if (orders.Count == 0)
            {
                MessageBox.Show("Данные не найдены");
            }
        }
    }
}
