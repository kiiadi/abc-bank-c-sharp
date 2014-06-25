using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public static class Constants
    {
        public const double MaxiSavings_IntRate_TranOcurredLessTenDays = .001;
        public const double MaxiSavings_IntRate_TranOcurredGreaterTenDays = .05;
        public const double Savings_IntRate_BalGreater1000 = .002;
        public const double Savings_IntRate_BalLess1000 = .001;
        public const double Checking_IntRate = .001;
    }
}
