using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Dtos.Alarms
{
    public class AlarmsIndexingDto
    {
        public IEnumerable<Alarm> Alarms { get; set; }
    }
}
