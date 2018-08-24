// MIT License
// 
// Copyright (c) 2018 Kazuya Ujihara
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ChemFormatter
{
    /// <summary>
    /// Convert <sup>13</sup>CHCl<sub>3</sub> to ¹³CHCl₃
    /// </summary>
    public static class StyleByCharQuery
    {
        public static Dictionary<char, char> SubscriptConversionTable { get; } = new Dictionary<char, char>()
        {
            ['a'] = 'ₐ',
            ['e'] = 'ₑ',
            ['h'] = 'ₕ',
            ['i'] = 'ᵢ',
            ['j'] = 'ⱼ',
            ['k'] = 'ₖ',
            ['l'] = 'ₗ',
            ['m'] = 'ₘ',
            ['n'] = 'ₙ',
            ['o'] = 'ₒ',
            ['p'] = 'ₚ',
            ['s'] = 'ₛ',
            ['r'] = 'ᵣ',
            ['u'] = 'ᵤ',
            ['v'] = 'ᵥ',
            ['x'] = 'ₓ',
            ['0'] = '₀',
            ['1'] = '₁',
            ['2'] = '₂',
            ['3'] = '₃',
            ['4'] = '₄',
            ['5'] = '₅',
            ['6'] = '₆',
            ['7'] = '₇',
            ['8'] = '₈',
            ['9'] = '₉',
            ['+'] = '₊',
            ['-'] = '₋',
            ['='] = '₌',
            ['('] = '₍',
            [')'] = '₎',
        };

        public static Dictionary<char, char> SuperscriptConversionTable { get; } = new Dictionary<char, char>()
        {
            ['A'] = 'ᴬ',
            ['B'] = 'ᴮ',
            ['D'] = 'ᴰ',
            ['E'] = 'ᴱ',
            ['G'] = 'ᴳ',
            ['H'] = 'ᴴ',
            ['I'] = 'ᴵ',
            ['J'] = 'ᴶ',
            ['K'] = 'ᴷ',
            ['L'] = 'ᴸ',
            ['M'] = 'ᴹ',
            ['N'] = 'ᴺ',
            ['O'] = 'ᴼ',
            ['P'] = 'ᴾ',
            ['R'] = 'ᴿ',
            ['T'] = 'ᵀ',
            ['U'] = 'ᵁ',
            ['V'] = 'ⱽ',
            ['W'] = 'ᵂ',
            ['a'] = 'ᵃ',
            ['b'] = 'ᵇ',
            ['c'] = 'ᶜ',
            ['d'] = 'ᵈ',
            ['e'] = 'ᵉ',
            ['g'] = 'ᵍ',
            ['h'] = 'ʰ',
            ['i'] = 'ⁱ',
            ['j'] = 'ʲ',
            ['k'] = 'ᵏ',
            ['l'] = 'ˡ',
            ['m'] = 'ᵐ',
            ['n'] = 'ⁿ',
            ['o'] = 'ᵒ',
            ['p'] = 'ᵖ',
            ['r'] = 'ʳ',
            ['t'] = 'ᵗ',
            ['u'] = 'ᵘ',
            ['v'] = 'ᵛ',
            ['w'] = 'ʷ',
            ['x'] = 'ˣ',
            ['y'] = 'ʸ',
            ['z'] = 'ᶻ',
            ['0'] = '⁰',
            ['1'] = '¹',
            ['2'] = '²',
            ['3'] = '³',
            ['4'] = '⁴',
            ['5'] = '⁵',
            ['6'] = '⁶',
            ['7'] = '⁷',
            ['8'] = '⁸',
            ['9'] = '⁹',
            ['+'] = '⁺',
            ['-'] = '⁻',
            ['='] = '⁼',
            ['('] = '⁽',
            [')'] = '⁾',
        };

        public static Dictionary<char, char> SubscriptRevTable { get; } = MakeRev(SubscriptConversionTable);
        public static Dictionary<char, char> SuperscriptRevTable { get; } = MakeRev(SuperscriptConversionTable);

        private static Dictionary<char, char> MakeRev(Dictionary<char, char> map)
        {
            var newMap = new Dictionary<char, char>();
            foreach (var e in map)
                newMap[e.Value] = e.Key;
            return newMap;
        }

        public static SortedSet<char> StylableChars = MakeChars(SubscriptConversionTable.Keys, SuperscriptConversionTable.Keys);
        public static SortedSet<char> StyledChars = MakeChars(SubscriptConversionTable.Values, SuperscriptConversionTable.Values);

        static Regex ReStylableChars = new Regex(MakeRep(StylableChars), RegexOptions.Compiled);
        static Regex ReStyledChars = new Regex(MakeRep(StyledChars), RegexOptions.Compiled);

        private static SortedSet<char> MakeChars(params IEnumerable<char>[] lists)
        {
            var chars = new SortedSet<char>();
            foreach (var list in lists)
                chars.UnionWith(list);
            return chars;
        }

        private static string MakeRep(IEnumerable<char> list)
        {
            var sb = new StringBuilder();
            sb.Append(@"(?<c>[");
            foreach (var c in list)
            {
                if (!(('a' <= c && c <= 'z')
                   || ('A' <= c && c <= 'Z')
                   || ('0' <= c && c <= '9')
                   || c == '_'
                   || c >= 0x128))
                {
                    sb.Append('\\');
                }
                sb.Append(c);
            }
            sb.Append("])");

            return sb.ToString();
        }

        public static IEnumerable<PCommand> MakeCommand(string text)
        {
            var commands = new List<PCommand>();

            Func<int, int, RangeCommand> commandMaker;
            Regex finder;
            if (ReStyledChars.IsMatch(text))
            {
                commandMaker = (index, length) => new UnstyleByCharCommand(index, length);
                finder = ReStyledChars;
            }
            else
            {
                commandMaker = (index, length) => new StyleByCharCommand(index, length);
                finder = ReStylableChars;
            }

            foreach (Match match in finder.Matches(text))
            {
                Group g;
                g = match.Groups["c"];
                if (g.Success)
                    commands.Add(commandMaker(g.Index, g.Length));
            }

            return commands;
        }
    }
}
