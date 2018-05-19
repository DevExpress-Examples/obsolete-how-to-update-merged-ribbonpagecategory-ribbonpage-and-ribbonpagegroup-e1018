Imports Microsoft.VisualBasic
Imports DevExpress.XtraBars.Ribbon
Imports System.Windows.Forms
Imports System
Imports System.Collections.Generic
Imports System.Collections
Imports System.ComponentModel

Namespace DXSample
	Public Interface IItemRefreshSupport
		Function GetCategory(ByVal text As String) As RibbonPageCategory
		Function GetPage(ByVal text As String) As RibbonPage
		Function GetGroup(ByVal text As String) As RibbonPageGroup
		ReadOnly Property IsMerged() As Boolean
	End Interface

	Public Enum ItemType
		Category
		Page
		Group
	End Enum
	Public Enum ItemRefreshHelperState
		Merged
		Unmerged
	End Enum

	Public Class ItemRefreshHelper
		Private master As IItemRefreshSupport
		Private owner As IItemRefreshSupport
		Private items As Dictionary(Of String, String)

		Public Sub New(ByVal master As IItemRefreshSupport, ByVal owner As IItemRefreshSupport)
			Me.master = master
			Me.owner = owner
			items = New Dictionary(Of String, String)()
		End Sub

		Public Sub AddItem(ByVal name As String, ByVal text As String)
			items.Add(name, text)
		End Sub

		Public Sub AddItems(ByVal collection As CollectionBase)
			If collection.Count = 0 Then
				Return
			End If
			Dim categories As RibbonPageCategoryCollection = TryCast(collection, RibbonPageCategoryCollection)
			Dim pages As RibbonPageCollection = TryCast(collection, RibbonPageCollection)
			Dim groups As RibbonPageGroupCollection = TryCast(collection, RibbonPageGroupCollection)
			If categories IsNot Nothing Then
				For Each category As RibbonPageCategory In categories
					items.Add(category.Name, category.Text)
				Next category
			End If
			If pages IsNot Nothing Then
				For Each page As RibbonPage In pages
					items.Add(page.Name, page.Text)
				Next page
			End If
			If groups IsNot Nothing Then
				For Each group As RibbonPageGroup In groups
					items.Add(group.Name, group.Text)
				Next group
			End If
		End Sub

		Public Sub SetItemText(ByVal itemName As String, ByVal itemText As String, ByVal itemType As ItemType)
			Dim item As Component = Nothing
			If owner.IsMerged Then
				Dim oldText As String = items(itemName)
				If String.IsNullOrEmpty(oldText) Then
					Return
				Else
					items(itemName) = itemText
				End If
				Select Case itemType
					Case ItemType.Category
						item = master.GetCategory(oldText)
					Case ItemType.Group
						item = master.GetGroup(oldText)
					Case ItemType.Page
						item = master.GetPage(oldText)
				End Select
			Else
				If (Not String.IsNullOrEmpty(items(itemName))) Then
					items(itemName) = itemText
				End If
				Select Case itemType
					Case ItemType.Category
						item = owner.GetCategory(itemName)
					Case ItemType.Group
						item = owner.GetGroup(itemName)
					Case ItemType.Page
						item = owner.GetPage(itemName)
				End Select
			End If
			SetItemTextInternal(item, itemText)
		End Sub

		Private Sub SetItemTextInternal(ByVal item As Component, ByVal text As String)
			Dim category As RibbonPageCategory = TryCast(item, RibbonPageCategory)
			Dim group As RibbonPageGroup = TryCast(item, RibbonPageGroup)
			Dim page As RibbonPage = TryCast(item, RibbonPage)
			If category IsNot Nothing Then
				category.Text = text
			End If
			If group IsNot Nothing Then
				group.Text = text
			End If
			If page IsNot Nothing Then
				page.Text = text
			End If
		End Sub
	End Class
End Namespace