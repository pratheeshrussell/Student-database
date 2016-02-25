Imports System.IO
Imports System.Management
Imports Microsoft.Win32

Public Class Form3
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

    Dim password
    Dim first_launch = 0
    Private Sub Form1_Paint(ByVal sender As Object, _
   ByVal e As PaintEventArgs) Handles MyBase.Paint

        Dim borderRectangle As Rectangle = Me.ClientRectangle
        borderRectangle.Inflate(0, 0)
        ControlPaint.DrawBorder3D(e.Graphics, borderRectangle, _
            Border3DStyle.Raised)
    End Sub
    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
        Dim p = 0
        If System.IO.File.Exists("settings.dat") Then
            Dim info As New FileInfo("settings.dat")
            Dim length As Long = info.Length
            If length < 1 Then
                Form1.Close()
            End If
            Dim a = Nothing
            Dim readupdate() As String = System.IO.File.ReadAllLines("settings.dat")
            For Each line As String In readupdate
                a = line
                Exit For
            Next
            If Decrypt(a, "may032013") = "@password_not_set" Then
                p = set_registry()
                If p = 1 Then
                    TextBox2.Text = "1"
                Else
                    Dim f4 As New Form4
                    f4.Show()
                    Me.Close()
                End If      
            Else
                password = Decrypt(a, "may032013")
                check_comp()
            End If
        Else
            Form1.Close()
        End If

        ' Me.ShowInTaskbar = False

    End Sub
    Function allow()
        If first_launch = 1 Then
            Form1.Close()
        End If
        If TextBox2.Text = "1" Then
            Form1.Close()
        End If
        Form1.filelist()
        Form1.Opacity = 1
        Me.Close()
    End Function
    Function check_comp()
        Dim regKey1 As Microsoft.Win32.RegistryKey
        regKey1 = Registry.LocalMachine.OpenSubKey("SOFTWARE\DB", True)
        'check DB files    
        If regKey1 Is Nothing Then
            first_launch = 1
        End If
    End Function
    Function set_registry()
        Dim warn = 0
        Dim regKey1 As Microsoft.Win32.RegistryKey
        Dim x
        regKey1 = Registry.LocalMachine.OpenSubKey("SOFTWARE\DB", True)
        Dim newkey As RegistryKey
        Dim regKey As Object

        Dim files() As String
        Dim path = CurDir() & "\databases\"
        Dim name
        files = Directory.GetFiles(path, "*.deptbase", SearchOption.TopDirectoryOnly)


        'check DB files    
        If regKey1 Is Nothing Then


            Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("Software", True)
            newkey = key.CreateSubKey("DB")
            For Each FileName As String In files
                name = FileName.Replace(path, "")
                name = name.Replace(".deptbase", "")
                newkey.SetValue(name, "1")
            Next
        Else

            set_registry = 1

        End If
    End Function
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If Form1.encrypt(TextBox1.Text.ToLower, "july182013") = "›¥œ©k–™£¥ÏëÛ¹" Then '<- override code1
            code_001()
            TextBox1.Text = ""
        ElseIf Form1.encrypt(TextBox1.Text.ToLower, "july182013") = "œ¥œ©k–™£¥ÏëÛ¹" Then '<- override code2
            code_002()
            allow()
        ElseIf password = "" Then
        ElseIf password = TextBox1.Text Then
            allow()
        End If
    End Sub
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
    'override codes
    Function code_001()
        On Error Resume Next
        first_launch = 0
        Dim files() As String
        Dim path = CurDir() & "\databases\"
        files = Directory.GetFiles(path, "*.corbase", SearchOption.TopDirectoryOnly)
        Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\DB", True)
        For Each FileName As String In files
            Name = FileName.Replace(path, "")
            Name = Name.Replace(".corbase", "")
            My.Computer.FileSystem.RenameFile(CurDir() & "\databases\" & Name & ".corbase", Name & ".deptbase")
            key.SetValue(Name, "1")
        Next
        MsgBox("override code accepted Databases Restored")
    End Function

    Function code_002()
        On Error Resume Next
        Dim objWriter As New System.IO.StreamWriter("settings.dat", True)
        objWriter.WriteLine("")
        objWriter.Close()
        Using sw As StreamWriter = New StreamWriter("settings.dat")
            sw.Write(Form1.encrypt("@password_not_set", "may032013"))
        End Using

        Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("Software", True)
        key.DeleteSubKey("DB")
        MsgBox("override code accepted Kindly restart the program")
        first_launch = 1
    End Function

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        MsgBox("I won't display the databases")
        Form1.Close()
    End Sub
End Class