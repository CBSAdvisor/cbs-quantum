using System;
using System.Collections.Generic;
using System.Text;

namespace FFG.Core
{
    public class RNumber
    {
        private int _number = 0;

        public RNumber() : this(0)
        {
        }

        public RNumber(int n)
        {
            Number = n;
        }

        public static Color GetColor(int n)
        {
            switch (n)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 9:
                case 12:
                case 14:
                case 16:
                case 18:
                case 19:
                case 21:
                case 23:
                case 25:
                case 27:
                case 30:
                case 32:
                case 34:
                case 36:
                    return Color.Red;
                case 2:
                case 4:
                case 6:
                case 8:
                case 10:
                case 11:
                case 13:
                case 15:
                case 17:
                case 20:
                case 22:
                case 24:
                case 26:
                case 28:
                case 29:
                case 31:
                case 33:
                case 35:
                    return Color.Black;
                default:
                    return Color.None;
            }
        }

        public static Parity GetParity(int n)
        {
            if (n == 0)
            {
                return Parity.None;
            }

            return (n % 2) == 0 ? Parity.Even : Parity.Odd;
        }

        public static MoreLess GetMoreLess(int n)
        {
            if (n == 0)
            {
                return MoreLess.None;
            }

            return ((n > 0) && (n < 19)) ? MoreLess.Less : MoreLess.More;
        }

        public static Dozen GetDozen(int n)
        {
            if (n == 0)
            {
                return Dozen.None;
            }

            return (Dozen)((int)Math.Floor((double)(n-1) / 12) + 1);
        }

        public static Column GetColumn(int n)
        {
            if (n == 0)
            {
                return Column.None;
            }

            switch (n % 3)
            { 
                case 0:
                    return Column.Three;
                case 1:
                    return Column.One;
                case 2:
                    return Column.Two;
                default:
                    return Column.None;
            }
        }

        public static Street GetStreet(int n)
        {
            if (n == 0)
            {
                return Street.None;
            }

            return (Street)((int)Math.Floor((double)(n - 1) / 3) + 1);
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", Number, Enum.GetName(typeof(Color), Color));
        }

        private void Initialize(int n)
        {
            _number = n;
            Color = GetColor(n);
            Parity = GetParity(n);
            MoreLess = GetMoreLess(n);
            Dozen = GetDozen(n);
            Column = GetColumn(n);
            Street = GetStreet(n);
        }

        #region Properties
        public int Number 
        {
            get { return _number; }
            set 
            {
                Initialize(value);
            }
        }

        public Color Color { get; private set; }
        public Parity Parity { get; private set; }
        public MoreLess MoreLess { get; private set; }
        public Dozen Dozen { get; private set; }
        public Column Column { get; private set; }
        public Street Street { get; private set; }
        #endregion
    }
}
