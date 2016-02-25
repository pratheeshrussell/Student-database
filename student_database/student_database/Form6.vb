Public Class Form6
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

    Private Sub Form1_Paint(ByVal sender As Object, _
  ByVal e As PaintEventArgs) Handles MyBase.Paint

        Dim borderRectangle As Rectangle = Me.ClientRectangle
        Dim borderRectangle1 As Rectangle = Me.ClientRectangle
        borderRectangle.Inflate(-35, -35)
        ControlPaint.DrawBorder3D(e.Graphics, borderRectangle, _
            Border3DStyle.Raised)

        borderRectangle1.Inflate(0, 0)
        ControlPaint.DrawBorder3D(e.Graphics, borderRectangle1, _
            Border3DStyle.Raised)
    End Sub
    Private Sub Form6_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       'not used anymore
	   Me.ShowInTaskbar = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class