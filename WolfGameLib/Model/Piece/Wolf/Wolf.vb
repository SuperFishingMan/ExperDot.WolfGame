Imports System.Numerics
Imports WolfGameLib
''' <summary>
''' 狼
''' </summary>
Public Class Wolf
    Inherits PieceBase

    Public Overrides ReadOnly Property Camp As Camp = Camp.Wolf

    Public Overrides Function Move(map As Map, loc As Vector2) As Boolean
        'Dim temp As Vector2 = (loc - Location).AbsNew
        'If map.LocateMapping(loc) Then
        '    If (InnerVectors.Contains(temp) AndAlso map.Locate(loc) Is Nothing) OrElse
        '    ((Location.X + Location.Y) Mod 2 = 0 AndAlso temp = InnerVectorEx) Then
        '        MoveTo(map, Me, loc)
        '        Return True
        '    ElseIf OuterVectors.Contains(temp) OrElse ((Location.X + Location.Y) Mod 2 = 0 AndAlso temp = OuterVectorEx) Then
        '        Dim mid As Vector2 = (loc + Location) / 2
        '        If map.Locate(mid)?.Camp = Camp.Sheep Then
        '            MoveTo(map, Me, loc)
        '            map.Assign(Nothing, mid)
        '            Return True
        '        End If
        '    End If
        'End If
        If map.Joints(loc.X, loc.Y).Connected(Vector2.Zero) Then
            Dim temp As Vector2 = (loc - Location)
            Dim tempAbs As Vector2 = temp.AbsNew
            If InnerVectors.Contains(tempAbs) Then
                If map.GetJoint(Location).Connected(temp) Then
                    MoveTo(map, Me, loc)
                    Return True
                End If
            ElseIf OuterVectors.Contains(tempAbs) Then
                Dim tempPart As Vector2 = temp / 2
                If map.Locate(Location + tempPart)?.Camp = Camp.Sheep Then
                    If map.GetJoint(Location).Connected(tempPart) Then
                        If map.GetJoint(Location + tempPart).Connected(tempPart) Then
                            map.Assign(Nothing, Location + tempPart)
                            MoveTo(map, Me, loc)
                            Return True
                        End If
                    End If
                End If
            End If
        End If
        Return False
    End Function
End Class
