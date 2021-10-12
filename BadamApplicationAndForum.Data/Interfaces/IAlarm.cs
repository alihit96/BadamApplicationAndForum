using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Interfaces
{
    public interface IAlarm
    {

        IEnumerable<Alarm> GetAlarms();
        Task AddAlarm(Alarm alarm);

        IEnumerable<Alarm> GetAlarmsById(string Id);


    }
}
