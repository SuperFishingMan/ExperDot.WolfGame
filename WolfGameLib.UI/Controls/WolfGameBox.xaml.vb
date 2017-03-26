Imports System.Windows.Input
Imports WolfGameLib.UI

Public Class WolfGameBox
    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Pause()
        Me.Focusable = True
        Me.Focus()
    End Sub

    Public Sub Start()
        WolfBoardBox1.IsEnabled = True
        SettingBox1.Visibility = Windows.Visibility.Collapsed
    End Sub
    Public Sub Pause()
        WolfBoardBox1.IsEnabled = False
        SettingBox1.Visibility = Windows.Visibility.Visible
    End Sub
    Private Sub SettingBox1_Confirmed(sender As Object, e As ConfirmedEventArgs) Handles SettingBox1.Confirmed
        WolfBoardBox1.Board.PlayerMode = e.PlayerMode
        WolfBoardBox1.Board.AI.Difficulty = e.DifficultyMode
        WolfBoardBox1.Board.PlayerCamp = e.PlayerCamp
        WolfBoardBox1.Board.Start()
        Start()
    End Sub
    Private Sub WolfBoardBox1_GameOver(sender As Object, e As EventArgs) Handles WolfBoardBox1.GameOver
        Pause()
    End Sub
End Class
