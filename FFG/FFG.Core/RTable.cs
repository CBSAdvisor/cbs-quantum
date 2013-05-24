using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using FFG.Configuration;

namespace FFG.Core
{
    public class RTable
    {
        private RNumberGroup _dozen1 = null;
        private RNumberGroup _dozen2 = null;
        private RNumberGroup _dozen3 = null;

        private RNumberGroup _column1 = null;
        private RNumberGroup _column2 = null;
        private RNumberGroup _column3 = null;

        private RNumberGroup _street1 = null;
        private RNumberGroup _street2 = null;
        private RNumberGroup _street3 = null;
        private RNumberGroup _street4 = null;
        private RNumberGroup _street5 = null;
        private RNumberGroup _street6 = null;
        private RNumberGroup _street7 = null;
        private RNumberGroup _street8 = null;
        private RNumberGroup _street9 = null;
        private RNumberGroup _street10 = null;
        private RNumberGroup _street11 = null;
        private RNumberGroup _street12 = null;

        private RNumberGroup _sixline1 = null;
        private RNumberGroup _sixline2 = null;
        private RNumberGroup _sixline3 = null;
        private RNumberGroup _sixline4 = null;
        private RNumberGroup _sixline5 = null;
        private RNumberGroup _sixline6 = null;
        private RNumberGroup _sixline7 = null;
        private RNumberGroup _sixline8 = null;
        private RNumberGroup _sixline9 = null;
        private RNumberGroup _sixline10 = null;
        private RNumberGroup _sixline11 = null;

        private RNumberGroup _red = null;
        private RNumberGroup _black = null;

        private RNumberGroup _odd = null;
        private RNumberGroup _even = null;

        private RNumberGroup _more = null;
        private RNumberGroup _less = null;

        private RNumberGroup[] _9Group1 = null;
        private RNumberGroup[] _9Group2 = null;
        private RNumberGroup[] _9Group3 = null;
        private RNumberGroup[] _9Group4 = null;

        public RTable()
        {
            Numbers = new RNumberGroup();

            _dozen1 = new RNumberGroup() { StepDownSpin = 1 };
            _dozen2 = new RNumberGroup() { StepDownSpin = 1 };
            _dozen3 = new RNumberGroup() { StepDownSpin = 1 };

            _column1 = new RNumberGroup() { StepDownSpin = 1 };
            _column2 = new RNumberGroup() { StepDownSpin = 1 };
            _column3 = new RNumberGroup() { StepDownSpin = 1 };

            _street1 = new RNumberGroup() { StepDownSpin = 1 };
            _street2 = new RNumberGroup() { StepDownSpin = 1 };
            _street3 = new RNumberGroup() { StepDownSpin = 1 };
            _street4 = new RNumberGroup() { StepDownSpin = 1 };
            _street5 = new RNumberGroup() { StepDownSpin = 1 };
            _street6 = new RNumberGroup() { StepDownSpin = 1 };
            _street7 = new RNumberGroup() { StepDownSpin = 1 };
            _street8 = new RNumberGroup() { StepDownSpin = 1 };
            _street9 = new RNumberGroup() { StepDownSpin = 1 };
            _street10 = new RNumberGroup() { StepDownSpin = 1 };
            _street11 = new RNumberGroup() { StepDownSpin = 1 };
            _street12 = new RNumberGroup() { StepDownSpin = 1 };

            _sixline1 = new RNumberGroup();
            _sixline2 = new RNumberGroup();
            _sixline3 = new RNumberGroup();
            _sixline4 = new RNumberGroup();
            _sixline5 = new RNumberGroup();
            _sixline6 = new RNumberGroup();
            _sixline7 = new RNumberGroup();
            _sixline8 = new RNumberGroup();
            _sixline9 = new RNumberGroup();
            _sixline10 = new RNumberGroup();
            _sixline11 = new RNumberGroup();

            _red = new RNumberGroup();
            _black = new RNumberGroup();

            _odd = new RNumberGroup();
            _even = new RNumberGroup();

            _more = new RNumberGroup();
            _less = new RNumberGroup();

            _9Group1 = new RNumberGroup[4]
            {
                new RNumberGroup(),
                new RNumberGroup(),
                new RNumberGroup(),
                new RNumberGroup()
            };

            _9Group2 = new RNumberGroup[4]
            {
                new RNumberGroup(),
                new RNumberGroup(),
                new RNumberGroup(),
                new RNumberGroup()
            };

            _9Group3 = new RNumberGroup[4]
            {
                new RNumberGroup(),
                new RNumberGroup(),
                new RNumberGroup(),
                new RNumberGroup()
            };

            _9Group4 = new RNumberGroup[4]
            {
                new RNumberGroup(),
                new RNumberGroup(),
                new RNumberGroup(),
                new RNumberGroup()
            };

            InitNumbers();
        }

        #region Public methods
        public void AddSpin(int n)
        {
            if (n == 0)
            {
                return;
            }

            Spin++;

            RStatNumber number = Numbers.FindByNumber(n);
            number.Spin = Spin;
        }

        public double GetDozenP(int d)
        {
            double result = 0;

            switch (d)
            {
                case 1:
                    result = _dozen1.CalcP(Spin);
                    break;
                case 2:
                    result = _dozen2.CalcP(Spin);
                    break;
                case 3:
                    result = _dozen3.CalcP(Spin);
                    break;
                default:
                    break;
            }
            return result;
        }

        public double GetColumnP(int c)
        {
            double result = 0;

            switch (c)
            {
                case 1:
                    result = _column1.CalcP(Spin);
                    break;
                case 2:
                    result = _column2.CalcP(Spin);
                    break;
                case 3:
                    result = _column3.CalcP(Spin);
                    break;
                default:
                    break;
            }
            return result;
        }

        public double GetColorP(Color c)
        {
            double result = 0;

            switch (c)
            {
                case Color.Red:
                    result = _red.CalcP(Spin);
                    break;
                case Color.Black:
                    result = _black.CalcP(Spin);
                    break;
                default:
                    break;
            }
            return result;
        }

        public double GetParityP(Parity p)
        {
            double result = 0;

            switch (p)
            {
                case Parity.Odd:
                    result = _odd.CalcP(Spin);
                    break;
                case Parity.Even:
                    result = _even.CalcP(Spin);
                    break;
                default:
                    break;
            }
            return result;
        }

        public double GetMoreLessP(MoreLess ml)
        {
            double result = 0;

            switch (ml)
            {
                case MoreLess.More:
                    result = _more.CalcP(Spin);
                    break;
                case MoreLess.Less:
                    result = _less.CalcP(Spin);
                    break;
                default:
                    break;
            }
            return result;
        }

        public double GetStreetP(Street s)
        {
            double result = 0;

            switch (s)
            {
                case Street.One:
                    result = _street1.CalcP(Spin);
                    break;
                case Street.Two:
                    result = _street2.CalcP(Spin);
                    break;
                case Street.Three:
                    result = _street3.CalcP(Spin);
                    break;
                case Street.Four:
                    result = _street4.CalcP(Spin);
                    break;
                case Street.Five:
                    result = _street5.CalcP(Spin);
                    break;
                case Street.Six:
                    result = _street6.CalcP(Spin);
                    break;
                case Street.Seven:
                    result = _street7.CalcP(Spin);
                    break;
                case Street.Eight:
                    result = _street8.CalcP(Spin);
                    break;
                case Street.Nine:
                    result = _street9.CalcP(Spin);
                    break;
                case Street.Ten:
                    result = _street10.CalcP(Spin);
                    break;
                case Street.Eleven:
                    result = _street11.CalcP(Spin);
                    break;
                case Street.Twelve:
                    result = _street12.CalcP(Spin);
                    break;
                default:
                    break;
            }
            return result;
        }

        public double GetSixLineP(SixLine s)
        {
            double result = 0;

            switch (s)
            {
                case SixLine.One:
                    result = _sixline1.CalcP(Spin);
                    break;
                case SixLine.Two:
                    result = _sixline2.CalcP(Spin);
                    break;
                case SixLine.Three:
                    result = _sixline3.CalcP(Spin);
                    break;
                case SixLine.Four:
                    result = _sixline4.CalcP(Spin);
                    break;
                case SixLine.Five:
                    result = _sixline5.CalcP(Spin);
                    break;
                case SixLine.Six:
                    result = _sixline6.CalcP(Spin);
                    break;
                case SixLine.Seven:
                    result = _sixline7.CalcP(Spin);
                    break;
                case SixLine.Eight:
                    result = _sixline8.CalcP(Spin);
                    break;
                case SixLine.Nine:
                    result = _sixline9.CalcP(Spin);
                    break;
                case SixLine.Ten:
                    result = _sixline10.CalcP(Spin);
                    break;
                case SixLine.Eleven:
                    result = _sixline11.CalcP(Spin);
                    break;
                default:
                    break;
            }
            return result;
        }

        public double Get_9Group1P(int g)
        {
            return _9Group1[g].CalcP(Spin);
        }

        public double Get_9Group2P(int g)
        {
            return _9Group2[g].CalcP(Spin);
        }
        #endregion

        #region Private methods

        private void InitNumbers()
        {
            Numbers.Clear();

            for (int i = 0; i < 37; i++)
            {
                RStatNumber n = new RStatNumber(i);
                Numbers.Add(n);

                switch (n.Dozen)
                {
                    case Dozen.One:
                        _dozen1.Add(n);
                        break;
                    case Dozen.Two:
                        _dozen2.Add(n);
                        break;
                    case Dozen.Three:
                        _dozen3.Add(n);
                        break;
                    default:
                        break;
                }

                switch (n.Column)
                {
                    case Column.One:
                        _column1.Add(n);
                        break;
                    case Column.Two:
                        _column2.Add(n);
                        break;
                    case Column.Three:
                        _column3.Add(n);
                        break;
                    default:
                        break;
                }

                switch (n.Color)
                {
                    case Color.Red:
                        _red.Add(n);
                        break;
                    case Color.Black:
                        _black.Add(n);
                        break;
                    default:
                        break;
                }

                switch (n.Parity)
                {
                    case Parity.Odd:
                        _odd.Add(n);
                        break;
                    case Parity.Even:
                        _even.Add(n);
                        break;
                    default:
                        break;
                }

                switch (n.MoreLess)
                {
                    case MoreLess.More:
                        _more.Add(n);
                        break;
                    case MoreLess.Less:
                        _less.Add(n);
                        break;
                    default:
                        break;
                }

                switch (n.Street)
                {
                    case Street.One:
                        _street1.Add(n);
                        break;
                    case Street.Two:
                        _street2.Add(n);
                        break;
                    case Street.Three:
                        _street3.Add(n);
                        break;
                    case Street.Four:
                        _street4.Add(n);
                        break;
                    case Street.Five:
                        _street5.Add(n);
                        break;
                    case Street.Six:
                        _street6.Add(n);
                        break;
                    case Street.Seven:
                        _street7.Add(n);
                        break;
                    case Street.Eight:
                        _street8.Add(n);
                        break;
                    case Street.Nine:
                        _street9.Add(n);
                        break;
                    case Street.Ten:
                        _street10.Add(n);
                        break;
                    case Street.Eleven:
                        _street11.Add(n);
                        break;
                    case Street.Twelve:
                        _street12.Add(n);
                        break;
                    default:
                        break;
                }

                // Initialize sixlines
                switch (n.Number)
                {
                    case 1:
                    case 2:
                    case 3:
                        _sixline1.Add(n);
                        break;
                    case 4:
                    case 5:
                    case 6:
                        _sixline1.Add(n);
                        _sixline2.Add(n);
                        break;
                    case 7:
                    case 8:
                    case 9:
                        _sixline2.Add(n);
                        _sixline3.Add(n);
                        break;
                    case 10:
                    case 11:
                    case 12:
                        _sixline3.Add(n);
                        _sixline4.Add(n);
                        break;
                    case 13:
                    case 14:
                    case 15:
                        _sixline4.Add(n);
                        _sixline5.Add(n);
                        break;
                    case 16:
                    case 17:
                    case 18:
                        _sixline5.Add(n);
                        _sixline6.Add(n);
                        break;
                    case 19:
                    case 20:
                    case 21:
                        _sixline6.Add(n);
                        _sixline7.Add(n);
                        break;
                    case 22:
                    case 23:
                    case 24:
                        _sixline7.Add(n);
                        _sixline8.Add(n);
                        break;
                    case 25:
                    case 26:
                    case 27:
                        _sixline8.Add(n);
                        _sixline9.Add(n);
                        break;
                    case 28:
                    case 29:
                    case 30:
                        _sixline9.Add(n);
                        _sixline10.Add(n);
                        break;
                    case 31:
                    case 32:
                    case 33:
                        _sixline10.Add(n);
                        _sixline11.Add(n);
                        break;
                    case 34:
                    case 35:
                    case 36:
                        _sixline11.Add(n);
                        break;
                    default:
                        break;
                }
            }

            Init_9Group(_9Group1, 0);
            Init_9Group(_9Group2, 1);
            Init_9Group(_9Group3, 2);
            Init_9Group(_9Group4, 3);

            Spin = 0;
        }

        /*
        *******************************************************************************

        ==============================
        24, 17, 8, 9, 18, 29, 31, 10, 21
        12, 3, 5, 19, 11, 27, 32, 30, 36
        34, 23, 20, 25, 13, 14, 26, 33, 28
        22, 1, 2, 16, 6, 4, 15, 35, 7
        ==============================
        28, 6, 16, 4, 9, 7, 8, 20, 5
        27, 30, 24, 23, 36, 12, 22, 26, 2
        17, 15, 34, 11, 10, 14, 3, 29, 35
        1, 33, 19, 32, 31, 21, 18, 25, 13
        ==============================
        8, 7, 13, 3, 34, 25, 29, 9, 24
        12, 18, 30, 11, 16, 23, 4, 21, 19
        36, 6, 10, 32, 14, 2, 1, 20, 22
        31, 28, 17, 26, 27, 15, 33, 5, 35
        ==============================
        25, 4, 9, 1, 32, 19, 30, 15, 20
        16, 26, 34, 29, 18, 23, 35, 33, 13
        21, 31, 28, 24, 7, 11, 2, 14, 12
        10, 5, 17, 8, 3, 22, 6, 36, 27
        ==============================

        *******************************************************************************

        ==============================
        18, 26, 16, 23, 36, 12, 22, 30, 24
        21, 35, 25, 2, 8, 1, 14, 34, 32
        11, 20, 4, 5, 9, 29, 13, 6, 17
        3, 15, 7, 10, 31, 27, 28, 33, 19
        ==============================
        31, 10, 1, 23, 18, 22, 16, 15, 29
        8, 11, 6, 17, 12, 27, 36, 28, 35
        33, 3, 14, 26, 5, 34, 7, 13, 25
        4, 24, 9, 2, 21, 20, 30, 32, 19
        ==============================
        3, 13, 30, 26, 17, 19, 35, 32, 10
        4, 15, 16, 29, 2, 22, 8, 36, 6
        33, 14, 12, 27, 25, 11, 31, 24, 9
        28, 34, 1, 7, 18, 23, 21, 20, 5
        ==============================
        30, 24, 17, 2, 27, 5, 12, 6, 23
        1, 21, 34, 13, 33, 8, 31, 15, 11
        3, 36, 14, 19, 18, 7, 4, 20, 22
        16, 32, 35, 9, 28, 26, 29, 10, 25
        ==============================

        *******************************************************************************
        */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nineGroup"></param>
        /// <param name="groupId">value from 0 to 3</param>
        private void Init_9Group(RNumberGroup[] nineGroup, int groupId)
        {
            FfgConfigSection ffgSection = (FfgConfigSection)ConfigurationManager.GetSection(FfgConfigSection.sConfigurationSectionConst);

            NumbersGroupElement nineGroupConfig = ffgSection.NineGroups[groupId];
            for (int i = 0; i < nineGroupConfig.Parts.Count; i++)
            {
                NumbersGroupPartElement part = nineGroupConfig.Parts[i];
                string[] numbers = part.Value.Split(new char[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string n in numbers)
                {
                    RStatNumber rNum = Numbers.FindByNumber(Convert.ToInt32(n));
                    nineGroup[i].Add(rNum);
                }
            }
        }

        private void Init_9Group1()
        {
            IList<IList<int>> groupConfig = new List<IList<int>>(4);
            IList<int> groupPartConfig;

            //==============================
            // 32 15 19 4 21 2 25 17 34
            // 6 27 13 36 11 30 8 23 10
            // 5 24 16 33 1 20 14 31 9
            // 22 18 29 7 28 12 35 3 26
            //==============================

            //// Add part 1
            //groupPartConfig = new List<int> { 32, 15, 19, 4, 21, 2, 25, 17, 34 };
            //groupConfig.Add(groupPartConfig);

            //// Add part 2
            //groupPartConfig = new List<int> { 6, 27, 13, 36, 11, 30, 8, 23, 10 };
            //groupConfig.Add(groupPartConfig);

            //// Add part 3
            //groupPartConfig = new List<int> { 5, 24, 16, 33, 1, 20, 14, 31, 9 };
            //groupConfig.Add(groupPartConfig);

            //// Add part 4
            //groupPartConfig = new List<int> { 22, 18, 29, 7, 28, 12, 35, 3, 26 };
            //groupConfig.Add(groupPartConfig);

            ///////////////////////////////////////////////////////////////////
            //===================================
            //18, 26, 16, 23, 36, 12, 22, 30, 24
            //21, 35, 25, 2, 8, 1, 14, 34, 32
            //11, 20, 4, 5, 9, 29, 13, 6, 17
            //3, 15, 7, 10, 31, 27, 28, 33, 19
            //===================================

            // Add part 1
            groupPartConfig = new List<int> { 18, 26, 16, 23, 36, 12, 22, 30, 24 };
            groupConfig.Add(groupPartConfig);

            // Add part 2
            groupPartConfig = new List<int> { 21, 35, 25, 2, 8, 1, 14, 34, 32 };
            groupConfig.Add(groupPartConfig);

            // Add part 3
            groupPartConfig = new List<int> { 11, 20, 4, 5, 9, 29, 13, 6, 17 };
            groupConfig.Add(groupPartConfig);

            // Add part 4
            groupPartConfig = new List<int> { 3, 15, 7, 10, 31, 27, 28, 33, 19 };
            groupConfig.Add(groupPartConfig);
            ///////////////////////////////////////////////////////////////////

            for (int row = 0; row < groupConfig.Count; row++)
            {
                for (int column = 0; column < groupConfig[row].Count; column++)
                {
                    RStatNumber rNum = Numbers.FindByNumber(groupConfig[row][column]);
                    _9Group1[row].Add(rNum);
                }
            }
        }

        private void Init_9Group2()
        {
            IList<IList<int>> groupConfig = new List<IList<int>>(4);
            IList<int> groupPartConfig;

            //==============================
            // 21 2 25 17 34 6 27 13 36
            // 11 30 8 23 10 5 24 16 33
            // 1 20 14 31 9 22 18 29 7
            // 28 12 35 3 26 32 15 19 4
            //==============================

            //// Add part 1
            //groupPartConfig = new List<int> { 21, 2, 25, 17, 34, 6, 27, 13, 36 };
            //groupConfig.Add(groupPartConfig);

            //// Add part 2
            //groupPartConfig = new List<int> { 11, 30, 8, 23, 10, 5, 24, 16, 33 };
            //groupConfig.Add(groupPartConfig);

            //// Add part 3
            //groupPartConfig = new List<int> { 1, 20, 14, 31, 9, 22, 18, 29, 7 };
            //groupConfig.Add(groupPartConfig);

            //// Add part 4
            //groupPartConfig = new List<int> { 28, 12, 35, 3, 26, 32, 15, 19, 4 };
            //groupConfig.Add(groupPartConfig);

            ///////////////////////////////////////////////////////////////////
            //===================================
            //31, 10, 1, 23, 18, 22, 16, 15, 29
            //8, 11, 6, 17, 12, 27, 36, 28, 35
            //33, 3, 14, 26, 5, 34, 7, 13, 25
            //4, 24, 9, 2, 21, 20, 30, 32, 19
            //===================================

            // Add part 1
            groupPartConfig = new List<int> { 31, 10, 1, 23, 18, 22, 16, 15, 29 };
            groupConfig.Add(groupPartConfig);

            // Add part 2
            groupPartConfig = new List<int> { 8, 11, 6, 17, 12, 27, 36, 28, 35 };
            groupConfig.Add(groupPartConfig);

            // Add part 3
            groupPartConfig = new List<int> { 33, 3, 14, 26, 5, 34, 7, 13, 25 };
            groupConfig.Add(groupPartConfig);

            // Add part 4
            groupPartConfig = new List<int> { 4, 24, 9, 2, 21, 20, 30, 32, 19 };
            groupConfig.Add(groupPartConfig);
            ///////////////////////////////////////////////////////////////////


            for (int row = 0; row < groupConfig.Count; row++)
            {
                for (int column = 0; column < groupConfig[row].Count; column++)
                {
                    RStatNumber rNum = Numbers.FindByNumber(groupConfig[row][column]);
                    _9Group2[row].Add(rNum);
                }
            }
        }

        private void Init_9Group3()
        {
            IList<IList<int>> groupConfig = new List<IList<int>>(4);
            IList<int> groupPartConfig;

            //==============================
            //8, 7, 13, 3, 34, 25, 29, 9, 24
            //12, 18, 30, 11, 16, 23, 4, 21, 19
            //36, 6, 10, 32, 14, 2, 1, 20, 22
            //31, 28, 17, 26, 27, 15, 33, 5, 35
            //==============================

            //// Add part 1
            //groupPartConfig = new List<int> { 8, 7, 13, 3, 34, 25, 29, 9, 24 };
            //groupConfig.Add(groupPartConfig);

            //// Add part 2
            //groupPartConfig = new List<int> { 12, 18, 30, 11, 16, 23, 4, 21, 19 };
            //groupConfig.Add(groupPartConfig);

            //// Add part 3
            //groupPartConfig = new List<int> { 36, 6, 10, 32, 14, 2, 1, 20, 22 };
            //groupConfig.Add(groupPartConfig);

            //// Add part 4
            //groupPartConfig = new List<int> { 31, 28, 17, 26, 27, 15, 33, 5, 35 };
            //groupConfig.Add(groupPartConfig);

            ///////////////////////////////////////////////////////////////////
            //===================================
            //3, 13, 30, 26, 17, 19, 35, 32, 10
            //4, 15, 16, 29, 2, 22, 8, 36, 6
            //33, 14, 12, 27, 25, 11, 31, 24, 9
            //28, 34, 1, 7, 18, 23, 21, 20, 5
            //===================================

            // Add part 1
            groupPartConfig = new List<int> { 3, 13, 30, 26, 17, 19, 35, 32, 10 };
            groupConfig.Add(groupPartConfig);

            // Add part 2
            groupPartConfig = new List<int> { 4, 15, 16, 29, 2, 22, 8, 36, 6 };
            groupConfig.Add(groupPartConfig);

            // Add part 3
            groupPartConfig = new List<int> { 33, 14, 12, 27, 25, 11, 31, 24, 9 };
            groupConfig.Add(groupPartConfig);

            // Add part 4
            groupPartConfig = new List<int> { 28, 34, 1, 7, 18, 23, 21, 20, 5 };
            groupConfig.Add(groupPartConfig);
            ///////////////////////////////////////////////////////////////////
            
            for (int row = 0; row < groupConfig.Count; row++)
            {
                for (int column = 0; column < groupConfig[row].Count; column++)
                {
                    RStatNumber rNum = Numbers.FindByNumber(groupConfig[row][column]);
                    _9Group3[row].Add(rNum);
                }
            }
        }

        private void Init_9Group4()
        {
            IList<IList<int>> groupConfig = new List<IList<int>>(4);
            IList<int> groupPartConfig;

            //==============================
            //25, 4, 9, 1, 32, 19, 30, 15, 20
            //16, 26, 34, 29, 18, 23, 35, 33, 13
            //21, 31, 28, 24, 7, 11, 2, 14, 12
            //10, 5, 17, 8, 3, 22, 6, 36, 27
            //==============================

            //// Add part 1
            //groupPartConfig = new List<int> { 25, 4, 9, 1, 32, 19, 30, 15, 20 };
            //groupConfig.Add(groupPartConfig);

            //// Add part 2
            //groupPartConfig = new List<int> { 16, 26, 34, 29, 18, 23, 35, 33, 13 };
            //groupConfig.Add(groupPartConfig);

            //// Add part 3
            //groupPartConfig = new List<int> { 21, 31, 28, 24, 7, 11, 2, 14, 12 };
            //groupConfig.Add(groupPartConfig);

            //// Add part 4
            //groupPartConfig = new List<int> { 10, 5, 17, 8, 3, 22, 6, 36, 27 };
            //groupConfig.Add(groupPartConfig);

            ///////////////////////////////////////////////////////////////////
            //===================================
            //30, 24, 17, 2, 27, 5, 12, 6, 23
            //1, 21, 34, 13, 33, 8, 31, 15, 11
            //3, 36, 14, 19, 18, 7, 4, 20, 22
            //16, 32, 35, 9, 28, 26, 29, 10, 25
            //===================================

            // Add part 1
            groupPartConfig = new List<int> { 30, 24, 17, 2, 27, 5, 12, 6, 23 };
            groupConfig.Add(groupPartConfig);

            // Add part 2
            groupPartConfig = new List<int> { 1, 21, 34, 13, 33, 8, 31, 15, 11 };
            groupConfig.Add(groupPartConfig);

            // Add part 3
            groupPartConfig = new List<int> { 3, 36, 14, 19, 18, 7, 4, 20, 22 };
            groupConfig.Add(groupPartConfig);

            // Add part 4
            groupPartConfig = new List<int> { 16, 32, 35, 9, 28, 26, 29, 10, 25 };
            groupConfig.Add(groupPartConfig);
            ///////////////////////////////////////////////////////////////////

            for (int row = 0; row < groupConfig.Count; row++)
            {
                for (int column = 0; column < groupConfig[row].Count; column++)
                {
                    RStatNumber rNum = Numbers.FindByNumber(groupConfig[row][column]);
                    _9Group4[row].Add(rNum);
                }
            }
        }
        #endregion

        #region Properties
        public RNumberGroup Numbers { get; private set; }
        public int Spin { get; private set; }
        public RNumberGroup[] Group9_n1 { get { return _9Group1; } }
        public RNumberGroup[] Group9_n2 { get { return _9Group2; } }
        public RNumberGroup[] Group9_n3 { get { return _9Group3; } }
        public RNumberGroup[] Group9_n4 { get { return _9Group4; } }

        public RNumberGroup SixLine1 { get { return _sixline1; } }
        public RNumberGroup SixLine2 { get { return _sixline2; } }
        public RNumberGroup SixLine3 { get { return _sixline3; } }
        public RNumberGroup SixLine4 { get { return _sixline4; } }
        public RNumberGroup SixLine5 { get { return _sixline5; } }
        public RNumberGroup SixLine6 { get { return _sixline6; } }
        public RNumberGroup SixLine7 { get { return _sixline7; } }
        public RNumberGroup SixLine8 { get { return _sixline8; } }
        public RNumberGroup SixLine9 { get { return _sixline9; } }
        public RNumberGroup SixLine10 { get { return _sixline10; } }
        public RNumberGroup SixLine11 { get { return _sixline11; } }
        #endregion
    }
}
