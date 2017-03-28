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
    ''' 连通器集合
    ''' </summary>
    Public ReadOnly Property Joints As Joint(,)
    ''' <summary>
    ''' 走步集合
    ''' </summary>
    Public ReadOnly Property Movements As List(Of Movement)

    Public Sub New(w As Integer, h As Integer)
        ReDim Pieces(w - 1, h - 1)
        ReDim Joints(4, 8)
        Movements = New List(Of Movement)
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
    ''' 返回指定位置的棋子
    ''' </summary>
    Public Function Locate(loc As VectorInt) As IPiece
        Return Pieces(loc.X, loc.Y)
    End Function
    ''' <summary>
    ''' 返回指定位置的的连通器
    ''' </summary>
    Public Function GetJoint(loc As VectorInt) As Joint
        Return Joints(loc.X, loc.Y)
    End Function
    ''' <summary>
    ''' 返回指定位置是否有效
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
                Movements.Add(New Movement() With {.Piece = piece, .Offset = loc})
            Else
                Return False
            End If
        Else
            Dim offset As VectorInt = (loc - piece.Location)
            If offset.LengthSquared > 2 Then
                Assign(Nothing, piece.Location + offset / 2)
            End If
            MovePiece(piece, loc)
            Movements.Add(New Movement() With {.Piece = piece, .Offset = offset})
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
        If Movements.Count > 0 Then
            Movements.RemoveAt(Movements.Count - 1)
        End If
        Return True
    End Function
    Private Sub Assign(piece As IPiece, loc As VectorInt)
        If piece IsNot Nothing Then piece.Location = loc
        Pieces(loc.X, loc.Y) = piece
    End Sub
    Private Sub MovePiece(piece As IPiece, loc As VectorInt)
        Dim temp As VectorInt = piece.Location
        Assign(piece, loc)
        Assign(Nothing, temp)
    End Sub
    ''' <summary>
    ''' 前进
    ''' </summary>
    Public Shared Sub GoForward(map As Map, movement As Movement)
        If movement.Piece Is Nothing Then
            map.MoveTo(Nothing, movement.Offset)
        Else
            map.MoveTo(movement.Piece, movement.Piece.Location + movement.Offset)
        End If
        map.Exchange()
    End Sub
    ''' <summary>
    ''' 后退
    ''' </summary>
    Public Shared Sub GoBack(map As Map, movement As Movement)
        map.Exchange()
        If movement.Piece Is Nothing Then
            map.MoveToRevert(Nothing, movement.Offset)
        Else
            map.MoveToRevert(movement.Piece, movement.Piece.Location - movement.Offset)
        End If
    End Sub
    ''' <summary>
    ''' 返回所有走法
    ''' </summary>
    Public Shared Function CalcMovements(map As Map) As Movement()
        Static InnerVecs() As VectorInt = New VectorInt() {New VectorInt(-1, -1), New VectorInt(0, -1), New VectorInt(1, -1), New VectorInt(1, 0), New VectorInt(1, 1), New VectorInt(0, 1), New VectorInt(0, -1), New VectorInt(-1, 0)}
        Static OuterVecs() As VectorInt = New VectorInt() {New VectorInt(-1, -1), New VectorInt(0, -1), New VectorInt(1, -1), New VectorInt(1, 0), New VectorInt(1, 1), New VectorInt(0, 1), New VectorInt(0, -1), New VectorInt(-1, 0),
                                                           New VectorInt(-2, -2), New VectorInt(0, -2), New VectorInt(2, -2), New VectorInt(2, 0), New VectorInt(2, 2), New VectorInt(0, 2), New VectorInt(0, -2), New VectorInt(-2, 0)}

        Dim results As New List(Of Movement)
        If map.ActivedCamp = Camp.Wolf Then
            For i = 0 To map.Width - 1
                For j = 0 To map.Height - 1
                    If map.Pieces(i, j)?.Camp = Camp.Wolf Then
                        For k = 0 To OuterVecs.Length - 1
                            Dim temp As VectorInt = map.Pieces(i, j).Location + OuterVecs(k)
                            If map.Pieces(i, j).Moveable(map, temp) Then
                                results.Add(New Movement With {.Piece = map.Pieces(i, j), .Offset = OuterVecs(k)})
                            End If
                        Next
                    End If
                Next
            Next
        ElseIf map.ActivedCamp = Camp.Sheep Then
            If map.SheepRemaining > 0 Then
                For i = 0 To 4
                    For j = 0 To 8
                        Dim temp As VectorInt = New VectorInt(i, j)
                        If map.Locate(temp) Is Nothing AndAlso map.GetJoint(temp).Connected Then
                            results.Add(New Movement With {.Piece = Nothing, .Offset = temp})
                        End If
                    Next
                Next
            Else
                For i = 0 To map.Width - 1
                    For j = 0 To map.Height - 1
                        If map.Pieces(i, j)?.Camp = Camp.Sheep Then
                            For k = 0 To InnerVecs.Length - 1
                                Dim temp As VectorInt = map.Pieces(i, j).Location + InnerVecs(k)
                                If map.Pieces(i, j).Moveable(map, temp) Then
                                    results.Add(New Movement With {.Piece = map.Pieces(i, j), .Offset = InnerVecs(k)})
                                End If
                            Next
                        End If
                    Next
                Next
            End If
        End If
        Return results.ToArray
    End Function
    ''' <summary>
    ''' 返回结束判定
    ''' </summary>
    Public Shared Function CheckGameOver(map As Map) As Boolean
        Static InnerVecs() As VectorInt = New VectorInt() {New VectorInt(-1, -1), New VectorInt(0, -1), New VectorInt(1, -1), New VectorInt(1, 0), New VectorInt(1, 1), New VectorInt(0, 1), New VectorInt(0, -1), New VectorInt(-1, 0)}
        Static OuterVecs() As VectorInt = New VectorInt() {New VectorInt(-1, -1), New VectorInt(0, -1), New VectorInt(1, -1), New VectorInt(1, 0), New VectorInt(1, 1), New VectorInt(0, 1), New VectorInt(0, -1), New VectorInt(-1, 0),
                                                           New VectorInt(-2, -2), New VectorInt(0, -2), New VectorInt(2, -2), New VectorInt(2, 0), New VectorInt(2, 2), New VectorInt(0, 2), New VectorInt(0, -2), New VectorInt(-2, 0)}
        If map.ActivedCamp = Camp.Wolf Then
            For i = 0 To map.Width - 1
                For j = 0 To map.Height - 1
                    If map.Pieces(i, j)?.Camp = Camp.Wolf Then
                        For k = 0 To OuterVecs.Length - 1
                            Dim temp As VectorInt = map.Pieces(i, j).Location + OuterVecs(k)
                            If map.Pieces(i, j).Moveable(map, temp) Then
                                Return False
                            End If
                        Next
                    End If
                Next
            Next
        ElseIf map.ActivedCamp = Camp.Sheep Then
            If map.SheepRemaining > 0 Then
                For i = 0 To 4
                    For j = 0 To 8
                        Dim temp As New VectorInt(i, j)
                        If map.Locate(temp) Is Nothing AndAlso map.GetJoint(temp).Connected Then
                            Return False
                        End If
                    Next
                Next
            Else
                For i = 0 To map.Width - 1
                    For j = 0 To map.Height - 1
                        If map.Pieces(i, j)?.Camp = Camp.Sheep Then
                            For k = 0 To InnerVecs.length - 1
                                Dim temp As VectorInt = map.Pieces(i, j).Location + InnerVecs(k)
                                If map.Pieces(i, j).Moveable(map, temp) Then
                                    Return False
                                End If
                            Next
                        End If
                    Next
                Next
            End If
        End If
        Return True
    End Function

End Class
