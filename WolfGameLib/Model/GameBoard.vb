Imports System.Numerics
''' <summary>
''' 棋盘
''' </summary>
Public Class GameBoard
    ''' <summary>
    ''' 地图改变时发生的事件
    ''' </summary>
    Public Event MapChanged(sender As Object, e As EventArgs)
    ''' <summary>
    ''' 地图
    ''' </summary>
    Public Property Map As Map
    ''' <summary>
    ''' 玩家阵营
    ''' </summary>
    Public Property PlayerCamp As Camp = Camp.Wolf
    ''' <summary>
    ''' 玩家模式
    ''' </summary>
    Public Property PlayerMode As PlayerMode = PlayerMode.Single
    ''' <summary>
    ''' 游戏AI
    ''' </summary>
    Public Property AI As GameAI

    Dim ActivePiece As IPiece

    Public Sub New()
        Start()
    End Sub
    ''' <summary>
    ''' 开始
    ''' </summary>
    Public Sub Start(Optional playerMode As PlayerMode = PlayerMode.Single)
        Me.PlayerMode = playerMode
        Map = New Map(5, 9)
        Dim wolfLoc As Vector2() = {New Vector2(2, 2), New Vector2(2, 6)}
        Dim sheepLoc As Vector2() = {New Vector2(1, 3), New Vector2(2, 3), New Vector2(3, 3),
                                     New Vector2(1, 4), New Vector2(3, 4),
                                     New Vector2(1, 5), New Vector2(2, 5), New Vector2(3, 5)}
        For Each SubVec In wolfLoc
            Map.Place(New Wolf With {.Location = SubVec})
        Next
        For Each SubVec In sheepLoc
            Map.Place(New Sheep With {.Location = SubVec})
        Next
        '初始化AI
        If playerMode = PlayerMode.Single Then
            AI = New GameAI()
            If Not Map.ActivedCamp = PlayerCamp Then
                AI.Move(Me)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 下一步
    ''' </summary>
    Public Sub NextStep()
        My.Computer.Audio.Play("Resources/1381.wav") '临时代码
        Map.Exchange()
        RaiseEvent MapChanged(Me, Nothing)
        If PlayerMode = PlayerMode.Single AndAlso Not Map.ActivedCamp = PlayerCamp Then
            AI.Move(Me)
        End If
    End Sub
    ''' <summary>
    ''' 移动
    ''' </summary>
    Public Sub Move(piece As IPiece, loc As Vector2)
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
    ''' 执子方认输
    ''' </summary>
    Public Sub Defeate()
        '临时代码
        If MsgBox(If(Map.ActivedCamp = Camp.Wolf, "狼方", "羊方") + "认输！是否重来一局？", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Start()
        End If
    End Sub

    Public Sub Clicked(loc As Vector2)
        If loc.X >= 0 AndAlso loc.X < Map.Width AndAlso loc.Y >= 0 AndAlso loc.Y < Map.Height Then
            If Map.Locate(loc) Is Nothing Then
                Move(ActivePiece, loc)
                ActivePiece = Nothing
            Else
                ActivePiece = Map.Locate(loc)
            End If
        End If
    End Sub
End Class
