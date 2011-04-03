MustInherit Class DataSource
    Protected mConnectionString As String
    Protected mProvider As DataProvider

    Sub New(ByVal connstr As String, ByVal provider As DataProvider)
        mConnectionString = connstr
        mProvider = provider
    End Sub

    Public Function GetDataConnection() As DataConnection
        Return New DataConnection(mConnectionString, mProvider)
    End Function
End Class
