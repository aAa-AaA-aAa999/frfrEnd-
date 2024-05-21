using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appFrench
{
    public partial class FormTranslate : Form
    {
        public FormTranslate()
        {
            InitializeComponent();
        }

        private void buttonTraslateEnter_ClickAsync(object sender, EventArgs e)
        {
            TtextGetButtonAsync();
        }
        private async Task TtextGetButtonAsync() {
            string textT = textBoxWord.Text;
            string translatedText = await TranslateTextAsync(textT, "fr", "ru");
            labelTranslete.Text = translatedText;
        }
        private async Task<string> TranslateTextAsync(string text, string source, string target)
        {
            string IAM_TOKEN = "t1.9euelZqNkJHGy8qLmYzMnc7Nycqdje3rnpWaiZGOlcuRm8nPlJaPnM-LiZ3l8_cFDhNR-e8OaCg9_d3z90U8EFH57w5oKD39zef1656Vms_PyJ6PnZGXlpWKlsrMlsae7_zF656Vms_PyJ6PnZGXlpWKlsrMlsae.Clk-iAt8lkrCJ7FFFRF3HqZN86XOw_JX2xJForkBo1UYlKJTBblI8q1VIQ7JDHF19NW5Oacedv9gaRQtn_rOBQ";
            string folder_id = "b1ge70ic3ls738d0gvfg";
            string source_language = "fr"; // Set the source language to French
            string target_language = "ru"; // Set the target language to Russian

            var body = new
            {
                sourceLanguageCode = source_language,
                targetLanguageCode = target_language,
                texts = text, // Здесь передаем один текст, а не массив texts
                folderId = folder_id
            };

            var jsonBody = JsonConvert.SerializeObject(body);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", IAM_TOKEN);
                httpClient.DefaultRequestHeaders.Add("ContentType", "application/json"); // Исправляем "ContentType" на "Content-Type"

                var content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("https://translate.api.cloud.yandex.net/translate/v2/translate", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    string pattern = @"\""text\"":\s*\""([^\""]*)\""";

                    Match match = Regex.Match(responseContent, pattern);
                    if (match.Success)
                    {
                        string translatedText = match.Groups[1].Value.Trim();
                        Console.WriteLine(translatedText);
                        return translatedText;
                    }
                    return $"Error: {response.StatusCode}";
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return $"Error: {response.StatusCode}";
                }
            }

        }
    }
}
