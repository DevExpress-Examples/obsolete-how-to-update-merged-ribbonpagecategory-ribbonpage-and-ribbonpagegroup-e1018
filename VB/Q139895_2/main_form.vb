Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraBars

Namespace DXSample
	Public Class MainForm
		Inherits RibbonForm
		Implements IItemRefreshSupport
		Public Sub New()
			MyBase.New()
		 InitializeComponent()
		End Sub
		Public Sub InitializeComponent()
			Me.ribbon = New DevExpress.XtraBars.Ribbon.RibbonControl()
			Me.bbiNew = New DevExpress.XtraBars.BarButtonItem()
			Me.bbiClose = New DevExpress.XtraBars.BarButtonItem()
			Me.rpMain = New DevExpress.XtraBars.Ribbon.RibbonPage()
			Me.rpgActions = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
			Me.SuspendLayout()
			' 
			' ribbon
			' 
			Me.ribbon.ApplicationButtonKeyTip = ""
			Me.ribbon.ApplicationIcon = Nothing
			Me.ribbon.Items.AddRange(New DevExpress.XtraBars.BarItem() { Me.bbiNew, Me.bbiClose})
			Me.ribbon.Location = New System.Drawing.Point(0, 0)
			Me.ribbon.MaxItemId = 2
			Me.ribbon.Name = "ribbon"
			Me.ribbon.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() { Me.rpMain})
			Me.ribbon.SelectedPage = Me.rpMain
			Me.ribbon.Size = New System.Drawing.Size(713, 143)
			' 
			' bbiNew
			' 
			Me.bbiNew.Caption = "New"
			Me.bbiNew.Id = 0
			Me.bbiNew.Name = "bbiNew"
'			Me.bbiNew.ItemClick += New DevExpress.XtraBars.ItemClickEventHandler(Me.bbiNew_ItemClick);
			' 
			' bbiClose
			' 
			Me.bbiClose.Caption = "Close"
			Me.bbiClose.Id = 1
			Me.bbiClose.Name = "bbiClose"
'			Me.bbiClose.ItemClick += New DevExpress.XtraBars.ItemClickEventHandler(Me.bbiClose_ItemClick);
			' 
			' rpMain
			' 
			Me.rpMain.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() { Me.rpgActions})
			Me.rpMain.KeyTip = ""
			Me.rpMain.Name = "rpMain"
			Me.rpMain.Text = "Main"
			' 
			' rpgActions
			' 
			Me.rpgActions.ItemLinks.Add(Me.bbiNew)
			Me.rpgActions.ItemLinks.Add(Me.bbiClose)
			Me.rpgActions.KeyTip = ""
			Me.rpgActions.Name = "rpgActions"
			' 
			' MainForm
			' 
			Me.ClientSize = New System.Drawing.Size(713, 558)
			Me.Controls.Add(Me.ribbon)
			Me.IsMdiContainer = True
			Me.Name = "MainForm"
			Me.Ribbon = Me.ribbon
			Me.Text = "DX Sample"
			Me.ResumeLayout(False)

		End Sub

		Private WithEvents bbiNew As DevExpress.XtraBars.BarButtonItem
		Private rpMain As RibbonPage
		Private rpgActions As RibbonPageGroup
		Private WithEvents bbiClose As DevExpress.XtraBars.BarButtonItem



		#Region "IItemRefreshSupport Members"

		Public Function GetCategory(ByVal text As String) As RibbonPageCategory Implements IItemRefreshSupport.GetCategory
			Return ribbon.MergedCategories(text)
		End Function
		Public Function GetPage(ByVal text As String) As RibbonPage Implements IItemRefreshSupport.GetPage
			Return ribbon.MergedPages(text)
		End Function
		Public ReadOnly Property IsMerged() As Boolean Implements IItemRefreshSupport.IsMerged
			Get
				Return False
			End Get
		End Property

		Public Function GetGroup(ByVal text As String) As RibbonPageGroup Implements IItemRefreshSupport.GetGroup
			For Each page As RibbonPage In ribbon.MergedPages
				For Each group As RibbonPageGroup In page.Groups
					If group.Text = text Then
						Return group
					End If
				Next group
			Next page
			Return Nothing
		End Function

		#End Region

		Private Sub bbiNew_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs) Handles bbiNew.ItemClick
            Dim _editor As Editor = New Editor()
            _editor.MdiParent = Me
            _editor.Show()
		End Sub

		Private Sub bbiClose_ItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs) Handles bbiClose.ItemClick
			Close()
		End Sub
	End Class
End Namespace