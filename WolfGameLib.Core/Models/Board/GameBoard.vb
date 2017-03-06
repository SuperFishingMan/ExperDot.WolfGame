''' <summary>
''' 棋盘
''' </summary>
Public Class GameBoard
    ''' <summary>
    ''' 地图改变时发生的事件
    ''' </summary>
    Public Event MapChanged(sender As Object, e As MapChangedEventArgs)
    ''' <summary>
    ''' 当游戏结束时发生的事件
    ''' </summary>
    Public Event GameOver(sender As Object, e As GameOverEventArgs)
    ''' <summary>
    ''' 地图
    ''' </summary>
    Public Property Map As Map
    ''' <summary>
    ''' 玩家阵营
    ''' </summary>
    Public Property PlayerCamp As Camp = Camp.Sheep
    ''' <summary>
    ''' 玩家模式
    ''' </summary>
    Public Property PlayerMode As PlayerMode = PlayerMode.Single
    ''' <summary>
    ''' 游戏AI
    ''' </summary>
    Public Property AI As GameAI

    ''' <summary>
    ''' 开始
    ''' </summary>
    Public Sub Start()
        Map = New Map(5, 9)
        Dim wolfLoc As VectorInt() = {New VectorInt(2, 2), New VectorInt(2, 6)}
        Dim sheepLoc As VectorInt() = {New VectorInt(1, 3), New VectorInt(2, 3), New VectorInt(3, 3),
                                     New VectorInt(1, 4), New VectorInt(3, 4),
                                     New VectorInt(1, 5), New VectorInt(2, 5), New VectorInt(3, 5)}
        For Each SubVec In wolfLoc
            Map.Place(New Wolf With {.Location = SubVec})
        Next
        For Each SubVec In sheepLoc
            Map.Place(New Sheep With {.Location = SubVec})
        Next
        '触发事件
        RaiseEvent MapChanged(Me, New MapChangedEventArgs(Map))
        '初始化AI
        If PlayerMode = PlayerMode.Single Then
            AI = New GameAI()
            If Not Map.ActivedCamp = PlayerCamp Then
                AI.Move(Me)
            End If
        End If
    End Sub
    ''' <summary>
    ''' 点击
    ''' </summary>
    Public Sub Clicked(loc As VectorInt)
        Static ActivePiece As IPiece
        If loc.X >= 0 AndAlso loc.X < Map.Width AndAlso loc.Y >= 0 AndAlso loc.Y < Map.Height Then
            If Map.Locate(loc) Is Nothing Then
                Move(ActivePiece, loc)
                ActivePiece = Nothing
            Else
                If Map.SheepRemaining > 0 AndAlso Map.ActivedCamp = Camp.Sheep Then
                    ActivePiece = Nothing
                Else
                    ActivePiece = Map.Locate(loc)
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' 点击,使用原始坐标
    ''' </summary>
    Public Sub ClickedRaw(rawX As Integer, rawY As Integer, r As Integer)
        Dim rb = r - 1
        Dim rc = r / 2 - 1
        Dim rd = rb * 2
        Dim re = rc / 4 - 1
        Dim x As Integer = rawX / rb
        Dim y As Integer = rawY / rb + 1
        If rawY <= rb - re Then
            x = 2 + (rawX - rd) / rc
            y = rawY / rc
        ElseIf rawY >= rb * 5 + re Then
            x = 2 + (rawX - rd) / rc
            y = (rawY - (rb * 5)) / rc + 6
        End If
        Clicked(New VectorInt(x, y))
    End Sub
    ''' <summary>
    ''' 移动
    ''' </summary>
    Public Sub Move(piece As IPiece, loc As VectorInt)
        If piece Is Nothing Then
            If Map.MoveTo(piece, loc) Then
                NextStep()
            End If
        Else
            If piece.Camp = Map.ActivedCamp Then
                If piece.Moveable(Map, loc) Then
                    Map.MoveTo(piece, loc)
                    NextStep()
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' 下一步
    ''' </summary>
    Public Sub NextStep()
        Map.Exchange()
        RaiseEvent MapChanged(Me, New MapChangedEventArgs(Map))
        If Map.CheckGameOver(Map) Then
            Defeate()
        End If
        If PlayerMode = PlayerMode.Single AndAlso Not Map.ActivedCamp = PlayerCamp Then
            AI.Move(Me)
        End If
    End Sub
    ''' <summary>
    ''' 悔棋
    ''' </summary>
    Public Sub GoBack(count As Integer)
        If Map.Movements.Count >= count AndAlso count > 0 Then
            For i = 0 To count - 1
                Dim last As Movement = Map.Movements.Last
                Map.GoBack(Map, last)
            Next
            RaiseEvent MapChanged(Me, New MapChangedEventArgs(Map))
        End If
    End Sub
    ''' <summary>
    ''' 执子方认输
    ''' </summary>
    Public Sub Defeate()
        Dim winner As Camp = If(Map.ActivedCamp = Camp.Wolf, Camp.Sheep, Camp.Wolf)
        RaiseEvent GameOver(Me, New GameOverEventArgs(Map, winner))
    End Sub

End Class
