using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        double summa;
        double summaDiscount;
        int countDay;
        public WindowTicket(Order order, List<PartialBask> partialBasks, double summa, double summaDiscount, int countDay)
        {
            InitializeComponent();
            this.order = order;
            this.partialBasks = partialBasks;
            this.summa = summa;
            this.summaDiscount = summaDiscount;
            this.countDay = countDay;
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
            PickupPoint pickupPoint = Base.WE.PickupPoint.FirstOrDefault(x => x.PickupPointID == order.OrderPickupPoint);
          
            List<PickupPoint> pickupPoints = Base.WE.PickupPoint.Where(x => x.PickupPointID == order.OrderPickupPoint).ToList();
            
            string citys="", street="";
       
            foreach (PickupPoint pickupPointd in pickupPoints) 
            {
               citys= pickupPointd.City.CityName;
                street = pickupPointd.Street.StreetName;
            }
            tbOrderPickupPoint.Text = tbOrderPickupPoint.Text  +order.PickupPoint.PPIndex+", " + citys + ", " + street + ", " + order.PickupPoint.PPHouse;
            tbCode.Text = tbCode.Text + order.OrderCode.ToString();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBasket_Click(object sender, RoutedEventArgs e)
        {
            PdfDocument document = new PdfDocument();
            int height = 0;
            document.Info.Title = "Талон для получения заказа";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont fontHeader = new XFont("Comic Sans MS", 14, XFontStyle.Bold);
            gfx.DrawString("Талон для получения заказа", fontHeader, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopCenter);
            XFont font = new XFont("Comic Sans MS", 14);
            height += 30;
            gfx.DrawString("Номер: " + order.OrderID, font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            gfx.DrawString("Дата заказа: " + order.OrderDate.ToString("D"), font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            if (countDay == 3)
            {
                gfx.DrawString("Заказ будет готов через 3 дня", font, XBrushes.Black,
                    new XRect(10, height, page.Width, page.Height),
                    XStringFormats.TopLeft);
            }
            else
            {
                gfx.DrawString("Заказ будет готов через 6 дней", font, XBrushes.Black,
                    new XRect(10, height, page.Width, page.Height),
                    XStringFormats.TopLeft);
            }
            height += 30;
            gfx.DrawString("Дата получения заказа: " + order.OrderDeliveryDate.ToString("D"), font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            gfx.DrawString("Состав заказа: ", font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            for (int i = 0; i < partialBasks.Count; i++)
            {
                height += 30;
                if (i != partialBasks.Count - 1)
                {
                    gfx.DrawString("" + partialBasks[i].product.ProductName + " Колличество: " + partialBasks[i].count + ";", font, XBrushes.Black,
                        new XRect(30, height, page.Width, page.Height),
                        XStringFormats.TopLeft);
                }
                else
                {
                    gfx.DrawString("" + partialBasks[i].product.ProductName + " Колличество: " + partialBasks[i].count + ".", font, XBrushes.Black,
                        new XRect(30, height, page.Width, page.Height),
                        XStringFormats.TopLeft);
                }
            }
            height += 30;
            gfx.DrawString("Сумма заказа: " + summa.ToString("0.00") + " руб.", font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            gfx.DrawString("Сумма скидки: " + summaDiscount.ToString("0.00") + " руб.", font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            gfx.DrawString("Пункт выдачи: " + order.PickupPoint.PPIndex+", "+order.PickupPoint.City.CityName+", "+order.PickupPoint.Street.StreetName+", "+order.PickupPoint.PPHouse, font, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            height += 30;
            gfx.DrawString("Код для получения: " + order.OrderCode, fontHeader, XBrushes.Black,
                new XRect(10, height, page.Width, page.Height),
                XStringFormats.TopLeft);
            string filename = "TicketPDF.pdf";
            document.Save(filename);
            Process.Start(filename);
        }
    }
}
