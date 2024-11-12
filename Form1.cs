using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Language_TRANSLATOR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtUserInput.Focus();
            btnClear.Focus();
            btnConvert.Focus();
            btnExit.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClear_Click(object sender, EventArgs e) 
        {
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblConversion_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void btnConvert_Click(object sender, EventArgs e)
        {
           
            string phrase = txtUserInput.Text;  

            if (string.IsNullOrWhiteSpace(phrase))
            {
                MessageBox.Show("Please enter some text to translate.");
                return;
            }

            var client = new HttpClient();
            var requestUri = "https://microsoft-translator-text-api3.p.rapidapi.com/translate?to=es&from=en&textType=plain";

   
            var data = new[]
            {
        new { text = phrase }  
    };

            // Serialize the data object to JSON
            var jsonData = JsonConvert.SerializeObject(data);

            
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri),
                Headers =
        {
            { "x-rapidapi-key", "c816e780famsha954dc5b767d651p117c51jsn4d78985866be" },
            { "x-rapidapi-host", "microsoft-translator-text-api3.p.rapidapi.com" },
        },
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };

            try
            {
                
                using (var response = await client.SendAsync(request))
                {
                    
                    response.EnsureSuccessStatusCode();

                    
                    var body = await response.Content.ReadAsStringAsync();

                   
                    var translation = ExtractTranslationFromResponse(body);

                    // Display the translated text on the label
                    lblConversion.Text = translation;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

       
        private string ExtractTranslationFromResponse(string responseBody)
        {
            
            try
            {
                var responseJson = JsonConvert.DeserializeObject<dynamic>(responseBody);
                return responseJson[0]?.translations[0]?.text ?? "No translation found.";
            }
            catch
            {
                return "Error parsing translation.";
            }
        }


        private void btnClear_Click_1(object sender, EventArgs e)
        {
            txtUserInput.Clear();
        }
    }
  }

    
