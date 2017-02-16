Imports System.Numerics
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input

Public Class WolfBoardBox

    Public Property Board As GameBoard
    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Board = New GameBoard
        For i = 0 To Board.Map.GetUpperBound(0) - 1
            For j = 2 To Board.Map.GetUpperBound(1) - 3
                Grid0.Children.Add(New CellBox With {.IsOpposite = Not ((i + j) Mod 2 = 0)，
                                                     .Margin = New Thickness(i * 39, j * 39, 0, 0)})
            Next
        Next
        Grid0.Children.Add(New CellBoxEx With {.Margin = New Thickness(2 * 39 - 20, 39, 0, 0)})
        Grid0.Children.Add(New CellBoxEx With {.Margin = New Thickness(2 * 39 - 20, 6 * 39, 0, 0)})
        Refresh()
    End Sub

    Private Sub Grid1_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles Grid1.MouseDown
        Dim point As Point = e.GetPosition(Grid0)
        Dim x As Integer = point.X / 39
        Dim y As Integer = point.Y / 39

        Board.Clicked(New Vector2(x, y))
        Refresh()

    End Sub


    Private Sub Refresh()
        Grid1.Children.Clear()

        For Each SubPiece In Board.Map
            If SubPiece Is Nothing Then Continue For
            If SubPiece.Camp = Camp.Wolf Then
                Grid1.Children.Add(New PieceBox With {.Margin = New Thickness(SubPiece.Location.X * 39, (SubPiece.Location.Y) * 39, 0, 0)})
            Else
                Grid1.Children.Add(New PieceBox With {.Margin = New Thickness(SubPiece.Location.X * 39, (SubPiece.Location.Y) * 39, 0, 0), .IsOpposite = True})
            End If
        Next
        TextBlock1.Text = Board.ActivedCamp.ToString
    End Sub
End Class
