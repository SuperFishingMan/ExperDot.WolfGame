Imports System.Numerics
Imports WolfGameLib
''' <summary>
''' 狼
''' </summary>
Public Class Wolf
    Inherits PieceBase

    Public Overrides ReadOnly Property Camp As Camp = Camp.Wolf

    Public Overrides Function Move(map(,) As IPiece, loc As Vector2) As Boolean
        Dim temp As Vector2 = (loc - Location)
        temp.X = Math.Abs(temp.X)
        temp.Y = Math.Abs(temp.Y)
        If InnerVectors.Contains(temp) AndAlso map(loc.X, loc.Y) Is Nothing Then
            MoveTo(map, Me, loc)
            Return True
        ElseIf OuterVectors.Contains(temp) Then
            Dim mid As Vector2 = (loc + Location) / 2
            If map(mid.X, mid.Y)?.Camp = Camp.Sheep Then
                MoveTo(map, Me, loc)
                map(mid.X, mid.Y) = Nothing
                Return True
            End If
        End If
        Return False
    End Function
End Class
