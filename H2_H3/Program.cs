using System;
using System.Net.Http;
using HtmlAgilityPack;

internal class Program
{
    static async System.Threading.Tasks.Task Main(string[] args)
    {
        string url = "https://auto.ria.com/uk/newauto/marka-audi/model-rs7-sportback/year-2023/";  
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string htmlContent = await response.Content.ReadAsStringAsync();

                    HtmlDocument htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(htmlContent);

                    var h2Nodes = htmlDocument.DocumentNode.SelectNodes("//h2");
                    var h3Nodes = htmlDocument.DocumentNode.SelectNodes("//h3");

                    if (h2Nodes != null)
                    {
                        foreach (var node in h2Nodes)
                        {
                            Console.WriteLine("Заголовок (h2): " + node.InnerText);
                        }
                    }

                    if (h3Nodes != null)
                    {
                        foreach (var node in h3Nodes)
                        {
                            Console.WriteLine("Заголовок (h3): " + node.InnerText);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Помилка при виконанні запиту. Код статусу: " + (int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Виникла помилка: " + ex.Message);
            }
        }
    }
}