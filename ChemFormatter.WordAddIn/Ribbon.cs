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



namespace ChemFormatter.WordAddIn
{
    public partial class Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        private void Ribbon_Load(object sender, Microsoft.Office.Tools.Ribbon.RibbonUIEventArgs e)
        {
        }

        private System.ComponentModel.IContainer components = null;

        public Ribbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabAddIns = this.Factory.CreateRibbonTab();
            this.groupChemFormatter = this.Factory.CreateRibbonGroup();
            this.buttonRDigitChanger = this.Factory.CreateRibbonButton();
            this.buttonChemFormula = this.Factory.CreateRibbonButton();
            this.buttonIonFormula = this.Factory.CreateRibbonButton();
            this.buttonChemName = this.Factory.CreateRibbonButton();
            this.buttonStyleCitation = this.Factory.CreateRibbonButton();
            this.buttonNMRSpec = this.Factory.CreateRibbonButton();
            this.dropDownNMRFormat = this.Factory.CreateRibbonDropDown();
            this.dropDownItem_NMRIPJC = this.Factory.CreateRibbonDropDownItem();
            this.dropDownItem_NMRPJIC = this.Factory.CreateRibbonDropDownItem();
            this.dropDownItem_NMRIPJC_N = this.Factory.CreateRibbonDropDownItem();
            this.dropDownItem_NMRPJIC_N = this.Factory.CreateRibbonDropDownItem();

            this.tabAddIns.SuspendLayout();
            this.groupChemFormatter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabAddIns
            // 
            this.tabAddIns.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tabAddIns.Groups.Add(this.groupChemFormatter);
            this.tabAddIns.Label = "TabAddIns";
            this.tabAddIns.Name = "tabAddIns";
            // 
            // groupChemFormatter
            // 
            this.groupChemFormatter.Items.Add(this.buttonRDigitChanger);
            this.groupChemFormatter.Items.Add(this.buttonChemFormula);
            this.groupChemFormatter.Items.Add(this.buttonIonFormula);
            this.groupChemFormatter.Items.Add(this.buttonChemName);
            this.groupChemFormatter.Items.Add(this.buttonStyleCitation);
            this.groupChemFormatter.Items.Add(this.buttonNMRSpec);
            this.groupChemFormatter.Items.Add(this.dropDownNMRFormat);
            this.groupChemFormatter.Label = "ChemFormatter";
            this.groupChemFormatter.Name = "groupChemFormatter";

            // 
            // buttonRDigitChanger
            // 
            {
                var im = CommonResourceManager.GetImage(CommonResourceManager.RDigitChangerImage);
                if (im != null)
                    this.buttonRDigitChanger.Image = im;
            }
            this.buttonRDigitChanger.Label = "Sub-digits Change";
            this.buttonRDigitChanger.Name = "buttonRDigitChanger";
            this.buttonRDigitChanger.Click += (sender, e) => Globals.ThisAddIn.ButtonRDigitChanger_Click(sender, e);
            this.buttonRDigitChanger.ShowLabel = true;
            this.buttonRDigitChanger.ShowImage = true;
            // 
            // buttonChemFormula
            // 
            {
                var im = CommonResourceManager.GetImage(CommonResourceManager.ChemFormulaImage);
                if (im != null)
                    this.buttonChemFormula.Image = im;
            }
            this.buttonChemFormula.Label = "Chem Formula";
            this.buttonChemFormula.Name = "buttonChemFormula";
            this.buttonChemFormula.Click += (sender, e) => Globals.ThisAddIn.ButtonChemFormular_Click(sender, e);
            this.buttonChemFormula.ShowLabel = true;
            this.buttonChemFormula.ShowImage = true;
            // 
            // buttonIonFormula
            // 
            {
                var im = CommonResourceManager.GetImage(CommonResourceManager.IonFormulaImage);
                if (im != null)
                    this.buttonIonFormula.Image = im;
            }
            this.buttonIonFormula.Label = "Ion Formula";
            this.buttonIonFormula.Name = "buttonIonFormula";
            this.buttonIonFormula.Click += (sender, e) => Globals.ThisAddIn.ButtonIonFormular_Click(sender, e);
            this.buttonIonFormula.ShowLabel = true;
            this.buttonIonFormula.ShowImage = true;
            // 
            // buttonChemName
            // 
            {
                var im = CommonResourceManager.GetImage(CommonResourceManager.ChemNameImage);
                if (im != null)
                    this.buttonChemName.Image = im;
            }
            this.buttonChemName.Label = "Chem Name";
            this.buttonChemName.Name = "buttonChemName";
            this.buttonChemName.Click += (sender, e) => Globals.ThisAddIn.ButtonChemName_Click(sender, e);
            this.buttonChemName.ShowLabel = true;
            this.buttonChemName.ShowImage = true;
            // 
            // buttonStyleCitation
            // 
            {
                var im = CommonResourceManager.GetImage(CommonResourceManager.JournalReferenceImage);
                if (im != null)
                    this.buttonStyleCitation.Image = im;
            }
            this.buttonStyleCitation.Label = "Citation";
            this.buttonStyleCitation.Name = "buttonStyleCitation";
            this.buttonStyleCitation.Click += (sender, e) => Globals.ThisAddIn.ButtonStyleCitation_Click(sender, e);
            this.buttonStyleCitation.ShowLabel = true;
            this.buttonStyleCitation.ShowImage = true;
            // 
            // buttonNMRSpec
            // 
            {
                var im = CommonResourceManager.GetImage(CommonResourceManager.NMRSpecImage);
                if (im != null)
                    this.buttonNMRSpec.Image = im;
            }
            this.buttonNMRSpec.Label = "NMR Spec";
            this.buttonNMRSpec.Name = "buttonNMRSpec";
            this.buttonNMRSpec.Click += (sender, e) => Globals.ThisAddIn.ButtonNMRSpec_Click(sender, e);
            this.buttonNMRSpec.ShowLabel = true;
            this.buttonNMRSpec.ShowImage = true;
            // 
            // dropDownNMRFormat
            // 
            this.dropDownNMRFormat.Label = "NMR Format";
            this.dropDownNMRFormat.Name = "dropDownNMRFormat";
			this.dropDownNMRFormat.SelectionChanged += (sender, e) => Globals.ThisAddIn.DropDownNMRFormat_SelectionChanged(sender, e);
            this.dropDownNMRFormat.ShowLabel = true;
            this.dropDownNMRFormat.ShowImage = false;
            this.dropDownNMRFormat.ShowItemLabel = false;
            this.dropDownNMRFormat.ShowItemImage = true;
            {
                var im = CommonResourceManager.GetImage(CommonResourceManager.NMRFormat_IPJC);
                if (im != null)
                    this.dropDownItem_NMRIPJC.Image = im;
                var f = ChemFormatter.NMRFormat.IPJC;
                this.dropDownItem_NMRIPJC.Label = f.Label;
                this.dropDownItem_NMRIPJC.Tag = f;
                this.dropDownNMRFormat.Items.Add(this.dropDownItem_NMRIPJC);
            }
            {
                var im = CommonResourceManager.GetImage(CommonResourceManager.NMRFormat_PJIC);
                if (im != null)
                    this.dropDownItem_NMRPJIC.Image = im;
                var f = ChemFormatter.NMRFormat.PJIC;
                this.dropDownItem_NMRPJIC.Label = f.Label;
                this.dropDownItem_NMRPJIC.Tag = f;
                this.dropDownNMRFormat.Items.Add(this.dropDownItem_NMRPJIC);
            }
            {
                var im = CommonResourceManager.GetImage(CommonResourceManager.NMRFormat_PlainIPJC);
                if (im != null)
                    this.dropDownItem_NMRIPJC_N.Image = im;
                var f = ChemFormatter.NMRFormat.PlainIPJC;
                this.dropDownItem_NMRIPJC_N.Label = f.Label;
                this.dropDownItem_NMRIPJC_N.Tag = f;
                this.dropDownNMRFormat.Items.Add(this.dropDownItem_NMRIPJC_N);
            }
            {
                var im = CommonResourceManager.GetImage(CommonResourceManager.NMRFormat_PlainPJIC);
                if (im != null)
                    this.dropDownItem_NMRPJIC_N.Image = im;
                var f = ChemFormatter.NMRFormat.PlainPJIC;
                this.dropDownItem_NMRPJIC_N.Label = f.Label;
                this.dropDownItem_NMRPJIC_N.Tag = f;
                this.dropDownNMRFormat.Items.Add(this.dropDownItem_NMRPJIC_N);
            }
            // 
            // Ribbon
            // 
            this.Name = "Ribbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tabAddIns);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler((sender, e) => Ribbon_Load(sender, e));
            this.tabAddIns.ResumeLayout(false);
            this.tabAddIns.PerformLayout();
            this.groupChemFormatter.ResumeLayout(false);
            this.groupChemFormatter.PerformLayout();
            this.ResumeLayout(false);
        }

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabAddIns;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupChemFormatter;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonRDigitChanger;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonChemFormula;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonIonFormula;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonChemName;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonStyleCitation;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonNMRSpec;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dropDownNMRFormat;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDownItem dropDownItem_NMRIPJC;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDownItem dropDownItem_NMRPJIC;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDownItem dropDownItem_NMRIPJC_N;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDownItem dropDownItem_NMRPJIC_N;
    }
    
    partial class ThisRibbonCollection
    {
        internal Ribbon Ribbon
        {
            get { return this.GetRibbon<Ribbon>(); }
        }
    }
}
