using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EBC.SurfaceComponent.Data.Entities.DataEntities;
using Microsoft.EBC.SurfaceComponent.Common;


namespace App10
{
    
   
    [DataContract]
    public class ServiceNameGuest
    {
        private static List<string> myGuestColString = new List<string>();

        public static async Task<IList<MyObject>> GetBriefingsByDateAndCenter(DateTime start, DateTime end, string brfCenter)
        {
          
            try
            {
                string briefing = string.Format(Constants.BRIFINGS_BY_CENTER_DATE, start.ToString("s", System.Globalization.CultureInfo.InvariantCulture), end.ToString("s", System.Globalization.CultureInfo.InvariantCulture), brfCenter);
                string str = await ServiceInvoker.GetResponseFromServiceAsync(briefing);
                var serializer = new DataContractJsonSerializer(typeof(IList<MyObject>));
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(str));
                var data = (IList<MyObject>)serializer.ReadObject(ms);
                if (data != null)
                {
                    foreach (var item in data)
                    {
                       // App.GuestFromService.Add(item.BriefingTitle);

                    }


                  
                }

                //log("GetBriefingDetails: " + str + "\r\n");
                return data;
            }
            catch (Exception ex)
            {
                //log(ex + "\r\n");
                throw ex;
            }
           
        }

        [DataContract]
        public class MyObject
        {
            [DataMember]
            public string BriefingId { get; set; }
            [DataMember]
            public string BriefingRoom { get; set; }
            [DataMember]
            public string BriefingTitle { get; set; }
            [DataMember]
            public string SessionDate { get; set; }
        }
       
    }
}
