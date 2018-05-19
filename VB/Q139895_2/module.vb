Imports Microsoft.VisualBasic
Imports DevExpress.XtraEditors
Imports DevExpress.XtraBars.Ribbon
Imports System.Windows.Forms
Imports DevExpress.XtraBars

Namespace DXSample
    Public Class Editor
        Inherits XtraForm
        Implements IItemRefreshSupport
        Public Sub New()
            MyBase.New()
            InitializeComponent()
        End Sub
        Private Sub InitializeComponent()
            Me.ribbon = New DevExpress.XtraBars.Ribbon.RibbonControl()
            Me.bbiSave = New DevExpress.XtraBars.BarButtonItem()
            Me.bbiClose = New DevExpress.XtraBars.BarButtonItem()
            Me.rpEdit = New DevExpress.XtraBars.Ribbon.RibbonPage()
            Me.rpgEdit = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
            Me.memo = New DevExpress.XtraEditors.MemoEdit()
            CType(Me.memo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' ribbon
            ' 
            Me.ribbon.ApplicationButtonKeyTip = ""
            Me.ribbon.ApplicationIcon = Nothing
            Me.ribbon.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bbiSave, Me.bbiClose})
            Me.ribbon.Location = New System.Drawing.Point(0, 0)
            Me.ribbon.MaxItemId = 2
            Me.ribbon.Name = "ribbon"
            Me.ribbon.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.rpEdit})
            Me.ribbon.SelectedPage = Me.rpEdit
            Me.ribbon.Size = New System.Drawing.Size(284, 141)
            ' 
            ' bbiSave
            ' 
            Me.bbiSave.Caption = "Save"
            Me.bbiSave.Id = 0
            Me.bbiSave.Name = "bbiSave"
            Me.bbiSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            '			Me.bbiSave.ItemClick += New DevExpress.XtraBars.ItemClickEventHandler(Me.bbiSave_ItemClick);
            ' 
            ' bbiClose
            ' 
            Me.bbiClose.Caption = "Close"
            Me.bbiClose.Id = 1
            Me.bbiClose.Name = "bbiClose"
            '			Me.bbiClose.ItemClick += New DevExpress.XtraBars.ItemClickEventHandler(Me.bbiClose_ItemClick);
            ' 
            ' rpEdit
            ' 
            Me.rpEdit.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.rpgEdit})
            Me.rpEdit.KeyTip = ""
            Me.rpEdit.Name = "rpEdit"
            Me.rpEdit.Text = "Edit"
            ' 
            ' rpgEdit
            ' 
            Me.rpgEdit.AllowTextClipping = False
            Me.rpgEdit.ItemLinks.Add(Me.bbiSave)
            Me.rpgEdit.ItemLinks.Add(Me.bbiClose)
            Me.rpgEdit.KeyTip = ""
            Me.rpgEdit.Name = "rpgEdit"
            Me.rpgEdit.Text = "Opened"
            ' 
            ' memo
            ' 
            Me.memo.Dock = System.Windows.Forms.DockStyle.Fill
            Me.memo.Location = New System.Drawing.Point(0, 141)
            Me.memo.Name = "memo"
            Me.memo.Size = New System.Drawing.Size(284, 123)
            Me.memo.TabIndex = 1
            '			Me.memo.TextChanged += New System.EventHandler(Me.memo_TextChanged);
            ' 
            ' Module
            ' 
            Me.ClientSize = New System.Drawing.Size(284, 264)
            Me.Controls.Add(Me.memo)
            Me.Controls.Add(Me.ribbon)
            Me.Name = "Module"
            Me.Text = "untitled"
            '			Me.Load += New System.EventHandler(Me.Module_Load);
            CType(Me.memo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Private ribbon As RibbonControl
        Private rpEdit As RibbonPage
        Private rpgEdit As RibbonPageGroup
        Private WithEvents bbiSave As DevExpress.XtraBars.BarButtonItem
        Private WithEvents bbiClose As DevExpress.XtraBars.BarButtonItem
        Private WithEvents memo As MemoEdit

#Region "IItemRefreshSupport Members"

        Public Function GetCategory(ByVal text As String) As RibbonPageCategory Implements IItemRefreshSupport.GetCategory
            For Each category As RibbonPageCategory In ribbon.PageCategories
                If category.Name = text Then
                    Return category
                End If
            Next category
            Return Nothing
        End Function

        Public Function GetPage(ByVal text As String) As RibbonPage Implements IItemRefreshSupport.GetPage
            For Each page As RibbonPage In ribbon.Pages
                If page.Name = text Then
                    Return page
                End If
            Next page
            Return Nothing
        End Function

        Public Function GetGroup(ByVal text As String) As RibbonPageGroup Implements IItemRefreshSupport.GetGroup
            For Each page As RibbonPage In ribbon.Pages
                For Each group As RibbonPageGroup In page.Groups
                    If group.Name = text Then
                        Return group
                    End If
                Next group
            Next page
            Return Nothing
        End Function

        Public ReadOnly Property IsMerged() As Boolean Implements IItemRefreshSupport.IsMerged
            Get
                Select Case ribbon.MdiMergeStyle
                    Case RibbonMdiMergeStyle.Always
                        Return True
                    Case RibbonMdiMergeStyle.Default, RibbonMdiMergeStyle.OnlyWhenMaximized
                        If WindowState = FormWindowState.Maximized Then
                            Return True
                        Else
                            Return False
                        End If
                    Case Else
                        Return False
                End Select
            End Get
        End Property

#End Region

        Private helper As ItemRefreshHelper

        Private Sub bbiSave_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiSave.ItemClick
            bbiSave.Visibility = BarItemVisibility.Never
            helper.SetItemText(rpEdit.Name, "Edit", ItemType.Page)
            helper.SetItemText(rpgEdit.Name, "Saved", ItemType.Group)
        End Sub

        Private Sub Module_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            helper = New ItemRefreshHelper(CType(MdiParent, IItemRefreshSupport), Me)
            helper.AddItem(rpEdit.Name, rpEdit.Text)
            helper.AddItem(rpgEdit.Name, rpgEdit.Text)
        End Sub

        Private Sub bbiClose_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs) Handles bbiClose.ItemClick
            Close()
        End Sub

        Private Sub memo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles memo.TextChanged
            bbiSave.Visibility = BarItemVisibility.Always
            helper.SetItemText(rpEdit.Name, "Edit *", ItemType.Page)
            helper.SetItemText(rpgEdit.Name, String.Format("Lines: {0}", memo.Lines.Length), ItemType.Group)
        End Sub
    End Class
End Namespace

