using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XMart.Models
{
    public class CarouselItem
    {
        public string ImgSource { get; set; }

        public string ImgUrl { get; set; }

        public Command CarouselCommand { get; set; }
    }
}
