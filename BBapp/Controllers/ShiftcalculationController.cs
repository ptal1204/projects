using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			var shiftEarned = 0;

			try
			{
				if (shiftworked.Bedtime != DateTime.MinValue)
				{
					shiftEarned = ((shiftworked.Bedtime.Subtract(shiftworked.Startshift).Hours) * rates.StartToBedtime +
									(Midnite.Subtract(shiftworked.Bedtime).Hours) * rates.BedtimeToMidnite +
									(shiftworked.EndShift.Subtract(Midnite).Hours) * rates.ShiftToEnd);

				}
				else
				{
					if (shiftworked.EndShift < Midnite)
					{
						shiftEarned = ((shiftworked.EndShift.Subtract(shiftworked.Startshift).Hours) * rates.StartToBedtime);
					}
					else
					{
						shiftEarned = (Midnite.Subtract(shiftworked.Startshift).Hours) * rates.StartToBedtime +
									  (shiftworked.EndShift.Subtract(Midnite).Hours) *rates.ShiftToEnd;
					}
				}

				return Request.CreateResponse(HttpStatusCode.Accepted, shiftEarned.ToString("C0", CultureInfo.CurrentCulture));
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error: " + ex.Message);
			}
		}
	}
}