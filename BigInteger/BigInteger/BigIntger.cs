using System;
using System.Linq;
using System.Text;

namespace Sample.Library
{
    public class BigIntger : IEquatable<BigIntger>
    {
        private string _bigInteger;
        public BigIntger(int value)
        {
            _bigInteger = value.ToString();
        }
        public BigIntger(string value)
        {
            _bigInteger = value;
        }

        public string GetCharctor(int index)
        {
            return (0 <= index) && (index < _bigInteger.Length) ? _bigInteger[index].ToString() : "0";
        }

        public static BigIntger operator +(BigIntger x, BigIntger y)
        {
            var xLength = x.ToString().Length;
            var yLength = y.ToString().Length;
            var max = xLength < yLength ? yLength : xLength;

            var up = false;
            var sb = new StringBuilder();
            for (int i = 0; (i < max) || up; i++)
            {
                var numX = 0;
                int.TryParse(x.GetCharctor(xLength - i - 1), out numX);
                var numY = 0;
                int.TryParse(y.GetCharctor(yLength - i - 1), out numY);

                var num = numX + numY;

                if (up)
                {
                    num++;
                    up = false;
                }

                if (9 < num)
                {
                    num -= 10;
                    up = true;
                }
                sb.Insert(0, num.ToString());
            }
            return new BigIntger(sb.ToString());
        }

        public static BigIntger operator ++(BigIntger x)
        {
            var tmp = x + new BigIntger(1);
            return tmp;
        }

        //xが大きい場合のみ
        public static BigIntger operator -(BigIntger x, BigIntger y)
        {
            var xLength = x.ToString().Length;
            var yLength = y.ToString().Length;
            var max = xLength < yLength ? yLength : xLength;

            var down = false;
            var sb = new StringBuilder();
            for (int i = 0; (i < max) || down; i++)
            {
                var numX = 0;
                int.TryParse(x.GetCharctor(xLength - i - 1), out numX);
                var numY = 0;
                int.TryParse(y.GetCharctor(yLength - i - 1), out numY);

                var num = numX - numY;

                if (down)
                {
                    num--;
                    down = false;
                }

                if (num < 0)
                {
                    num += 10;
                    down = true;
                }
                sb.Insert(0, num.ToString());

            }
            var numText = string.Concat(sb.ToString().SkipWhile(c => c == '0'));
            return new BigIntger(numText);
        }

        public static BigIntger operator *(int x, BigIntger y)
        {
            var result = new BigIntger(0);
            for (int i = 0; i < x; i++)
            {
                result += y;
            }
            return result;
        }

        private bool IsZero()
        {
            return String.IsNullOrEmpty(_bigInteger) || (_bigInteger == "0");
        }

        public override string ToString()
        {
            return _bigInteger;
        }

        public bool Equals(BigIntger other)
        {
            return _bigInteger == other.ToString();
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as BigIntger);
        }
        public override int GetHashCode()
        {
            return _bigInteger.GetHashCode();
        }
    }
}
