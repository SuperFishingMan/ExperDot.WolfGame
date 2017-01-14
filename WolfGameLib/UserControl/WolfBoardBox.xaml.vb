Imports System.Windows

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
                                                     .Margin = New Thickness(i * 39, (j - 2) * 39, 0, 0)})
            Next
        Next
        Grid0.Children.Add(New CellBoxEx With {.Margin = New Thickness(2 * 39 - 20, -39, 0, 0)})
        Grid0.Children.Add(New CellBoxEx With {.Margin = New Thickness(2 * 39 - 20, 4 * 39, 0, 0)})
        For Each SubPiece In Board.Map
            If SubPiece Is Nothing Then Continue For
            If SubPiece.Camp = Camp.Wolf Then
                Grid0.Children.Add(New PieceBox With {.Margin = New Thickness(SubPiece.Location.X * 39, (SubPiece.Location.Y - 2) * 39, 0, 0)})
            Else
                Grid0.Children.Add(New PieceBox With {.Margin = New Thickness(SubPiece.Location.X * 39, (SubPiece.Location.Y - 2) * 39, 0, 0), .IsOpposite = True})
            End If
        Next
    End Sub
End Class
