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

    Public MustOverride Function Move(map As Map, loc As Vector2) As Boolean Implements IPiece.Move
    ''' <summary>
    ''' 移动
    ''' </summary>
    Public Sub MoveTo(map As Map, piece As IPiece, loc As Vector2)
        Dim temp As Vector2 = piece.Location
        map.Assign(piece, loc)
        map.Assign(Nothing, temp)
    End Sub
    ''' <summary>
    ''' 是否可以移动
    ''' </summary>
    Public Function Moveable(map As Map, loc As Vector2, target As Vector2) As Boolean
        If map.Joints(target.X, target.Y).Connected(Vector2.Zero) Then
            Dim temp As Vector2 = (target - loc)
            If temp.Length < 2 Then
                If map.Joints(loc.X, loc.Y).Connected(temp) Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function
End Class
