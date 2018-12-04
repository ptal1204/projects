using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BBapp.Models;

namespace BBapp.Controllers
{
	public class ShiftcalculationController : ApiController
	{
		// get a post from the client
		[HttpPost]
		[ActionName("PostHours")]
		public HttpResponseMessage PostShifts(Shifts shiftworked)
		{
			ShiftRates rates = new ShiftRates();
			var Midnite = DateTime.Today.AddDays(1);

			try
			{
				var shiftEarned = ((shiftworked.Startshift.Subtract(shiftworked.Bedtime).Hours) * rates.StartToBeftime +
							(shiftworked.Bedtime.Subtract(Midnite).Hours) * rates.BedtimeToMidnite +
							(Midnite.Subtract(shiftworked.EndShift).Hours) * rates.ShiftToEnd);

				return Request.CreateResponse(HttpStatusCode.Accepted, shiftEarned.ToString("C0", CultureInfo.CurrentCulture));
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error: " + ex.Message);
			}
		}
	}
}