''' <summary>
''' 整型向量
''' </summary>
Public Structure VectorInt
    Public X As Integer
    Public Y As Integer

    Public Shared ReadOnly Property Zero As VectorInt = New VectorInt(0, 0)

    Public Sub New(Optional x As Integer = 0, Optional y As Integer = 0)
        Me.X = x
        Me.Y = y
    End Sub

    Public Sub Abs()
        X = Math.Abs(X)
        Y = Math.Abs(Y)
    End Sub
    Public Function AbsNew() As VectorInt
        Return New VectorInt(Math.Abs(X), Math.Abs(Y))
    End Function
    Public Function LengthSquared() As Integer
        Return X * X + Y * Y
    End Function

    Public Shared Operator +(left As VectorInt, right As VectorInt) As VectorInt
        Return New VectorInt(left.X + right.X, left.Y + right.Y)
    End Operator
    Public Shared Operator -(value As VectorInt) As VectorInt
        Return New VectorInt(-value.X, -value.Y)
    End Operator
    Public Shared Operator -(left As VectorInt, right As VectorInt) As VectorInt
        Return New VectorInt(left.X - right.X, left.Y - right.Y)
    End Operator


    Public Shared Operator *(left As VectorInt, right As Single) As VectorInt
        Return New VectorInt(left.X * right, left.Y * right)
    End Operator

    Public Shared Operator *(left As VectorInt, right As VectorInt) As VectorInt
        Throw New NotImplementedException()
    End Operator

    Public Shared Operator *(left As Single, right As VectorInt) As VectorInt
        Return New VectorInt(left * right.X, left * right.Y)
    End Operator

    Public Shared Operator /(left As VectorInt, right As Single) As VectorInt
        Return New VectorInt(left.X / right, left.Y / right)
    End Operator

    Public Shared Operator /(left As VectorInt, right As VectorInt) As VectorInt
        Throw New NotImplementedException()
    End Operator

    Public Shared Operator =(left As VectorInt, right As VectorInt) As Boolean
        Return left.X = left.Y AndAlso left.Y = right.Y
    End Operator

    Public Shared Operator <>(left As VectorInt, right As VectorInt) As Boolean
        Return Not (left.X = left.Y AndAlso left.Y = right.Y)
    End Operator
End Structure
