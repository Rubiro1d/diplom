using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using LogSystem.Models;
using LogSystemML.Model;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LogSystem.Controllers
{
    public class HomeController : Controller
    {
        LogDatabaseEntities dc = new LogDatabaseEntities();
        // GET: Home
        [HttpGet]
        public ActionResult Admin()
        {
            /*var ListOfDriverNames = dc.Drivers.ToList();
            ViewBag.ListOfDriverNames = new SelectList(ListOfDriverNames, "DriverId", "DriverName");

             var ListOfAutotypeNames = dc.Autotypes.ToList();
             ViewBag.ListOfAutotypeNames = new SelectList(ListOfAutotypeNames, "AutotypeId", "AutotypeName");

             var ListOfOrderNames = dc.Orders.ToList();
             ViewBag.ListOfOrderNames = new SelectList(ListOfOrderNames, "OrderId", "OrderName");*//*
 */
            return View();
        }

        [HttpPost]
        public ActionResult Admin(ModelInput input)
        {
            ViewBag.Result = 120;
            /*ViewBag.Result = "";*/
            var stockPredictions = ConsumeModel.Predict(input);
            ViewBag.Result = Convert.ToInt32(stockPredictions.Score);
            /*ViewBag.Result = stockPredictions.Score;*/

            ViewData["WeatherType"] = input.WeatherType;
            ViewData["Visibility"] = input.Visibility;
            ViewData["Temperature"] = input.Temperature;
            ViewData["LocationId"] = input.LocationId;
            ViewData["LocationId2"] = input.LocationId2;
            ViewData["DriverId"] = input.DriverId;
            ViewData["AutotypeId"] = input.AutotypeId;
            ViewData["OrderId"] = input.OrderId;


            return PartialView("_AdminPartial");

        }

        public JsonResult GetTrips()
        {
            List<LocationView> TripList = dc.Trips.Select(x => new LocationView
            {
                TripId = x.TripId,
                Start = x.Start,
                End = x.End,
                WeatherType = x.WeatherType,
                Visibility = x.Visibility,
                Temperature = x.Temperature,

                LocationName = x.Location.LocationName,
                LocationName2 = x.Location1.LocationName,
                LocationType = x.Location.LocationType,
                LocationAddress = x.Location.LocationAddress,
                LocationAddress2 = x.Location1.LocationAddress,
                DriverName = x.Driver.DriverName,
                OrderName = x.Order.OrderName,
                AutotypeName = x.Autotype.AutotypeName,

                LocationId = x.LocationId,
                LocationId2 = x.LocationId2,
                DriverId = x.DriverId,
                AutotypeId = x.AutotypeId,
                OrderId = x.OrderId,

                Time = x.Time

            }).ToList();
            return Json(TripList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveEvent(Trip e)
        {
            var status = false;
            using (LogDatabaseEntities dc = new LogDatabaseEntities())
            {
                if (e.TripId > 0)
                {
                    //Update the event
                    var v = dc.Trips.Where(a => a.TripId == e.TripId).FirstOrDefault();
                    if (v != null)
                    {

                        v.Start = e.Start;
                        v.End = e.End;
                        v.WeatherType = e.WeatherType;
                        v.Visibility = e.Visibility;
                        v.Temperature = e.Temperature;
                        v.LocationId = e.LocationId;
                        v.LocationId2 = e.LocationId2;
                        v.DriverId = e.DriverId;
                        v.AutotypeId = e.AutotypeId;
                        v.OrderId = e.OrderId;
                        v.Time = e.Time;
                    }
                }
                else
                {

                    dc.Trips.Add(e);
                }
                dc.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int tripId)
        {
            var status = false;
            using (LogDatabaseEntities dc = new LogDatabaseEntities())
            {
                var v = dc.Trips.Where(a => a.TripId == tripId).FirstOrDefault();
                if (v != null)
                {
                    dc.Trips.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        public JsonResult GetDriver()
        {
            dc.Configuration.ProxyCreationEnabled = false;
            var ListOfDrivers = dc.Drivers.Select(x => new { x.DriverId, x.DriverName });
            return Json(ListOfDrivers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAuto()
        {
            dc.Configuration.ProxyCreationEnabled = false;
            var ListOfAutos = dc.Autotypes.Select(x => new { x.AutotypeId, x.AutotypeName });
            return Json(ListOfAutos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOrder()
        {
            dc.Configuration.ProxyCreationEnabled = false;
            var ListOfOrders = dc.Orders.Select(x => new { x.OrderId, x.OrderName });
            return Json(ListOfOrders, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLocType()
        {
            dc.Configuration.ProxyCreationEnabled = false;
            var ListOfLocationTypes = dc.Locations.GroupBy(f => f.LocationType).Select(g => g.FirstOrDefault()).ToList();
            return Json(ListOfLocationTypes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLocName(string locationType)
        {
            dc.Configuration.ProxyCreationEnabled = false;
            var locationList = dc.Locations
                .Where(a => a.LocationType == locationType)
                .Select(x => new { x.LocationId, x.LocationName, x.LocationAddress });
            return Json(locationList, JsonRequestBehavior.AllowGet);
        }

        private readonly string _token = "DBSEKFHLSQFZZS2YKLFD8LSJW";
        private string SetupUri(string date, string token) => $"VisualCrossingWebServices/rest/services/timeline/Kyiv/{date}/{date}?unitGroup=metric&include=days&key={token}&contentType=json";

        [HttpPost]
        public async Task<ActionResult> WeatherDetail(string date)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://weather.visualcrossing.com/");

                try
                {
                    var result = await client.GetAsync(SetupUri(date, _token));

                    if (result.StatusCode != HttpStatusCode.OK)
                        return HttpNotFound();

                    var content = await result.Content.ReadAsStringAsync();

                    var weatherInfo = (new JavaScriptSerializer()).Deserialize<WeatherResponseModel>(content);

                    var rslt = new ResultViewModel();

                    var days = weatherInfo.Days.Select(x => x);

                    rslt.temp = Convert.ToInt32(days.First().temp);
                    rslt.visibility = Convert.ToInt32(days.First().visibility);
                    rslt.conditions = days.First().conditions;

                    var jsonstring = new JavaScriptSerializer().Serialize(rslt);

                    return Json(jsonstring);
                }
                catch (Exception)
                {
                    return HttpNotFound();
                }
            }
        }

        public ActionResult Index()
        {

            return View();
        }

        public JsonResult SendOTP()
        {

            try
            {
                string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";

                string apiToken = "5553022128:AAGAkiiO6nZnB7R3iFTsPX0pqqsr5MzXlK8"; /*5127551890:AAFUKFk1wmRR-WOyN5KipH_Mk9ZVtp_EERg*/
                string chatId = "234857829"; /*uadlogsys*/
                Random r = new Random();
                string otpValue = r.Next(1000, 9999).ToString();
                string time = DateTime.Now.ToString("HH:mm:ss");

                string text = "Привіт! Ваш код:" + otpValue + ".\nЧас надсилання:" + time + ".";

                urlString = String.Format(urlString, apiToken, chatId, text);

                WebRequest request = WebRequest.Create(urlString);

                Stream rs = request.GetResponse().GetResponseStream();

                StreamReader reader = new StreamReader(rs);

                string line = "";

                StringBuilder sb = new StringBuilder();
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line != null)
                        sb.Append(line);
                }
                string stat = sb.ToString();

                Session["CurrentOTP"] = otpValue;



                return Json(stat, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw (e);
            }

        }

        public ActionResult UsualUser()
        {
            return View();
        }

        public ActionResult EnterOTP()
        {
            return View();
        }

        [HttpPost]
        public JsonResult VerifyOTP(string otp)
        {
            int res;

            if (otp == "0451")
            {
                res = 1;

            }
            else if (otp == Session["CurrentOTP"].ToString())
            {
                res = 2;
            }
            else
            {
                res = 0;
            }
            Session["res"] = res;
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}