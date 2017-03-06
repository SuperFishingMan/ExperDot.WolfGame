''' <summary>
''' 游戏AI
''' </summary>
Public Class GameAI
    Public Property Difficulty As DifficultyMode = DifficultyMode.Easy
    ''' <summary>
    ''' 走棋
    ''' </summary>
    Public Sub Move(board As GameBoard)
        Dim temp = RootSearch(board.Map, Difficulty)
        If temp Is Nothing Then
            board.Defeate()
        Else
            If temp.Piece Is Nothing Then
                board.Move(Nothing, temp.Target)
            Else
                board.Move(temp.Piece, temp.Piece.Location + temp.Target)
            End If
        End If
    End Sub
    ''' <summary>
    ''' 根搜索
    ''' </summary>
    Public Function RootSearch(map As Map, depth As Integer) As Movement
        Dim value As Integer = Integer.MinValue
        Dim movement As Movement = Nothing
        Dim invert As Integer = If(map.ActivedCamp = Camp.Wolf, 1, -1)
        For Each SubMovement In Map.CalcMovements(map)
            Map.GoForward(map, SubMovement)
            Dim tempvalue = invert * AlphaBeta(map, depth - 1, Integer.MinValue + 10, Integer.MaxValue - 10)
            If value < tempvalue Then
                value = tempvalue
                movement = SubMovement
            End If
            Map.GoBack(map, SubMovement)
        Next
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
            For Each SubMovement In Map.CalcMovements(map)
                Map.GoForward(map, SubMovement)
                value = -AlphaBeta(map, depth - 1, -beta, -alpha)
                Map.GoBack(map, SubMovement)
                If value >= beta Then
                    Return beta
                ElseIf value > alpha Then
                    alpha = value
                End If
            Next
            Return alpha
        End If
    End Function



    ''' <summary>
    ''' 返回局面评估值
    ''' </summary>
    Public Function Evaluate(map As Map) As Integer
        Dim value As Integer = 0
        Dim sheepCount As Integer = map.SheepRemaining
        Dim round As Integer = 0
        Static InnerVecs = New VectorInt() {New VectorInt(-1, -1), New VectorInt(0, -1), New VectorInt(1, -1), New VectorInt(1, 0), New VectorInt(1, 1), New VectorInt(0, 1), New VectorInt(0, -1), New VectorInt(-1, 0)}
        Static OuterVecs = New VectorInt() {New VectorInt(-2, -2), New VectorInt(0, -2), New VectorInt(2, -2), New VectorInt(2, 0), New VectorInt(2, 2), New VectorInt(0, 2), New VectorInt(0, -2), New VectorInt(-2, 0)}
        For Each SubPiece In map.Pieces
            If SubPiece Is Nothing Then Continue For

            If SubPiece.Camp = Camp.Wolf Then
                For Each SubVec In InnerVecs
                    Dim temp As VectorInt = SubPiece.Location + SubVec
                    If SubPiece.Moveable(map, temp) Then
                        value += 1
                        round += 1
                    End If
                Next
                For Each SubVec In OuterVecs
                    Dim temp As VectorInt = SubPiece.Location + SubVec
                    If SubPiece.Moveable(map, temp) Then
                        value += 5
                        round += 1
                    End If
                Next
            ElseIf SubPiece.Camp = Camp.Sheep Then
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
