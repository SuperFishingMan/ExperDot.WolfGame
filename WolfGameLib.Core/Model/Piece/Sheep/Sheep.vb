''' <summary>
''' 羊
''' </summary>
Public Class Sheep
    Inherits PieceBase

    Public Overrides ReadOnly Property Camp As Camp = Camp.Sheep

    Public Overrides Function Moveable(map As Map, loc As VectorInt) As Boolean
        If map.GetAvailiable(loc) AndAlso map.GetJoint(loc).Connected() AndAlso map.Locate(loc) Is Nothing Then
            Dim temp As VectorInt = (loc - Location)
            If InnerVectors.Contains(temp.AbsNew) Then
                If map.GetJoint(Location).Connected(temp) Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function

End Class
