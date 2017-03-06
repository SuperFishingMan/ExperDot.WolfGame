''' <summary>
''' 连通器
''' </summary>
Public Class Joint
    ''' <summary>
    ''' 连通映射表
    ''' </summary>
    Public Property Round As Integer(,)
    ''' <summary>
    ''' 返回自身连通
    ''' </summary>
    Public Function Connected() As Boolean
        Return Round(1, 1) = 1
    End Function
    ''' <summary>
    ''' 返回与相邻位置是否连通
    ''' </summary>
    Public Function Connected(offset As VectorInt) As Boolean
        Return Round(1 + offset.Y, 1 + offset.X) = 1
    End Function
End Class
