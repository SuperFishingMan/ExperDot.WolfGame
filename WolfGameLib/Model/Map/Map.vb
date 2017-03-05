Imports System.Numerics
''' <summary>
''' 地图
''' </summary>
Public Class Map
    ''' <summary>
    ''' 棋子集合
    ''' </summary>
    Public Property Pieces As IPiece(,)
    ''' <summary>
    ''' 羊只闲置数量
    ''' </summary>
    Public Property SheepRemaining As Integer = 16
    ''' <summary>
    ''' 当前阵营
    ''' </summary>
    Public Property ActivedCamp As Camp = Camp.Wolf
    ''' <summary>
    ''' 宽度
    ''' </summary>
    Public ReadOnly Property Width As Integer
        Get
            Return Pieces.GetUpperBound(0) + 1
        End Get
    End Property
    ''' <summary>
    ''' 高度
    ''' </summary>
    Public ReadOnly Property Height As Integer
        Get
            Return Pieces.GetUpperBound(1) + 1
        End Get
    End Property
    ''' <summary>
    ''' 映射
    ''' </summary>
    Public ReadOnly Property Mapping As Integer(,) = New Integer(,) {{0, 0, 1, 0, 0}, {0, 1, 1, 1, 0}, {1, 1, 1, 1, 1}, {1, 1, 1, 1, 1}, {1, 1, 1, 1, 1}, {1, 1, 1, 1, 1}, {1, 1, 1, 1, 1}, {0, 1, 1, 1, 0}, {0, 0, 1, 0, 0}}
    ''' <summary>
    ''' 连通器集
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Joints As Joint(,)
    Public Sub New(w As Integer, h As Integer)
        ReDim Pieces(w - 1, h - 1)
        ReDim Joints(4, 8)
        '0
        Joints(0, 0) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}}}
        Joints(1, 0) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}}}
        Joints(2, 0) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {0, 1, 0}, {1, 1, 1}}}
        Joints(3, 0) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}}}
        Joints(4, 0) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}}}
        '1
        Joints(0, 1) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}}}
        Joints(1, 1) = New Joint With {.Round = New Integer(,) {{0, 0, 1}, {0, 1, 1}, {0, 0, 1}}}
        Joints(2, 1) = New Joint With {.Round = New Integer(,) {{0, 1, 0}, {1, 1, 1}, {0, 1, 0}}}
        Joints(3, 1) = New Joint With {.Round = New Integer(,) {{1, 0, 0}, {1, 1, 0}, {1, 0, 0}}}
        Joints(4, 1) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}}}
        '2
        Joints(0, 2) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {0, 1, 1}, {0, 1, 1}}}
        Joints(1, 2) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {1, 1, 1}, {0, 1, 0}}}
        Joints(2, 2) = New Joint With {.Round = New Integer(,) {{1, 1, 1}, {1, 1, 1}, {1, 1, 1}}}
        Joints(3, 2) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {1, 1, 1}, {0, 1, 0}}}
        Joints(4, 2) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {1, 1, 0}, {1, 1, 0}}}
        '3
        Joints(0, 3) = New Joint With {.Round = New Integer(,) {{0, 1, 0}, {0, 1, 1}, {0, 1, 0}}}
        Joints(1, 3) = New Joint With {.Round = New Integer(,) {{1, 1, 1}, {1, 1, 1}, {1, 1, 1}}}
        Joints(2, 3) = New Joint With {.Round = New Integer(,) {{0, 1, 0}, {1, 1, 1}, {0, 1, 0}}}
        Joints(3, 3) = New Joint With {.Round = New Integer(,) {{1, 1, 1}, {1, 1, 1}, {1, 1, 1}}}
        Joints(4, 3) = New Joint With {.Round = New Integer(,) {{0, 1, 0}, {1, 1, 0}, {0, 1, 0}}}
        '4
        Joints(0, 4) = New Joint With {.Round = New Integer(,) {{0, 1, 1}, {0, 1, 1}, {0, 1, 1}}}
        Joints(1, 4) = New Joint With {.Round = New Integer(,) {{0, 1, 0}, {1, 1, 1}, {0, 1, 0}}}
        Joints(2, 4) = New Joint With {.Round = New Integer(,) {{1, 1, 1}, {1, 1, 1}, {1, 1, 1}}}
        Joints(3, 4) = New Joint With {.Round = New Integer(,) {{0, 1, 0}, {1, 1, 1}, {0, 1, 0}}}
        Joints(4, 4) = New Joint With {.Round = New Integer(,) {{1, 1, 0}, {1, 1, 0}, {1, 1, 0}}}
        '5
        Joints(0, 5) = New Joint With {.Round = New Integer(,) {{0, 1, 0}, {0, 1, 1}, {0, 1, 0}}}
        Joints(1, 5) = New Joint With {.Round = New Integer(,) {{1, 1, 1}, {1, 1, 1}, {1, 1, 1}}}
        Joints(2, 5) = New Joint With {.Round = New Integer(,) {{0, 1, 0}, {1, 1, 1}, {0, 1, 0}}}
        Joints(3, 5) = New Joint With {.Round = New Integer(,) {{1, 1, 1}, {1, 1, 1}, {1, 1, 1}}}
        Joints(4, 5) = New Joint With {.Round = New Integer(,) {{0, 1, 0}, {1, 1, 0}, {0, 1, 0}}}
        '6
        Joints(0, 6) = New Joint With {.Round = New Integer(,) {{0, 1, 1}, {0, 1, 1}, {0, 0, 0}}}
        Joints(1, 6) = New Joint With {.Round = New Integer(,) {{0, 1, 0}, {1, 1, 1}, {0, 0, 0}}}
        Joints(2, 6) = New Joint With {.Round = New Integer(,) {{1, 1, 1}, {1, 1, 1}, {1, 1, 1}}}
        Joints(3, 6) = New Joint With {.Round = New Integer(,) {{0, 1, 0}, {1, 1, 1}, {0, 0, 0}}}
        Joints(4, 6) = New Joint With {.Round = New Integer(,) {{1, 1, 0}, {1, 1, 0}, {0, 0, 0}}}
        '7
        Joints(0, 7) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}}}
        Joints(1, 7) = New Joint With {.Round = New Integer(,) {{0, 0, 1}, {0, 1, 1}, {0, 0, 1}}}
        Joints(2, 7) = New Joint With {.Round = New Integer(,) {{0, 1, 0}, {1, 1, 1}, {0, 1, 0}}}
        Joints(3, 7) = New Joint With {.Round = New Integer(,) {{1, 0, 0}, {1, 1, 0}, {1, 0, 0}}}
        Joints(4, 7) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}}}
        '8
        Joints(0, 8) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}}}
        Joints(1, 8) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}}}
        Joints(2, 8) = New Joint With {.Round = New Integer(,) {{1, 1, 1}, {0, 1, 0}, {0, 0, 0}}}
        Joints(3, 8) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}}}
        Joints(4, 8) = New Joint With {.Round = New Integer(,) {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}}}
    End Sub
    ''' <summary>
    ''' 交换阵营
    ''' </summary>
    Public Sub Exchange()
        ActivedCamp = If(ActivedCamp = Camp.Wolf, Camp.Sheep, Camp.Wolf)
    End Sub
    ''' <summary>
    ''' 放置
    ''' </summary>
    Public Sub Place(piece As IPiece)
        Pieces(piece.Location.X, piece.Location.Y) = piece
    End Sub
    ''' <summary>
    ''' 定位
    ''' </summary>
    Public Function Locate(loc As VectorInt) As IPiece
        Return Pieces(loc.X, loc.Y)
    End Function
    ''' <summary>
    ''' 赋值
    ''' </summary>
    Public Sub Assign(piece As IPiece, loc As VectorInt)
        If piece IsNot Nothing Then piece.Location = loc
        Pieces(loc.X, loc.Y) = piece
    End Sub
    ''' <summary>
    ''' 返回指定位置的的连通器
    ''' </summary>
    Public Function GetJoint(loc As VectorInt) As Joint
        Return Joints(loc.X, loc.Y)
    End Function
    ''' <summary>
    ''' 是否有效
    ''' </summary>
    Public Function GetAvailiable(loc As VectorInt)
        If loc.X >= 0 AndAlso loc.X < Width AndAlso loc.Y >= 0 AndAlso loc.Y < Height Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' 移动
    ''' </summary>
    Public Function MoveTo(piece As IPiece, loc As VectorInt) As Boolean
        If piece Is Nothing Then
            If ActivedCamp = Camp.Sheep AndAlso SheepRemaining > 0 AndAlso GetJoint(loc).Connected Then
                Assign(New Sheep, loc)
                SheepRemaining -= 1
            Else
                Return False
            End If
        Else
            Dim temp As VectorInt = (loc - piece.Location)
            If temp.LengthSquared <= 2 Then
                MovePiece(piece, loc)
            Else
                Assign(Nothing, piece.Location + temp / 2)
                MovePiece(piece, loc)
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' 移动还原
    ''' </summary>
    Public Function MoveToRevert(piece As IPiece, loc As VectorInt) As Boolean
        If piece Is Nothing Then
            Assign(Nothing, loc)
            SheepRemaining += 1
        Else
            Dim temp As VectorInt = (loc - piece.Location)
            If temp.LengthSquared <= 2 Then
                MovePiece(piece, loc)
            Else
                Assign(New Sheep, piece.Location + temp / 2)
                MovePiece(piece, loc)
            End If
        End If
        Return True
    End Function
    Private Sub MovePiece(piece As IPiece, loc As VectorInt)
        Dim temp As VectorInt = piece.Location
        Assign(piece, loc)
        Assign(Nothing, temp)
    End Sub
    ''' <summary>
    ''' 胜负判定
    ''' </summary>
    Public Shared Function CheckVictory(map As Map) As Boolean
        Return True
    End Function

End Class
