Imports System.Numerics
''' <summary>
''' 棋子
''' </summary>
Public Class Piece
    Implements IPiece
    Public Overridable Property Camp As Camp Implements IPiece.Camp
    Public Property Location As Vector2 Implements IPiece.Location
End Class
