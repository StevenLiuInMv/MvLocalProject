namespace MvLocalProject.Viewer
{
    partial class frmMainDev
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainDev));
            DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer dockingContainer1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer();
            this.documentGroup1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup(this.components);
            this.document1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(this.components);
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbgTesting = new DevExpress.XtraNavBar.NavBarGroup();
            this.toolTestProcessBar = new DevExpress.XtraNavBar.NavBarItem();
            this.toolTestExtract = new DevExpress.XtraNavBar.NavBarItem();
            this.toolTestMvSecurityScanBo = new DevExpress.XtraNavBar.NavBarItem();
            this.toolTestDiskScan = new DevExpress.XtraNavBar.NavBarItem();
            this.toolTestAD = new DevExpress.XtraNavBar.NavBarItem();
            this.nbpErpReport = new DevExpress.XtraNavBar.NavBarGroup();
            this.reportBomP09 = new DevExpress.XtraNavBar.NavBarItem();
            this.reportMocP07 = new DevExpress.XtraNavBar.NavBarItem();
            this.reportMocP10 = new DevExpress.XtraNavBar.NavBarItem();
            this.reportPurP17 = new DevExpress.XtraNavBar.NavBarItem();
            this.nbgErpFoms = new DevExpress.XtraNavBar.NavBarGroup();
            this.formBom = new DevExpress.XtraNavBar.NavBarItem();
            this.formMoc = new DevExpress.XtraNavBar.NavBarItem();
            this.formBomCompare = new DevExpress.XtraNavBar.NavBarItem();
            this.formBomCompareDev = new DevExpress.XtraNavBar.NavBarItem();
            this.formBomToMocCompare = new DevExpress.XtraNavBar.NavBarItem();
            this.formErpCreatePR = new DevExpress.XtraNavBar.NavBarItem();
            this.formMcBatchJob = new DevExpress.XtraNavBar.NavBarItem();
            this.nbgItTools = new DevExpress.XtraNavBar.NavBarGroup();
            this.toolItCisco = new DevExpress.XtraNavBar.NavBarItem();
            this.toolItMxMail = new DevExpress.XtraNavBar.NavBarItem();
            this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.documentGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.dockPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
            this.SuspendLayout();
            // 
            // documentGroup1
            // 
            this.documentGroup1.Items.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document[] {
            this.document1});
            // 
            // document1
            // 
            this.document1.Caption = "Dialog";
            this.document1.ControlName = "dockPanel2";
            this.document1.FloatLocation = new System.Drawing.Point(0, 0);
            this.document1.FloatSize = new System.Drawing.Size(200, 200);
            this.document1.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.True;
            this.document1.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;
            this.document1.Properties.AllowFloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1,
            this.dockPanel2});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("b053b59f-56e7-4326-9072-8ec99227e130");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel1.Size = new System.Drawing.Size(200, 600);
            this.dockPanel1.Text = "Menu";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.navBarControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(191, 573);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbgTesting;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbpErpReport,
            this.nbgErpFoms,
            this.nbgItTools,
            this.nbgTesting});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.reportBomP09,
            this.reportMocP07,
            this.reportMocP10,
            this.toolTestProcessBar,
            this.toolTestExtract,
            this.toolTestMvSecurityScanBo,
            this.toolTestDiskScan,
            this.toolTestAD,
            this.formBom,
            this.formBomCompare,
            this.formBomCompareDev,
            this.reportPurP17,
            this.formMoc,
            this.toolItCisco,
            this.toolItMxMail,
            this.formBomToMocCompare,
            this.formErpCreatePR,
            this.formMcBatchJob});
            this.navBarControl1.Location = new System.Drawing.Point(3, 3);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 185;
            this.navBarControl1.Size = new System.Drawing.Size(185, 638);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarControl1_LinkClicked);
            // 
            // nbgTesting
            // 
            this.nbgTesting.Caption = "Testing";
            this.nbgTesting.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("nbgTesting.ImageOptions.LargeImage")));
            this.nbgTesting.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.toolTestProcessBar),
            new DevExpress.XtraNavBar.NavBarItemLink(this.toolTestExtract),
            new DevExpress.XtraNavBar.NavBarItemLink(this.toolTestMvSecurityScanBo),
            new DevExpress.XtraNavBar.NavBarItemLink(this.toolTestDiskScan),
            new DevExpress.XtraNavBar.NavBarItemLink(this.toolTestAD)});
            this.nbgTesting.Name = "nbgTesting";
            this.nbgTesting.Visible = false;
            // 
            // toolTestProcessBar
            // 
            this.toolTestProcessBar.Caption = "TestProcessBar";
            this.toolTestProcessBar.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("toolTestProcessBar.ImageOptions.SmallImage")));
            this.toolTestProcessBar.Name = "toolTestProcessBar";
            // 
            // toolTestExtract
            // 
            this.toolTestExtract.Caption = "TestExtract";
            this.toolTestExtract.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("toolTestExtract.ImageOptions.SmallImage")));
            this.toolTestExtract.Name = "toolTestExtract";
            // 
            // toolTestMvSecurityScanBo
            // 
            this.toolTestMvSecurityScanBo.Caption = "TestMvSecurityScanBo";
            this.toolTestMvSecurityScanBo.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("toolTestMvSecurityScanBo.ImageOptions.SmallImage")));
            this.toolTestMvSecurityScanBo.Name = "toolTestMvSecurityScanBo";
            // 
            // toolTestDiskScan
            // 
            this.toolTestDiskScan.Caption = "TestDiskScan";
            this.toolTestDiskScan.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("toolTestDiskScan.ImageOptions.SmallImage")));
            this.toolTestDiskScan.Name = "toolTestDiskScan";
            // 
            // toolTestAD
            // 
            this.toolTestAD.Caption = "TestAD";
            this.toolTestAD.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("toolTestAD.ImageOptions.SmallImage")));
            this.toolTestAD.Name = "toolTestAD";
            // 
            // nbpErpReport
            // 
            this.nbpErpReport.Caption = "ERP Reports";
            this.nbpErpReport.Expanded = true;
            this.nbpErpReport.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbpErpReport.ImageOptions.SmallImage")));
            this.nbpErpReport.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.reportBomP09),
            new DevExpress.XtraNavBar.NavBarItemLink(this.reportMocP07),
            new DevExpress.XtraNavBar.NavBarItemLink(this.reportMocP10),
            new DevExpress.XtraNavBar.NavBarItemLink(this.reportPurP17)});
            this.nbpErpReport.Name = "nbpErpReport";
            this.nbpErpReport.Visible = false;
            // 
            // reportBomP09
            // 
            this.reportBomP09.Caption = "BomP09";
            this.reportBomP09.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("reportBomP09.ImageOptions.SmallImage")));
            this.reportBomP09.Name = "reportBomP09";
            // 
            // reportMocP07
            // 
            this.reportMocP07.Caption = "MocP07";
            this.reportMocP07.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("reportMocP07.ImageOptions.SmallImage")));
            this.reportMocP07.Name = "reportMocP07";
            // 
            // reportMocP10
            // 
            this.reportMocP10.Caption = "MocP10";
            this.reportMocP10.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("reportMocP10.ImageOptions.SmallImage")));
            this.reportMocP10.Name = "reportMocP10";
            // 
            // reportPurP17
            // 
            this.reportPurP17.Caption = "PurP17(採購預計進貨表)";
            this.reportPurP17.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("reportPurP17.ImageOptions.SmallImage")));
            this.reportPurP17.Name = "reportPurP17";
            // 
            // nbgErpFoms
            // 
            this.nbgErpFoms.Caption = "ERP Forms";
            this.nbgErpFoms.Expanded = true;
            this.nbgErpFoms.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbgErpFoms.ImageOptions.SmallImage")));
            this.nbgErpFoms.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.formBom),
            new DevExpress.XtraNavBar.NavBarItemLink(this.formMoc),
            new DevExpress.XtraNavBar.NavBarItemLink(this.formBomCompare),
            new DevExpress.XtraNavBar.NavBarItemLink(this.formBomCompareDev),
            new DevExpress.XtraNavBar.NavBarItemLink(this.formBomToMocCompare),
            new DevExpress.XtraNavBar.NavBarItemLink(this.formErpCreatePR),
            new DevExpress.XtraNavBar.NavBarItemLink(this.formMcBatchJob)});
            this.nbgErpFoms.Name = "nbgErpFoms";
            // 
            // formBom
            // 
            this.formBom.Caption = "SearchBom";
            this.formBom.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("formBom.ImageOptions.SmallImage")));
            this.formBom.Name = "formBom";
            // 
            // formMoc
            // 
            this.formMoc.Caption = "SearchMoc";
            this.formMoc.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("formMoc.ImageOptions.SmallImage")));
            this.formMoc.Name = "formMoc";
            // 
            // formBomCompare
            // 
            this.formBomCompare.Caption = "BomCompare";
            this.formBomCompare.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("formBomCompare.ImageOptions.SmallImage")));
            this.formBomCompare.Name = "formBomCompare";
            this.formBomCompare.Visible = false;
            // 
            // formBomCompareDev
            // 
            this.formBomCompareDev.Caption = "BomCompareDev";
            this.formBomCompareDev.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("formBomCompareDev.ImageOptions.SmallImage")));
            this.formBomCompareDev.Name = "formBomCompareDev";
            // 
            // formBomToMocCompare
            // 
            this.formBomToMocCompare.Caption = "BomToMocCompare";
            this.formBomToMocCompare.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("formBomToMocCompare.ImageOptions.SmallImage")));
            this.formBomToMocCompare.Name = "formBomToMocCompare";
            // 
            // formErpCreatePR
            // 
            this.formErpCreatePR.Caption = "ErpCreatePR";
            this.formErpCreatePR.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("formErpCreatePR.ImageOptions.SmallImage")));
            this.formErpCreatePR.Name = "formErpCreatePR";
            // 
            // formMcBatchJob
            // 
            this.formMcBatchJob.Caption = "McBatchJob";
            this.formMcBatchJob.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("formMcBatchJob.ImageOptions.SmallImage")));
            this.formMcBatchJob.Name = "formMcBatchJob";
            // 
            // nbgItTools
            // 
            this.nbgItTools.Caption = "ItTools";
            this.nbgItTools.Expanded = true;
            this.nbgItTools.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbgItTools.ImageOptions.SmallImage")));
            this.nbgItTools.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.toolItCisco),
            new DevExpress.XtraNavBar.NavBarItemLink(this.toolItMxMail)});
            this.nbgItTools.Name = "nbgItTools";
            // 
            // toolItCisco
            // 
            this.toolItCisco.Caption = "Cisco";
            this.toolItCisco.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("toolItCisco.ImageOptions.SmallImage")));
            this.toolItCisco.Name = "toolItCisco";
            // 
            // toolItMxMail
            // 
            this.toolItMxMail.Caption = "MxMail";
            this.toolItMxMail.ImageOptions.SmallImage = ((System.Drawing.Image)(resources.GetObject("toolItMxMail.ImageOptions.SmallImage")));
            this.toolItMxMail.Name = "toolItMxMail";
            // 
            // dockPanel2
            // 
            this.dockPanel2.Controls.Add(this.dockPanel2_Container);
            this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float;
            this.dockPanel2.DockedAsTabbedDocument = true;
            this.dockPanel2.ID = new System.Guid("3d71af3a-70d5-4251-a1be-4f72f561039a");
            this.dockPanel2.Location = new System.Drawing.Point(0, 0);
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel2.Size = new System.Drawing.Size(513, 571);
            this.dockPanel2.Text = "Dialog";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(513, 571);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // documentManager1
            // 
            this.documentManager1.ContainerControl = this;
            this.documentManager1.View = this.tabbedView1;
            this.documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView1});
            // 
            // tabbedView1
            // 
            this.tabbedView1.DocumentGroups.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup[] {
            this.documentGroup1});
            this.tabbedView1.Documents.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseDocument[] {
            this.document1});
            dockingContainer1.Element = this.documentGroup1;
            this.tabbedView1.RootContainer.Nodes.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer[] {
            dockingContainer1});
            // 
            // frmMainDev
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 600);
            this.Controls.Add(this.dockPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMainDev";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMainDev";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMainDev_FormClosed);
            this.Load += new System.EventHandler(this.frmMainDev_Load);
            ((System.ComponentModel.ISupportInitialize)(this.documentGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.dockPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbpErpReport;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup documentGroup1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.Document document1;
        private DevExpress.XtraNavBar.NavBarItem reportBomP09;
        private DevExpress.XtraNavBar.NavBarItem reportMocP07;
        private DevExpress.XtraNavBar.NavBarGroup nbgErpFoms;
        private DevExpress.XtraNavBar.NavBarItem reportMocP10;
        private DevExpress.XtraNavBar.NavBarGroup nbgItTools;
        private DevExpress.XtraNavBar.NavBarItem toolTestProcessBar;
        private DevExpress.XtraNavBar.NavBarItem toolTestExtract;
        private DevExpress.XtraNavBar.NavBarItem toolTestMvSecurityScanBo;
        private DevExpress.XtraNavBar.NavBarItem toolTestDiskScan;
        private DevExpress.XtraNavBar.NavBarItem toolTestAD;
        private DevExpress.XtraNavBar.NavBarItem formBom;
        private DevExpress.XtraNavBar.NavBarItem formBomCompare;
        private DevExpress.XtraNavBar.NavBarItem formBomCompareDev;
        private DevExpress.XtraNavBar.NavBarItem reportPurP17;
        private DevExpress.XtraNavBar.NavBarItem formMoc;
        private DevExpress.XtraNavBar.NavBarGroup nbgTesting;
        private DevExpress.XtraNavBar.NavBarItem toolItCisco;
        private DevExpress.XtraNavBar.NavBarItem toolItMxMail;
        private DevExpress.XtraNavBar.NavBarItem formBomToMocCompare;
        private DevExpress.XtraNavBar.NavBarItem formErpCreatePR;
        private DevExpress.XtraNavBar.NavBarItem formMcBatchJob;
    }
}