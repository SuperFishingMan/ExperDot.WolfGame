''' <summary>
'''  为地图改变时的事件提供数据
''' </summary>
Public Class MapChangedEventArgs
    ''' <summary>
    ''' 源地图
    ''' </summary>
    Public Property Source As Map
    ''' <summary>
    ''' 由指定的地图初始化实例
    ''' </summary>
    Public Sub New(map As Map)
        Source = map
    End Sub
End Class
