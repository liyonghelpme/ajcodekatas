Imports System.Data.SqlClient

Class SqlProvider
    Inherits DataProvider

    Public Overrides Function GetConnection(ByVal connstr As String) As IDbConnection
        Return New SqlConnection(connstr)
    End Function

    Public Overrides Function GetDataAdapter() As IDbDataAdapter
        Return New SqlDataAdapter()
    End Function

    Public Overrides Sub DeriveParameters(ByVal cmd As IDbCommand)
        Dim conn As SqlConnection = Nothing
        Dim mustCloseConnection As Boolean

        Try
            conn = DirectCast(cmd, SqlCommand).Connection
            If conn.State <> ConnectionState.Open Then
                conn.Open()
                mustCloseConnection = True
            End If
            SqlCommandBuilder.DeriveParameters(DirectCast(cmd, SqlCommand))
        Finally
            If Not conn Is Nothing Then
                If mustCloseConnection Then
                    conn.Close()
                End If
            End If
        End Try
    End Sub
End Class
