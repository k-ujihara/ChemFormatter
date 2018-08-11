using System.Text;

namespace ChemFormatter
{
    public static class Utility
    {
        public static string Normalize(string str)
        {
            var sb = new StringBuilder();
            var n = str.Length;
            for (int i = 0; i < n; i++)
            {
                var c = Normalize(str[i]);
                sb.Append(c);
            }
            return sb.ToString();
        }

        public static char Normalize(char c)
        {
            // It should be re-written for Mac version.
            const int StartBasicLatin = 0x20;
            const int StartZenkaku = 0xFF00;
            const int EndZenkaku = 0xFF5F;
            // const int StartHanKana = 0xFF61;
            // const int EndHanKana = 0xFF9F;
            int i = (int)c;
            switch (i)
            {
                case 0x2019: //Zenkaku "'"
                    return '\'';
                case 0x3000: // ZenkakuSpace
                    return ' ';
                case 0xFFE0: // cent
                    return '¢';
                case 0xFFE1: // pond
                    return '£';
                case 0xFFE2: // not
                    return '¬';
                case 0xFFE3: // overline
                    return '¯';
                case 0xFFE4: // HARETSUSEN
                    return '¦';
                case 0xFFE5: // Japanese Yen
                    return '¥';
                case 0xFFE8: // TATESEN
                    return '|';
                default:
                    if (0xFE50 <= i && i <= 0xFE6F)
                    {
                        const string t = ",、.・;:?!-(){}[]#"
                                       + "&*+-<>=あ\\$%@ああああ";
                        var cc = t[i - 0xFE50];
                        if (cc != 'あ')
                            return cc;
                    }
                    if (StartZenkaku <= i && i <= EndZenkaku)
                        return (char)(i - StartZenkaku + StartBasicLatin);
                    if (0xFFE9 <= i && i <= 0xFFEC) // ARROWS
                        return (char)(i - 0xFFE9 + 0x2190);
                    return c;
            }
        }
    }
}
