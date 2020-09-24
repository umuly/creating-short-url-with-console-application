using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;



namespace CSharp
{
   
    public class Project
    {
        static void Main()
        {
            string EMail, password;
            string url,token1;
        
            FileStream FS = new FileStream("database.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter SW = new StreamWriter(FS);

            Console.Write("Please enter the EMail:");
            EMail = Console.ReadLine();
            

            Console.Write("Please enter the password:");
            password = Console.ReadLine();
            

            Console.WriteLine("Getting token is started...");

            var client = new RestClient("https://umuly.com/api/Token?Email="+EMail+"&Password="+password+"");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
            
            tokenClass token = JsonConvert.DeserializeObject<tokenClass>(response.Content.ToString());
            Console.WriteLine(response.Content);

            token1 = token.token;
            SW.WriteLine(token1);

            Console.Write("Enter the url to shorten:");
            url = Console.ReadLine();

            var client1 = new RestClient("https://umuly.com/api/url/RedirectUrlAdd");
            client.Timeout = -1;
            var request1 = new RestRequest(Method.POST);
            request1.AlwaysMultipartFormData = true;
            request1.AddParameter("RedirectUrl", url);
            request1.AddParameter("Title", "");
            request1.AddParameter("Description", "");
            request1.AddParameter("Tags", "");
            request1.AddParameter("DomainId", "");
            request1.AddParameter("Code", "");
            request1.AddParameter("UrlAccessType", "5");
            request1.AddParameter("SpecificMembersOnly", "");
            request1.AddParameter("UrlType", "1");
            request1.AddHeader("Authorization", "Bearer " + token1 + "");
            IRestResponse response1 = client1.Execute(request1);
            Console.WriteLine(response1.Content);



            shorturlresponseClass shorturlresponse = JsonConvert.DeserializeObject<shorturlresponseClass>(response1.Content.ToString());
            Console.WriteLine(shorturlresponse.item.shortUrl);

            SW.Close();
            
            
        }

    }
}
