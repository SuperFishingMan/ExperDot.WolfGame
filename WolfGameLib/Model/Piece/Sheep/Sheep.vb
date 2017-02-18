Imports System.Numerics
Imports WolfGameLib
''' <summary>
''' 羊
''' </summary>
Public Class Sheep
    Inherits PieceBase

    Public Overrides ReadOnly Property Camp As Camp = Camp.Sheep

    Public Overrides Function Move(map As Map, loc As Vector2) As Boolean
        If Moveable(map, Location, loc) Then
            MoveTo(map, Me, loc)
            Return True
        End If
        Return False
    End Function

End Class
