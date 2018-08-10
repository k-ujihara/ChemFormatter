

using Microsoft.Office.Tools.Ribbon;
using static ChemFormatter.CommonResourceManager;

namespace ChemFormatter.WordAddIn
{
    public class Ribbon : RibbonBase
    {
        private void Ribbon_Load(object sender, RibbonUIEventArgs e)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ribbon));
            this.tabAddIns = this.Factory.CreateRibbonTab();
            this.groupChemFormatter = this.Factory.CreateRibbonGroup();
            this.buttonRDigitChanger = this.Factory.CreateRibbonButton();
            this.tabAddIns.SuspendLayout();
            this.groupChemFormatter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabAddIns
            // 
            this.tabAddIns.ControlId.ControlIdType = RibbonControlIdType.Office;
            this.tabAddIns.Groups.Add(this.groupChemFormatter);
            this.tabAddIns.Label = "TabAddIns";
            this.tabAddIns.Name = "tabAddIns";
            // 
            // groupChemFormatter
            // 
            this.groupChemFormatter.Items.Add(this.buttonRDigitChanger);
            this.groupChemFormatter.Label = "ChemFormatter";
            this.groupChemFormatter.Name = "groupChemFormatter";

            // 
            // buttonRDigitChanger
            // 
            this.buttonRDigitChanger.Image = CommonResourceManager.GetImage(RDigitChangerImage);
			this.buttonRDigitChanger.Label = "Sub-digits Changer";
            this.buttonRDigitChanger.Name = "buttonRDigitChanger";
            this.buttonRDigitChanger.Click += new RibbonControlEventHandler((sender, e) => Applyer.ButtonRDigitChanger_Click(sender, e));
            this.buttonRDigitChanger.ShowLabel = true;
            this.buttonRDigitChanger.ShowImage = true;
            // 
            // Ribbon
            // 
            this.Name = "Ribbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tabAddIns);
            this.Load += new RibbonUIEventHandler(this.Ribbon_Load);
            this.tabAddIns.ResumeLayout(false);
            this.tabAddIns.PerformLayout();
            this.groupChemFormatter.ResumeLayout(false);
            this.groupChemFormatter.PerformLayout();
            this.ResumeLayout(false);
        }

        internal RibbonTab tabAddIns;
        internal RibbonGroup groupChemFormatter;
        internal RibbonButton buttonRDigitChanger;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon Ribbon
        {
            get { return this.GetRibbon<Ribbon>(); }
        }
    }
}

