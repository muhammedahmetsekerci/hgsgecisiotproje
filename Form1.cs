using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hgsgecisiotproje
{
    public partial class Form1 : Form
    {
        int bakiye;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "hRhqH8Wl5bMv1unhUpXBWfnFuC9Thcu37zu4QId4",
            BasePath = "https://iot2022test-f698d-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string eventName = "iotproje1";
            string secretKey = "ewnhNKulL_QLHdqooYhqKsY_Mr44msi-V7NxIc2Gr2X";

            // Set up the HTTP client and request
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://maker.ifttt.com/trigger/{eventName}/with/key/{secretKey}?value1={100}");

            // Send the request and handle the response
            HttpResponseMessage response = client.SendAsync(request).Result;
            try
            {
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Success");
                
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int tutar = Convert.ToInt32(textBox1.Text);
            bakiye = client.Get("bakiye").ResultAs<int>();

            bakiye = bakiye + tutar;
            client.Set("bakiye", bakiye);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new FirebaseClient(config);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FirebaseResponse response = client.Get("bakiye");
            label2.Text = response.ResultAs<string>();
        }
    }
}
