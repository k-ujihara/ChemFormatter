# ChemFormatter

ChemFormatter is a chemical formatter add-in for Microsoft Word, Excel, and PowerPoint. It is implemented using VSTO.

## How to Use

Select text to format and click one of the below button in ChemFormatter tab within Add-in ribbon.

### <img src="ChemFormatter.Lib/Resources/RDigitChanger.png?raw=true" width="32" height="32" border="1" /> Change sub-digit style to subscript/superscript.

R1 ⇒ R<sub>1</sub> ⇒ R<sup>1</sup>

R1-3 ⇒ R<sub>1-3</sub> ⇒ R<sup>1-3</sup>

### <img src="ChemFormatter.Lib/Resources/ChemFormula.png?raw=true" width="32" height="32" border="1" /> Set style as chemical formula.

CH3OH ⇒ CH<sub>3</sub>OH

HC#CCH2OH ⇒ HC≡CCH<sub>2</sub>OH

(CH3)2CHOH ⇒ (CH<sub>3</sub>)<sub>2</sub>CHOH

t-BuOH ⇒ <i>t</i>-BuOH

### <img src="ChemFormatter.Lib/Resources/IonFormula.png?raw=true" width="32" height="32" border="1" /> Set style as chemical ion formula.

Na+ + Cl- → NaCl ⇒ Na<sup>+</sup> + Cl<sup>-</sup> → NaCl

Ca2+ + SO42- → CaSO4 ⇒ Ca<sup>2+</sup> + SO<sub>4</sub><sup>2-</sup> → CaSO<sub>4</sub>

### <img src="ChemFormatter.Lib/Resources/ChemName.png?raw=true" width="32" height="32" border="1" /> Set style as chemical name.

tert-butyldimethylsilane ⇒ <i>tert</i>-butyldimethylsilane

(E)-olefine ⇒ (<i>E</i>)-olefine

(2E,4Z)-hexa-2,4-diene ⇒ (2<i>E</i>,4<i>Z</i>)-hexa-2,4-diene

L-phenylalanine ⇒ <small>L</small>-phenylalanine

acetone O-ethyl S-methyl monothioketal ⇒ acetone <i>O</i>-ethyl <i>S</i>-methyl monothioketal

dichloro(N,N,N',N'-tetramethylethylenediamine)zinc(II) ⇒ dichloro(<i>N</i>,<i>N</i>,<i>N</i>',<i>N</i>'-tetramethylethylenediamine)zinc(II)

### <img src="ChemFormatter.Lib/Resources/Journal.png?raw=true" width="32" height="32" border="1" /> Style journal references.

<i>J. Am. Chem. Soc.</i> <b>2017</b>, <i>123</i>, 1.

<i>J. Am. Chem. Soc.</i> <b>2017</b>, <i>123</i>, 1-10.

<i>J. Am. Chem. Soc.</i> <b>2017</b>, <i>123</i>(1), 1.

<i>J. Am. Chem. Soc.</i> <b>2017</b>, <i>123</i>(1), 1-10.

<i>Proc. Natl. Acad. Sci. USA</i> <b>104</b>, 1234-1235 (2007).

<i>Proc. Natl. Acad. Sci. USA</i> <b>104</b>, 1234 (2007).

<i>Proc. Natl. Acad. Sci. USA</i> <b>104</b>(10), 1234-1235 (2007).

<i>Proc. Natl. Acad. Sci. USA</i> <b>104</b>, 1234–1235 (2007).

<i>Phys. Rev.</i>, vol. 134, pp. A635-A646, Dec. 1965.

<i>IEEE Trans. Electron. Devices</i>, vol. ED-11, pp. 34-39, Jan. 1959.

<i>IEEE Trans. Comp.</i>, vol. 46, pp. 695-701, June 1997.

<i>IEEE Transactions on Pattern Analysis and Machine Intelligence</i>, vol. 19, pp. 564-579, June 1997.

<i>Microelectronics Reliability</i>, vol. 33, no. 9, pp. 1387-1395, Dec. 1993.

<i>IEEE Transactions on Antennas and Propagation</i>, to be published.

### <img src="ChemFormatter.Lib/Resources/alphaD.png?raw=true" width="32" height="32" border="1" /> Format optical rotation.

[a]D +25.5 ⇒ [α]<sub>D</sub> +25.5

[a]D20 +25.5 ⇒ [α]<sub>D</sub><sup>20</sup> +25.5

[a]D20 +25.5 (c 1.5) ⇒ [α]<sub>D</sub><sup>20</sup> +25.5 (<i>c</i> 1.5)

[a]D20 +25.5 (c 1.5, CHCl<sub>3</sub>) ⇒ [α]<sub>D</sub><sup>20</sup> +25.5 (<i>c</i> 1.5, CHCl<sub>3</sub>)

[a]D20 -25.5 (c 1.5, CHCl<sub>3</sub>) ⇒ [α]<sub>D</sub><sup>20</sup> -25.5 (<i>c</i> 1.5, CHCl<sub>3</sub>)

[a]D20 -25.5° (c 1.5, CHCl<sub>3</sub>) ⇒ [α]<sub>D</sub><sup>20</sup> -25.5° (<i>c</i> 1.5, CHCl<sub>3</sub>)

### <img src="ChemFormatter.Lib/Resources/NMRSpectrum.png?raw=true" width="32" height="32" border="1" /> Format NMR spectrum specification from tab separated table.

1.45&nbsp;<font color="gray">␉</font>&nbsp;&nbsp;1H

1.55&nbsp;<font color="gray">␉</font>&nbsp;&nbsp;3H&nbsp;<font color="gray">␉</font>&nbsp;&nbsp;d&nbsp;<font color="gray">␉</font>&nbsp;&nbsp;1.3

1.95&nbsp;<font color="gray">␉</font>&nbsp;&nbsp;1.4H&nbsp;<font color="gray">␉</font>&nbsp;&nbsp;t&nbsp;<font color="gray">␉</font>&nbsp;&nbsp;7.3, 2.3&nbsp;<font color="gray">␉</font>&nbsp;&nbsp;CH<sub>3</sub>

2.2-3.2&nbsp;<font color="gray">␉</font>&nbsp;&nbsp;15H&nbsp;<font color="gray">␉</font>&nbsp;&nbsp;m

&nbsp;↓&nbsp;↑

1.45 (1H, s), 1.55 (3H, d, <i>J</i> = 1.3 Hz), 1.95 (1.4H, dd, <i>J</i> = 7.3, 2.3 Hz, CH<sub>3</sub>), 2.2-3.2 (15H, m).

## Old versions

The old versions are available at [SourceForge](https://sourceforge.net/projects/chemformatter/). The Microsoft Word template version works on both Windows and Mac. Very old COM add-in version implemented by Visual Basic 6.0 works on Word, Excel, PowerPoint, Publisher, and Visio.
