using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetDriversApp.WebAPI.Models;

namespace TimeSheetDriversApp.Web.Helpers
{
    public static class LayoutHelper
    {
        public static int GetNumPendingRequests(this HttpContext context)
        {
            Context db = context.RequestServices.GetService(typeof(Context)) as Context;
            if (db != null)
            {
                return db.Requests.Count(x => x.Status == RequestStatus.Pending);
            }

            return 0;
        }
    }
}
