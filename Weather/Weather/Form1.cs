using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using System.IO;


namespace Weather
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?q=Moscow&APPID=339ec1932650a0effff53b12ca9f8fb4");

            request.Method = "POST";

            request.ContentType = "application/x-www-urlencoded";

            WebResponse responce = await request.GetResponseAsync();

            string answer = string.Empty;

            using (Stream s = responce.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }
            responce.Close();

            richTextBox1.Text = answer;

            OpenWeather.OpenWeather oW = JsonConvert.DeserializeObject<OpenWeather.OpenWeather>(answer);

            label1.Text = "General weather condition: " + oW.weather[0].main;

            label2.Text = "Description: " + oW.weather[0].description;

            label3.Text = "Temperature (°С): " + oW.main.temp.ToString("0.##"); // форматирование (урез ненужных чисел)

            label6.Text = "speed (m/s): " + oW.wind.speed.ToString();

            label7.Text = "direction °: " + oW.wind.deg.ToString();

            label4.Text = "Humidity (%): " + oW.main.humidity.ToString();

            label5.Text = "Atmospheric pressure (mm): " + ((int)oW.main.pressure).ToString();

            label8.Text = "City: " + oW.name.ToString();
        }
    }
}
