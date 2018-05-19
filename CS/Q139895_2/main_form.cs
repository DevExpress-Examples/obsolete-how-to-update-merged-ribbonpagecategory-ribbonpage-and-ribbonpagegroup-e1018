using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;

namespace DXSample {
    public class MainForm :RibbonForm, IItemRefreshSupport {
        public MainForm() : base() { InitializeComponent(); }
        public void InitializeComponent() {
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbiNew = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClose = new DevExpress.XtraBars.BarButtonItem();
            this.rpMain = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgActions = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonKeyTip = "";
            this.ribbon.ApplicationIcon = null;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiNew,
            this.bbiClose});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 2;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpMain});
            this.ribbon.SelectedPage = this.rpMain;
            this.ribbon.Size = new System.Drawing.Size(713, 143);
            // 
            // bbiNew
            // 
            this.bbiNew.Caption = "New";
            this.bbiNew.Id = 0;
            this.bbiNew.Name = "bbiNew";
            this.bbiNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiNew_ItemClick);
            // 
            // bbiClose
            // 
            this.bbiClose.Caption = "Close";
            this.bbiClose.Id = 1;
            this.bbiClose.Name = "bbiClose";
            this.bbiClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiClose_ItemClick);
            // 
            // rpMain
            // 
            this.rpMain.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgActions});
            this.rpMain.KeyTip = "";
            this.rpMain.Name = "rpMain";
            this.rpMain.Text = "Main";
            // 
            // rpgActions
            // 
            this.rpgActions.ItemLinks.Add(this.bbiNew);
            this.rpgActions.ItemLinks.Add(this.bbiClose);
            this.rpgActions.KeyTip = "";
            this.rpgActions.Name = "rpgActions";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(713, 558);
            this.Controls.Add(this.ribbon);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Ribbon = this.ribbon;
            this.Text = "DX Sample";
            this.ResumeLayout(false);

        }

        private DevExpress.XtraBars.BarButtonItem bbiNew;
        private RibbonPage rpMain;
        private RibbonPageGroup rpgActions;
        private DevExpress.XtraBars.BarButtonItem bbiClose;
        private RibbonControl ribbon;



        #region IItemRefreshSupport Members

        public RibbonPageCategory GetCategory(string text) { return ribbon.MergedCategories[text]; }
        public RibbonPage GetPage(string text) { return ribbon.MergedPages[text]; }
        public bool IsMerged { get { return false; } }

        public RibbonPageGroup GetGroup(string text) {
            foreach (RibbonPage page in ribbon.MergedPages) foreach (RibbonPageGroup group in page.Groups) if (group.Text == text) return group;
            return null;
        }

        #endregion

        private void bbiNew_ItemClick(object sender, ItemClickEventArgs e) {
            Module module = new Module();
            module.MdiParent = this;
            module.Show();
        }

        private void bbiClose_ItemClick(object sender, ItemClickEventArgs e) { Close(); }
    }
}