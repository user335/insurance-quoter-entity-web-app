using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace insuranceQuoter.ViewModels
{
    public class QuoteVm
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string quotedPrice { get; set; }
    }
}