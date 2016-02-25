Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.Win32

Public Class Form2
    Dim close_main = 0
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim path = CurDir() & "\databases\"
        If Not Directory.Exists(path) Then
            Directory.CreateDirectory(path)
        End If

        Me.TopMost = True
        Me.ShowInTaskbar = False
        security_check()
        Timer1.Enabled = True
    End Sub
    Function security_check()
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
        'check settings
        Dim path2 = CurDir() & "\settings.dat"
        Dim md5 = GenerateFileMD5(path2)

        If regKey1 Is Nothing Then
        Else
            regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\DB", True).GetValue("settings")
            If regKey Is Nothing Then
            Else
                If md5 = regKey Then
                Else
                    close_main = 1
                End If
            End If
        End If
      

        'check DB files    
        If regKey1 Is Nothing Then

        Else
            For Each FileName As String In files
                name = FileName.Replace(path, "")
                name = name.Replace(".deptbase", "")

                regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\DB", True).GetValue(name)
                If regKey Is Nothing Then
                    My.Computer.FileSystem.RenameFile(CurDir() & "\databases\" & name & ".deptbase", name & ".corbase")
                    warn = 1
                End If
            Next

            If warn = 1 Then
                MsgBox("Some Databases are corrupted")
            End If
        End If
    End Function
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Opacity = Me.Opacity - 0.05
        If Me.Opacity < 0.1 Then
            Dim f3 As New Form3
            If close_main = 1 Then
                f3.TextBox2.Text = 1
            End If
            f3.Show()
            Me.Close()
        End If
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

End Class