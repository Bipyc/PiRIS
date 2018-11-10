using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Common
{
    public struct GetConstantAccountParam
    {
        public ConstantAccounts ConstantAccount { get; set; }

        public Common.CurrencyType CurrencyType { get; set; }


        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is GetConstantAccountParam)
                {
                    GetConstantAccountParam compared = (GetConstantAccountParam)obj;

                    return ConstantAccount == compared.ConstantAccount && CurrencyType == compared.CurrencyType;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
