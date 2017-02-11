using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProbeTests
{
    public partial class Form1 : Form
    {
        static HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            client.BaseAddress = new Uri(urlTextBox.Text);
            var paths = File.ReadAllLines(fileTextBox.Text);
            progressBar1.Maximum = paths.Length;
            progressBar1.Step = 1;

            foreach (var path in paths)
            {
                Probe(path);
            }
        }

        private async void Probe(string path)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                string fullpath = client.BaseAddress + path;
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    successTextBox.AppendText("\n" + fullpath);
                }
                else
                {
                    failTextBox.AppendText("\n" + fullpath + ", " + response.StatusCode);
                }

                progressBar1.PerformStep();
            }
            catch (Exception ex)
            {
                successTextBox.AppendText(ex.ToString());                
            }
        }
        
    }
}
