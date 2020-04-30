using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timesheet.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Dapper;

namespace Timesheet.Functions
{
    public class TMfunctions
    {
        private IDbConnection _db;

        public TMfunctions(IDbConnection db)
        {
            _db = db;
        }

        public List<Users> searchUsers(string SearchString = "0")
        {
            List<Users> users = null;
            if (!String.IsNullOrEmpty(SearchString))
            {
                users = _db.Query<Users>($"SELECT * FROM Users where Username like '%{SearchString}%';").ToList();
            }
            return users;
        }
        

        public HoursViewModel SearchHour(string SearchDate = "2017/02/12 12:00:00 AM")
        {
            HoursViewModel hvm = new HoursViewModel();
            hvm.PersonH = new List<HoursPerPerson>();
            hvm.ProjectH = new List<HoursPerProject>();

            if (DateTime.TryParse(SearchDate, out DateTime temp) == true)
            {
                DateTime date = Convert.ToDateTime(SearchDate);
                var month = date.Month;
                var year = date.Year;

                hvm.PersonH = (_db.Query<HoursPerPerson>($"Select sum (T.hoursCaptured) [PeHours], U.Username[Username] FROM Timeslots T JOIN Users U on T.UserId = U.UserId where MONTH(T.Date) = {month} and YEAR(T.Date) = {year} group by  U.Username; "));

                hvm.ProjectH = (_db.Query<HoursPerProject>($" Select sum (T.hoursCaptured) [PrHours],P.Name [Project] FROM Timeslots T JOIN Projects P on T.UserId = P.ProjectId where MONTH(T.Date) = {month} and YEAR(T.Date) = {year} group by  P.Name"));
                
            }

            return hvm;
        }

        public HoursPerPerson SearchUser(string SearchDate, string SearchString)
        {
            HoursPerPerson PersonH = new HoursPerPerson();
            if (SearchDate != null && DateTime.TryParse(SearchDate, out DateTime temp) == true && SearchString != null)
            {
                DateTime date = Convert.ToDateTime(SearchDate);
                var month = date.Month;
                var year = date.Year;

                PersonH = (_db.QuerySingleOrDefault<HoursPerPerson>($"Select sum (T.hoursCaptured) [PeHours], U.Username[Username] FROM Timeslots T JOIN Users U on T.UserId = U.UserId where MONTH(T.Date) = {month} and YEAR(T.Date) = {year} and U.Username = '{SearchString}' group by  U.Username "));


                return PersonH;
            }

            return PersonH;
        }
    }
}