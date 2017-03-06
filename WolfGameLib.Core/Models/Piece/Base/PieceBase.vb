''' <summary>
''' 棋子
''' </summary>
Public MustInherit Class PieceBase
    Implements IPiece

    Public Overridable ReadOnly Property Camp As Camp Implements IPiece.Camp
    Public Property Location As VectorInt Implements IPiece.Location

    Public Shared InnerVectors As VectorInt() = New VectorInt() {New VectorInt(0, 1), New VectorInt(1, 0), New VectorInt(1, 1)}
    Public Shared OuterVectors As VectorInt() = New VectorInt() {New VectorInt(0, 2), New VectorInt(2, 0), New VectorInt(2, 2)}

    Public MustOverride Function Moveable(map As Map, loc As VectorInt) As Boolean Implements IPiece.Moveable

End Class
