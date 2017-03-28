''' <summary>
''' 游戏AI
''' </summary>
Public Class GameAI
    Public Property Difficulty As DifficultyMode = DifficultyMode.Easy
    Dim count As Integer
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
        If (depth <= 0 OrElse Map.CheckGameOver(map)) Then
            Return Evaluate(map)
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
        Dim sheepCount As Integer = map.SheepRemaining
        Dim round As Integer = 0
        Static InnerVecs() As VectorInt = New VectorInt() {New VectorInt(-1, -1), New VectorInt(0, -1), New VectorInt(1, -1), New VectorInt(1, 0), New VectorInt(1, 1), New VectorInt(0, 1), New VectorInt(0, -1), New VectorInt(-1, 0)}
        Static OuterVecs() As VectorInt = New VectorInt() {New VectorInt(-2, -2), New VectorInt(0, -2), New VectorInt(2, -2), New VectorInt(2, 0), New VectorInt(2, 2), New VectorInt(0, 2), New VectorInt(0, -2), New VectorInt(-2, 0)}
        Dim subPiece As IPiece
        For i = 0 To map.Pieces.Length - 1
            subPiece = map.Pieces(i \ 9, i Mod 9)
            If subPiece Is Nothing Then Continue For
            If subPiece.Camp = Camp.Wolf Then
                For k = 0 To InnerVecs.Length - 1
                    Dim temp As VectorInt = subPiece.Location + InnerVecs(k)
                    If subPiece.Moveable(map, temp) Then
                        value += 5
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
            End If
        Next
        If round = 0 Then
            value = -100000000
        Else
            value -= sheepCount * 10 * Math.Log(sheepCount)
        End If

        Return value
    End Function

End Class
