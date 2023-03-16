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
    /// Логика взаимодействия для WindowTicket.xaml
    /// </summary>
    public partial class WindowTicket : Window
    {
        Order order;
        List<PartialBask> partialBasks;
        
        public WindowTicket(Order order, List<PartialBask> partialBasks, double summa, double summaDiscount)
        {
            InitializeComponent();
            this.order = order;
            this.partialBasks = partialBasks;
            tbOrderNomer.Text = tbOrderNomer.Text + order.OrderID.ToString();
            tbDateOrder.Text = tbDateOrder.Text + order.OrderDate.ToString("d");
            for (int i = 0; i < partialBasks.Count; i++)
            {
                if (i == partialBasks.Count - 1)
                {
                    tbOrders.Text = tbOrders.Text + partialBasks[i].product.ProductName + " Количество: " + partialBasks[i].count;
                }
                else
                {
                    tbOrders.Text = tbOrders.Text + partialBasks[i].product.ProductName + " Количество: " + partialBasks[i].count + "\n";
                }
            }
            tbSumma.Text = tbSumma.Text + summa.ToString("0.00") + " руб.";
            tbSummaDiscount.Text = tbSummaDiscount.Text + summaDiscount.ToString("0.00") + " руб.";
            
            tbOrderPickupPoint.Text = tbOrderPickupPoint.Text + order.PickupPoint.PPIndex ;
            tbCode.Text = tbCode.Text + order.OrderCode.ToString();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBasket_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
