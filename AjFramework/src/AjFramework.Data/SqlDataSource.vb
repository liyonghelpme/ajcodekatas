Class SqlDataSource
    Inherits DataSource

    Private Shared mSqlProvider As New SqlProvider()

    Public Sub New(ByVal connstr As String)
        MyBase.New(connstr, mSqlProvider)
    End Sub
End Class
