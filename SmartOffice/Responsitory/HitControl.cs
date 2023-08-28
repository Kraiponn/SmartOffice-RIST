using SmartOffice.IResponsitory;
using SmartOffice.ModelsDocControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Responsitory
{
    public class HitControl: IHitControl
    {
        private readonly DocumentControlContext _DocumentContext;
        public HitControl(DocumentControlContext DocumentContext)
        {
            _DocumentContext = DocumentContext;
        }
        public async Task AddhitAsync(string ipaddress)
        {
            HitCounter hit = new HitCounter()
            {
                Ipaddress = ipaddress,
                CreateDate = DateTime.Now.Date
            };
            await _DocumentContext.HitCounter.AddAsync(hit);
            await _DocumentContext.SaveChangesAsync();
        }
    }
}
