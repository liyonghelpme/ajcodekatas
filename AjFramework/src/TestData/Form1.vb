Imports AjFramework.Core
Imports AjFramework.Data

Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents btnLeer As System.Windows.Forms.Button
    Friend WithEvents dtgDatos As System.Windows.Forms.DataGrid
    Friend WithEvents btnLeerTr As System.Windows.Forms.Button
    Friend WithEvents btnLeerSp As System.Windows.Forms.Button
    Friend WithEvents OleDbConnection1 As System.Data.OleDb.OleDbConnection
    Friend WithEvents cmbEmpleados As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnLeer = New System.Windows.Forms.Button()
        Me.dtgDatos = New System.Windows.Forms.DataGrid()
        Me.btnLeerTr = New System.Windows.Forms.Button()
        Me.btnLeerSp = New System.Windows.Forms.Button()
        Me.OleDbConnection1 = New System.Data.OleDb.OleDbConnection()
        Me.cmbEmpleados = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.dtgDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnLeer
        '
        Me.btnLeer.Location = New System.Drawing.Point(16, 8)
        Me.btnLeer.Name = "btnLeer"
        Me.btnLeer.TabIndex = 0
        Me.btnLeer.Text = "Leer"
        '
        'dtgDatos
        '
        Me.dtgDatos.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.dtgDatos.DataMember = ""
        Me.dtgDatos.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dtgDatos.Location = New System.Drawing.Point(16, 40)
        Me.dtgDatos.Name = "dtgDatos"
        Me.dtgDatos.Size = New System.Drawing.Size(476, 224)
        Me.dtgDatos.TabIndex = 1
        '
        'btnLeerTr
        '
        Me.btnLeerTr.Location = New System.Drawing.Point(104, 8)
        Me.btnLeerTr.Name = "btnLeerTr"
        Me.btnLeerTr.TabIndex = 2
        Me.btnLeerTr.Text = "Leer Tr"
        '
        'btnLeerSp
        '
        Me.btnLeerSp.Location = New System.Drawing.Point(192, 8)
        Me.btnLeerSp.Name = "btnLeerSp"
        Me.btnLeerSp.TabIndex = 3
        Me.btnLeerSp.Text = "Leer Sp"
        '
        'OleDbConnection1
        '
        Me.OleDbConnection1.ConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=sa;Initial Catalog=pubs;D" & _
        "ata Source=(local);Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4" & _
        "096;Workstation ID=BOMBADIL;Use Encryption for Data=False;Tag with column collat" & _
        "ion when possible=False"
        '
        'cmbEmpleados
        '
        Me.cmbEmpleados.Location = New System.Drawing.Point(280, 8)
        Me.cmbEmpleados.Name = "cmbEmpleados"
        Me.cmbEmpleados.Size = New System.Drawing.Size(136, 21)
        Me.cmbEmpleados.TabIndex = 4
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(424, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Leer Rd"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(504, 273)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Button1, Me.cmbEmpleados, Me.btnLeerSp, Me.btnLeerTr, Me.dtgDatos, Me.btnLeer})
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.dtgDatos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnLeer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLeer.Click
        Try
            dtgDatos.DataSource = DataService.ExecuteDataSet("select * from employee", CommandType.Text).Tables(0)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnLeerTr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLeerTr.Click
        Try
            DataService.BeginTransaction()
            dtgDatos.DataSource = DataService.ExecuteDataSet("select * from employee", CommandType.Text).Tables(0)
            DataService.Commit()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnLeerSp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLeerSp.Click
        Try
            dtgDatos.DataSource = DataService.ExecuteDataSet("byroyalty", CommandType.StoredProcedure, 25).Tables(0)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim dr As IDataReader

        Try
            dr = DataService.ExecuteReader("select * from employee", CommandType.Text)

            cmbEmpleados.Items.Clear()

            While dr.Read
                cmbEmpleados.Items.Add(dr("lname"))
            End While

            dr.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
