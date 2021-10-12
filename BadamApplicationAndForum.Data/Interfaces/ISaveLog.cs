using ExamCards.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Interfaces
{
    public interface ISaveLog
    {
        IEnumerable<SystemLog> GetSystemLogs();
        Task AddLog(SystemLog log);
    }
}
