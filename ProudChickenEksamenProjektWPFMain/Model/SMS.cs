using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudChickenEksamen.Model
{
    /// <summary>
    /// Denne del er lavet af Christian, Jonas, Thomas og William med Pair-Programming på storskærm
    /// </summary>
    class SMS
    {
        public int ID { get; set; }
        public string SMSStandardBesked { get; set; }
        public string StandardBesked
        {
            get { return $"{ID}: {SMSStandardBesked}"; }
        }
    }
}

