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
        public void bakiyeekle()
        {
            client.Set("bakiye", bakiye);

        }
        public void hgsgecison()
        {
            client.Set("hgsgecis", "gecis yapıldı");
        }
        public void hgsgecisoff()
        {
            client.Set("hgsgecis", "ödeme yapıldı");
        }
    private void button1_Click(object sender, EventArgs e)
        {
            string tarih = dateTimePicker1.Value.AddDays(10).ToString("dd/MM/yyyy");
            
            string eventName = "";
            if (smschbox.Checked && mailchbox.Checked)
            {
                eventName = "iotproje";
            }
            else if (smschbox.Checked)
            {
                eventName = "iotprojesms";
            }
            else if (mailchbox.Checked)
            {
                eventName = "iotprojemail";
            }
            else
            {
                MessageBox.Show("Mesaj alma tercihleri boş bırakılamaz.!");
                return;
            }
            
            string secretKey = "ewnhNKulL_QLHdqooYhqKsY_Mr44msi-V7NxIc2Gr2X";

            // Set up the HTTP client and request
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://maker.ifttt.com/trigger/{eventName}/with/key/{secretKey}?value1={"100TL "} &value2={tarih} ");


            // Send the request and handle the response
            HttpResponseMessage response = client.SendAsync(request).Result;
            try
            {
                response.EnsureSuccessStatusCode();
                hgsgecison();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("HATA!: Hata mesajı : " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int tutar = Convert.ToInt32(textBox1.Text);
            bakiye = client.Get("bakiye").ResultAs<int>();

            bakiye = bakiye + tutar;
            bakiyeekle();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new FirebaseClient(config);
            FirebaseResponse response = client.Get("bakiye");
            bakiye = response.ResultAs<int>();
            label2.Text = response.ResultAs<string>();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FirebaseResponse response = client.Get("bakiye");
            label2.Text = response.ResultAs<string>();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tarih = dateTimePicker1.Value.AddDays(10).ToString("dd/MM/yyyy");

            string eventName = "";
            if (smschbox.Checked && mailchbox.Checked)
            {
                eventName = "iotprojeodeme";
            }
            else if (smschbox.Checked)
            {
                eventName = "iotprojeodemesms";
            }
            else if (mailchbox.Checked)
            {
                eventName = "iotprojeodememail";
            }
            else
            {
                MessageBox.Show("Mesaj alma tercihleri boş bırakılamaz.!");
                return;
            }

            string secretKey = "ewnhNKulL_QLHdqooYhqKsY_Mr44msi-V7NxIc2Gr2X";

            // Set up the HTTP client and request
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://maker.ifttt.com/trigger/{eventName}/with/key/{secretKey}?value1={"100TL "} &value2={tarih} ");
            
            
            

            // Send the request and handle the response
            HttpResponseMessage response = client.SendAsync(request).Result;
            try
            {
                response.EnsureSuccessStatusCode();
                bakiye = bakiye - 100;
                bakiyeekle();
                hgsgecisoff();

            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("HATA!: Hata mesajı : " + ex.Message);
            }
        }
    }
}
