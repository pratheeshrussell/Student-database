Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.Win32
Imports System.Text.RegularExpressions

Public Class Form1
#Region " ClientAreaMove Handling "
    Const WM_NCHITTEST As Integer = &H84
    Const HTCLIENT As Integer = &H1
    Const HTCAPTION As Integer = &H2
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Select Case m.Msg
            Case WM_NCHITTEST
                MyBase.WndProc(m)
                If m.Result = HTCLIENT Then m.Result = HTCAPTION
            Case Else
                MyBase.WndProc(m)
        End Select
    End Sub
#End Region
    Dim searched = 0
    Dim pass = "my_key" '<----change
    Private Sub PictureBox1_MouseHover(ByVal sender As System.Object, _
ByVal e As System.EventArgs) Handles PictureBox1.MouseHover
        PictureBox1.Image = My.Resources.ResourceManager.GetObject("close_red mousehover")
    End Sub
    Private Sub PictureBox1_leave(ByVal sender As System.Object, _
ByVal e As System.EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.Image = My.Resources.ResourceManager.GetObject("close_red normal")
    End Sub
    Private Sub PictureBox1_down(ByVal sender As System.Object, _
ByVal e As System.EventArgs) Handles PictureBox1.MouseDown
        PictureBox1.Image = My.Resources.ResourceManager.GetObject("close_red mousedown")
    End Sub
    Private Sub PictureBox1_up(ByVal sender As System.Object, _
ByVal e As System.EventArgs) Handles PictureBox1.MouseUp
        PictureBox1.Image = My.Resources.ResourceManager.GetObject("close_red mousehover")
    End Sub

    Private Sub PictureBox2_MouseHover(ByVal sender As System.Object, _
ByVal e As System.EventArgs) Handles PictureBox2.MouseHover
        PictureBox2.Image = My.Resources.ResourceManager.GetObject("min_hover")
    End Sub
    Private Sub PictureBox2_leave(ByVal sender As System.Object, _
ByVal e As System.EventArgs) Handles PictureBox2.MouseLeave
        PictureBox2.Image = My.Resources.ResourceManager.GetObject("min_normal")
    End Sub
    Private Sub PictureBox2_down(ByVal sender As System.Object, _
ByVal e As System.EventArgs) Handles PictureBox2.MouseDown
        PictureBox2.Image = My.Resources.ResourceManager.GetObject("min_mousedown")
    End Sub
    Private Sub PictureBox2_up(ByVal sender As System.Object, _
ByVal e As System.EventArgs) Handles PictureBox2.MouseUp
        PictureBox2.Image = My.Resources.ResourceManager.GetObject("min_hover")
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Form1_Paint(ByVal sender As Object, _
    ByVal e As PaintEventArgs) Handles MyBase.Paint

        Dim borderRectangle As Rectangle = Me.ClientRectangle
        borderRectangle.Inflate(0, 0)
        ControlPaint.DrawBorder3D(e.Graphics, borderRectangle, _
            Border3DStyle.Raised)
    End Sub
    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        If ComboBox4.Text.ToLower = "yes" Then
            TextBox20.Enabled = True
        Else
            TextBox20.Text = ""
            TextBox20.Enabled = False
        End If
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged
        If ComboBox5.Text.ToLower = "yes" Then
            TextBox37.Enabled = True
        Else
            TextBox37.Text = ""
            TextBox37.Enabled = False
        End If
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Dim f2 As New Form2
        f2.Show()
        Me.Opacity = 0.01

        Dim path = CurDir() & "\databases\"
        Dim name

        If Not Directory.Exists(path) Then
            Directory.CreateDirectory(path)
        End If

        ComboBox2.Items.Add("yes")
        ComboBox2.Items.Add("No")
        ComboBox2.Text = "No"
        ComboBox3.Items.Add("yes")
        ComboBox3.Items.Add("No")
        ComboBox3.Text = "No"
        ComboBox4.Items.Add("yes")
        ComboBox4.Items.Add("No")
        ComboBox4.Text = "No"

        ComboBox7.Items.Add("yes")
        ComboBox7.Items.Add("No")
        ComboBox6.Items.Add("yes")
        ComboBox6.Items.Add("No")
        ComboBox5.Items.Add("yes")
        ComboBox5.Items.Add("No")

        ComboBox8.Items.Add("Name")
        ComboBox8.Items.Add("Register No")
        ComboBox8.Items.Add("placed")
        ComboBox8.Items.Add("CGPA")
        ComboBox8.Items.Add("All")
        ComboBox8.Text = "All"
        ComboBox9.Hide()

        ListView1.Columns.Add("Error Details", 300)

        DataGridView1.ColumnCount = 25
        DataGridView2.ColumnCount = 25
        Dim row As String() = New String() {"Register No", "Name", "Fathers Name", "Date Of Birth", "Address", "Phone No", "SSLC %", _
                                            "HSC %", "Gpa 1", "Gpa 2", "Gpa 3", "Gpa 4", "Gpa 5", "Gpa 6", "Gpa 7", "Gpa 8", _
                                            "Department", "CGPA", "Area Of Interest", "History Of Arrear", "Standing Arrear", _
                                            "No Of Arrear", "Placed Status", "Company Name", "Mail Id"}
        DataGridView1.Rows.Add(row)
        searched = 1



    End Sub
    Private Sub Form1_resized(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize

    End Sub
    Function filelist()
        Dim files() As String
        Dim path = CurDir() & "\databases\"
        Dim name
        files = Directory.GetFiles(Path, "*.deptbase", SearchOption.TopDirectoryOnly)
        For Each FileName As String In files
            Name = FileName.Replace(Path, "")
            Name = Name.Replace(".deptbase", "")
            ComboBox1.Items.Add(Name)
            ComboBox1.Text = Name
            ComboBox10.Items.Add(Name)
            ComboBox10.Text = Name
            ComboBox12.Items.Add(Name)
            ComboBox12.Text = Name
        Next
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'add new dept
        Dim newcourse = InputBox("Enter The Name Of The New Department", "Enter A Name")
        Dim fs As FileStream = Nothing
        If File.Exists(CurDir() & "\databases\" & newcourse & ".deptbase") Then
            MsgBox("Department Already Exists", MsgBoxStyle.Critical, "Error")
        Else
            If newcourse.Replace(" ", "") = "" Then
                Exit Sub
            End If
            fs = File.Create(CurDir() & "\databases\" & newcourse & ".deptbase")

            Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\DB", True)
            key.SetValue(newcourse, "1")

            MsgBox("Department Added", MsgBoxStyle.Information)
            ComboBox1.Items.Add(newcourse)
            ComboBox1.Text = newcourse
            ComboBox10.Items.Add(newcourse)
            ComboBox10.Text = newcourse
            ComboBox12.Items.Add(newcourse)
            ComboBox12.Text = newcourse
            fs.Close()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox8.Text = "" Then
            MsgBox("enter a reg no")
        Else
            save_database()
            newstud()
        End If

    End Sub
    Function newstud()
        TextBox8.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
        TextBox11.Text = ""
        TextBox12.Text = ""
        TextBox13.Text = ""
        TextBox14.Text = ""
        TextBox15.Text = ""
        TextBox16.Text = ""
        TextBox17.Text = "0"
        TextBox18.Text = ""
        TextBox19.Text = ""
        TextBox20.Text = ""
        TextBox43.Text = ""
    End Function
    Function save_database()
        Dim path = CurDir() & "\databases\" & ComboBox1.Text & ".deptbase"
        Dim readupdate() As String = System.IO.File.ReadAllLines(path)
        For Each line As String In readupdate
            If line = TextBox8.Text Then
                MsgBox("Already Enrolled")
                Exit Function
            End If
        Next

        Dim i As Integer
        Dim aryText(100) As String

        aryText(0) = encrypt(TextBox8.Text, pass) 'reg no
        aryText(1) = encrypt(TextBox1.Text, pass) 'name
        aryText(2) = encrypt(TextBox2.Text, pass) 'fathers name
        aryText(3) = encrypt(TextBox3.Text, pass) 'dob
        aryText(4) = encrypt(TextBox4.Text, pass) 'address
        aryText(5) = encrypt(TextBox5.Text, pass) 'phone no
        aryText(6) = encrypt(TextBox6.Text, pass) 'sslc
        aryText(7) = encrypt(TextBox7.Text, pass) 'hsc
        aryText(8) = encrypt(TextBox9.Text, pass) 'till 16 gpa each sem
        aryText(9) = encrypt(TextBox10.Text, pass)
        aryText(10) = encrypt(TextBox11.Text, pass)
        aryText(11) = encrypt(TextBox12.Text, pass)
        aryText(12) = encrypt(TextBox13.Text, pass)
        aryText(13) = encrypt(TextBox14.Text, pass)
        aryText(14) = encrypt(TextBox15.Text, pass)
        aryText(15) = encrypt(TextBox16.Text, pass) 'gpa 8th sem
        aryText(16) = encrypt(ComboBox1.Text, pass) 'dept
        aryText(17) = encrypt(TextBox17.Text, pass) 'cgpa till now
        aryText(18) = encrypt(TextBox18.Text, pass) 'interest
        aryText(19) = encrypt(ComboBox2.Text, pass) 'hist of arrear
        aryText(20) = encrypt(ComboBox3.Text, pass) 'standing arrear
        aryText(21) = encrypt(TextBox19.Text, pass) 'no of standing arrear
        aryText(22) = encrypt(ComboBox4.Text, pass) 'is placed
        aryText(23) = encrypt(TextBox20.Text, pass) 'company name
        aryText(24) = encrypt(TextBox43.Text, pass) 'mail id
        Dim objWriter As New System.IO.StreamWriter(path, True)

        For i = 0 To 24
            objWriter.WriteLine(aryText(i))
        Next

        objWriter.Close()
        MsgBox("Student Details Saved")

    End Function

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ComboBox11.Items.Add(TextBox29.Text)
        show_details()
    End Sub
    Function show_details()
        Dim filenames() As String = Directory.GetFiles(CurDir() & "\databases\", "*.deptbase")
        Dim found = 0
        For Each item As String In filenames
            If found = 1 Then
                Exit For
            End If
            Dim FILE_NAME As String = item
            Dim readupdate() As String = System.IO.File.ReadAllLines(FILE_NAME)
            Dim str(24) As String
            Dim x = 0
            For Each line As String In readupdate
                x = x + 1
                If x = 1 Then
                    str(0) = Decrypt(line, pass) 'regno
                ElseIf x = 2 Then
                    str(1) = Decrypt(line, pass) 'name
                ElseIf x = 3 Then
                    str(2) = Decrypt(line, pass) 'fathername
                ElseIf x = 4 Then
                    str(3) = Decrypt(line, pass) 'dob
                ElseIf x = 5 Then
                    str(4) = Decrypt(line, pass) 'address
                ElseIf x = 6 Then
                    str(5) = Decrypt(line, pass) 'ph no
                ElseIf x = 7 Then
                    str(6) = Decrypt(line, pass) 'sslc
                ElseIf x = 8 Then
                    str(7) = Decrypt(line, pass) 'hsc
                ElseIf x = 9 Then
                    str(8) = Decrypt(line, pass) 'till 16 gpa
                ElseIf x = 10 Then
                    str(9) = Decrypt(line, pass)
                ElseIf x = 11 Then
                    str(10) = Decrypt(line, pass)
                ElseIf x = 12 Then
                    str(11) = Decrypt(line, pass)
                ElseIf x = 13 Then
                    str(12) = Decrypt(line, pass)
                ElseIf x = 14 Then
                    str(13) = Decrypt(line, pass)
                ElseIf x = 15 Then
                    str(14) = Decrypt(line, pass)
                ElseIf x = 16 Then
                    str(15) = Decrypt(line, pass)
                ElseIf x = 17 Then
                    str(16) = Decrypt(line, pass) 'dept
                ElseIf x = 18 Then
                    str(17) = Decrypt(line, pass) 'cgpa
                ElseIf x = 19 Then
                    str(18) = Decrypt(line, pass) 'interest
                ElseIf x = 20 Then
                    str(19) = Decrypt(line, pass) 'hist of arrear
                ElseIf x = 21 Then
                    str(20) = Decrypt(line, pass) 'standing arrear
                ElseIf x = 22 Then
                    str(21) = Decrypt(line, pass) 'no of standing arrear
                ElseIf x = 23 Then
                    str(22) = Decrypt(line, pass) 'is placed
                ElseIf x = 24 Then
                    str(23) = Decrypt(line, pass) 'company name
                ElseIf x = 25 Then
                    str(24) = Decrypt(line, pass) 'mail id
                    If str(0) = TextBox29.Text Then
                        found = 1
                        TextBox36.Text = str(1) 'name
                        TextBox35.Text = str(2) 'father name
                        TextBox34.Text = str(3) 'dob
                        TextBox44.Text = str(24) 'mail id
                        TextBox33.Text = str(4) 'add
                        TextBox32.Text = str(5) 'ph no
                        TextBox31.Text = str(6) 'sslc
                        TextBox30.Text = str(7) 'hsc
                        TextBox39.Text = str(17) 'cgpa

                        TextBox28.Text = str(8) 'gpa per sem
                        TextBox27.Text = str(9)
                        TextBox26.Text = str(10)
                        TextBox25.Text = str(11)
                        TextBox24.Text = str(12)
                        TextBox22.Text = str(13)
                        TextBox23.Text = str(14)
                        TextBox21.Text = str(15)

                        TextBox41.Text = str(16) 'dept
                        TextBox38.Text = str(18) 'interest
                        ComboBox7.Text = str(19) 'history of arrear
                        ComboBox6.Text = str(20) 'standing arrear
                        TextBox40.Text = str(21) 'no of standing arrear
                        ComboBox5.Text = str(22) 'is placed
                        TextBox37.Text = str(23) ' company name
                        TextBox45.Text = FILE_NAME
                        Exit For
                    End If
                    x = 0
                End If
            Next
        Next

        If found = 0 Then
            MsgBox("Not Found")
        Else
            Button4.Enabled = True
            Button5.Enabled = True
            Button6.Enabled = True
            Button15.Enabled = True
            TextBox29.ReadOnly = True
            ComboBox11.Enabled = False
        End If
    End Function

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        update()
    End Sub
    Function update()
        Dim str(24) As String
        TextBox42.Text = ""
        ' TextBox45.Text
        Dim editmode = 0
        Dim x = 0
        Dim readupdate() As String = System.IO.File.ReadAllLines(TextBox45.Text)
        For Each line As String In readupdate
            line = Decrypt(line, pass)
            If line = TextBox29.Text Then
                editmode = 1
            End If
            If x > 24 Then
                editmode = 0
            End If
            If editmode = 0 Then
                TextBox42.Text = TextBox42.Text & encrypt(line, pass) & vbCrLf
            Else
                If x = 0 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox29.Text, pass) & vbCrLf 'no
                ElseIf x = 1 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox36.Text, pass) & vbCrLf 'name
                ElseIf x = 2 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox35.Text, pass) & vbCrLf 'father name
                ElseIf x = 3 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox34.Text, pass) & vbCrLf 'dob
                ElseIf x = 4 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox33.Text, pass) & vbCrLf 'address
                ElseIf x = 5 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox32.Text, pass) & vbCrLf 'ph no
                ElseIf x = 6 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox31.Text, pass) & vbCrLf 'sslc
                ElseIf x = 7 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox30.Text, pass) & vbCrLf 'hsc
                ElseIf x = 8 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox28.Text, pass) & vbCrLf 'gpa
                ElseIf x = 9 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox27.Text, pass) & vbCrLf
                ElseIf x = 10 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox26.Text, pass) & vbCrLf
                ElseIf x = 11 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox25.Text, pass) & vbCrLf
                ElseIf x = 12 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox24.Text, pass) & vbCrLf
                ElseIf x = 13 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox22.Text, pass) & vbCrLf
                ElseIf x = 14 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox23.Text, pass) & vbCrLf
                ElseIf x = 15 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox21.Text, pass) & vbCrLf
                ElseIf x = 16 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox41.Text, pass) & vbCrLf 'dept
                ElseIf x = 17 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox39.Text, pass) & vbCrLf 'cgpa
                ElseIf x = 18 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox38.Text, pass) & vbCrLf 'interest
                ElseIf x = 19 Then
                    TextBox42.Text = TextBox42.Text & encrypt(ComboBox7.Text, pass) & vbCrLf 'hist of arrear
                ElseIf x = 20 Then
                    TextBox42.Text = TextBox42.Text & encrypt(ComboBox6.Text, pass) & vbCrLf 'standing arrear
                ElseIf x = 21 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox40.Text, pass) & vbCrLf 'no of arrear
                ElseIf x = 22 Then
                    TextBox42.Text = TextBox42.Text & encrypt(ComboBox5.Text, pass) & vbCrLf 'is placed
                ElseIf x = 23 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox37.Text, pass) & vbCrLf 'company name
                ElseIf x = 24 Then
                    TextBox42.Text = TextBox42.Text & encrypt(TextBox44.Text, pass) & vbCrLf 'mail id
                End If
                x += 1
            End If
        Next
        'build file
        Dim objWriter As New System.IO.StreamWriter(TextBox45.Text, True)
        objWriter.WriteLine("")
        objWriter.Close()

        Using sw As StreamWriter = New StreamWriter(TextBox45.Text)
            For Each items In TextBox42.Text
                sw.Write(items)
            Next

        End Using
        update_reset()
        MsgBox("Student Details updated")
    End Function
    Function delete()
        TextBox42.Text = ""

        Dim editmode = 0
        Dim x = 0
        Dim readupdate() As String = System.IO.File.ReadAllLines(TextBox45.Text)

        '  Exit Function
        For Each line As String In readupdate
            line = Decrypt(line, pass)
            If line = TextBox29.Text Then
                editmode = 1
            End If

            If editmode = 0 Then
                TextBox42.Text = TextBox42.Text & encrypt(line, pass) & vbCrLf
            Else
                x += 1
            End If
            If x > 24 Then 'this was x >= 24 but hs to be x > 24
                editmode = 0
            End If
        Next

        'build file
        Dim objWriter As New System.IO.StreamWriter(TextBox45.Text, True)
        objWriter.WriteLine("")
        objWriter.Close()

        Using sw As StreamWriter = New StreamWriter(TextBox45.Text)
            For Each items In TextBox42.Text
                sw.Write(items)
            Next
        End Using
        update_reset()
        MsgBox("Student Details Removed")
    End Function
    Function update_reset()
        TextBox29.Text = "" 'regno
        TextBox36.Text = "" 'name
        TextBox35.Text = "" 'father name
        TextBox34.Text = "" 'dob
        TextBox44.Text = "" 'mail id
        TextBox33.Text = "" 'add
        TextBox32.Text = "" 'ph no
        TextBox31.Text = "" 'sslc
        TextBox30.Text = "" 'hsc
        TextBox39.Text = "" 'cgpa

        TextBox28.Text = "" 'gpa per sem
        TextBox27.Text = ""
        TextBox26.Text = ""
        TextBox25.Text = ""
        TextBox24.Text = ""
        TextBox22.Text = ""
        TextBox23.Text = ""
        TextBox21.Text = ""

        TextBox41.Text = "" 'dept
        TextBox38.Text = "" 'interest
        ComboBox7.Text = "" 'history of arrear
        ComboBox6.Text = "" 'standing arrear
        TextBox40.Text = "" 'no of standing arrear
        ComboBox5.Text = "" 'is placed
        TextBox37.Text = "" ' company name

        Button4.Enabled = False
        Button5.Enabled = False
        Button6.Enabled = False
        Button15.Enabled = False
        TextBox29.ReadOnly = False
        ComboBox11.Enabled = True
        ComboBox11.Text = ""
    End Function

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        update_reset()
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        delete()
    End Sub
    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox8.SelectedIndexChanged
        ComboBox9.Items.Clear()
        ComboBox9.Show()
        If ComboBox8.Text = "Name" Then
            Label40.Show()
            TextBox46.Show()
            ComboBox9.Hide()
        ElseIf ComboBox8.Text = "Register No" Then
            Label40.Show()
            TextBox46.Show()
            ComboBox9.Hide()
            'Register No
        ElseIf ComboBox8.Text = "All" Then
            Label40.Hide()
            TextBox46.Hide()
            ComboBox9.Hide()
        ElseIf ComboBox8.Text = "placed" Then
            Label40.Hide()
            TextBox46.Hide()
            ComboBox9.Items.Add("Yes")
            ComboBox9.Items.Add("No")
            ComboBox9.Text = "Yes"
        ElseIf ComboBox8.Text = "CGPA" Then
            Label40.Show()
            TextBox46.Show()
            ComboBox9.Items.Add("greater than")
            ComboBox9.Items.Add("less than")
            ComboBox9.Items.Add("greater than equal to")
            ComboBox9.Items.Add("less than equal to")
            ComboBox9.Items.Add("equal to")
            ComboBox9.Text = "equal to"
        End If
    End Sub
    'search
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        clear_data()
        If searched = 0 Then
            DataGridView1.ColumnCount = 25
            Dim row As String() = New String() {"Register No", "Name", "Fathers Name", "Date Of Birth", "Address", "Phone No", "SSLC %", _
                                                "HSC %", "Gpa 1", "Gpa 2", "Gpa 3", "Gpa 4", "Gpa 5", "Gpa 6", "Gpa 7", "Gpa 8", _
                                                "Department", "CGPA", "Area Of Interest", "History Of Arrear", "Standing Arrear", _
                                                "No Of Arrear", "Placed Status", "Company Name", "Mail Id"}
            DataGridView1.Rows.Add(row)
        End If
        searched = 1
        search()
    End Sub
    Function search()
        Dim test
        If ComboBox10.Text = "" Then
            MsgBox("Select a Database")
            Exit Function
        End If
        If ComboBox8.Text = "Name" Then
            Label40.Show()
            TextBox46.Show()
            studentname()
        ElseIf ComboBox8.Text = "Register No" Then
            test = TextBox46.Text.Replace(" ", "")
            Label40.Show()
            TextBox46.Show()
            If test = "" Then
                MsgBox("Enter A Register Number")
            Else
                registerno()
            End If
        ElseIf ComboBox8.Text = "All" Then
            Label40.Hide()
            TextBox46.Hide()
            all()
        ElseIf ComboBox8.Text = "placed" Then
            Label40.Hide()
            TextBox46.Hide()
            placed()
        ElseIf ComboBox8.Text = "CGPA" Then
            Label40.Visible = True
            TextBox46.Visible = True
            test = TextBox46.Text.Replace(" ", "")
            If test = "" Then
                MsgBox("Enter A Value")
            Else
                cgpa()
            End If
        End If
    End Function
    Function all()
        Dim row As String()
        Dim found = 0
        Dim filename = CurDir() & "\databases\" & ComboBox10.Text & ".deptbase"
        Dim readupdate() As String = System.IO.File.ReadAllLines(filename)
        Dim str(24) As String
        Dim x = 0
        For Each line As String In readupdate
            x = x + 1
            If x = 1 Then
                str(0) = Decrypt(line, pass) 'regno
            ElseIf x = 2 Then
                str(1) = Decrypt(line, pass) 'name
            ElseIf x = 3 Then
                str(2) = Decrypt(line, pass) 'fathername
            ElseIf x = 4 Then
                str(3) = Decrypt(line, pass) 'dob
            ElseIf x = 5 Then
                str(4) = Decrypt(line, pass) 'address
            ElseIf x = 6 Then
                str(5) = Decrypt(line, pass) 'ph no
            ElseIf x = 7 Then
                str(6) = Decrypt(line, pass) 'sslc
            ElseIf x = 8 Then
                str(7) = Decrypt(line, pass) 'hsc
            ElseIf x = 9 Then
                str(8) = Decrypt(line, pass) 'till 16 gpa
            ElseIf x = 10 Then
                str(9) = Decrypt(line, pass)
            ElseIf x = 11 Then
                str(10) = Decrypt(line, pass)
            ElseIf x = 12 Then
                str(11) = Decrypt(line, pass)
            ElseIf x = 13 Then
                str(12) = Decrypt(line, pass)
            ElseIf x = 14 Then
                str(13) = Decrypt(line, pass)
            ElseIf x = 15 Then
                str(14) = Decrypt(line, pass)
            ElseIf x = 16 Then
                str(15) = Decrypt(line, pass)
            ElseIf x = 17 Then
                str(16) = Decrypt(line, pass) 'dept
            ElseIf x = 18 Then
                str(17) = Decrypt(line, pass) 'cgpa
            ElseIf x = 19 Then
                str(18) = Decrypt(line, pass) 'interest
            ElseIf x = 20 Then
                str(19) = Decrypt(line, pass) 'hist of arrear
            ElseIf x = 21 Then
                str(20) = Decrypt(line, pass) 'standing arrear
            ElseIf x = 22 Then
                str(21) = Decrypt(line, pass) 'no of standing arrear
            ElseIf x = 23 Then
                str(22) = Decrypt(line, pass) 'is placed
            ElseIf x = 24 Then
                str(23) = Decrypt(line, pass) 'company name
            ElseIf x = 25 Then
                str(24) = Decrypt(line, pass) 'mail id
                'If str(0) = TextBox29.Text Then
                row = New String() {str(0), str(1), str(2), str(3), str(4), str(5), str(6), str(7), str(8), _
str(9), str(10), str(11), str(12), str(13), str(14), str(15), str(16), str(17), str(18), str(19), str(20), _
str(21), str(22), str(23), str(24)}
                DataGridView1.Rows.Add(row)
                'Exit For
                'End I
                x = 0
            End If
        Next
    End Function
    Function placed()
        Dim row As String()
        Dim found = 0
        Dim filename = CurDir() & "\databases\" & ComboBox10.Text & ".deptbase"
        Dim readupdate() As String = System.IO.File.ReadAllLines(filename)
        Dim str(24) As String
        Dim x = 0
        For Each line As String In readupdate
            x = x + 1
            If x = 1 Then
                str(0) = Decrypt(line, pass) 'regno
            ElseIf x = 2 Then
                str(1) = Decrypt(line, pass) 'name
            ElseIf x = 3 Then
                str(2) = Decrypt(line, pass) 'fathername
            ElseIf x = 4 Then
                str(3) = Decrypt(line, pass) 'dob
            ElseIf x = 5 Then
                str(4) = Decrypt(line, pass) 'address
            ElseIf x = 6 Then
                str(5) = Decrypt(line, pass) 'ph no
            ElseIf x = 7 Then
                str(6) = Decrypt(line, pass) 'sslc
            ElseIf x = 8 Then
                str(7) = Decrypt(line, pass) 'hsc
            ElseIf x = 9 Then
                str(8) = Decrypt(line, pass) 'till 16 gpa
            ElseIf x = 10 Then
                str(9) = Decrypt(line, pass)
            ElseIf x = 11 Then
                str(10) = Decrypt(line, pass)
            ElseIf x = 12 Then
                str(11) = Decrypt(line, pass)
            ElseIf x = 13 Then
                str(12) = Decrypt(line, pass)
            ElseIf x = 14 Then
                str(13) = Decrypt(line, pass)
            ElseIf x = 15 Then
                str(14) = Decrypt(line, pass)
            ElseIf x = 16 Then
                str(15) = Decrypt(line, pass)
            ElseIf x = 17 Then
                str(16) = Decrypt(line, pass) 'dept
            ElseIf x = 18 Then
                str(17) = Decrypt(line, pass) 'cgpa
            ElseIf x = 19 Then
                str(18) = Decrypt(line, pass) 'interest
            ElseIf x = 20 Then
                str(19) = Decrypt(line, pass) 'hist of arrear
            ElseIf x = 21 Then
                str(20) = Decrypt(line, pass) 'standing arrear
            ElseIf x = 22 Then
                str(21) = Decrypt(line, pass) 'no of standing arrear
            ElseIf x = 23 Then
                str(22) = Decrypt(line, pass) 'is placed
            ElseIf x = 24 Then
                str(23) = Decrypt(line, pass) 'company name
            ElseIf x = 25 Then
                str(24) = Decrypt(line, pass) 'mail id
                If str(22).ToLower = ComboBox9.Text.ToLower Then
                    row = New String() {str(0), str(1), str(2), str(3), str(4), str(5), str(6), str(7), str(8), _
    str(9), str(10), str(11), str(12), str(13), str(14), str(15), str(16), str(17), str(18), str(19), str(20), _
    str(21), str(22), str(23), str(24)}
                    DataGridView1.Rows.Add(row)
                    'Exit For
                End If
                x = 0
            End If
        Next
    End Function
    Function studentname()
        Dim row As String()
        Dim found = 0
        Dim filename = CurDir() & "\databases\" & ComboBox10.Text & ".deptbase"
        Dim readupdate() As String = System.IO.File.ReadAllLines(filename)
        Dim str(24) As String
        Dim x = 0
        For Each line As String In readupdate
            x = x + 1
            If x = 1 Then
                str(0) = Decrypt(line, pass) 'regno
            ElseIf x = 2 Then
                str(1) = Decrypt(line, pass) 'name
            ElseIf x = 3 Then
                str(2) = Decrypt(line, pass) 'fathername
            ElseIf x = 4 Then
                str(3) = Decrypt(line, pass) 'dob
            ElseIf x = 5 Then
                str(4) = Decrypt(line, pass) 'address
            ElseIf x = 6 Then
                str(5) = Decrypt(line, pass) 'ph no
            ElseIf x = 7 Then
                str(6) = Decrypt(line, pass) 'sslc
            ElseIf x = 8 Then
                str(7) = Decrypt(line, pass) 'hsc
            ElseIf x = 9 Then
                str(8) = Decrypt(line, pass) 'till 16 gpa
            ElseIf x = 10 Then
                str(9) = Decrypt(line, pass)
            ElseIf x = 11 Then
                str(10) = Decrypt(line, pass)
            ElseIf x = 12 Then
                str(11) = Decrypt(line, pass)
            ElseIf x = 13 Then
                str(12) = Decrypt(line, pass)
            ElseIf x = 14 Then
                str(13) = Decrypt(line, pass)
            ElseIf x = 15 Then
                str(14) = Decrypt(line, pass)
            ElseIf x = 16 Then
                str(15) = Decrypt(line, pass)
            ElseIf x = 17 Then
                str(16) = Decrypt(line, pass) 'dept
            ElseIf x = 18 Then
                str(17) = Decrypt(line, pass) 'cgpa
            ElseIf x = 19 Then
                str(18) = Decrypt(line, pass) 'interest
            ElseIf x = 20 Then
                str(19) = Decrypt(line, pass) 'hist of arrear
            ElseIf x = 21 Then
                str(20) = Decrypt(line, pass) 'standing arrear
            ElseIf x = 22 Then
                str(21) = Decrypt(line, pass) 'no of standing arrear
            ElseIf x = 23 Then
                str(22) = Decrypt(line, pass) 'is placed
            ElseIf x = 24 Then
                str(23) = Decrypt(line, pass) 'company name
            ElseIf x = 25 Then
                str(24) = Decrypt(line, pass) 'mail id
                If str(1).ToLower.Contains(TextBox46.Text.ToLower) Then
                    row = New String() {str(0), str(1), str(2), str(3), str(4), str(5), str(6), str(7), str(8), _
    str(9), str(10), str(11), str(12), str(13), str(14), str(15), str(16), str(17), str(18), str(19), str(20), _
    str(21), str(22), str(23), str(24)}
                    DataGridView1.Rows.Add(row)
                    'Exit For
                End If
                x = 0
            End If
        Next
    End Function

    Function registerno()
        Dim row As String()
        Dim found = 0
        Dim filename = CurDir() & "\databases\" & ComboBox10.Text & ".deptbase"
        Dim readupdate() As String = System.IO.File.ReadAllLines(filename)
        Dim str(24) As String
        Dim x = 0
        For Each line As String In readupdate
            x = x + 1
            If x = 1 Then
                str(0) = Decrypt(line, pass) 'regno
            ElseIf x = 2 Then
                str(1) = Decrypt(line, pass) 'name
            ElseIf x = 3 Then
                str(2) = Decrypt(line, pass) 'fathername
            ElseIf x = 4 Then
                str(3) = Decrypt(line, pass) 'dob
            ElseIf x = 5 Then
                str(4) = Decrypt(line, pass) 'address
            ElseIf x = 6 Then
                str(5) = Decrypt(line, pass) 'ph no
            ElseIf x = 7 Then
                str(6) = Decrypt(line, pass) 'sslc
            ElseIf x = 8 Then
                str(7) = Decrypt(line, pass) 'hsc
            ElseIf x = 9 Then
                str(8) = Decrypt(line, pass) 'till 16 gpa
            ElseIf x = 10 Then
                str(9) = Decrypt(line, pass)
            ElseIf x = 11 Then
                str(10) = Decrypt(line, pass)
            ElseIf x = 12 Then
                str(11) = Decrypt(line, pass)
            ElseIf x = 13 Then
                str(12) = Decrypt(line, pass)
            ElseIf x = 14 Then
                str(13) = Decrypt(line, pass)
            ElseIf x = 15 Then
                str(14) = Decrypt(line, pass)
            ElseIf x = 16 Then
                str(15) = Decrypt(line, pass)
            ElseIf x = 17 Then
                str(16) = Decrypt(line, pass) 'dept
            ElseIf x = 18 Then
                str(17) = Decrypt(line, pass) 'cgpa
            ElseIf x = 19 Then
                str(18) = Decrypt(line, pass) 'interest
            ElseIf x = 20 Then
                str(19) = Decrypt(line, pass) 'hist of arrear
            ElseIf x = 21 Then
                str(20) = Decrypt(line, pass) 'standing arrear
            ElseIf x = 22 Then
                str(21) = Decrypt(line, pass) 'no of standing arrear
            ElseIf x = 23 Then
                str(22) = Decrypt(line, pass) 'is placed
            ElseIf x = 24 Then
                str(23) = Decrypt(line, pass) 'company name
            ElseIf x = 25 Then
                str(24) = Decrypt(line, pass) 'mail id
                If str(0).ToLower = TextBox46.Text.ToLower Then
                    row = New String() {str(0), str(1), str(2), str(3), str(4), str(5), str(6), str(7), str(8), _
    str(9), str(10), str(11), str(12), str(13), str(14), str(15), str(16), str(17), str(18), str(19), str(20), _
    str(21), str(22), str(23), str(24)}
                    DataGridView1.Rows.Add(row)
                    'Exit For
                End If
                x = 0
            End If
        Next
    End Function
    Function cgpa()
        'On Error GoTo a
        Dim row As String()
        Dim val1 As Integer
        Dim val2 As Integer
        Dim found = 0
        Dim filename = CurDir() & "\databases\" & ComboBox10.Text & ".deptbase"
        Dim readupdate() As String = System.IO.File.ReadAllLines(filename)
        Dim str(24) As String
        Dim x = 0
        For Each line As String In readupdate
            x = x + 1
            If x = 1 Then
                str(0) = Decrypt(line, pass) 'regno
            ElseIf x = 2 Then
                str(1) = Decrypt(line, pass) 'name
            ElseIf x = 3 Then
                str(2) = Decrypt(line, pass) 'fathername
            ElseIf x = 4 Then
                str(3) = Decrypt(line, pass) 'dob
            ElseIf x = 5 Then
                str(4) = Decrypt(line, pass) 'address
            ElseIf x = 6 Then
                str(5) = Decrypt(line, pass) 'ph no
            ElseIf x = 7 Then
                str(6) = Decrypt(line, pass) 'sslc
            ElseIf x = 8 Then
                str(7) = Decrypt(line, pass) 'hsc
            ElseIf x = 9 Then
                str(8) = Decrypt(line, pass) 'till 16 gpa
            ElseIf x = 10 Then
                str(9) = Decrypt(line, pass)
            ElseIf x = 11 Then
                str(10) = Decrypt(line, pass)
            ElseIf x = 12 Then
                str(11) = Decrypt(line, pass)
            ElseIf x = 13 Then
                str(12) = Decrypt(line, pass)
            ElseIf x = 14 Then
                str(13) = Decrypt(line, pass)
            ElseIf x = 15 Then
                str(14) = Decrypt(line, pass)
            ElseIf x = 16 Then
                str(15) = Decrypt(line, pass)
            ElseIf x = 17 Then
                str(16) = Decrypt(line, pass) 'dept
            ElseIf x = 18 Then
                str(17) = Decrypt(line, pass) 'cgpa
            ElseIf x = 19 Then
                str(18) = Decrypt(line, pass) 'interest
            ElseIf x = 20 Then
                str(19) = Decrypt(line, pass) 'hist of arrear
            ElseIf x = 21 Then
                str(20) = Decrypt(line, pass) 'standing arrear
            ElseIf x = 22 Then
                str(21) = Decrypt(line, pass) 'no of standing arrear
            ElseIf x = 23 Then
                str(22) = Decrypt(line, pass) 'is placed
            ElseIf x = 24 Then
                str(23) = Decrypt(line, pass) 'company name
            ElseIf x = 25 Then
                str(24) = Decrypt(line, pass) 'mail id
                val1 = Decimal.Parse(str(17)) * 100
                val2 = Decimal.Parse(TextBox46.Text) * 100

                If ComboBox9.Text = "greater than" Then
                    If val1 > val2 Then
                        row = New String() {str(0), str(1), str(2), str(3), str(4), str(5), str(6), str(7), str(8), _
        str(9), str(10), str(11), str(12), str(13), str(14), str(15), str(16), str(17), str(18), str(19), str(20), _
        str(21), str(22), str(23), str(24)}
                        DataGridView1.Rows.Add(row)
                        'Exit For
                    End If
                ElseIf ComboBox9.Text = "less than" Then
                    If val1 < val2 Then
                        row = New String() {str(0), str(1), str(2), str(3), str(4), str(5), str(6), str(7), str(8), _
        str(9), str(10), str(11), str(12), str(13), str(14), str(15), str(16), str(17), str(18), str(19), str(20), _
        str(21), str(22), str(23), str(24)}
                        DataGridView1.Rows.Add(row)
                        'Exit For
                    End If
                ElseIf ComboBox9.Text = "greater than equal to" Then
                    If val1 >= val2 Then
                        row = New String() {str(0), str(1), str(2), str(3), str(4), str(5), str(6), str(7), str(8), _
        str(9), str(10), str(11), str(12), str(13), str(14), str(15), str(16), str(17), str(18), str(19), str(20), _
        str(21), str(22), str(23), str(24)}
                        DataGridView1.Rows.Add(row)
                        'Exit For
                    End If
                ElseIf ComboBox9.Text = "less than equal to" Then
                    If val1 <= val2 Then
                        row = New String() {str(0), str(1), str(2), str(3), str(4), str(5), str(6), str(7), str(8), _
        str(9), str(10), str(11), str(12), str(13), str(14), str(15), str(16), str(17), str(18), str(19), str(20), _
        str(21), str(22), str(23), str(24)}
                        DataGridView1.Rows.Add(row)
                        'Exit For
                    End If
                ElseIf ComboBox9.Text = "equal to" Then
                    If val1 = val2 Then
                        row = New String() {str(0), str(1), str(2), str(3), str(4), str(5), str(6), str(7), str(8), _
        str(9), str(10), str(11), str(12), str(13), str(14), str(15), str(16), str(17), str(18), str(19), str(20), _
        str(21), str(22), str(23), str(24)}
                        DataGridView1.Rows.Add(row)
                        'Exit For
                    End If
                End If
                x = 0
            End If
        Next
        Exit Function

a:      MsgBox("an error occured")
    End Function



    Function clear_data()
        If CheckBox1.Checked = True Then
        Else
            DataGridView1.Rows.Clear()
            searched = 0
        End If
    End Function




    'export to excel
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        On Error Resume Next
        Dim MyFolderBrowser As New System.Windows.Forms.FolderBrowserDialog
        Dim path
        MyFolderBrowser.Description = "Select the Path To Save Log File"
        ' Sets the root folder where the browsing starts from 
        MyFolderBrowser.RootFolder = Environment.SpecialFolder.DesktopDirectory
        Dim dlgResult As DialogResult = MyFolderBrowser.ShowDialog()
        If dlgResult = Windows.Forms.DialogResult.OK Then
            path = MyFolderBrowser.SelectedPath & "\" & ComboBox10.Text & ".xlsx"
        Else
            Exit Sub
        End If


        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim i As Integer
        Dim j As Integer

        xlApp = New Excel.ApplicationClass
        xlWorkBook = xlApp.Workbooks.Add(misValue)
        xlWorkSheet = xlWorkBook.Sheets("sheet1")

        For i = 0 To DataGridView1.RowCount - 1
            For j = 0 To DataGridView1.ColumnCount - 1
                If CheckBox4.Checked = True Then
                    If DataGridView1.Columns(j).Visible = True Then
                        xlWorkSheet.Cells(i + 1, j + 1) = _
                        DataGridView1(j, i).Value.ToString()
                    End If

                Else
                    xlWorkSheet.Cells(i + 1, j + 1) = _
                   DataGridView1(j, i).Value.ToString()
                End If
            Next
        Next
        xlWorkSheet.SaveAs(path)
        xlWorkBook.Close()
        xlApp.Quit()
        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)

        If CheckBox4.Checked = True Then
            Dim xlBook As Excel.Workbook
            Dim xlSheet As Excel.Worksheet
            Dim rg As Excel.Range
            xlApp = New Excel.ApplicationClass 'CreateObject("Excel.Application")
            xlBook = xlApp.Workbooks.Open(path)
            xlSheet = xlBook.Worksheets(1)
            If CheckBox3.Checked = True Then
                rg = xlSheet.Columns("V") 'arrear
                rg.Select()
                rg.Delete()
                rg = xlSheet.Columns("U") 'arrear
                rg.Select()
                rg.Delete()
                rg = xlSheet.Columns("T") 'arrear
                rg.Select()
                rg.Delete()
            End If
            If CheckBox2.Checked = True Then
                rg = xlSheet.Columns("P") 'gpa
                rg.Select()
                rg.Delete()
                rg = xlSheet.Columns("O") 'gpa
                rg.Select()
                rg.Delete()
                rg = xlSheet.Columns("N") 'gpa
                rg.Select()
                rg.Delete()
                rg = xlSheet.Columns("M") 'gpa
                rg.Select()
                rg.Delete()
                rg = xlSheet.Columns("L") 'gpa
                rg.Select()
                rg.Delete()
                rg = xlSheet.Columns("K") 'gpa
                rg.Select()
                rg.Delete()
                rg = xlSheet.Columns("J") 'gpa
                rg.Select()
                rg.Delete()
                rg = xlSheet.Columns("I") 'gpa
                rg.Select()
                rg.Delete()
            End If
            rg = xlSheet.Columns("A") 'gpa
            rg.Select()
            xlBook.Save()
            xlApp.Quit()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp)
            xlSheet = Nothing
            xlBook = Nothing
            xlApp = Nothing
        End If



    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'label38 - label in about box
        Label38.Text = Label38.Text.Substring(1) & Label38.Text.Substring(0, 1)
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("http://www.ucentp.in")
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            DataGridView1.Columns(8).Visible = False
            DataGridView1.Columns(9).Visible = False
            DataGridView1.Columns(10).Visible = False
            DataGridView1.Columns(11).Visible = False
            DataGridView1.Columns(12).Visible = False
            DataGridView1.Columns(13).Visible = False
            DataGridView1.Columns(14).Visible = False
            DataGridView1.Columns(15).Visible = False
        Else
            DataGridView1.Columns(8).Visible = True
            DataGridView1.Columns(9).Visible = True
            DataGridView1.Columns(10).Visible = True
            DataGridView1.Columns(11).Visible = True
            DataGridView1.Columns(12).Visible = True
            DataGridView1.Columns(13).Visible = True
            DataGridView1.Columns(14).Visible = True
            DataGridView1.Columns(15).Visible = True
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            DataGridView1.Columns(19).Visible = False
            DataGridView1.Columns(20).Visible = False
            DataGridView1.Columns(21).Visible = False
        Else
            DataGridView1.Columns(19).Visible = True
            DataGridView1.Columns(20).Visible = True
            DataGridView1.Columns(21).Visible = True
        End If
    End Sub



    Private Sub PictureBox1_up(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp

    End Sub

    Private Sub PictureBox1_down(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown

    End Sub

    Private Sub ComboBox11_textchanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox11.TextChanged
        TextBox29.Text = ComboBox11.Text
    End Sub
    'db options

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        For Each line In ListView1.Items
            line.remove()
        Next
        prelimanalysis()
    End Sub
    Function prelimanalysis()
       'no longer needed
	   On Error GoTo a
        Dim n As Decimal = 0
        Dim n1 As String
        Dim n2 As Integer
        Dim readupdate() As String = System.IO.File.ReadAllLines(CurDir() & "\databases\" & ComboBox12.Text & ".deptbase")
        For Each line As String In readupdate
            n = n + 1
        Next
        n = n / 25

        n1 = Convert.ToString(n)
        n2 = Integer.Parse(n1)
        ListView1.Items.Add("No Error Was Identified In The Database")
        Exit Function
a:
        ListView1.Items.Add("There is an error in the database")
        majorlimanalysis()
        'MsgBox("There seems to be some error in the database")
    End Function
    Function majorlimanalysis()
        'no longer needed
		On Error GoTo a
        Dim n As Integer = 0
        Dim check
        Dim asd
        Dim detail As Integer
        Dim filename = CurDir() & "\databases\" & ComboBox12.Text & ".deptbase"
        Dim readupdate() As String = System.IO.File.ReadAllLines(filename)
        Dim str(24) As String
        Dim x = 0
        For Each line As String In readupdate
            n = n + 1
            x = x + 1
            If x = 1 Then
                str(0) = Decrypt(line, pass) 'regno
                If str(0).Length > 12 Then
                    GoTo a
                End If
                asd = str(0).Substring(0, str(0).Length - 5)
                check = Integer.Parse(asd)
            ElseIf x = 25 Then
                x = 0
            End If
        Next

        ListView1.Items.Add("Error couldnot be identified")
        Exit Function

a:

        detail = (n - 1) / 25
        ListView1.Items.Add("Error identified in student Detail " & detail + 1)
        If CheckBox5.Checked = True Then
            fix(n)
        Else
        End If
    End Function
    Function fix(ByVal lineno As Integer)
        'no longer needed
		On Error GoTo a
        ListView1.Items.Add("Attempting To Correct Error")
        Dim path As String = CurDir() & "\databases\" & ComboBox12.Text & ".deptbase"
        Dim TheFileLines As New List(Of String)
        TheFileLines.AddRange(System.IO.File.ReadAllLines(path))

        ' if line is beyond end of list the exit sub

        If lineno > TheFileLines.Count Then

            ListView1.Items.Add("Problem Was Not Fixed")
            Exit Function
        End If
        TheFileLines.RemoveAt(lineno - 1)
        System.IO.File.WriteAllLines(path, TheFileLines.ToArray)
        ListView1.Items.Add("Problem Was Fixed")
        Exit Function
a:
        ListView1.Items.Add("Problem Was Not Fixed")
    End Function

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        'rename db
        Dim x = InputBox("Enter The New Name:", "Enter A Name", ComboBox12.Text)
        Dim y
        Dim n
        Dim nofol = 0
        Dim newname As String
        Dim oldname As String = CurDir() & "\databases\" & ComboBox12.Text & ".deptbase"

a:      n = x.Length
        y = x.Replace(" ", "")
        If y = "" Then
            If n = 0 Then
                Exit Sub
            Else
                MsgBox("enter a valid name, database will not  be renamed")
            End If
            Exit Sub
        End If

        If ComboBox12.Text = x Then
            MsgBox("filenames cannot be the same please recheck")
            Exit Sub
        End If

        If File.Exists(CurDir() & "\databases\" & x & ".deptbase") Then
        Else
            nofol = 1
        End If


        While nofol = 0
            x = InputBox("A Folder By The Name " & x & " Already Exists So Specify A NewName", "Enter A Name", x)
            GoTo a
        End While
        newname = CurDir() & "\databases\" & x & ".deptbase"
        Rename(oldname, newname)
        ComboBox1.Items.Clear()
        ComboBox10.Items.Clear()
        ComboBox12.Items.Clear()
        filelist()
        MsgBox("Database renamed")
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        'delete
        Dim x = MsgBox("Are You Sure You Want To Remove This Database", vbYesNo)
        If x = MsgBoxResult.Yes Then

        Else
            Exit Sub
        End If
        Dim FileToDelete As String
        FileToDelete = CurDir() & "\databases\" & ComboBox12.Text & ".deptbase"
        If System.IO.File.Exists(FileToDelete) = True Then
            System.IO.File.Delete(FileToDelete)
            MsgBox("Database Removed")
        Else
            MsgBox("Database Not Found")
        End If
        ComboBox1.Items.Clear()
        ComboBox10.Items.Clear()
        ComboBox12.Items.Clear()
        filelist()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        DataGridView1.AllowUserToDeleteRows = 1

        DataGridView1.Rows.RemoveAt(0)
        DataGridView1.Sort(DataGridView1.Columns(0), System.ComponentModel.ListSortDirection.Ascending)


        DataGridView1.ColumnCount = 25
        Dim row As String() = New String() {"Register No", "Name", "Fathers Name", "Date Of Birth", "Address", "Phone No", "SSLC %", _
                                            "HSC %", "Gpa 1", "Gpa 2", "Gpa 3", "Gpa 4", "Gpa 5", "Gpa 6", "Gpa 7", "Gpa 8", _
                                            "Department", "CGPA", "Area Of Interest", "History Of Arrear", "Standing Arrear", _
                                            "No Of Arrear", "Placed Status", "Company Name", "Mail Id"}
        'DataGridView1.Rows.Add(row)
        DataGridView1.Rows.Insert(0, row)
    End Sub
    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Dim k
        k = is_encrypted()
        If k = 1 Then
            encrypt_old()
        Else
            MsgBox("Database Already Encrypted")
        End If
    End Sub
    Function encrypt_old()
        On Error Resume Next
        Dim n = 0
        Dim path = CurDir() & "\databases\" & ComboBox12.Text & ".deptbase"
        If CheckBox6.Checked = True Then
            Dim path2 = CurDir() & "\databases\" & ComboBox12.Text & ".olddeptbase"
            System.IO.File.Copy(path, path2)
        End If
        Dim readupdate() As String = System.IO.File.ReadAllLines(path)
        For Each line As String In readupdate
            n += 1
            Label43.Text = "Encrypting line: " & n
            Me.Refresh()
            TextBox47.Text = TextBox47.Text & encrypt(line, pass) & vbCrLf
        Next
        'build file
        Label43.Text = "Creating file"
        Me.Refresh()
        Dim objWriter As New System.IO.StreamWriter(path, True)
        objWriter.WriteLine("")
        objWriter.Close()
        Using sw As StreamWriter = New StreamWriter(path)
            For Each items In TextBox47.Text
                sw.Write(items)
            Next
        End Using
        Label43.Text = "File created successfully"
        MsgBox("Database File Encrypted")
    End Function
    Function is_encrypted()
        On Error GoTo a
        Dim n As Integer = 0
        Dim check
        Dim asd
        Dim detail As Integer
        Dim filename = CurDir() & "\databases\" & ComboBox12.Text & ".deptbase"
        Dim readupdate() As String = System.IO.File.ReadAllLines(filename)
        Dim str(1) As String
        For Each line As String In readupdate
            str(0) = line 'regno
            asd = str(0).Substring(0, str(0).Length - 5)
            check = Integer.Parse(asd)
            Exit For
        Next
        is_encrypted = 1
        Exit Function
a:
        is_encrypted = 0

    End Function
    'data encrypt and decrypt
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


    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim f5 As New Form5
        f5.Show()
    End Sub



    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If Label44.Top = -Label44.Height Then
            Label44.Top = Panel1.Height
        Else
            Label44.Top -= 1
        End If
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveToolStripMenuItem.Click
        For Each n As System.Windows.Forms.DataGridViewCell In Me.DataGridView1.SelectedCells
            DataGridView1.Rows(n.RowIndex).Selected = True
        Next
        For Each selected_row As System.Windows.Forms.DataGridViewRow In Me.DataGridView1.SelectedRows
            If selected_row.Index = 0 Then
            Else
                Me.DataGridView1.Rows.Remove(selected_row)
            End If
        Next
    End Sub
    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        For Each arow As System.Windows.Forms.DataGridViewRow In Me.DataGridView1.Rows
            If arow.Index > 0 Then
                arow.Selected = True
            End If
        Next
        For Each selected_row As System.Windows.Forms.DataGridViewRow In Me.DataGridView1.SelectedRows
            If selected_row.Index = 0 Then
            Else
                Me.DataGridView1.Rows.Remove(selected_row)
            End If
        Next

    End Sub
    Private Sub SelectAllToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem1.Click
        For Each arow As System.Windows.Forms.DataGridViewRow In Me.DataGridView1.Rows
            If arow.Index > 0 Then
                arow.Selected = True
            End If
        Next
    End Sub
    Private Sub InvertSelectionToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InvertSelectionToolStripMenuItem1.Click
        For Each n As System.Windows.Forms.DataGridViewCell In Me.DataGridView1.SelectedCells
            DataGridView1.Rows(n.RowIndex).Selected = True
        Next
        For Each arow As System.Windows.Forms.DataGridViewRow In Me.DataGridView1.Rows
            If arow.Index > 0 Then
                If arow.Selected = True Then
                    arow.Selected = False
                Else
                    arow.Selected = True
                End If
            End If
        Next
    End Sub
    Private Sub SelectEntireRowToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectEntireRowToolStripMenuItem1.Click
        For Each n As System.Windows.Forms.DataGridViewCell In Me.DataGridView1.SelectedCells
            DataGridView1.Rows(n.RowIndex).Selected = True
        Next
    End Sub

    Private Sub SelectNoneToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectNonToolStripMenuItem.Click
        For Each arow As System.Windows.Forms.DataGridViewRow In Me.DataGridView1.Rows
            arow.Selected = False
        Next
    End Sub

    Private Sub AddToAlreadyCopiedDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToAlreadyCopiedDetailsToolStripMenuItem.Click
        For Each n As System.Windows.Forms.DataGridViewCell In Me.DataGridView1.SelectedCells
            If n.RowIndex > 0 Then
                DataGridView1.Rows(n.RowIndex).Selected = True
            End If
        Next
        Dim dr As New System.Windows.Forms.DataGridViewRow
        For Each dr In Me.DataGridView1.SelectedRows
            Me.DataGridView2.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, _
                                      dr.Cells(5).Value, dr.Cells(6).Value, dr.Cells(7).Value, dr.Cells(8).Value, dr.Cells(9).Value, _
                                      dr.Cells(10).Value, dr.Cells(11).Value, dr.Cells(12).Value, dr.Cells(13).Value, dr.Cells(14).Value, _
                                      dr.Cells(15).Value, dr.Cells(16).Value, dr.Cells(17).Value, dr.Cells(18).Value, dr.Cells(19).Value, _
                                      dr.Cells(20).Value, dr.Cells(21).Value, dr.Cells(22).Value, dr.Cells(23).Value, dr.Cells(24).Value)
        Next
    End Sub

    Private Sub ReplaceAlreadyCopiedDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReplaceAlreadyCopiedDetailsToolStripMenuItem.Click
        For Each n As System.Windows.Forms.DataGridViewCell In Me.DataGridView1.SelectedCells
            If n.RowIndex > 0 Then
                DataGridView1.Rows(n.RowIndex).Selected = True
            End If
        Next
        For Each arow As System.Windows.Forms.DataGridViewRow In Me.DataGridView2.Rows
            arow.Selected = True
        Next
        For Each selected_row As System.Windows.Forms.DataGridViewRow In Me.DataGridView2.SelectedRows
            Me.DataGridView2.Rows.Remove(selected_row)
        Next
        Dim dr As New System.Windows.Forms.DataGridViewRow
        For Each dr In Me.DataGridView1.SelectedRows
            Me.DataGridView2.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, _
                                      dr.Cells(5).Value, dr.Cells(6).Value, dr.Cells(7).Value, dr.Cells(8).Value, dr.Cells(9).Value, _
                                      dr.Cells(10).Value, dr.Cells(11).Value, dr.Cells(12).Value, dr.Cells(13).Value, dr.Cells(14).Value, _
                                      dr.Cells(15).Value, dr.Cells(16).Value, dr.Cells(17).Value, dr.Cells(18).Value, dr.Cells(19).Value, _
                                      dr.Cells(20).Value, dr.Cells(21).Value, dr.Cells(22).Value, dr.Cells(23).Value, dr.Cells(24).Value)
        Next
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        Dim dr As New System.Windows.Forms.DataGridViewRow
        For Each dr In Me.DataGridView2.Rows
            Me.DataGridView1.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, _
                                      dr.Cells(5).Value, dr.Cells(6).Value, dr.Cells(7).Value, dr.Cells(8).Value, dr.Cells(9).Value, _
                                      dr.Cells(10).Value, dr.Cells(11).Value, dr.Cells(12).Value, dr.Cells(13).Value, dr.Cells(14).Value, _
                                      dr.Cells(15).Value, dr.Cells(16).Value, dr.Cells(17).Value, dr.Cells(18).Value, dr.Cells(19).Value, _
                                      dr.Cells(20).Value, dr.Cells(21).Value, dr.Cells(22).Value, dr.Cells(23).Value, dr.Cells(24).Value)
        Next
    End Sub

    Private Sub LinkLabel3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Dim f6 As New Form6
        f6.Show()
    End Sub

    'show biodata
    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Dim p As New ProcessStartInfo
        p.FileName = "Aux_apps\Create_biodata.exe"
        p.Arguments = TextBox36.Text.Replace(" ", "_") & " " & TextBox35.Text.Replace(" ", "_") & " " & _
        TextBox34.Text & " " & TextBox33.Text.Replace(" ", "_") & " " & TextBox32.Text & " " & _
        TextBox44.Text & " " & TextBox41.Text.Replace(" ", "_") & " " & TextBox38.Text.Replace(" ", "_") & " " & _
        TextBox31.Text & " " & TextBox30.Text & " " & TextBox39.Text
        '"pratheesh_russell fname 11-5-92 address 94434 mailid mech design 83 93 9"
        p.WindowStyle = ProcessWindowStyle.Hidden
        Process.Start(p)
    End Sub

    Private Sub TextBox17_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox17.TextChanged
        If TextBox17.Text = String.Empty Then
            ErrorProvider1.SetError(TextBox17, "Enter as zero, if CGPA is not known")
        Else
           
            If IsNumeric(TextBox17.Text) Then
                ErrorProvider1.SetError(TextBox17, "")
            Else
                ErrorProvider1.SetError(TextBox17, "Please Enter only numbers")
            End If
        End If
    End Sub

    Private Sub TextBox39_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox39.TextChanged
        If TextBox39.Text = String.Empty Then
            ErrorProvider1.SetError(TextBox39, "Enter as zero, if CGPA is not known")
        Else

            If IsNumeric(TextBox39.Text) Then
                ErrorProvider1.SetError(TextBox39, "")
            Else
                ErrorProvider1.SetError(TextBox39, "Please Enter only numbers")
            End If
        End If
    End Sub
End Class