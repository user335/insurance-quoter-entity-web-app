using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace insuranceQuoter.Models
{
    public class InsuranceQuote
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int carYear { get; set; }
        public string carMake { get; set; }
        public string carModel { get; set; }
        public bool hadAPreviousDUI { get; set; }
        public int numberOfSpeedingTickets { get; set; }
        public bool fullCoverageDesired { get; set; }
        public int quotedPrice { get; set; }
    }
}