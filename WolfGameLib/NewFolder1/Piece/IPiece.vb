Imports System.Numerics
Imports WolfGameLib
''' <summary>
''' 棋子接口
''' </summary>
Public Interface IPiece
    ''' <summary>
    ''' 阵营
    ''' </summary>
    Property Camp As Camp
    ''' <summary>
    ''' 位置
    ''' </summary>
    Property Location As Vector2
End Interface
