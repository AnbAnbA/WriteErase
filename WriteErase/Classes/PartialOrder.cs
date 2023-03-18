using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WriteErase
{
    public partial class Order
    {
        public string OrderList
        {
            get
            {
                List<OrderProduct> products = Base.WE.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                string ordL = "";

                foreach (OrderProduct order in products) 
                {
                    Product product = Base.WE.Product.FirstOrDefault(x => x.ProductArticleNumber == order.ProductArticleNumber);
                    ordL=ordL+product.TitleProduct.Title + " Количество: " + order.ProductCount +", ";
                }

             
                return ordL;
            }
        }
        public double Summa
        {
            get
            {
                List<OrderProduct> products = Base.WE.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                double summa = 0;
                foreach (OrderProduct product in products)
                {
                    foreach (OrderProduct order in products)
                    {
                        summa = summa + ((double)order.Product.ProductCost * product.Product.costWithDiscount / 100) * (double)product.ProductCount;
                    }
                }
                return summa;
            }
        }

        public string StrSumma
        {
            get
            {
                return "" + Summa.ToString("0.00");
            }
        }

        public double DiscountProcent
        {
            get
            {
                List<OrderProduct> products = Base.WE.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                double summaDiscount = 0;
                foreach (OrderProduct product in products)
                {
                    foreach (OrderProduct order in products)
                    {
                        summaDiscount = summaDiscount + (double)(order.Product.costWithDiscount * product.ProductCount);
                    }
                }
                double summa = 0;
                foreach (OrderProduct product in products)
                {
                    foreach (OrderProduct order in products)
                    {
                        summa = summa + ((double)order.Product.ProductCost * (double)product.ProductCount);
                    }
                }
                double procent = (summa - summaDiscount) / summa * 100;
                return procent;
            }
        }

        public string StrDiscount
        {
            get
            {
                return "" + DiscountProcent.ToString("0.00");
            }
        }
        public SolidColorBrush colorBackground
        {
            get
            {
                bool b = true;
                List<OrderProduct> orderProducts = Base.WE.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                foreach (OrderProduct product in orderProducts)
                {
                    if (product.ProductCount > product.Product.ProductQuantityInStock || product.Product.ProductQuantityInStock <= 3)
                    {
                        b = false;
                        break;
                    }
                }
                if (b)
                {
                    SolidColorBrush color = (SolidColorBrush)new BrushConverter().ConvertFromString("#20b2aa");
                    return color;
                }
                else
                {
                    SolidColorBrush color = (SolidColorBrush)new BrushConverter().ConvertFromString("#ff8c00");
                    return color;
                }
                return Brushes.White;
            }
        }
    }
}
