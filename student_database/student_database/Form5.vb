Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.Win32

Public Class Form5
#Region " ClientAreaMove Handling "
    Private mouseOffset As Point
    Private isMouseDown As Boolean = False
    Private Sub Form1_MouseDown(ByVal sender As Object, _
    ByVal e As MouseEventArgs) Handles MyBase.MouseDown
        Dim xOffset As Integer
        Dim yOffset As Integer
        If e.Button = MouseButtons.Left Then
            xOffset = -e.X - SystemInformation.FrameBorderSize.Width
            yOffset = -e.Y - SystemInformation.CaptionHeight - _
            SystemInformation.FrameBorderSize.Height
            mouseOffset = New Point(xOffset, yOffset)
            isMouseDown = True
        End If
    End Sub
    Private Sub Form1_MouseMove(ByVal sender As Object, _
    ByVal e As MouseEventArgs) Handles MyBase.MouseMove
        If isMouseDown Then
            Dim mousePos As Point = Control.MousePosition
            mousePos.Offset(mouseOffset.X, mouseOffset.Y)
            Location = mousePos
        End If
    End Sub
    Private Sub Form1_MouseUp(ByVal sender As Object, _
    ByVal e As MouseEventArgs) Handles MyBase.MouseUp
        ' Changes the isMouseDown field so that the form does
        ' not move unless the user is pressing the left mouse button.
        If e.Button = MouseButtons.Left Then
            isMouseDown = False
        End If
    End Sub
#End Region
    Private Sub Form5_Paint(ByVal sender As Object, _
  ByVal e As PaintEventArgs) Handles MyBase.Paint

        Dim borderRectangle As Rectangle = Me.ClientRectangle
        borderRectangle.Inflate(0, 0)
        ControlPaint.DrawBorder3D(e.Graphics, borderRectangle, _
            Border3DStyle.Raised)
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox2.Text.Contains("@override:") = True Then
            MsgBox("Override code cannot be set as password, enter another password")
            Exit Sub
        End If

        Dim a
        Dim readupdate() As String = System.IO.File.ReadAllLines("settings.dat")
        For Each line As String In readupdate
            a = Decrypt(line, "may032013")
            Exit For
        Next
        If a = TextBox1.Text Then
            Dim objWriter As New System.IO.StreamWriter("settings.dat", True)
            objWriter.WriteLine("")
            objWriter.Close()
            Using sw As StreamWriter = New StreamWriter("settings.dat")
                sw.Write(encrypt(TextBox2.Text, "may032013"))
            End Using
            Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\DB", True)
            key.SetValue("settings", GenerateFileMD5("settings.dat"))
            MsgBox("Password changed")
            Me.Close()
        End If
    End Sub

    Function encrypt(ByVal Str, ByVal key)
        Dim lenKey, KeyPos, LenStr, x, Newstr, EncCharNum
        Newstr = ""
        lenKey = Len(key)
        KeyPos = 1
        LenStr = Len(Str)
        Str = StrReverse(Str)
        For x = 1 To LenStr
            EncCharNum = Asc(Mid(Str, x, 1)) + Asc(Mid(key, KeyPos, 1))
            Newstr = Newstr & Chr(EncCharNum Mod 256)
            KeyPos = KeyPos + 1
            If KeyPos > lenKey Then KeyPos = 1
        Next
        encrypt = Newstr
    End Function
    Function Decrypt(ByVal str, ByVal key)
        Dim lenKey, KeyPos, LenStr, x, Newstr, DecCharNum
        Newstr = ""
        lenKey = Len(key)
        KeyPos = 1
        LenStr = Len(str)
        str = StrReverse(str)
        For x = LenStr To 1 Step -1
            DecCharNum = Asc(Mid(str, x, 1)) - Asc(Mid(key, KeyPos, 1)) + 256
            Newstr = Newstr & Chr(DecCharNum Mod 256)
            KeyPos = KeyPos + 1
            If KeyPos > lenKey Then KeyPos = 1
        Next
        Newstr = StrReverse(Newstr)
        Decrypt = Newstr
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Function GenerateFileMD5(ByVal filePath As String)
        On Error Resume Next
        Dim md5 As MD5CryptoServiceProvider = New MD5CryptoServiceProvider
        Dim f As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Delete, 8192)
        f = New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Delete, 8192)
        md5.ComputeHash(f)
        f.Dispose()
        f.Close()
        Dim hash As Byte() = md5.Hash
        Dim buff As StringBuilder = New StringBuilder
        Dim hashByte As Byte
        For Each hashByte In hash
            buff.Append(String.Format("{0:X2}", hashByte))
        Next
        Dim md5string As String
        md5string = buff.ToString()
        Return md5string
    End Function

    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class