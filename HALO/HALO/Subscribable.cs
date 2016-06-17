using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HALO
{
    public interface Subscribable<T>
    {
        Action<T> OnUpdate { get; set; }
    }
}
