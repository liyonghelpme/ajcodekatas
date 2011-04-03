Class OleDbDataSource
    Inherits DataSource

    Private Shared mOleDbProvider As New OleDbProvider()

    Public Sub New(ByVal connstr As String)
        MyBase.New(connstr, mOleDbProvider)
    End Sub
End Class
