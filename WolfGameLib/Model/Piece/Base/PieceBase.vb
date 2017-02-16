Imports System.Numerics
''' <summary>
''' 棋子
''' </summary>
Public MustInherit Class PieceBase
    Implements IPiece

    Public Overridable ReadOnly Property Camp As Camp Implements IPiece.Camp
    Public Property Location As Vector2 Implements IPiece.Location

    Public Shared InnerVectors As Vector2() = New Vector2() {New Vector2(0, 1), New Vector2(1, 0), New Vector2(1, 1)}
    Public Shared OuterVectors As Vector2() = New Vector2() {New Vector2(0, 2), New Vector2(2, 0), New Vector2(2, 2)}

    Public MustOverride Function Move(map(,) As IPiece, loc As Vector2) As Boolean Implements IPiece.Move

    Public Sub MoveTo(map(,) As IPiece, piece As IPiece, loc As Vector2)
        Dim temp As Vector2 = piece.Location
        piece.Location = loc
        map(loc.X, loc.Y) = piece
        map(temp.X, temp.Y) = Nothing
    End Sub
End Class
