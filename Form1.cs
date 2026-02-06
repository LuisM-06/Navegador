using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Navegador
{

    public partial class Form1 : Form
    {

        private String historialPath = "historial.txt";

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


        //Leer historial y cambiar al combo
        private void Form1_Load(object sender, EventArgs e)
        {
            LeerHistorial();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void navegarToolStripMenuItem_Click(object sender, EventArgs e)
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

                GuardarHistorial(Url);
                LeerHistorial();
                comboBox1.Text = Url;
            }

            /*
             * 
             * 
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


        //Para guardar
        private void GuardarHistorial(String texto)
        {
            FileStream stream = new FileStream(historialPath, FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(texto);
            writer.Close();


            RecortarHistorialA10();
        }

        private void LeerHistorial()
        {
            comboBox1.Items.Clear();

            if (!File.Exists(historialPath))
                return;

            string[] lineas = File.ReadAllLines(historialPath);

        
            int inicio = Math.Max(0, lineas.Length - 10);


            for (int i = inicio; i < lineas.Length; i++)
            {
                string linea = lineas[i].Trim();
                if (linea != "")
                {
                    comboBox1.Items.Add(linea);
                }
            }

        }

        private void RecortarHistorialA10()
        {
            if (!File.Exists(historialPath))
                return;

            List<string> lineas = File.ReadAllLines(historialPath)
                                      .Where(l => !string.IsNullOrWhiteSpace(l))
                                      .ToList();

          
            List<string> sinDup = new List<string>();
            foreach (string l in lineas) { sinDup.Remove(l); sinDup.Add(l); }
            lineas = sinDup;

            if (lineas.Count > 10)
            {
                lineas = lineas.Skip(lineas.Count - 10).ToList();
                File.WriteAllLines(historialPath, lineas);
            }
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
