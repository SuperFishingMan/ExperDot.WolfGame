''' <summary>
''' 游戏AI
''' </summary>
Public Class GameAI
    Public Property Difficulty As DifficultyMode = DifficultyMode.Easy
    Dim count As Integer

    Private PositionValue(,) As Integer = New Integer(,) {{0, 0, 10, 0, 0},
                                                          {0, 10, 5, 10, 0},
                                                          {40, 30, 10, 30, 40},
                                                          {60, 20, 40, 20, 60},
                                                          {50, 40, 100, 40, 50},
                                                          {60, 20, 40, 20, 60},
                                                          {40, 30, 10, 30, 40},
                                                          {0, 10, 5, 10, 0},
                                                          {0, 0, 10, 0, 0}}
    ''' <summary>
    ''' 走棋
    ''' </summary>
    Public Sub Move(board As GameBoard)
        count = 0
        Dim stopwatch As New Stopwatch
        stopwatch.Start()
        Dim temp = RootSearch(board.Map, Difficulty * 2 + 3)
        If temp.Offset.X = -10 Then
            board.Defeate()
        Else
            If temp.Piece Is Nothing Then
                board.Move(Nothing, temp.Offset)
            Else
                board.Move(temp.Piece, temp.Piece.Location + temp.Offset)
            End If
        End If
        stopwatch.Stop()
        Debug.WriteLine($"第{board.Map.Movements.Count}步,搜索量:{count},用时:{stopwatch.ElapsedMilliseconds}ms")
    End Sub
    ''' <summary>
    ''' 根搜索
    ''' </summary>
    Public Function RootSearch(map As Map, depth As Integer) As Movement
        Dim value As Integer = Integer.MinValue
        Dim movement As New Movement With {.Offset = New VectorInt(-10, 0)}
        Dim invert As Integer = If(map.ActivedCamp = Camp.Wolf, 1, -1)
        Dim movements = Map.CalcMovements(map)
        If movements.Length > 0 Then
            For i = 0 To movements.Count - 1
                Map.GoForward(map, movements(i))
                Dim tempvalue = invert * AlphaBeta(map, depth - 1, Integer.MinValue + 10, Integer.MaxValue - 10)
                If value < tempvalue Then
                    value = tempvalue
                    movement = movements(i)
                End If
                Map.GoBack(map, movements(i))
            Next
        End If
        Return movement
    End Function

    ''' <summary>
    ''' AlphaBeta搜索
    ''' </summary>
    Public Function AlphaBeta(map As Map, depth As Integer, alpha As Integer, beta As Integer) As Integer
        If depth <= 0 Then
            Return Evaluate(map)
        ElseIf Map.CheckGameOver(map) Then
            Return -10000000
        Else
            Dim value As Integer = Integer.MinValue
            Dim movements = Map.CalcMovements(map)
            If movements.Length > 0 Then
                For i = 0 To movements.Count - 1
                    Map.GoForward(map, movements(i))
                    value = -AlphaBeta(map, depth - 1, -beta, -alpha)
                    Map.GoBack(map, movements(i))
                    If value >= beta Then
                        Return beta
                    ElseIf value > alpha Then
                        alpha = value
                    End If
                Next
            End If
            Return alpha
        End If
    End Function
    ''' <summary>
    ''' 返回局面评估值
    ''' </summary>
    Public Function Evaluate(map As Map) As Integer
        count += 1
        Dim value As Integer = 0
        Dim sheepCount As Integer = 0
        Dim round As Integer = 0
        Dim realPosValue = GetRealPositionValue(map)
        Static InnerVecs() As VectorInt = New VectorInt() {New VectorInt(-1, -1), New VectorInt(0, -1), New VectorInt(1, -1), New VectorInt(1, 0), New VectorInt(1, 1), New VectorInt(0, 1), New VectorInt(-1, 1), New VectorInt(-1, 0)}
        Static OuterVecs() As VectorInt = New VectorInt() {New VectorInt(-2, -2), New VectorInt(0, -2), New VectorInt(2, -2), New VectorInt(2, 0), New VectorInt(2, 2), New VectorInt(0, 2), New VectorInt(-2, 2), New VectorInt(-2, 0)}
        Dim subPiece As IPiece
        For i = 0 To map.Pieces.Length - 1
            subPiece = map.Pieces(i \ 9, i Mod 9)
            If subPiece Is Nothing Then Continue For
            If subPiece.Camp = Camp.Wolf Then
                For k = 0 To InnerVecs.Length - 1
                    Dim temp As VectorInt = subPiece.Location + InnerVecs(k)
                    If subPiece.Moveable(map, temp) Then
                        round += 1
                    End If
                Next
                For k = 0 To OuterVecs.Length - 1
                    Dim temp As VectorInt = subPiece.Location + OuterVecs(k)
                    If subPiece.Moveable(map, temp) Then
                        value += 100
                        round += 1
                    End If
                Next
            ElseIf subPiece.Camp = Camp.Sheep Then
                sheepCount += 1
                'value -= realPosValue(subPiece.Location.Y, subPiece.Location.X) / 10
            End If
        Next
        If round = 0 Then
            value = -10000000
        Else
            value -= map.SheepRemaining * 1000 * Math.Log(10 + map.SheepRemaining)
            value -= sheepCount * 300 * Math.Log(10 + sheepCount)
        End If
        Return value
    End Function

    ''' <summary>
    ''' 返回位置评估值
    ''' </summary>
    Public Function GetRealPositionValue(map As Map) As Integer(,)
        Dim result(8, 4) As Integer
        For x = 0 To 4
            For y = 0 To 8
                result(y, x) = PositionValue(y, x)
            Next
        Next
        Dim subPiece As IPiece
        For i = 0 To map.Pieces.Length - 1
            subPiece = map.Pieces(i \ 9, i Mod 9)
            If subPiece Is Nothing Then Continue For
            If subPiece.Camp = Camp.Wolf Then
                For x = 0 To 4
                    For y = 0 To 8
                        Dim temp = New VectorInt(x, y) - subPiece.Location
                        result(y, x) += 20 / (Math.Abs(temp.X) + Math.Abs(temp.Y) + 1)
                    Next
                Next
            End If
        Next
        Return result
    End Function

End Class
