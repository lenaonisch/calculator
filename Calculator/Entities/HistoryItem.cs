using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Entities
{
    public class HistoryItem
    {
        public long Id { get; set; }
        public string Operation { get; set; }
        public double Argument1 { get; set; }
        public double? Argument2 { get; set; }
        public double? Result { get; set; }
        public DateTime DateTime { get; set; }
        public string Expression { get; set; }
    }
}
