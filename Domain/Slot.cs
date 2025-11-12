using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public readonly record struct Slot(int Aisle, int X, int Z);
}
