Imports System.Numerics
''' <summary>
''' 棋盘
''' </summary>
Public Class GameBoard
    ''' <summary>
    ''' 地图
    ''' </summary>
    Public Property Map As IPiece(,)
    ''' <summary>
    ''' 羊只闲置数量
    ''' </summary>
    Public Property SheepRemaining As Integer = 10
    ''' <summary>
    ''' 当前阵营
    ''' </summary>
    Public Property ActivedCamp As Camp = Camp.Wolf

    Private Property ActivePiece As IPiece
    Public Sub New()
        Start()
    End Sub
    ''' <summary>
    ''' 开始
    ''' </summary>
    Public Sub Start()
        ReDim Map(4, 8)
        Dim wolfLoc As Vector2() = {New Vector2(2, 2), New Vector2(2, 6)}
        Dim sheepLoc As Vector2() = {New Vector2(1, 3), New Vector2(2, 3), New Vector2(3, 3),
                                     New Vector2(1, 4), New Vector2(3, 4),
                                     New Vector2(1, 5), New Vector2(2, 5), New Vector2(3, 5)}
        For Each SubVec In wolfLoc
            Place(New Wolf With {.Location = SubVec})
        Next
        For Each SubVec In sheepLoc
            Place(New Sheep With {.Location = SubVec})
        Next
    End Sub
    ''' <summary>
    ''' 放置
    ''' </summary>
    Public Sub Place(piece As IPiece)
        Map(piece.Location.X, piece.Location.Y) = piece
    End Sub
    ''' <summary>
    ''' 交换阵营
    ''' </summary>
    Public Sub Exchange()
        If ActivedCamp = Camp.Wolf Then
            ActivedCamp = Camp.Sheep
        Else
            ActivedCamp = Camp.Wolf
        End If
        ActivePiece = Nothing
    End Sub
    ''' <summary>
    ''' 移动
    ''' </summary>
    Public Sub Move(piece As IPiece, loc As Vector2)
        If ActivedCamp = Camp.Sheep AndAlso SheepRemaining > 0 Then
            Place(New Sheep With {.Location = loc})
            SheepRemaining -= 1 '闲置数量减一
            Exchange()
        Else
            If piece?.Camp = ActivedCamp Then
                If piece.Move(Map, loc) Then
                    Exchange()
                End If
            End If
        End If
    End Sub

    Public Sub Clicked(loc As Vector2)
        If loc.X >= 0 AndAlso loc.X <= Map.GetUpperBound(0) AndAlso loc.Y >= 0 AndAlso loc.Y <= Map.GetUpperBound(1) Then
            If Map(loc.X, loc.Y) Is Nothing Then
                Move(ActivePiece, loc)
            Else
                ActivePiece = Map(loc.X, loc.Y)
            End If
        End If
    End Sub
End Class
