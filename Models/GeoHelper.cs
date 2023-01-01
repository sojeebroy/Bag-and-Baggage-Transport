using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AspDotNetSummerProject.Models
{
    public class GeoHelper
    {
        private readonly HttpClient _httpClient;
        //create constructor and call HttpClient
        public GeoHelper()
        {
            _httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(5)
            };
        }
        private async Task<string> GetIPAddress()
        {
            var ipAddress = await _httpClient.GetAsync($"http://ipinfo.io/ip");
            if (ipAddress.IsSuccessStatusCode)
            {
                var json = await ipAddress.Content.ReadAsStringAsync();
                return json.ToString();
            }
            return "";

        }
        public async Task<string> GetGeoInfo()
        {
            //I have already created this function under GeoInfoProvider class.
            var ipAddress = await GetIPAddress();
            // When geting ipaddress, call this function and pass ipaddress as given below
            var response = await _httpClient.GetAsync($"http://api.ipstack.com/" + ipAddress + "?access_key=d8dd3e8c997cf67b5d9a8fb3a0439e6f");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return json;
            }
            return null;
        }
    }
}