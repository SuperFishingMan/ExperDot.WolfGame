Public Class CellBox
    Public Property IsOpposite As Boolean
        Set(value As Boolean)
            m_IsOpposite = value
            If value Then
                Line0.Visibility = System.Windows.Visibility.Collapsed
                Line1.Visibility = System.Windows.Visibility.Visible
            Else
                Line0.Visibility = System.Windows.Visibility.Visible
                Line1.Visibility = System.Windows.Visibility.Collapsed
            End If
        End Set
        Get
            Return m_IsOpposite
        End Get
    End Property
    Private m_IsOpposite As Boolean
End Class
