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

        //trabajamos primero sobre una lista de objetos
        private List<Direccion> historial = new List<Direccion>();

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
        }

        

        //Para guardar (ahora actualiza contador y fecha, y NO duplica)
        private void GuardarHistorial(String texto)
        {
            //1) Buscar si ya existe
            Direccion existe = historial.FirstOrDefault(x => x.Url == texto);

            if (existe != null)
            {
                //2) Si existe: sumar veces y actualizar fecha
                existe.Veces = existe.Veces + 1;
                existe.FechaAcceseso = DateTime.Now;
            }
            else
            {
                //3) Si no existe: agregar nuevo
                Direccion nuevo = new Direccion();
                nuevo.Url = texto;
                nuevo.Veces = 1;
                nuevo.FechaAcceseso = DateTime.Now;
                historial.Add(nuevo);
            }

            //4) Guardar TODA la lista al archivo (no append)
            GuardarArchivoCompleto();

            //5) Limitar a 10 (como tu versión)
            RecortarHistorialA10();
        }

        private void LeerHistorial()
        {
            comboBox1.Items.Clear();
            historial.Clear();

            if (!File.Exists(historialPath))
                return;

            string[] lineas = File.ReadAllLines(historialPath);

            foreach (string lineaOriginal in lineas)
            {
                string linea = lineaOriginal.Trim();
                if (linea == "")
                    continue;

                // Formato: url|veces|fecha
                string[] partes = linea.Split('|');

                if (partes.Length >= 3)
                {
                    Direccion d = new Direccion();
                    d.Url = partes[0];

                    int v;
                    if (!int.TryParse(partes[1], out v)) v = 1;
                    d.Veces = v;

                    DateTime f;
                    if (!DateTime.TryParse(partes[2], out f)) f = DateTime.Now;
                    d.FechaAcceseso = f;

                    //Por si el archivo trae duplicados viejos, se unifica aquí
                    Direccion existe = historial.FirstOrDefault(x => x.Url == d.Url);
                    if (existe != null)
                    {
                        existe.Veces = existe.Veces + d.Veces;
                        if (d.FechaAcceseso > existe.FechaAcceseso)
                            existe.FechaAcceseso = d.FechaAcceseso;
                    }
                    else
                    {
                        historial.Add(d);
                    }
                }
            }

            //Mostrar en el combo (solo URL como tenías)
            //(Si quieres mostrar "URL (veces)" te lo dejo abajo comentado)
            for (int i = 0; i < historial.Count; i++)
            {
                comboBox1.Items.Add(historial[i].Url);
                //comboBox1.Items.Add(historial[i].Url + " (" + historial[i].Veces + ")");
            }
        }

        //Guarda toda la lista al archivo (reemplaza el contenido)
        private void GuardarArchivoCompleto()
        {
            using (FileStream stream = new FileStream(historialPath, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                for (int i = 0; i < historial.Count; i++)
                {
                    //Formato: url|veces|fecha
                    writer.WriteLine(historial[i].Url + "|" + historial[i].Veces + "|" + historial[i].FechaAcceseso);
                }
            }
        }

        // Mantiene máximo 10 URLs (como tu idea original)
        private void RecortarHistorialA10()
        {
            //Quitar duplicados manteniendo el último registro
            List<Direccion> sinDup = new List<Direccion>();
            foreach (Direccion d in historial)
            {
                //Si ya existe, lo quitamos y lo volvemos a agregar al final
                Direccion ya = sinDup.FirstOrDefault(x => x.Url == d.Url);
                if (ya != null) sinDup.Remove(ya);
                sinDup.Add(d);
            }
            historial = sinDup;

            //Si hay más de 10, dejamos los últimos 10 (los más recientes en la lista)
            if (historial.Count > 10)
            {
                historial = historial.Skip(historial.Count - 10).ToList();
            }

            //Guardar recortado al archivo
            GuardarArchivoCompleto();
        }

      

        private void ordenarPorVecesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //de más visitadas a menos visitadas
            historial = historial.OrderByDescending(x => x.Veces).ToList();
            comboBox1.Items.Clear();
            for (int i = 0; i < historial.Count; i++)
                comboBox1.Items.Add(historial[i].Url);
        }

        private void ordenarPorFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //de más recientes a más antiguas
            historial = historial.OrderByDescending(x => x.FechaAcceseso).ToList();
            comboBox1.Items.Clear();
            for (int i = 0; i < historial.Count; i++)
                comboBox1.Items.Add(historial[i].Url);
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;

            string seleccion = comboBox1.SelectedItem.ToString();

            historial = historial.Where(x => x.Url != seleccion).ToList();
            GuardarArchivoCompleto();
            LeerHistorial();
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
