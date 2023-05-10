using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class product
    {
        private static int numero=0;
        private string num;
        private string title;
        private float price;
        private string shiping;
        private string numberbuyer;
        private string productLink;

        public string ProductLink
        {
            get { return productLink; }
            set { productLink = value; }
        }


        public product(string tit, float price,string shipi,string numbyer,string link)
        {
            numero ++;
            this.num = numero.ToString();
            this.title = tit;
            this.price = price;
            this.shiping = shipi;
            this.numberbuyer = numbyer;
            this.productLink = link;
        }
        public product()
        {
            numero++;
            this.num = numero.ToString();
        }
        public string Nbuyer
        {
            set { numberbuyer = value; }
            get { return numberbuyer; }
        }

        public string Shiping
        {
            get { return shiping; }
            set { shiping= value; }
        }



        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }


        public string Num
        {
            get { return num; }
          
        }

    }
}
