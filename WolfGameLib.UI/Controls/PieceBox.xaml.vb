Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Effects

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

    Public Property IsScale As Boolean
        Set(value As Boolean)
            If value Then
                Grid0.RenderTransform = New ScaleTransform(0.5F, 0.5F)
            Else
                Grid0.RenderTransform = New ScaleTransform(1.0F, 1.0F)
            End If
        End Set
        Get
            Return m_IsScale
        End Get
    End Property

    Private m_IsOpposite As Boolean
    Private m_IsScale As Boolean

    Private Sub Grid1_MouseEnter(sender As Object, e As MouseEventArgs) Handles Grid1.MouseEnter
        Grid1.Effect = New DropShadowEffect With {.ShadowDepth = 1}
    End Sub

    Private Sub Grid1_MouseLeave(sender As Object, e As MouseEventArgs) Handles Grid1.MouseLeave
        Grid1.Effect = Nothing
    End Sub
End Class
