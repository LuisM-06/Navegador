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
            this.Resize += new System.EventHandler(this.Form_Resize);
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            webView21.Size = this.ClientSize - new System.Drawing.Size(webView21.Location);
            button1.Left = this.ClientSize.Width - button1.Width;
            comboBox1.Width = button1.Left - comboBox1.Left;
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
            //webBrowser1.GoHome();
        }

        private void goForwardToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            //webBrowser1.GoForward();
        }

        private void goBackToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            //webBrowser1.GoBack();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((webView21 != null && webView21.CoreWebView2 != null))
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

                webView21.CoreWebView2.Navigate(Url);
            }

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void webView21_Click(object sender, EventArgs e)
        {

        }
    }
}
