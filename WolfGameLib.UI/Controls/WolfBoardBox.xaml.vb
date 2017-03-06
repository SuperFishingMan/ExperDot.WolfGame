Imports System.Windows
Imports System.Windows.Input
Imports WolfGameLib.Core

Public Class WolfBoardBox

    Public WithEvents Board As GameBoard
    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        For i = 0 To 3
            For j = 2 To 5
                Grid0.Children.Add(New CellBox With {.IsOpposite = Not ((i + j) Mod 2 = 0)，
                                                     .Margin = New Thickness(i * 39, (j - 1) * 39, 0, 0)})
            Next
        Next
        Grid0.Children.Add(New CellBoxEx With {.Margin = New Thickness(2 * 39 - 20, 0, 0, 0)})
        Grid0.Children.Add(New CellBoxEx With {.Margin = New Thickness(2 * 39 - 20, 5 * 39, 0, 0)})

        Board = New GameBoard
        Board.Start()
    End Sub

    Private Sub Grid1_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles Grid1.MouseDown
        Dim point As Point = e.GetPosition(Grid0)
        'Dim x As Integer = point.X / 39
        'Dim y As Integer = point.Y / 39 + 1
        'If point.Y <= 39 - 9 Then
        '    x = 2 + (point.X - 78) / 19
        '    y = point.Y / 19
        'ElseIf point.Y >= 39 * 5 + 9 Then
        '    x = 2 + (point.X - 78) / 19
        '    y = (point.Y - (39 * 5)) / 19 + 6
        'End If
        'Board.Clicked(New VectorInt(x, y))
        Board.ClickedRaw(point.X, point.Y, 40)
    End Sub

    Private Sub Board_GameOver(sender As Object, e As GameOverEventArgs) Handles Board.GameOver
        If MsgBox(If(e.Winner = Camp.Wolf, "狼方", "羊方") + "胜利！是否重来一局？", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Board.Start()
        End If
    End Sub
    Private Sub Board_MapChanged(sender As Object, e As MapChangedEventArgs) Handles Board.MapChanged
        Refresh()
    End Sub
    Private Sub Refresh()
        Grid1.Children.Clear()

        For Each SubPiece In Board.Map.Pieces
            If SubPiece Is Nothing Then Continue For
            Dim dx As Integer = If(SubPiece.Location.Y < 2 OrElse SubPiece.Location.Y > 6, 19, 39)
            Dim dy As Integer = (SubPiece.Location.Y - 1) * 39
            If SubPiece.Location.Y < 2 Then dy = SubPiece.Location.Y * 19
            If SubPiece.Location.Y > 6 Then dy = (SubPiece.Location.Y - 6) * 19 + 5 * 39
            Dim margin As New Thickness(78 + (SubPiece.Location.X - 2) * dx, dy, 0, 0)
            Dim isOpposite As Boolean = If(SubPiece.Camp = Camp.Sheep, True, False)
            Dim isScale As Boolean = If(SubPiece.Location.Y < 2 OrElse SubPiece.Location.Y > 6, True, False)
            Grid1.Children.Add(New PieceBox With {.Margin = margin, .IsOpposite = isOpposite, .IsScale = isScale})
        Next

        TextBlock1.Text = Board.Map.ActivedCamp.ToString & $"---闲置数量:{Board.Map.SheepRemaining}"
    End Sub
End Class
