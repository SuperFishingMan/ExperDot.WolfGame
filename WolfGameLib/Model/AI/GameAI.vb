Imports System.Numerics
''' <summary>
''' 游戏AI
''' </summary>
Public Class GameAI
    ''' <summary>
    ''' 走棋
    ''' </summary>
    Public Sub Move(board As GameBoard)
        Dim extra As Integer = If(board.Map.ActivedCamp = Camp.Wolf, 1, 0)
        Dim temp = RootSearch(board.Map, 6 + extra)
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
        For Each SubMovement In CalcMovements(map)
            GoForward(map, SubMovement)
            Dim tempvalue = invert * AlphaBeta(map, depth - 1, Integer.MinValue + 10, Integer.MaxValue - 10)
            If value < tempvalue Then
                value = tempvalue
                movement = SubMovement
            End If
            GoBack(map, SubMovement)
        Next
        Return movement
    End Function

    ''' <summary>
    ''' AlphaBeta搜索
    ''' </summary>
    Public Function AlphaBeta(map As Map, depth As Integer, alpha As Integer, beta As Integer) As Integer
        If (depth <= 0 OrElse Map.CheckVictory(map)) Then
            Return Evaluate(map)
        Else
            Dim value As Integer = Integer.MinValue
            For Each SubMovement In CalcMovements(map)
                GoForward(map, SubMovement)
                value = -AlphaBeta(map, depth - 1, -beta, -alpha)
                GoBack(map, SubMovement)
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
    ''' 前进
    ''' </summary>
    Public Sub GoForward(map As Map, movement As Movement)
        If movement.Piece Is Nothing Then
            map.MoveTo(Nothing, movement.Target)
        Else
            map.MoveTo(movement.Piece, movement.Piece.Location + movement.Target)
        End If
        map.Exchange()
    End Sub
    ''' <summary>
    ''' 后退
    ''' </summary>
    Public Sub GoBack(map As Map, movement As Movement)
        map.Exchange()
        If movement.Piece Is Nothing Then
            map.MoveToRevert(Nothing, movement.Target)
        Else
            map.MoveToRevert(movement.Piece, movement.Piece.Location - movement.Target)
        End If
    End Sub

    ''' <summary>
    ''' 局面评价
    ''' </summary>
    Public Function Evaluate(map As Map) As Integer
        Dim value As Integer = 0
        Dim sheepCount As Integer = map.SheepRemaining
        Dim round As Integer = 0
        Static InnerVecs = New Vector2() {New Vector2(-1, -1), New Vector2(0, -1), New Vector2(1, -1), New Vector2(1, 0), New Vector2(1, 1), New Vector2(0, 1), New Vector2(0, -1), New Vector2(-1, 0)}
        Static OuterVecs = New Vector2() {New Vector2(-2, -2), New Vector2(0, -2), New Vector2(2, -2), New Vector2(2, 0), New Vector2(2, 2), New Vector2(0, 2), New Vector2(0, -2), New Vector2(-2, 0)}
        For Each SubPiece In map.Pieces
            If SubPiece Is Nothing Then Continue For

            If SubPiece.Camp = Camp.Wolf Then
                For Each SubVec In InnerVecs
                    Dim temp As Vector2 = SubPiece.Location + SubVec
                    If SubPiece.Moveable(map, temp) Then
                        value += 1
                        round += 1
                    End If
                Next
                For Each SubVec In OuterVecs
                    Dim temp As Vector2 = SubPiece.Location + SubVec
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
            value -= sheepCount * 500 '* Math.Log(sheepCount)
        End If

        Return value
    End Function


    Public Function CalcMovements(map As Map) As Movement()
        Static InnerVecs = New Vector2() {New Vector2(-1, -1), New Vector2(0, -1), New Vector2(1, -1), New Vector2(1, 0), New Vector2(1, 1), New Vector2(0, 1), New Vector2(0, -1), New Vector2(-1, 0)}
        Static OuterVecs = New Vector2() {New Vector2(-1, -1), New Vector2(0, -1), New Vector2(1, -1), New Vector2(1, 0), New Vector2(1, 1), New Vector2(0, 1), New Vector2(0, -1), New Vector2(-1, 0),
                                          New Vector2(-2, -2), New Vector2(0, -2), New Vector2(2, -2), New Vector2(2, 0), New Vector2(2, 2), New Vector2(0, 2), New Vector2(0, -2), New Vector2(-2, 0)}

        Dim movements As New List(Of Movement)
        If map.ActivedCamp = Camp.Wolf Then
            For Each SubPiece In map.Pieces
                If SubPiece?.Camp = Camp.Wolf Then
                    For Each SubVec In OuterVecs
                        Dim temp As Vector2 = SubPiece.Location + SubVec
                        If SubPiece.Moveable(map, temp) Then
                            movements.Add(New Movement With {.Piece = SubPiece, .Target = SubVec})
                        End If
                    Next
                End If
            Next
        ElseIf map.ActivedCamp = Camp.Sheep Then
            If map.SheepRemaining > 0 Then
                For i = 0 To 4
                    For j = 0 To 8
                        Dim temp As Vector2 = New Vector2(i, j)
                        If map.Locate(temp) Is Nothing AndAlso map.GetJoint(temp).Connected Then
                            movements.Add(New Movement With {.Piece = Nothing, .Target = temp})
                        End If
                    Next
                Next
            Else
                For Each SubPiece In map.Pieces
                    If SubPiece?.Camp = Camp.Sheep Then
                        For Each SubVec In InnerVecs
                            Dim temp As Vector2 = SubPiece.Location + SubVec
                            If SubPiece.Moveable(map, temp) Then
                                movements.Add(New Movement With {.Piece = SubPiece, .Target = SubVec})
                            End If
                        Next
                    End If
                Next
            End If
        End If
        Return movements.ToArray
    End Function
End Class
