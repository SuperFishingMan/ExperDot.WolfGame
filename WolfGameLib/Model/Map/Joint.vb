Imports System.Numerics
''' <summary>
''' 连通器
''' </summary>
Public Class Joint
    ''' <summary>
    ''' 连通映射表
    ''' </summary>
    Public Property Round As Integer(,)

    Public Function Connected(offset As Vector2) As Boolean
        If Round(1 + offset.Y, 1 + offset.X) = 1 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
