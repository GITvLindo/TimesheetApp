using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timesheet.Models
{
    public class HoursViewModel
    {
        public IEnumerable<HoursPerPerson> PersonH { get; set; }
        public IEnumerable<HoursPerProject> ProjectH { get; set; }
    }
}