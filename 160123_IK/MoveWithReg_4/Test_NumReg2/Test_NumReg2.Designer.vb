<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Test_NumReg2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtRegValue = New System.Windows.Forms.TextBox()
        Me.cmdConnect = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtHostName = New System.Windows.Forms.TextBox()
        Me.outbox = New System.Windows.Forms.TextBox()
        Me.ibox = New System.Windows.Forms.TextBox()
        Me.rbox = New System.Windows.Forms.TextBox()
        Me.zbox = New System.Windows.Forms.TextBox()
        Me.picbox = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ybox = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.xbox = New System.Windows.Forms.TextBox()
        CType(Me.picbox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(185, 146)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(70, 20)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "SEND"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(36, 146)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "R[1]"
        '
        'txtRegValue
        '
        Me.txtRegValue.Location = New System.Drawing.Point(69, 146)
        Me.txtRegValue.Name = "txtRegValue"
        Me.txtRegValue.Size = New System.Drawing.Size(110, 20)
        Me.txtRegValue.TabIndex = 9
        Me.txtRegValue.Text = "Not Connected"
        '
        'cmdConnect
        '
        Me.cmdConnect.Location = New System.Drawing.Point(176, 64)
        Me.cmdConnect.Name = "cmdConnect"
        Me.cmdConnect.Size = New System.Drawing.Size(79, 35)
        Me.cmdConnect.TabIndex = 8
        Me.cmdConnect.Text = "Connect"
        Me.cmdConnect.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(53, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Robot"
        '
        'txtHostName
        '
        Me.txtHostName.Location = New System.Drawing.Point(30, 80)
        Me.txtHostName.Name = "txtHostName"
        Me.txtHostName.Size = New System.Drawing.Size(87, 20)
        Me.txtHostName.TabIndex = 6
        Me.txtHostName.Text = "192.168.0.3"
        '
        'outbox
        '
        Me.outbox.Location = New System.Drawing.Point(277, 12)
        Me.outbox.Multiline = True
        Me.outbox.Name = "outbox"
        Me.outbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.outbox.Size = New System.Drawing.Size(265, 457)
        Me.outbox.TabIndex = 12
        '
        'ibox
        '
        Me.ibox.Location = New System.Drawing.Point(118, 330)
        Me.ibox.Name = "ibox"
        Me.ibox.Size = New System.Drawing.Size(137, 20)
        Me.ibox.TabIndex = 13
        Me.ibox.Text = "6"
        '
        'rbox
        '
        Me.rbox.Location = New System.Drawing.Point(118, 293)
        Me.rbox.Name = "rbox"
        Me.rbox.Size = New System.Drawing.Size(137, 20)
        Me.rbox.TabIndex = 14
        Me.rbox.Text = "110"
        '
        'zbox
        '
        Me.zbox.Location = New System.Drawing.Point(118, 256)
        Me.zbox.Name = "zbox"
        Me.zbox.Size = New System.Drawing.Size(137, 20)
        Me.zbox.TabIndex = 15
        Me.zbox.Text = "170"
        '
        'picbox
        '
        Me.picbox.Location = New System.Drawing.Point(582, 64)
        Me.picbox.Name = "picbox"
        Me.picbox.Size = New System.Drawing.Size(380, 334)
        Me.picbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picbox.TabIndex = 16
        Me.picbox.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(36, 259)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Z Offset"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(36, 300)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Radius"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(36, 333)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 13)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "# of People"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(36, 224)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 13)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Y Offset"
        '
        'ybox
        '
        Me.ybox.Location = New System.Drawing.Point(118, 221)
        Me.ybox.Name = "ybox"
        Me.ybox.Size = New System.Drawing.Size(137, 20)
        Me.ybox.TabIndex = 20
        Me.ybox.Text = "10"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(36, 188)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "X Offset"
        '
        'xbox
        '
        Me.xbox.Location = New System.Drawing.Point(118, 185)
        Me.xbox.Name = "xbox"
        Me.xbox.Size = New System.Drawing.Size(137, 20)
        Me.xbox.TabIndex = 22
        Me.xbox.Text = "380"
        '
        'Test_NumReg2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(983, 481)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.xbox)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ybox)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.picbox)
        Me.Controls.Add(Me.zbox)
        Me.Controls.Add(Me.rbox)
        Me.Controls.Add(Me.ibox)
        Me.Controls.Add(Me.outbox)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtRegValue)
        Me.Controls.Add(Me.cmdConnect)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtHostName)
        Me.Name = "Test_NumReg2"
        Me.Text = "Test_NumReg2"
        CType(Me.picbox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtRegValue As System.Windows.Forms.TextBox
    Friend WithEvents cmdConnect As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtHostName As System.Windows.Forms.TextBox
    Friend WithEvents outbox As System.Windows.Forms.TextBox
    Friend WithEvents ibox As System.Windows.Forms.TextBox
    Friend WithEvents rbox As System.Windows.Forms.TextBox
    Friend WithEvents zbox As System.Windows.Forms.TextBox
    Friend WithEvents picbox As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ybox As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents xbox As System.Windows.Forms.TextBox

End Class
