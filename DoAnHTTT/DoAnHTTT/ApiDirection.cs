﻿using DoAnHTTT.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DoAnHTTT
{
    public class ApiDirection
    {
        private JsonSerializer _serializer = new JsonSerializer();
        private static ApiDirection _ServiceClientInstance;
        public static ApiDirection ServiceClientInstance
        {
            get
            {
                if (_ServiceClientInstance == null)
                {
                    _ServiceClientInstance = new ApiDirection();
                }
                return _ServiceClientInstance;
            }
        }
        private HttpClient client = new HttpClient();
        
        public async Task<GoogleDirection> GetDirection(string startX, string startY, string desX, string desY)
        {
            GoogleDirection googleDirection = new GoogleDirection();

            var response = await client.GetAsync($"https://maps.googleapis.com/maps/api/directions/json?destination={desX},{desY}&origin={startX},{startY}&mode=driving&key={AppConstrant.GoogleApiKey}");//.ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    googleDirection = await Task.Run(() => JsonConvert.DeserializeObject<GoogleDirection>(json)).ConfigureAwait(false);
                }
            }
            return googleDirection;
        }
    }
}
