using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public readonly record struct PalletId(string Value)
    {
        public override string ToString() => Value;
        public static bool IsValid(string v) => !string.IsNullOrWhiteSpace(v);
    }
}
