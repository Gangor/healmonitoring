using Saleforce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealMonitoring.Controllers
{
    [Produces("application/json")]
    [Route("api/Saleforce")]
    public class FakeSaleForceController : Controller
    {
        [HttpGet]
        [Route("Conges/{id}")]
        public IActionResult GetConges(string id)
        {
            var conges = new List<Conges>();
            var rnd = new Random();

            while (conges.Count < 600)
            {
                var day = rnd.Next(1, 28);
                var month = rnd.Next(1, 12);
                var year = rnd.Next(2000, 2022);

                var startdate = new DateTime(year, month, day);
                if (startdate.DayOfWeek == DayOfWeek.Saturday ||
                    startdate.DayOfWeek == DayOfWeek.Sunday)
                    continue;

                var count = rnd.Next(1, 7);
                var enddate = startdate.AddDays(count);
                if (startdate.DayOfWeek == DayOfWeek.Saturday ||
                    startdate.DayOfWeek == DayOfWeek.Sunday)
                    continue;

                conges.Add(new Conges()
                {
                    StartDate = startdate,
                    EndDate = enddate,
                    User = id
                });
            }

            return Json(conges.OrderBy(u => u.StartDate));
        }
    }
}
