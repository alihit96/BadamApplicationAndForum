using System;
using System.Globalization;

namespace BadamApplicationAndForum.Helpers
{
    public class CurrentPersianDate
    {
        public DateTime GetPersianDateNow()
        {
            var date = DateTime.Now;
            PersianCalendar pc = new PersianCalendar();
            var persianDate = new DateTime(pc.GetYear(date), pc.GetMonth(date), pc.GetDayOfMonth(date), pc.GetHour(date), pc.GetMinute(date), pc.GetSecond(date));
            return persianDate;
        }

        public DateTime GetPersianDate(DateTime dateTime)
        {
            var date = dateTime;
            PersianCalendar pc = new PersianCalendar();
            var persianDate = new DateTime(pc.GetYear(date), pc.GetMonth(date), pc.GetDayOfMonth(date), pc.GetHour(date), pc.GetMinute(date), pc.GetSecond(date));
            return persianDate;
        }
    }
}
