Imports System.Data.OleDb

Class OleDbProvider
    Inherits DataProvider

    Public Overrides Function GetConnection(ByVal connstr As String) As IDbConnection
        Return New OleDbConnection(connstr)
    End Function

    Public Overrides Function GetDataAdapter() As IDbDataAdapter
        Return New OleDbDataAdapter()
    End Function

    Public Overrides Sub DeriveParameters(ByVal cmd As IDbCommand)
        Dim conn As OleDbConnection = Nothing
        Dim mustCloseConnection As Boolean

        Try
            conn = DirectCast(cmd, OleDbCommand).Connection
            If conn.State <> ConnectionState.Open Then
                conn.Open()
                mustCloseConnection = True
            End If

            OleDbCommandBuilder.DeriveParameters(DirectCast(cmd, OleDbCommand))
        Finally
            If Not conn Is Nothing Then
                If mustCloseConnection Then
                    conn.Close()
                End If
            End If
        End Try
    End Sub

End Class
