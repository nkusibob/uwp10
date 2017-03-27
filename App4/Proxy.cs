using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;


namespace App4

{
    [DataContract]
    public class Proxy
    {

        static string adresse = "http://172.16.1.113";
        //static String adresse = "http://172.18.99.128";
        public static async Task<RootObject> GetWeather()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://www.prevision-meteo.ch/services/json/Saint-Gilles");
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject)serializer.ReadObject(ms);
            return data;
        }
        public static async Task<RootObject2> GetWeatherDesciption()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://api.wunderground.com/api/7e314ab733d1d056/conditions/q/Belgium/Bruxelles.json");
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject2));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject2)serializer.ReadObject(ms);
            return data;

        }
        public static async Task<List<Uri>> GetMedia()
        {

            int iLine = 0;
            try
            {

                List<Uri> target = new List<Uri>();
                iLine = 47;
                HtmlDocument document = new HtmlDocument();

                var httpClient = new HttpClient();
                var urlVideos = adresse + "/Videos";
                iLine = 52;
                var response = await httpClient.GetAsync(urlVideos);
                iLine = 54;
                var result = await response.Content.ReadAsStringAsync();
                iLine = 56;
                string htmlString = result;
                iLine = 58;
                document.LoadHtml(htmlString);
                iLine = 60;
                var collection = document.DocumentNode.DescendantsAndSelf();
                iLine = 62;
                foreach (HtmlNode link in collection)
                {
                    if (link.Attributes.Contains("href") && !string.IsNullOrEmpty(link.Attributes["href"].Value.Trim().Trim('/')))
                    {
                        target.Add(new Uri(adresse + "" + link.Attributes["href"].Value));
                    }
                }
                iLine = 70;
                return target;
            }
            catch (Exception)
            {
                string errors = "Proxy.getMedia" + iLine.ToString();
                App.ProxyErrors = errors;
                throw;
            }

        }

        public static async Task<string> GetAddedMessage()
        {
            try
            {
                HtmlDocument document = new HtmlDocument();
                var httpClient = new HttpClient();
                var urlVideos = adresse + "/Guest/Guest.txt";
                var response = await httpClient.GetAsync(urlVideos);
                var result = await response.Content.ReadAsStringAsync();
                string htmlString = result;

                return htmlString;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static async Task<List<Uri>> GetPictures()
        {
            List<Uri> target = new List<Uri>();
            HtmlDocument document = new HtmlDocument();
            var httpClient = new HttpClient();
            var urlVideos = adresse + "/Pictures";
            var response = await httpClient.GetAsync(urlVideos);
            var result = await response.Content.ReadAsStringAsync();
            string htmlString = result;

            document.LoadHtml(htmlString);
            var collection = document.DocumentNode.DescendantsAndSelf();
            foreach (HtmlNode link in collection)
            {
                if (link.Attributes.Contains("href") && !string.IsNullOrEmpty(link.Attributes["href"].Value.Trim().Trim('/')))
                {
                    target.Add(new Uri(adresse + "" + link.Attributes["href"].Value));

                }
            }



            return target;

        }

        [DataContract]

        public class Weather
        {
            [DataMember]
            public int id { get; set; }

            [DataMember]
            public string description { get; set; }

        }


        [DataContract]
        public class CurrentObservation
        {

            [DataMember]
            public string weather { get; set; }


        }

        [DataContract]

        public class current_condition
        {
            [DataMember]
            public int tmp { get; set; }
            //[DataMember]
            //public string main { get; set; }
            [DataMember(Name = "condition")]
            public string Condition { get; set; }
            [DataMember]
            public string icon { get; set; }
        }

        [DataContract]
        public class RootObject
        {
            [DataMember]
            public current_condition current_condition { get; set; }

        }
        [DataContract]
        public class RootObject2
        {
            [DataMember]
            public CurrentObservation current_observation { get; set; }

        }
    }
}


