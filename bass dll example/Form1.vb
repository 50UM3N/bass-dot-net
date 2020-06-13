Imports Un4seen.Bass
Public Class Form1
    Public par As New BASS_DX8_PARAMEQ
    Public fx As Integer() = New Integer(4 - 0) {}
    Public chan As Integer

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, Me.Handle)
        Un4seen.Bass.BassNet.Registration("sdsadasd@gmail.com", "b88d1754d700e49a")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        OpenFileDialog1.Filter = "MediaFiles(*.wav;*.mp3)|*.wav;*.mp3"
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            If CBool(chan) Then Bass.BASS_StreamFree(chan)
            chan = Bass.BASS_StreamCreateFile(OpenFileDialog1.FileName, 0, 0, BASSFlag.BASS_DEFAULT)
            CheckBox1_CheckedChanged(sender, e)
            Bass.BASS_ChannelPlay(chan, False)
        End If
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        BASS_SetParametersEQ(fx(0), 100, TrackBar1.Value)
    End Sub

    Private Sub TrackBar2_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar2.Scroll
        BASS_SetParametersEQ(fx(1), 500, TrackBar2.Value)
    End Sub

    Private Sub TrackBar3_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar3.Scroll
        BASS_SetParametersEQ(fx(2), 1000, TrackBar3.Value)
    End Sub

    Private Sub TrackBar4_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar4.Scroll
        BASS_SetParametersEQ(fx(3), 4000, TrackBar4.Value)
    End Sub

    Private Sub TrackBar5_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar5.Scroll
        BASS_SetParametersEQ(fx(4), 8000, TrackBar5.Value)
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            For i As Integer = 0 To 4
                fx(i) = Bass.BASS_ChannelSetFX(chan, BASSFXType.BASS_FX_DX8_PARAMEQ, 0)
            Next i
            BASS_UpdateParamsEQ(sender, e)
        Else
            For x As Integer = 0 To 4
                Bass.BASS_ChannelRemoveFX(chan, fx(x))
            Next x
        End If
    End Sub

    Public Function BASS_SetParametersEQ(ByVal fx As Integer, ByVal center As Integer, ByVal gain As Integer) As Boolean
        par.fBandwidth = 18.0!
        par.fCenter = CSng(center)
        par.fGain = CSng(gain)
        Return Bass.BASS_FXSetParameters(fx, par)
    End Function

    Public Sub BASS_UpdateParamsEQ(ByVal sender As Object, ByVal e As EventArgs)
        TrackBar1_Scroll(sender, e)
        TrackBar2_Scroll(sender, e)
        TrackBar3_Scroll(sender, e)
        TrackBar4_Scroll(sender, e)
        TrackBar5_Scroll(sender, e)
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Bass.BASS_Free()
    End Sub
End Class
