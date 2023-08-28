using SmartOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.IResponsitory
{
    public interface IESmartNotiRepository
    {
        //IEnumerable<DocItem> DocItem { get; }
        void Boardcastnotify(string DocumentNo,string Username);
    }
}
