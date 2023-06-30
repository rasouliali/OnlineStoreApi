using OnlineStoreApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreApi.Domain.Entities
{
    public class Color:BaseEntity
    {
        public string Name { get; set; }
        public string ColorCode { get; set; }
    }
}
