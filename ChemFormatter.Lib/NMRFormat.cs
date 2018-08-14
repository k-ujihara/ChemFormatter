using System;
using System.Collections.Generic;

namespace ChemFormatter
{
    public class NMRFormat
    {
        internal delegate bool AddSpec(List<PCommand> commands, PeakInfo info, bool isFirst);
        internal delegate void AddTab(List<PCommand> commands, PeakInfo info);

        public static NMRFormat Default => IPJC;

        public static NMRFormat IPJC { get; } = new NMRFormat
        {
            Label = "1H, d, *J* = 7.3 Hz",
            ActionsToSpec = new AddSpec[]
            {
                NMRSpectrumQuery.AddSpecI,
                NMRSpectrumQuery.AddSpecPJ_I,
            },
            ActionsToTab = new AddTab[]
            {
                NMRSpectrumQuery.AddTabI,
                NMRSpectrumQuery.AddTabPJ,
            },
        };

        public static NMRFormat PJIC { get; } = new NMRFormat
        {
            Label = "d, *J* = 7.3 Hz, 1H",
            ActionsToSpec = new AddSpec[]
            {
                NMRSpectrumQuery.AddSpecPJ_I,
                NMRSpectrumQuery.AddSpecI,
            },
            ActionsToTab = new AddTab[]
            {
                NMRSpectrumQuery.AddTabPJ,
                NMRSpectrumQuery.AddTabI,
            },
        };

        public static NMRFormat PlainIPJC { get; } = new NMRFormat
        {
            Label = "1H, d, J = 7.3 Hz",
            ActionsToSpec = new AddSpec[]
            {
                NMRSpectrumQuery.AddSpecI,
                NMRSpectrumQuery.AddSpecPJ_N,
            },
            ActionsToTab = IPJC.ActionsToTab,
        };

        public static NMRFormat PlainPJIC { get; } = new NMRFormat
        {
            Label = "d, J = 7.3 Hz, 1H",
            ActionsToSpec = new AddSpec[]
            {
                NMRSpectrumQuery.AddSpecPJ_N,
                NMRSpectrumQuery.AddSpecI,
            },
            ActionsToTab = PJIC.ActionsToTab,
        };

        private static readonly IReadOnlyList<NMRFormat> Values = new NMRFormat[] { IPJC, PJIC, PlainIPJC, PlainPJIC, };

        public static NMRFormat FromLabel(string label)
        {
            foreach (var value in Values)
            {
                if (value.Label == label)
                    return value;
            }
            return Default;
        }

        public string Label { get; private set; }
        internal IEnumerable<AddSpec> ActionsToSpec { get; private set; }
        internal IEnumerable<AddTab> ActionsToTab { get; private set; }

        private NMRFormat()
        {
        }
    }
}
