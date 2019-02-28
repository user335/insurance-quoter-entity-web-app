using insuranceQuoter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace insuranceQuoter.Controllers
{
    public class HomeController : Controller
    {
        private readonly string connectionString = @"Data Source=DESKTOP-LQ59KO4\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();  
        }
        [HttpPost]
        public ActionResult GetQuote(string firstName, string lastName, string emailAddress, DateTime dateOfBirth,
                                    int carYear, string carMake, string carModel, 
                                    int numberOfSpeedingTickets, bool fullCoverageDesired = false, bool hadAPreviousDUI = false)
        {
            Console.Write("had a dui = " + hadAPreviousDUI);
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) ||
                dateOfBirth == null || carYear < 1900 || carYear > DateTime.Now.Year + 1 || string.IsNullOrEmpty(carMake) ||
                string.IsNullOrEmpty(carModel) || numberOfSpeedingTickets < 0)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                var quote = new Quote();
                using (InsuranceQuotesEntities db = new InsuranceQuotesEntities())
                {
                    quote.FirstName = firstName;
                    quote.LastName = lastName;
                    quote.EmailAddress = emailAddress;
                    quote.DateOfBirth = dateOfBirth;
                    quote.CarYear = carYear;
                    quote.CarMake = carMake;
                    quote.CarModel = carModel;
                    quote.HadAPreviousDUI = hadAPreviousDUI;
                    quote.NumberOfSpeedingTickets = numberOfSpeedingTickets;
                    quote.FullCoverageDesired = fullCoverageDesired;
                    decimal pricequote = 50;
                    if (dateOfBirth.AddYears(18) > DateTime.Today) pricequote += 100;
                    else if (dateOfBirth.AddYears(25) > DateTime.Today) pricequote += 25;
                    else if (dateOfBirth.AddYears(100) < DateTime.Today) pricequote += 25;
                    if (carYear < 2000) pricequote += 25;
                    else if (carYear > 2015) pricequote += 25;
                    if (carMake.ToLower() == "porsche")
                    {
                        pricequote += 25;
                        if (carModel.ToLower() == "911 carrera") pricequote += 25;
                    }
                    if (numberOfSpeedingTickets > 0) pricequote += numberOfSpeedingTickets * 10;
                    if (hadAPreviousDUI) pricequote *= 1.25m;
                    if (fullCoverageDesired) pricequote *= 1.5m;

                    quote.QuotedPrice = pricequote.ToString("0.##");
                    db.Quotes.Add(quote);
                    db.SaveChanges();
                }
                return View(quote);
            }
        }

        public ActionResult Admin()
        {
            using (InsuranceQuotesEntities db = new InsuranceQuotesEntities())
            {
                var quotes = db.Quotes;
                var quoteVms = new List<QuoteVm>();
                foreach (var quote in quotes)
                {
                    var quoteVm = new QuoteVm();
                    quoteVm.firstName = quote.FirstName;
                    quoteVm.lastName = quote.LastName;
                    quoteVm.emailAddress = quote.EmailAddress;
                    quoteVm.quotedPrice = quote.QuotedPrice;
                    quoteVms.Add(quoteVm);
                }
                return View(quoteVms);
            } 
        }
    }
}