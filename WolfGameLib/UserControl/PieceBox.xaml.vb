Public Class PieceBox
    Public Property IsOpposite As Boolean
        Set(value As Boolean)
            m_IsOpposite = value
            If value Then
                Image0.Visibility = System.Windows.Visibility.Collapsed
                Image1.Visibility = System.Windows.Visibility.Visible
            Else
                Image0.Visibility = System.Windows.Visibility.Visible
                Image1.Visibility = System.Windows.Visibility.Collapsed
            End If
        End Set
        Get
            Return m_IsOpposite
        End Get
    End Property

    Private m_IsOpposite As Boolean
End Class
