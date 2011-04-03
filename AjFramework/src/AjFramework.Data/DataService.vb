Imports System.Data.SqlClient
Imports System.Runtime.Remoting.Messaging

Imports AjFramework.Core

Public Class DataService
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
            Return dc
        End Get
    End Property

    Private Shared Function GetCurrentConnection() As DataConnection
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

    Public Shared Function ExecuteReader(ByVal cmdtext As String) As IDataReader
        Return GetCurrentConnection.ExecuteReader(cmdtext, CommandType.StoredProcedure, DirectCast(Nothing, Object()))
    End Function

    Public Shared Function ExecuteReader(ByVal cmdtext As String, ByVal cmdtype As CommandType) As IDataReader
        Return GetCurrentConnection.ExecuteReader(cmdtext, cmdtype, DirectCast(Nothing, Object()))
    End Function

    Public Shared Function ExecuteReader(ByVal cmdtext As String, ByVal cmdtype As CommandType, ByVal ParamArray params As Object()) As IDataReader
        Return GetCurrentConnection.ExecuteReader(cmdtext, cmdtype, params)
    End Function

    Public Shared Function ExecuteDataSet(ByVal cmdtext As String) As DataSet
        Return GetCurrentConnection.ExecuteDataSet(cmdtext, CommandType.StoredProcedure, DirectCast(Nothing, Object()))
    End Function

    Public Shared Function ExecuteDataSet(ByVal cmdtext As String, ByVal cmdtype As CommandType) As DataSet
        Return GetCurrentConnection.ExecuteDataSet(cmdtext, cmdtype, DirectCast(Nothing, Object()))
    End Function

    Public Shared Function ExecuteDataSet(ByVal cmdtext As String, ByVal cmdtype As CommandType, ByVal ParamArray params As Object()) As DataSet
        Return GetCurrentConnection.ExecuteDataSet(cmdtext, cmdtype, params)
    End Function

    Public Shared Function ExecuteNonQuery(ByVal cmdtext As String) As Integer
        Return GetCurrentConnection.ExecuteNonQuery(cmdtext, CommandType.StoredProcedure, DirectCast(Nothing, Object()))
    End Function

    Public Shared Function ExecuteNonQuery(ByVal cmdtext As String, ByVal cmdtype As CommandType) As Integer
        Return GetCurrentConnection.ExecuteNonQuery(cmdtext, cmdtype, DirectCast(Nothing, Object()))
    End Function

    Public Shared Function ExecuteNonQuery(ByVal cmdtext As String, ByVal cmdtype As CommandType, ByVal ParamArray params As Object()) As Integer
        Return GetCurrentConnection.ExecuteNonQuery(cmdtext, cmdtype, params)
    End Function
End Class
