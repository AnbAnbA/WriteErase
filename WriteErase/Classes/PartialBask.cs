﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteErase
{
    public class PartialBask
    {
        public Product product { get; set; }
        public int count { get; set; }
        public string article { get; set; }
        public string textDecoration
        {
            get
            {
                if (product.ProductDiscountAmount != 0)
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
