Imports System.Numerics
''' <summary>
''' 连通器
''' </summary>
Public Class Joint
    ''' <summary>
    ''' 连通映射表
    ''' </summary>
    Public Property Round As Integer(,)

    Public Function Connected(Optional offset As Vector2 = Nothing) As Boolean
        If offset = Nothing Then
            offset = Vector2.Zero
        End If
        Return Round(1 + offset.Y, 1 + offset.X) = 1
    End Function
End Class
