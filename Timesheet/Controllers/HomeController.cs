using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timesheet.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Dapper;
using Timesheet.Functions;
using Dapper;

namespace Timesheet.Controllers
{

    public class HomeController : Controller
    {
        private IDbConnection _db = new SqlConnection
        (ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        TMfunctions ts = new TMfunctions(new SqlConnection
            (ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));

        public ActionResult Index(string SearchString)
        {
            if (!String.IsNullOrEmpty(SearchString))
            {
                return View(ts.searchUsers(SearchString));
            }

            return View(_db.Query<Users>("SELECT * FROM Users"));
        }

        public ActionResult SearchHour(DateTime SearchDate)
        {
            if (SearchDate != null)
            {
                return View(ts.SearchHour(SearchDate.ToString()));
            }

            return View("Index", _db.Query<Users>("SELECT * FROM Users"));
        }

        public ActionResult SearchUser(DateTime SearchDate, string SearchString)
        {
            if (SearchDate != null && SearchString != null)
            {
                return View(ts.SearchUser(SearchDate.ToString(), SearchString));
            }

            return View("Index", _db.Query<Users>("SELECT * FROM Users"));
        }
    }
}