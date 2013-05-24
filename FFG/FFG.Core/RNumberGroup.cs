using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FFG.Core
{
    public class RNumberGroup : List<RStatNumber>
    {
        public RNumberGroup()
            : base(37)
        {
        }

        public RStatNumber FindByNumber(int n)
        {
            return Find(
                delegate(RStatNumber number)
                {
                    return (number.Number == n);
                });
        }

        public double CalcP(int spin)
        {
            double p = Count * RStatNumber.PROBABILITY;
            int lastSpin = GetLastSpin();

            int pow = spin - (lastSpin + StepDownSpin);
            pow = pow < 0 ? 0 : pow;

            return ((1 - Math.Pow(1 - p, pow)) * 100);
        }

        public override string ToString()
        {
            string result = string.Empty;

            Sort((x, y) => x.Number - y.Number);

            ForEach(rNum => {
                result += (String.IsNullOrEmpty(result)) ? String.Empty : " ";
                //result += rNum.Number.ToString((rNum.Number < 10) ? "\\ 0" : "00");
                result += String.Format("{0,2:##}", rNum.Number);
            });
            return result;
        }

        public string GetMD5HashCode()
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(this.ToString());
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public int GetCount()
        {
            int result = 0;

            ForEach(delegate(RStatNumber n) { result += n.Count; });

            return result;
        }

        public int GetLastSpin()
        {
            int result = 0;

            ForEach(
                delegate(RStatNumber n)
                {
                    if (result < n.Spin)
                    {
                        result = n.Spin;
                    }
                });

            return result;
        }

        #region Properties
        public int StepDownSpin { get; set; }
        #endregion
    }
}
