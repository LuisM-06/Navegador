using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Navegador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void navegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void homeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            webBrowser1.GoHome();
        }

        private void goForwardToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void goBackToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string Url = comboBox1.Text.Trim();

            if (!Url.Contains("."))
            {
                //Cuando no tiene punto
                Url = "https://www.google.com/search?q=" + Url.Replace(" ", "+");
            }
            else
            {
                //CON punto
                if (Url.Contains("https://") || Url.Contains("http://"))
                {

                    //No se hacen cambios
                }
                else
                {
                    Url = "https://" + Url;
                }
            }

            webBrowser1.Navigate(new Uri(Url));


            /*
            string Url = comboBox1.Text;

            if( (Url.Contains("https://")) || (Url.Contains("http://")) ){
                webBrowser1.Navigate(new Uri(Url));
            }
            else
            {
                Url = "https://" + Url;
                webBrowser1.Navigate(new Uri(Url));
            }
            */
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }


    }
}
