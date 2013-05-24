using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFG.Core
{
    public class RStatNumber : RNumber
    {
        public const double PROBABILITY = (double)1 / 37;
        private int _spin = 0;

        public RStatNumber()
            : base()
        {
            Count = 0;
        }

        public RStatNumber(int n)
            : base(n)
        {
            Count = 0;
        }

        public void ResetStat()
        {
            Spin = 0;
        }

        #region Properties
        public int Spin
        {
            get
            {
                return _spin;
            }
            set
            {
                _spin = value;

                if (value == 0)
                {
                    Count = 0;
                }
                else
                {
                    Count++;
                }
            }
        }
        public int Count { get; private set; }
        #endregion
    }
}
