using DevExpress.XtraEditors;
using DevExpress.XtraBars.Ribbon;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace DXSample {
    public class Module : XtraForm, IItemRefreshSupport {
        public Module() : base() { InitializeComponent(); }
        private void InitializeComponent() {
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClose = new DevExpress.XtraBars.BarButtonItem();
            this.rpEdit = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.rpgEdit = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.memo = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.memo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonKeyTip = "";
            this.ribbon.ApplicationIcon = null;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiSave,
            this.bbiClose});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 2;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpEdit});
            this.ribbon.SelectedPage = this.rpEdit;
            this.ribbon.Size = new System.Drawing.Size(284, 141);
            // 
            // bbiSave
            // 
            this.bbiSave.Caption = "Save";
            this.bbiSave.Id = 0;
            this.bbiSave.Name = "bbiSave";
            this.bbiSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.bbiSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSave_ItemClick);
            // 
            // bbiClose
            // 
            this.bbiClose.Caption = "Close";
            this.bbiClose.Id = 1;
            this.bbiClose.Name = "bbiClose";
            this.bbiClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiClose_ItemClick);
            // 
            // rpEdit
            // 
            this.rpEdit.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.rpgEdit});
            this.rpEdit.KeyTip = "";
            this.rpEdit.Name = "rpEdit";
            this.rpEdit.Text = "Edit";
            // 
            // rpgEdit
            // 
            this.rpgEdit.AllowTextClipping = false;
            this.rpgEdit.ItemLinks.Add(this.bbiSave);
            this.rpgEdit.ItemLinks.Add(this.bbiClose);
            this.rpgEdit.KeyTip = "";
            this.rpgEdit.Name = "rpgEdit";
            this.rpgEdit.Text = "Opened";
            // 
            // memo
            // 
            this.memo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memo.Location = new System.Drawing.Point(0, 141);
            this.memo.Name = "memo";
            this.memo.Size = new System.Drawing.Size(284, 123);
            this.memo.TabIndex = 1;
            this.memo.TextChanged += new System.EventHandler(this.memo_TextChanged);
            // 
            // Module
            // 
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.memo);
            this.Controls.Add(this.ribbon);
            this.Name = "Module";
            this.Text = "untitled";
            this.Load += new System.EventHandler(this.Module_Load);
            ((System.ComponentModel.ISupportInitialize)(this.memo.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        private RibbonControl ribbon;
        private RibbonPage rpEdit;
        private RibbonPageGroup rpgEdit;
        private DevExpress.XtraBars.BarButtonItem bbiSave;
        private DevExpress.XtraBars.BarButtonItem bbiClose;
        private MemoEdit memo;

        #region IItemRefreshSupport Members

        public RibbonPageCategory GetCategory(string text) { 
            foreach (RibbonPageCategory category in ribbon.PageCategories) if (category.Name == text) return category;
            return null;
        }

        public RibbonPage GetPage(string text) { 
            foreach (RibbonPage page in ribbon.Pages) if (page.Name == text) return page;
            return null;
        }

        public RibbonPageGroup GetGroup(string text) {
            foreach (RibbonPage page in ribbon.Pages) foreach (RibbonPageGroup group in page.Groups) if (group.Name == text) return group;
            return null;
        }

        public bool IsMerged {
            get {
                switch (ribbon.MdiMergeStyle) {
                    case RibbonMdiMergeStyle.Always: return true;
                    case RibbonMdiMergeStyle.Default:
                    case RibbonMdiMergeStyle.OnlyWhenMaximized: return WindowState == FormWindowState.Maximized ? true : false;
                    default: return false;
                }
            }
        }

        #endregion

        private ItemRefreshHelper helper;

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            bbiSave.Visibility = BarItemVisibility.Never;
            helper.SetItemText(rpEdit.Name, "Edit", ItemType.Page);
            helper.SetItemText(rpgEdit.Name, "Saved", ItemType.Group);
        }

        private void Module_Load(object sender, System.EventArgs e) {
            helper = new ItemRefreshHelper((IItemRefreshSupport)MdiParent, this);
            helper.AddItem(rpEdit.Name, rpEdit.Text);
            helper.AddItem(rpgEdit.Name, rpgEdit.Text);
        }

        private void bbiClose_ItemClick(object sender, ItemClickEventArgs e) { Close(); }

        private void memo_TextChanged(object sender, System.EventArgs e) {
            bbiSave.Visibility = BarItemVisibility.Always;
            helper.SetItemText(rpEdit.Name, "Edit *", ItemType.Page);
            helper.SetItemText(rpgEdit.Name, string.Format("Lines: {0}", memo.Lines.Length), ItemType.Group);
        }
    }
}

