﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WriteErase
{
   public partial class Product
    {
        public SolidColorBrush colorBackground
        {
            get
            {
                if (ProductDiscountAmount > 0)
                {
                    SolidColorBrush color = (SolidColorBrush)new BrushConverter().ConvertFromString("#7fff00");
                    return color;
                }
                return Brushes.White;
            }
        }
        public double costWithDiscount
        {
            get
            {
                if (ProductDiscountAmount != 0)
                {
                    return (double)(Convert.ToDouble(ProductCost) - (Convert.ToDouble(ProductCost) * ProductDiscountAmount / 100));
                }
                else 
                {
                    return 0;
                }
               
            }
        }

        public string costWithDiscountString
        {
            get
            {
                if (ProductDiscountAmount != 0)
                {
                    return costWithDiscount.ToString("0.00");
                }
                else
                {
                    return "";
                }
            }
        }

        public string productName 
        {
            get 
            {
                return TitleProduct.Title; 
            }
        }


        public string textDecoration
        {
            get
            {
                if (ProductDiscountAmount != 0)
                {
                    return "Strikethrough";
                }
                else
                {
                    return "Baseline";
                }
            }
        }
    }
}
