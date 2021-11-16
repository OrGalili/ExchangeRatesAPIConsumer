using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRatesAPIConsumer
{
    /// <summary>
    /// In order to make a multiselect in the combobox and remeber the choise, we need to create a class with property with the data
    /// to remember : Key of symbol is the key in the json, Value is the value of the property and isChecked is for the checkbox if the
    /// user chosed this symbol.
    /// </summary>
    class SymbolViewModel
    {
        public bool IsChecked { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public SymbolViewModel(string key, string value)
        {
            IsChecked = false;
            Key = key;
            Value = value;
        }
    }
}
