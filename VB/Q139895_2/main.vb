Imports Microsoft.VisualBasic
Imports System.Windows.Forms

Namespace DXSample
	Public Class Starter
		Shared Sub Main()
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(True)
			Application.Run(New MainForm())
		End Sub
	End Class
End Namespace