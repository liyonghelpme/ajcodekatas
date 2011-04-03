Class DataConnection
    Private mConnectionString As String
    Private mProvider As DataProvider
    Private mConnection As IDbConnection
    Private mTransaction As IDbTransaction

    Public Sub New(ByVal connstr As String, ByVal provider As DataProvider)
        mConnectionString = connstr
        mProvider = provider
    End Sub

    Public Sub New(ByVal conn As IDbConnection)
        mConnection = conn
    End Sub

    Public ReadOnly Property InTransaction() As Boolean
        Get
            Return Not mTransaction Is Nothing
        End Get
    End Property

    Public Sub BeginTransaction()
        If mConnection Is Nothing OrElse mConnection.State = ConnectionState.Closed Then
            Open()
        End If

        mTransaction = mConnection.BeginTransaction
    End Sub

    Public Sub Commit()
        mTransaction.Commit()
        mTransaction = Nothing
    End Sub

    Public Sub Rollback()
        mTransaction.Rollback()
        mTransaction = Nothing
    End Sub

    Public Sub Open()
        If mConnection Is Nothing Then
            mConnection = mProvider.GetConnection(mConnectionString)
        End If

        mConnection.Open()
    End Sub

    Public Sub Close()
        If Not mTransaction Is Nothing Then
            Rollback()
        End If
        mConnection.Close()
        mConnection = Nothing
    End Sub

    Public Function ExecuteDataSet(ByVal cmdtext As String, ByVal cmdtype As CommandType, ByVal ParamArray params As Object()) As DataSet
        If Not mTransaction Is Nothing Then
            Return mProvider.ExecuteDataSet(mTransaction, cmdtext, cmdtype, params)
        End If
        If Not mConnection Is Nothing Then
            Return mProvider.ExecuteDataSet(mConnection, cmdtext, cmdtype, params)
        End If
        Return mProvider.ExecuteDataSet(mConnectionString, cmdtext, cmdtype, params)
    End Function

    Public Function ExecuteReader(ByVal cmdtext As String, ByVal cmdtype As CommandType, ByVal ParamArray params As Object()) As IDataReader
        If Not mTransaction Is Nothing Then
            Return mProvider.ExecuteReader(mTransaction, cmdtext, cmdtype, params)
        End If
        If Not mConnection Is Nothing Then
            Return mProvider.ExecuteReader(mConnection, cmdtext, cmdtype, params)
        End If
        Return mProvider.ExecuteReader(mConnectionString, cmdtext, cmdtype, params)
    End Function

    Public Function ExecuteNonQuery(ByVal cmdtext As String, ByVal cmdtype As CommandType, ByVal ParamArray params As Object()) As Integer
        If Not mTransaction Is Nothing Then
            Return mProvider.ExecuteNonQuery(mTransaction, cmdtext, cmdtype, params)
        End If
        If Not mConnection Is Nothing Then
            Return mProvider.ExecuteNonQuery(mConnection, cmdtext, cmdtype, params)
        End If
        Return mProvider.ExecuteNonQuery(mConnectionString, cmdtext, cmdtype, params)
    End Function
End Class
