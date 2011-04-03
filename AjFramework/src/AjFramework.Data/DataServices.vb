Imports System.Data.SqlClient
Imports System.Runtime.Remoting.Messaging

Imports AjFramework.Core

Public Class DataServices
    Const DataConnectionName As String = "AjFramework.DataConnection"

    Private Shared Property ContextConnection() As DataConnection
        Get
            Return DirectCast(CallContext.GetData(DataConnectionName), DataConnection)
        End Get
        Set(ByVal Value As DataConnection)
            If Value Is Nothing Then
                CallContext.FreeNamedDataSlot(DataConnectionName)
            Else
                CallContext.SetData(DataConnectionName, Value)
                Value.AutoClose = False
            End If
        End Set
    End Property

    Private Shared ReadOnly Property DefaultConnection() As DataConnection
        Get
            Dim ds As DataSource
            ds = DirectCast(Application.Context("DataSource"), DataSource)
            If ds Is Nothing Then
                ds = New SqlDataSource(DirectCast(Application.Context("ConnectionString"), String))
            End If
            Dim dc As DataConnection
            dc = ds.GetDataConnection
            dc.AutoClose = True
            Return dc
        End Get
    End Property

    Shared Function GetCurrentConnection() As DataConnection
        Dim dc As DataConnection = ContextConnection

        If dc Is Nothing Then
            dc = DefaultConnection
        End If

        Return dc
    End Function

    Public Shared Sub BeginTransaction()
        Dim dc As DataConnection

        dc = ContextConnection

        If dc Is Nothing Then
            dc = DefaultConnection
            ContextConnection = dc
        End If

        dc.BeginTransaction()
    End Sub

    Public Shared Sub BeginTransaction(ByVal dc As DataConnection)
        ContextConnection = dc

        dc.BeginTransaction()
    End Sub

    Public Shared Sub BeginTransaction(ByVal ds As DataSource)
        BeginTransaction(ds.GetDataConnection)
    End Sub

    Public Shared Sub Commit()
        ContextConnection.Commit()
        ContextConnection.Close()
        ContextConnection = Nothing
    End Sub

    Public Shared Sub Rollback()
        ContextConnection.Rollback()
        ContextConnection.Close()
        ContextConnection = Nothing
    End Sub

    Public Shared Function ExecuteScalar(ByVal cmd As String) As Object
        Return ExecuteScalar(GetCurrentConnection, cmd)
    End Function

    Public Shared Function ExecuteScalar(ByVal dc As DataConnection, ByVal cmd As String) As Object
        Try
            If dc.InTransaction Then
                Return SqlHelper.ExecuteScalar(DirectCast(dc.Transaction, SqlTransaction), CommandType.Text, cmd)
            End If
            Return SqlHelper.ExecuteScalar(DirectCast(dc.Connection, SqlConnection), CommandType.Text, cmd)
        Finally
            dc.Dispose()
        End Try
    End Function

    Public Shared Function ExecuteReader(ByVal cmd As String) As IDataReader
        Return ExecuteReader(GetCurrentConnection, cmd)
    End Function

    Public Shared Function ExecuteReader(ByVal dc As DataConnection, ByVal cmd As String) As IDataReader
        If dc.InTransaction Then
            Return SqlHelper.ExecuteReader(DirectCast(dc.Transaction, SqlTransaction), CommandType.Text, cmd)
        End If
        Return SqlHelper.ExecuteReader(DirectCast(dc.Connection, SqlConnection), CommandType.Text, cmd)
    End Function

    Public Shared Function ExecuteSpReader(ByVal cmd As String, ByVal ParamArray params As Object()) As IDataReader
        Return ExecuteSpReader(GetCurrentConnection, cmd, params)
    End Function

    Public Shared Function ExecuteSpReader(ByVal dc As DataConnection, ByVal cmd As String, ByVal ParamArray params As Object()) As IDataReader
        If dc.InTransaction Then
            Return SqlHelper.ExecuteReader(DirectCast(dc.Transaction, SqlTransaction), cmd, params)
        End If
        Return SqlHelper.ExecuteReader(DirectCast(dc.Connection, SqlConnection), cmd, params)
    End Function

    Public Shared Function ExecuteDataSet(ByVal cmd As String) As DataSet
        Return ExecuteDataSet(GetCurrentConnection, cmd)
    End Function

    Public Shared Function ExecuteDataSet(ByVal dc As DataConnection, ByVal cmd As String) As DataSet
        Try
            If dc.InTransaction Then
                Return SqlHelper.ExecuteDataset(DirectCast(dc.Transaction, SqlTransaction), CommandType.Text, cmd)
            End If
            Return SqlHelper.ExecuteDataset(DirectCast(dc.Connection, SqlConnection), CommandType.Text, cmd)
        Finally
            dc.Dispose()
        End Try
    End Function

    Public Shared Function ExecuteSpDataSet(ByVal cmd As String, ByVal ParamArray params As Object()) As DataSet
        Return ExecuteSpDataSet(GetCurrentConnection, cmd, params)
    End Function

    Public Shared Function ExecuteSpDataSet(ByVal dc As DataConnection, ByVal cmd As String, ByVal ParamArray params As Object()) As DataSet
        Try
            If dc.InTransaction Then
                Return SqlHelper.ExecuteDataset(DirectCast(dc.Transaction, SqlTransaction), cmd, params)
            End If
            Return SqlHelper.ExecuteDataset(DirectCast(dc.Connection, SqlConnection), cmd, params)
        Finally
            dc.Dispose()
        End Try
    End Function

    Public Shared Sub ExecuteSp(ByVal cmd As String, ByVal ParamArray params As Object())
        ExecuteSp(GetCurrentConnection, cmd, params)
    End Sub

    Public Shared Sub ExecuteSp(ByVal dc As DataConnection, ByVal cmd As String, ByVal ParamArray params As Object())
        Try
            If dc.InTransaction Then
                SqlHelper.ExecuteNonQuery(DirectCast(dc.Transaction, SqlTransaction), cmd, params)
            End If
            SqlHelper.ExecuteNonQuery(DirectCast(dc.Connection, SqlConnection), cmd, params)
        Finally
            dc.Dispose()
        End Try
    End Sub
End Class
