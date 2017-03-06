''' <summary>
''' 棋子接口
''' </summary>
Public Interface IPiece
    ''' <summary>
    ''' 阵营
    ''' </summary>
    ReadOnly Property Camp As Camp
    ''' <summary>
    ''' 位置
    ''' </summary>
    Property Location As VectorInt
    ''' <summary>
    ''' 是否可移动
    ''' </summary>
    Function Moveable(map As Map, loc As VectorInt) As Boolean
End Interface