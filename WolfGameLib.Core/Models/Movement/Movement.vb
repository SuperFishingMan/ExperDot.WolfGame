''' <summary>
''' 走步
''' </summary>
Public Structure Movement
    ''' <summary>
    ''' 目标棋子
    ''' </summary>
    Public Property Piece As IPiece
    ''' <summary>
    ''' 目标位置相对当前位置的偏移量
    ''' </summary>
    Public Property Offset As VectorInt
End Structure
