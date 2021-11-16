using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ExchangeRatesAPIConsumer
{
    class ViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        
        private List<SymbolViewModel> symbolList = new List<SymbolViewModel>();
        public List<SymbolViewModel> SymbolList
        {
            get
            {
                var client = new RestClient("http://api.exchangeratesapi.io/v1/symbols?access_key=a6ff2f32bde7c95ef9c2faf5a6213fc4");
                var response = client.Execute(new RestRequest());

                JToken outer = JToken.Parse(response.Content);
                JObject inner = outer["symbols"].Value<JObject>();

                var coinDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(inner.ToString());

                foreach (KeyValuePair<string, string> coin in coinDic)
                    symbolList.Add(new SymbolViewModel(coin.Key, coin.Value));

                return symbolList;
            }
            set {; }
        }

        private Dictionary<string, string> keyValueCoins = new Dictionary<string, string>();

        

        public Dictionary<string, string> KeyValueCoins
        {
            get { return keyValueCoins; }
            set
            {
                keyValueCoins = value;
                OnPropertyChanged();
            }
        }

        public RelayCommandNoParameter SearchCommand { get; private set; }

        public ViewModel()
        {
            SearchCommand = new RelayCommandNoParameter(getLatestChoosedCoins);
        }

        private void getLatestChoosedCoins()
        {
            List<SymbolViewModel> checkedSymbols = symbolList.FindAll(coin => coin.IsChecked);
            string symbols = "";
            foreach (SymbolViewModel symbol in checkedSymbols)
            {
                symbols += symbol.Key + ",";
            }
            if (symbols == "")
                return;

            string baseurl = "http://api.exchangeratesapi.io/v1/latest?access_key=a6ff2f32bde7c95ef9c2faf5a6213fc4&symbols=";
            string fullUrl = baseurl + symbols.Remove(symbols.Length - 1);

            var client = new RestClient(fullUrl);
            var response = client.Execute(new RestRequest());

            JToken outer = JToken.Parse(response.Content);
            JObject inner = outer["rates"].Value<JObject>();

            KeyValueCoins = JsonConvert.DeserializeObject<Dictionary<string, string>>(inner.ToString());

        }
    }
}
