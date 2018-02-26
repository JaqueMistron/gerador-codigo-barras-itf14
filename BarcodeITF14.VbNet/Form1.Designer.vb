<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.btnLimpar = New System.Windows.Forms.Button()
        Me.btnGerar = New System.Windows.Forms.Button()
        Me.label1 = New System.Windows.Forms.Label()
        Me.textBox1 = New System.Windows.Forms.TextBox()
        Me.pbCodigo = New System.Windows.Forms.PictureBox()
        CType(Me.pbCodigo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnLimpar
        '
        Me.btnLimpar.Location = New System.Drawing.Point(305, 234)
        Me.btnLimpar.Name = "btnLimpar"
        Me.btnLimpar.Size = New System.Drawing.Size(75, 23)
        Me.btnLimpar.TabIndex = 9
        Me.btnLimpar.Text = "Limpar"
        Me.btnLimpar.UseVisualStyleBackColor = True
        '
        'btnGerar
        '
        Me.btnGerar.Location = New System.Drawing.Point(224, 234)
        Me.btnGerar.Name = "btnGerar"
        Me.btnGerar.Size = New System.Drawing.Size(75, 23)
        Me.btnGerar.TabIndex = 8
        Me.btnGerar.Text = "Gerar"
        Me.btnGerar.UseVisualStyleBackColor = True
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(12, 12)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(160, 15)
        Me.label1.TabIndex = 7
        Me.label1.Text = "Código DUN 14 (ITF 14)"
        '
        'textBox1
        '
        Me.textBox1.Location = New System.Drawing.Point(13, 31)
        Me.textBox1.MaxLength = 14
        Me.textBox1.Name = "textBox1"
        Me.textBox1.Size = New System.Drawing.Size(259, 20)
        Me.textBox1.TabIndex = 6
        '
        'pbCodigo
        '
        Me.pbCodigo.Location = New System.Drawing.Point(12, 63)
        Me.pbCodigo.Name = "pbCodigo"
        Me.pbCodigo.Size = New System.Drawing.Size(367, 168)
        Me.pbCodigo.TabIndex = 5
        Me.pbCodigo.TabStop = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(557, 312)
        Me.Controls.Add(Me.btnLimpar)
        Me.Controls.Add(Me.btnGerar)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.textBox1)
        Me.Controls.Add(Me.pbCodigo)
        Me.Name = "Form1"
        Me.Text = "Gerador de Código de Barras ITF-14 em VB.Net"
        CType(Me.pbCodigo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents btnLimpar As Button
    Private WithEvents btnGerar As Button
    Private WithEvents label1 As Label
    Private WithEvents textBox1 As TextBox
    Private WithEvents pbCodigo As PictureBox
End Class
