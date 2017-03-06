''' <summary>
''' 为游戏结束时的事件提供数据
''' </summary>
Public Class GameOverEventArgs
    ''' <summary>
    ''' 源地图
    ''' </summary>
    Public Property Source As Map
    ''' <summary>
    ''' 胜利方
    ''' </summary>
    Public Property Winner As Camp
    ''' <summary>
    ''' 由指定的地图和胜利阵营初始化实例
    ''' </summary>
    Public Sub New(map As Map, camp As Camp)
        Source = map
        Winner = camp
    End Sub
End Class
