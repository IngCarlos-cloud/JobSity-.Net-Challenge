using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JobSityChallenge.Bot
{
    public class BrokerBot
    {
        private const string BrokerPath = @"Software\JobSity\Broker";
        public string getStockQuote(string stockCode)
        {
            brokerResponse StockQuote = new brokerResponse();
            
            try
            {
                string stockResponse = callService(stockCode);

                if (string.IsNullOrEmpty(stockResponse))
                    throw new Exception();

                    StockQuote.message = getQuote(stockResponse);
                if (string.IsNullOrEmpty(StockQuote.message))
                    throw new Exception();

                StockQuote.Result = true;

                                                
            }
            catch (Exception e)
            {
                StockQuote.message =$"Unable to retrive a Quote for the stock requested.";
                StockQuote.Result = false;
            }

            return JsonConvert.SerializeObject(StockQuote); 
        }

        private string callService(string stockCode)
        {

            try
            {
                string response = string.Empty;
                string fullurl = string.Empty;
                using (RegistryKey dbKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64).OpenSubKey(BrokerPath))
                {
                    if (dbKey != null)
                    {
                        fullurl = string.Format((string)dbKey.GetValue("Url"), stockCode);

                    }

                }
                //string fullurl = $"https://stooq.com/q/l/?s={stockCode}&f=sd2t2ohlcv&h&e=csv";
                
                var request = (HttpWebRequest)WebRequest.Create(fullurl);
                request.Method = "GET";
                request.ContentType = "text/csv";
                
                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }

                return response;
            }
            catch (Exception)
            {
                throw new Exception("Unable to conect with server");
            }
            
        }

        private string getQuote(string stockResponse)
        {
            try
            {
                stockResponse = stockResponse.Replace(Environment.NewLine, "|");

                var stockList = stockResponse.Split('|').Where(x => !string.IsNullOrEmpty(x)).ToArray();

                var headersName = stockList[0].Split(',');
                var stockJson = stockList.Skip(1)
                                 .Select((x) => x.Split(',')
                                                 .Select((y, i) => new
                                                 {
                                                     Key = headersName[i].Trim('"'),
                                                     Value = y.Trim('"')
                                                 })
                                                 .ToDictionary(d => d.Key, d => d.Value));

                var JsonString = JsonConvert.SerializeObject(stockJson, Formatting.Indented);

                var jsontxt = JToken.Parse(JsonString);
                var closeValue = ((JValue)jsontxt[0]["Close"])?.ToString() ?? string.Empty;
                if (isNumber(closeValue))
                    return closeValue;
                else
                return  null ;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private bool isNumber(string value)
        {
            try
            {
                double.Parse(value);
                return true;
            }
            catch
            { return false; }
              
            
        }


        class brokerResponse
        { 
            public bool Result { get; set; }
            public string message { get; set; }
            
        }
    }
}
