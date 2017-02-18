Imports System.Numerics
''' <summary>
''' 游戏AI
''' </summary>
Public Class GameAI
    ''' <summary>
    ''' 走棋
    ''' </summary>
    Public Sub Move(board As GameBoard)
        Dim temp = Search(board.Map, 9)
        If temp.Piece Is Nothing Then
            board.Move(Nothing, temp.Target)
        Else
            board.Move(temp.Piece, temp.Piece.Location + temp.Target)
        End If
    End Sub
    ''' <summary>
    ''' 搜索
    ''' </summary>
    Public Function Search(map As Map, depth As Integer) As Movement
        Dim value As Integer = Integer.MinValue
        Dim movement As Movement = Nothing
        For Each SubMovement In CalcMovements(map)
            GoForward(map, SubMovement)
            Dim tempvalue = -NegaMax(map, depth - 1)
            If value < tempvalue Then
                value = tempvalue
                movement = SubMovement
            End If
            GoBack(map, SubMovement)
        Next
        Return movement
    End Function

    ''' <summary>
    ''' 负极大值搜索
    ''' </summary>
    Public Function NegaMax(map As Map, depth As Integer) As Integer
        If (depth <= 0 OrElse Map.CheckVictory(map)) Then
            Return Evaluate(map)
        Else
            Dim value As Integer = Integer.MinValue
            For Each SubMovement In CalcMovements(map)
                GoForward(map, SubMovement)
                value = Math.Max(value, -NegaMax(map, depth - 1))
                GoBack(map, SubMovement)
            Next
            Return value
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
        Static InnerVecs = New Vector2() {New Vector2(-1, -1), New Vector2(0, -1), New Vector2(1, -1), New Vector2(1, 0), New Vector2(1, 1), New Vector2(0, 1), New Vector2(0, -1), New Vector2(-1, 0)}
        Static OuterVecs = New Vector2() {New Vector2(-2, -2), New Vector2(0, -2), New Vector2(2, -2), New Vector2(2, 0), New Vector2(2, 2), New Vector2(0, 2), New Vector2(0, -2), New Vector2(-2, 0)}
        For Each SubPiece In map.Pieces
            If SubPiece?.Camp = Camp.Wolf Then
                For Each SubVec In InnerVecs
                    Dim temp As Vector2 = SubPiece.Location + SubVec
                    If SubPiece.Moveable(map, temp) Then
                        value += 10
                    End If
                Next
                For Each SubVec In OuterVecs
                    Dim temp As Vector2 = SubPiece.Location + SubVec
                    If SubPiece.Moveable(map, temp) Then
                        value += 30
                    End If
                Next
            End If
        Next
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
