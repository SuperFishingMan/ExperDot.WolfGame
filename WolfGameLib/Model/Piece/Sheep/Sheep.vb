Imports System.Numerics
Imports WolfGameLib
''' <summary>
''' 羊
''' </summary>
Public Class Sheep
    Inherits PieceBase

    Public Overrides ReadOnly Property Camp As Camp = Camp.Sheep


    Public Overrides Function Move(map(,) As IPiece, loc As Vector2) As Boolean
        Dim temp As Vector2 = (loc - Location)
        temp.X = Math.Abs(temp.X)
        temp.Y = Math.Abs(temp.Y)
        If InnerVectors.Contains(temp) AndAlso map(loc.X, loc.Y) Is Nothing Then
            MoveTo(map, Me, loc)
            Return True
        End If
        Return False
    End Function
End Class
