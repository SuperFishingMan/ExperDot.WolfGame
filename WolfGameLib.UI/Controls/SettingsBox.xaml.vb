Imports System.Windows.Controls
Imports WolfGameLib.Core

Public Class SettingsBox
    ''' <summary>
    ''' 设置确认时发生的事件
    ''' </summary>
    Public Event Confirmed(sender As Object, e As ConfirmedEventArgs)

    Public Property PlayerMode As PlayerMode = PlayerMode.Single
    Public Property DifficultyMode As DifficultyMode = DifficultyMode.Easy
    Public Property PlayerCamp As Camp = Camp.Wolf

    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        comboBox1.ItemsSource = [Enum].GetNames(GetType(PlayerMode))
        comboBox2.ItemsSource = [Enum].GetNames(GetType(DifficultyMode))
        comboBox3.ItemsSource = [Enum].GetNames(GetType(Camp))
        comboBox1.SelectedIndex = 0
        comboBox2.SelectedIndex = 0
        comboBox3.SelectedIndex = 0
    End Sub
    Private Sub button_Click(sender As Object, e As Windows.RoutedEventArgs) Handles button.Click
        RaiseEvent Confirmed(Me, New ConfirmedEventArgs(PlayerMode, DifficultyMode, PlayerCamp))
    End Sub

    Private Sub comboBox1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboBox1.SelectionChanged
        If comboBox1.SelectedIndex >= 0 Then
            If comboBox1.SelectedIndex = 0 Then
                comboBox2.IsEnabled = True
                comboBox3.IsEnabled = True
            Else
                comboBox2.IsEnabled = False
                comboBox3.IsEnabled = False
            End If
            PlayerMode = comboBox1.SelectedIndex
        End If
    End Sub
    Private Sub comboBox2_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboBox2.SelectionChanged
        If comboBox2.SelectedIndex >= 0 Then
            DifficultyMode = comboBox2.SelectedIndex
        End If
    End Sub
    Private Sub comboBox3_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles comboBox3.SelectionChanged
        If comboBox3.SelectedIndex >= 0 Then
            PlayerCamp = comboBox3.SelectedIndex
        End If
    End Sub
End Class

Public Class ConfirmedEventArgs

    Public Property PlayerMode As PlayerMode
    Public Property DifficultyMode As DifficultyMode
    Public Property PlayerCamp As Camp

    Public Sub New(playerMode As PlayerMode, difficultyMode As DifficultyMode, playerCamp As Camp)
        Me.PlayerMode = playerMode
        Me.DifficultyMode = difficultyMode
        Me.PlayerCamp = playerCamp
    End Sub

End Class
