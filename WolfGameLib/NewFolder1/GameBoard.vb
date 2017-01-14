Imports System.Numerics
''' <summary>
''' 棋盘
''' </summary>
Public Class GameBoard
    Public Property Map As IPiece(,)

    Public Property Sheeps As List(Of Sheep)
    Public Sub New()
        ReDim Map(4, 8)
        Sheeps = New List(Of Sheep)
        Dim capacity As Integer = 16
        For i = 0 To capacity - 1
            Sheeps.Add(New Sheep)
        Next
        Dim wolfLoc As Vector2() = {New Vector2(2, 2), New Vector2(2, 6)}
        Dim sheepLoc As Vector2() = {New Vector2(1, 3), New Vector2(2, 3), New Vector2(3, 3),
                                     New Vector2(1, 4), New Vector2(3, 4),
                                     New Vector2(1, 5), New Vector2(2, 5), New Vector2(3, 5)}
        For Each SubVec In wolfLoc
            Map(SubVec.X, SubVec.Y) = New Wolf With {.Location = SubVec}
        Next
        For Each SubVec In sheepLoc
            Map(SubVec.X, SubVec.Y) = New Sheep With {.Location = SubVec}
        Next
    End Sub
End Class
