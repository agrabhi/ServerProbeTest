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
        static HttpClient client;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Init();
            client = new HttpClient();
            client.BaseAddress = new Uri(urlTextBox.Text);
            var paths = File.ReadAllLines(fileTextBox.Text);
            progressBar1.Maximum = paths.Length;
            progressBar1.Step = 1;

            foreach (var path in paths)
            {
                Probe(path);
            }
        }

        private void Init()
        {
            progressBar1.Value = 1;
            exceptionPath.Text = "Some Exception Occured while calling these. Try rerun of these paths separately.";
            failTextBox.Text = "Queries which did not return 404. Csv format with actual reposnse.";
            successTextBox.Text = "Queries which returned 404";
        }

        private async void Probe(string path)
        {
            string fullpath = client.BaseAddress + path;

            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    successTextBox.AppendText("\n" + fullpath);
                }
                else
                {
                    failTextBox.AppendText("\n" + fullpath + ", " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                exceptionPath.AppendText("\n" + path);
                //failTextBox.AppendText(ex.ToString());                
            }
            progressBar1.PerformStep();
        }
        
    }
}
