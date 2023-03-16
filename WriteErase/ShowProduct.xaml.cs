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
    /// Логика взаимодействия для ShowProduct.xaml
    /// </summary>
    public partial class ShowProduct : Page
    {
        List<PartialBask> partialBasks = new List<PartialBask>();
        User user;
        public ShowProduct(User user)
        {
            InitializeComponent();
            this.user = user;
            creatFile();
            tbFIO.Text = "" + user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
            if (user.Role.RoleName == "Менеджер" || user.Role.RoleName == "Администратор")
            {
                btnOrders.Visibility = Visibility.Visible;
            }
        }

        public ShowProduct()
        {
            InitializeComponent();
            creatFile();
        }

        public void creatFile() 
        {
            lvProduct.ItemsSource = Base.WE.Product.ToList();
            cbFilt.SelectedIndex = 0;
            cbSort.SelectedIndex = 0;
            tbCountProduct.Text = "" + Base.WE.Product.ToList().Count() + " из " + Base.WE.Product.ToList().Count();
        }

        public void filt() //метод фильтрации
        {
            List<Product> products = Base.WE.Product.ToList();
            if (tbSearch.Text.Length > 0)
            {
                products = products.Where(x => x.TitleProduct.Title.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }
            if (cbFilt.SelectedIndex > 0)
            {
                switch (cbFilt.SelectedIndex)
                {
                    case 1:
                        products = products.Where(x => x.ProductDiscountAmount > 0 && x.ProductDiscountAmount < 9.99).ToList();
                        break;
                    case 2:
                        products = products.Where(x => x.ProductDiscountAmount > 10 && x.ProductDiscountAmount < 14.99).ToList();
                        break;
                    case 3:
                        products = products.Where(x => x.ProductDiscountAmount > 15).ToList();
                        break;
                }
            }
            if (cbSort.SelectedIndex > 0)
            {
                switch (cbSort.SelectedIndex)
                {
                    case 1:
                        products = products.OrderBy(x => x.costWithDiscount).ToList();
                        break;
                    case 2:
                        products = products.OrderByDescending(x => x.costWithDiscount).ToList();
                        break;
                }
            }
            lvProduct.ItemsSource = products;
            if (products.Count == 0)
            {
                MessageBox.Show("Данные не найдены");
            }
            tbCountProduct.Text = "" + products.Count() + " из " + Base.WE.Product.ToList().Count();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            filt();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filt();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string index = btn.Uid;
                Product product = Base.WE.Product.FirstOrDefault(x => x.ProductArticleNumber == index);
                List<OrderProduct> orderProducts = Base.WE.OrderProduct.Where(x => x.ProductArticleNumber == index).ToList();
                if (orderProducts.Count == 0)
                {
                    foreach (OrderProduct orderProduct in orderProducts)
                    {
                        Base.WE.OrderProduct.Remove(orderProduct);
                    }
                    Base.WE.Product.Remove(product);
                    Base.WE.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Товар нельзя удалить так как он указан в заказе!");
                }

            }
            catch
            {
                MessageBox.Show("Oшибка!");
            }
        }

        private void btnDelete_Loaded(object sender, RoutedEventArgs e)
        {
            if (user == null)
            {
                return;
            }
            Button btnDelete = sender as Button;
            if (user.Role.RoleName == "Менеджер" || user.Role.RoleName == "Администратор")
            {
                btnDelete.Visibility = Visibility.Visible;
            }
            else
            {
                btnDelete.Visibility = Visibility.Collapsed;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameC.frameM.Navigate(new LogInUser());
        }

        private void btnBasket_Click(object sender, RoutedEventArgs e)
        {
            WindowBask WindowBask = new WindowBask(partialBasks, user);
            WindowBask.ShowDialog();
            if (partialBasks.Count == 0)
            {
                btnBasket.Visibility = Visibility.Collapsed;
            }
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {

        }

        private void miAddBasket_Click(object sender, RoutedEventArgs e)
        {
            Product product=(Product)lvProduct.SelectedItem;
            bool s = false;
            foreach (PartialBask partialBask in partialBasks) 
            {
                if (partialBask.product == product) 
                {
                    partialBask.count = partialBask.count += 1;
                    s = true;
                }
            }
            if (!s) 
            {
                PartialBask partialBaske = new PartialBask();
                partialBaske.product = product;
                partialBaske.count = 1;
                partialBasks.Add(partialBaske);
            }
            btnBasket.Visibility = Visibility.Visible;
        }
    }
}
