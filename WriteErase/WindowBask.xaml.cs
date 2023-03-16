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
    /// Логика взаимодействия для WindowBask.xaml
    /// </summary>
    public partial class WindowBask : Window
    {
        double summa;
        double summaDiscount;
        User user;

        List<PartialBask> partialBasks;
        List<PickupPoint> pickupPoints;
        public WindowBask(List<PartialBask> partialBasks, User user)
        {
            InitializeComponent();
            if (user != null)
            {
                tbFIO.Text = "" + user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
            }
            this.partialBasks = partialBasks;
            this.user = user;
            lvProduct.ItemsSource = partialBasks;
            calculateSummaAndDiscount();
            
            
            
           
            pickupPoints = Base.WE.PickupPoint.ToList();

            for (int i = 0; i < pickupPoints.Count; i++) 
            {
                cmbPickupPoint.Items.Add(pickupPoints[i].PPIndex + ", " + pickupPoints[i].City.CityName + ", " + pickupPoints[i].Street.StreetName + ", " + pickupPoints[i].PPHouse);
            }

            cmbPickupPoint.SelectedIndex = 0;
        }

        private void btdelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string index =btn.Uid;
            PartialBask partialBask = partialBasks.FirstOrDefault(x => x.product.ProductArticleNumber == index);
            partialBasks.Remove(partialBask);
            if (partialBasks.Count == 0)
            {
                this.Close();
            }
            lvProduct.Items.Refresh();
            calculateSummaAndDiscount();
        }

        private void tbCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string index = tb.Uid;
            PartialBask partialBasr = partialBasks.FirstOrDefault(x => x.product.ProductArticleNumber == index);
            if (tb.Text.Replace(" ", "") == "")
            {
                partialBasr.count = 0;
            }
            else
            {
                partialBasr.count = Convert.ToInt32(tb.Text);
            }
            if (partialBasr.count == 0)
            {
                partialBasks.Remove(partialBasr);
            }
            if (partialBasks.Count == 0)
            {
                this.Close();
            }
            lvProduct.Items.Refresh();
            calculateSummaAndDiscount();
        }

        private void tbCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0)))
            {
                e.Handled = true;
            }
        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btBask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order order = new Order();
                int countDay = 0;
                List<Order> orderLast = Base.WE.Order.OrderBy(x => x.OrderID).ToList();
                order.OrderID = orderLast[orderLast.Count - 1].OrderID + 1;
                order.OrderStatus = Base.WE.OrderStatus.FirstOrDefault(x => x.OrderStatusName == "Новый").OrderStatusID;
                order.OrderDate = DateTime.Now;
                if (getDeliveryTime())
                {
                    countDay = 6;
                }
                else
                {
                    countDay = 3;
                }
                order.OrderDeliveryDate = order.OrderDate.AddDays(countDay);
                order.OrderPickupPoint = (int)((PickupPoint)cmbPickupPoint.SelectedItem).PickupPointID;
                if (user != null)
                {
                    order.OrderClient = user.UserID;
                }
                Random rand = new Random();
                string textCode = "";
                for (int i = 0; i < 3; i++)
                {
                    textCode = textCode + rand.Next(10).ToString();
                }
                order.OrderCode = Convert.ToInt32(textCode);
                Base.WE.Order.Add(order);
                Base.WE.SaveChanges();
                foreach (PartialBask partialBask in partialBasks)
                {
                    OrderProduct orderProduct = new OrderProduct();
                    orderProduct.OrderID = order.OrderID;
                    orderProduct.ProductArticleNumber = partialBask.product.ProductArticleNumber;
                    orderProduct.ProductCount = partialBask.count;
                    Base.WE.OrderProduct.Add(orderProduct);
                }
                Base.WE.SaveChanges();
                MessageBox.Show("Заказ успешно создан");
                WindowTicket ticket = new WindowTicket(order, partialBasks, summa, summaDiscount,countDay);
                ticket.ShowDialog();
                partialBasks.Clear();
                this.Close();
            }
            catch
            {
                MessageBox.Show("При создание заказа возникла ошибка!");
            }
        }


        private bool getDeliveryTime()
        {
            foreach (PartialBask partialBask in partialBasks)
            {
                if (partialBask.product.ProductQuantityInStock < 3 || partialBask.product.ProductQuantityInStock < partialBask.count)
                {
                    return true;
                }
            }
            return false;
        }
        private void calculateSummaAndDiscount()
        {
            summa = 0;
            summaDiscount = 0;
            foreach (PartialBask partialBask in partialBasks)
            {
                summa += partialBask.count * partialBask.product.costWithDiscount;
                summaDiscount += partialBask.count * ((double)partialBask.product.ProductCost - partialBask.product.costWithDiscount);
            }
            tbSumma.Text = "Сумма заказа: " + summa.ToString("0.00") + " руб.";
            tbSummaDiscount.Text = "Сумма скидки: " + summaDiscount.ToString("0.00") + " руб.";
        }
    }
}
