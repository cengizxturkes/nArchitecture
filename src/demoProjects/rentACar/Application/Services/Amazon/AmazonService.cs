using Application.Features.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Services.Amazon
{
    public class AmazonService : IAmazonService
    {
        public async Task<CreatedProductDto> FindAsync(string asincode)
        {

            var client = new RestClient("https://www.amazon.com/dp/"+asincode);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("authority", "www.amazon.com");
            request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            request.AddHeader("accept-language", "tr-TR,tr;q=0.9,en-US;q=0.8,en;q=0.7");
            request.AddHeader("cache-control", "max-age=0");
            request.AddHeader("cookie", "session-id=134-1931731-0783505; session-id-time=2082787201l; i18n-prefs=USD; sp-cdn=\"L5Z9:TR\"; ubid-main=134-1459442-8853822; lc-main=en_US; session-token=\"b/zY6K5t5toD+LI2rb+0c6Wzue6lHc4Xznb1Xk7p2Hw9ahThZC0ugx3XZu6aNmHI+QDlAbJeBNt2xUyEgMsV0PShhR6ClzM0T8c23VN3Z8zvczvs9Pac67SIeyqJUbJBV5RUDFuZYGLJnewlcSA5e7l4imm43uZNnjSpXK8EQi1dYgZ1ZlEkcY+4L3PQiYtOKZ7WLXC4/jEKi6AS3mPdV8+6qVKYyh7BM6IJ+ll9dX4=\"; csm-hit=96F28V5SW47K61VG04DA+s-96F28V5SW47K61VG04DA|1666472990927; session-token=\"yeBoxMQL+UI8wUNYiM9V2JR0EiVyhS2bCacLKbBp0qgk5CHmID0A9Mam78kHhbq0fhq42qy0kEQd1VXgAwdq5VpGRMDFI85HpLhntsS2mw9EI0m19TjJb5f9WjetmBEVxp+RBYU9ZK3+BePk7iwAEPvHuTxED1upy/6FGE3dngnVBIoxJ9Kjtrpk28im7CO/x6I80PINCGMstGZrQC6ofV8UKIb/3VgzAeKE2jlW3Fk=\"");
            request.AddHeader("device-memory", "8");
            request.AddHeader("downlink", "10");
            request.AddHeader("dpr", "1");
            request.AddHeader("ect", "4g");
            request.AddHeader("rtt", "200");
            request.AddHeader("sec-ch-device-memory", "8");
            request.AddHeader("sec-ch-dpr", "1");
            request.AddHeader("sec-ch-ua", "\"Not-A.Brand\";v=\"99\", \"Opera\";v=\"91\", \"Chromium\";v=\"105\"");
            request.AddHeader("sec-ch-ua-mobile", "?0");
            request.AddHeader("sec-ch-ua-platform", "\"Windows\"");
            request.AddHeader("sec-ch-viewport-width", "1920");
            request.AddHeader("sec-fetch-dest", "document");
            request.AddHeader("sec-fetch-mode", "navigate");
            request.AddHeader("sec-fetch-site", "none");
            request.AddHeader("sec-fetch-user", "?1");
            request.AddHeader("upgrade-insecure-requests", "1");
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36 OPR/91.0.4516.67";
            request.AddHeader("viewport-width", "1920");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            HtmlDocument doc = new HtmlDocument();
            HtmlDocument docımage = new HtmlDocument();
            doc.LoadHtml(response.Content);
            
            var productNameNode = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[2]/div[9]/div[6]/div[4]/div[1]/div/h1/span");
           
            if (productNameNode == null) return new();
            var productName = productNameNode.InnerText.Trim();
            var productImageNode = doc.DocumentNode.SelectSingleNode("//*[@id=\"landingImage\"]").Attributes["src"].Value;
           if (productImageNode == null) return new();
           var productImage = productImageNode;
          
           
            var test = doc.DocumentNode.SelectSingleNode("//*[@id=\"productDetails_detailBullets_sections1\"]");
            
           
            CreatedProductDto productDto = new();
           productDto.ImageUrl = productImage;
            productDto.Name = productName;
            List<AmazonProp> items = new List<AmazonProp>();


           

            foreach (var row in test.ChildNodes)
            {
                var th = row.SelectSingleNode("th");
                var td = row.SelectSingleNode("td");

                if (th != null)
                {

                    items.Add(new() { name = th.InnerText.ToLower().Trim(), value = td.InnerText.ToLower().Trim() });
                }
            }

            //foreach (var row in propertiesNode.ChildNodes)
            //{
            //    var th = row.SelectSingleNode("/th");
            //    var td = row.SelectSingleNode("/td");
            //}


            //var url = "https://www.amazon.com/dp/B004SC6YV0";
            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
            //var response =await client.GetAsync(url);
            //var ress=await response.Content.ReadAsStringAsync();
            //// From Web
            //var web = new HtmlWeb();
            //web.OverrideEncoding = Encoding.UTF8;
            //var doc = web.Load(url);
            //string unicodestring = doc.DocumentNode.InnerHtml;

            //// Create two different encodings.
            //Encoding ascii = Encoding.ASCII;
            //Encoding unicode = Encoding.Unicode;
            ////Encoding Utf8 = Encoding.UTF8;

            //// // Convert the string into a byte array.
            //byte[] unicodeBytes = unicode.GetBytes(unicodestring);

            //// // Perform the conversion from one encoding to the other.
            //byte[] ascibytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            //// // Convert the new byte[] into a char[] and then into a string.
            //char[] asciiChars = new char[ascii.GetCharCount(ascibytes, 0, ascibytes.Length)];
            //ascii.GetChars(ascibytes, 0, ascibytes.Length, asciiChars, 0);
            //string asciiString = new string(asciiChars);
        
           
            var weightProp = items.Where(x => x.name.Contains("weight")).FirstOrDefault();
            if (weightProp != null)
            {

                productDto.Weight = GetNumbers(weightProp.value);
            }
           
            var capacityProp = items.Where(x => x.name.Contains("capacity")).FirstOrDefault();
            if (capacityProp != null)
            {

                productDto.Capacity = GetNumbers(capacityProp.value);
            }
           
            var dimensionsProp = items.Where(x => x.name.Contains("dimensions")).Last();
            if (dimensionsProp != null)
            {
                productDto.Width = GetNumbers(dimensionsProp.value);
                productDto.Lenght = GetNumbers2(dimensionsProp.value);

            }

            return productDto; 
        }

        private static double GetNumbers(string input)
        {
            return Convert.ToDouble(input.Split(" ").FirstOrDefault().Replace(".",",") ?? "0");
        }
        private static double GetNumbers2(string input)
        {
            char[] ayrac = {  'x' };
            string[] parcalar = input.Split(ayrac);
            return Convert.ToDouble(input.Split(" x ").FirstOrDefault().Replace(".", ",") ?? "0");
        }

    }
    class AmazonProp
    {
        public string name { get; set; }
        public string value { get; set; }
    }

}
