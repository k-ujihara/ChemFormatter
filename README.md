# ChemFormatter

ChemFormatter is a chemical formatter add-in for Microsoft Office. It is implemented using VSTO. 
The Microsoft Word template version also available at [SourceForge](https://sourceforge.net/projects/chemformatter/), which is works on both Windows and Mac.

Currently Microsoft Word version is implemented.

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
(2E,4Z)-hexadiene ⇒ (2<i>E</i>,4<i>Z</i>)-hexadiene
L-phenylalanine ⇒ <span style="font-variant: small-caps;">l</span>-phenylalanine
acetone O-ethyl S-methyl monothioketal ⇒ acetone <i>O</i>-ethyl <i>S</i>-methyl monothioketal
dichloro(N,N,N',N'-tetramethylethylenediamine)zinc(II) ⇒ dichloro(<i>N</i>,<i>N</i>,<i>N</i>',<i>N</i>'-tetramethylethylenediamine)zinc(II)

### <img src="ChemFormatter.Lib/Resources/Journal.png?raw=true" width="32" height="32" border="1" /> Set style as a journal reference.

<i>J. Am. Chem. Soc.</i> <b>2017</b>, <i>123</i>, 1.
<i>J. Am. Chem. Soc.</i> <b>2017</b>, <i>123</i>, 1-10.
<i>J. Am. Chem. Soc.</i> <b>2017</b>, <i>123</i>(1), 1.
<i>J. Am. Chem. Soc.</i> <b>2017</b>, <i>123</i>(1), 1-10.
<i>Proc. Natl. Acad. Sci. USA</i> <b>104</b>, 1234-1235 (2007).
<i>Proc. Natl. Acad. Sci. USA</i> <b>104</b>, 1234 (2007).
<i>Proc. Natl. Acad. Sci. USA</i> <b>104</b>(10), 1234-1235 (2007).
<i>Proc. Natl. Acad. Sci. USA</i> <b>104</b>, 1234–1235 (2007).
