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

            foreach (var path in paths)
            {
                Probe(path);
            }
        }

        private async void Probe(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                successTextBox.AppendText("\n" + path);
            }
            else
            {
                failTextBox.AppendText("\n" + path + ", " + response.StatusCode);
            }
        }
        
    }
}
