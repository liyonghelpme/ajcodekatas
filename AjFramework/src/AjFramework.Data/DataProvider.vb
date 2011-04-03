Public MustInherit Class DataProvider
    Public MustOverride Function GetConnection(ByVal connstr As String) As IDbConnection
    Public MustOverride Function GetDataAdapter() As IDbDataAdapter
    Public MustOverride Sub DeriveParameters(ByVal cmd As IDbCommand)

    Protected Overridable Sub AttachParameters(ByVal cmd As IDbCommand, ByVal params As IDataParameter())
        Dim p As IDataParameter

        For Each p In params
            If Not p Is Nothing Then
                cmd.Parameters.Add(p)
            End If
        Next
    End Sub

    Protected Overridable Sub SetParameterValues(ByVal params As IDataParameterCollection, ByVal ParamArray values() As Object)
        If params Is Nothing Or values Is Nothing Then
            Return
        End If

        Dim p As IDataParameter
        Dim i As Integer

        For Each p In params
            If p.Direction <> ParameterDirection.ReturnValue Then
                If TypeOf values(i) Is IDataParameter Then
                    Dim param As IDataParameter = DirectCast(values(i), IDataParameter)
                    If param.Value Is Nothing Then
                        p.Value = DBNull.Value
                    Else
                        p.Value = param.Value
                    End If
                ElseIf TypeOf values(i) Is DataParameter Then
                    Dim param As DataParameter = DirectCast(values(i), DataParameter)
                    If param.Value Is Nothing Then
                        p.Value = DBNull.Value
                    Else
                        p.Value = param.Value
                    End If
                Else
                    If values(i) Is Nothing Then
                        p.Value = DBNull.Value
                    Else
                        p.Value = values(i)
                    End If
                End If
                i += 1
            End If
        Next
    End Sub

    Protected Overridable Sub GetParameterValues(ByVal params As IDataParameterCollection, ByVal ParamArray values() As Object)
        If params Is Nothing Or values Is Nothing Then
            Return
        End If

        Dim p As IDataParameter
        Dim i As Integer

        For Each p In params
            If p.Direction <> ParameterDirection.ReturnValue Then
                If p.Direction = ParameterDirection.InputOutput Or p.Direction = ParameterDirection.Output Then
                    If TypeOf values(i) Is DataParameter Then
                        Dim dp As DataParameter = DirectCast(values(i), DataParameter)
                        If p.Value Is DBNull.Value Then
                            dp.Value = Nothing
                        Else
                            dp.Value = p.Value
                        End If
                    End If
                End If
                i += 1
            End If
        Next
    End Sub

    Protected Overridable Function DiscoverParameterSet(ByVal connstr As String, ByVal cmdtext As String) As IDataParameter()
        Dim conn As IDbConnection
        Dim cmd As IDbCommand

        conn = GetConnection(connstr)
        conn.Open()

        cmd = conn.CreateCommand
        cmd.CommandText = cmdtext
        cmd.CommandType = CommandType.StoredProcedure
        DeriveParameters(cmd)

        conn.Close()

        Dim pars(cmd.Parameters.Count - 1) As IDataParameter

        cmd.Parameters.CopyTo(pars, 0)

        Dim p As IDataParameter

        For Each p In pars
            p.Value = DBNull.Value
        Next

        Return pars
    End Function

    Protected Sub MakeParameters(ByVal cmd As IDbCommand)
        Dim params As IDataParameter()

        params = ParameterCache.GetCachedParameters(cmd.Connection.ConnectionString, cmd.CommandText)

        If params Is Nothing Then
            params = DiscoverParameterSet(cmd.Connection.ConnectionString, cmd.CommandText)
            ParameterCache.CachedParameterSet(cmd.Connection.ConnectionString, cmd.CommandText) = params
            params = ParameterCache.CloneParameters(params)
        End If

        AttachParameters(cmd, params)
    End Sub

    Public Function ExecuteDataSet(ByVal cmd As IDbCommand) As DataSet
        Dim da As IDbDataAdapter = Nothing
        Dim mustCloseConnection As Boolean

        Try
            da = GetDataAdapter()
            da.SelectCommand = cmd

            If cmd.Connection.State <> ConnectionState.Open Then
                cmd.Connection.Open()
                mustCloseConnection = True
            End If

            Dim ds As New DataSet()

            da.Fill(ds)

            Return ds
        Finally
            If Not da Is Nothing Then
                DirectCast(da, IDisposable).Dispose()
            End If
            cmd.Parameters.Clear()
            If mustCloseConnection Then
                cmd.Connection.Close()
            End If
        End Try
    End Function

    Public Function ExecuteDataSet(ByVal connstr As String, ByVal cmdtext As String, ByVal cmdtype As CommandType) As DataSet
        Return ExecuteDataSet(connstr, cmdtext, cmdtype, DirectCast(Nothing, Object()))
    End Function

    Public Function ExecuteDataSet(ByVal connstr As String, ByVal cmdtext As String, ByVal cmdtype As CommandType, ByVal ParamArray params As Object()) As DataSet
        Dim conn As IDbConnection = Nothing

        Try
            conn = GetConnection(connstr)
            Return ExecuteDataSet(conn, cmdtext, cmdtype, params)
        Finally
            If Not conn Is Nothing Then
                conn.Dispose()
            End If
        End Try
    End Function

    Public Function ExecuteDataSet(ByVal conn As IDbConnection, ByVal cmdtext As String, ByVal cmdtype As CommandType, ByVal ParamArray params As Object()) As DataSet
        Dim cmd As IDbCommand

        cmd = conn.CreateCommand()
        cmd.CommandText = cmdtext
        cmd.CommandType = cmdtype

        If cmdtype = CommandType.StoredProcedure Then
            MakeParameters(cmd)
            SetParameterValues(cmd.Parameters, params)
        End If

        Return ExecuteDataSet(cmd)
    End Function

    Public Function ExecuteDataSet(ByVal trans As IDbTransaction, ByVal cmdtext As String, ByVal cmdtype As CommandType, ByVal ParamArray params As Object()) As DataSet
        Dim cmd As IDbCommand

        cmd = trans.Connection.CreateCommand()
        cmd.Transaction = trans
        cmd.CommandText = cmdtext
        cmd.CommandType = cmdtype

        If cmdtype = CommandType.StoredProcedure Then
            MakeParameters(cmd)
            SetParameterValues(cmd.Parameters, params)
        End If

        cmd.Transaction = trans

        Return ExecuteDataSet(cmd)
    End Function

    Public Function ExecuteReader(ByVal cmd As IDbCommand) As IDataReader
        Try
            If cmd.Connection.State <> ConnectionState.Open Then
                cmd.Connection.Open()
                Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
            End If

            Return cmd.ExecuteReader()
        Finally
            cmd.Parameters.Clear()
        End Try
    End Function

    Public Function ExecuteReader(ByVal connstr As String, ByVal cmdtext As String, ByVal cmdtype As CommandType, ByVal ParamArray params As Object()) As IDataReader
        Dim conn As IDbConnection
        conn = GetConnection(connstr)
        Return ExecuteReader(conn, cmdtext, cmdtype, params)
    End Function

    Public Function ExecuteReader(ByVal conn As IDbConnection, ByVal cmdtext As String, ByVal cmdtype As CommandType, ByVal ParamArray params As Object()) As IDataReader
        Dim cmd As IDbCommand

        cmd = conn.CreateCommand()
        cmd.CommandText = cmdtext
        cmd.CommandType = cmdtype

        If cmdtype = CommandType.StoredProcedure Then
            MakeParameters(cmd)
            SetParameterValues(cmd.Parameters, params)
        End If

        Return ExecuteReader(cmd)
    End Function

    Public Function ExecuteReader(ByVal trans As IDbTransaction, ByVal cmdtext As String, ByVal cmdtype As CommandType, ByVal ParamArray params As Object()) As IDataReader
        Dim cmd As IDbCommand

        cmd = trans.Connection.CreateCommand()
        cmd.Transaction = trans
        cmd.CommandText = cmdtext
        cmd.CommandType = cmdtype

        If cmdtype = CommandType.StoredProcedure Then
            MakeParameters(cmd)
            SetParameterValues(cmd.Parameters, params)
        End If

        Return ExecuteReader(cmd)
    End Function

    Public Function ExecuteNonQuery(ByVal cmd As IDbCommand) As Integer
        Dim mustCloseConnection As Boolean

        Try
            If cmd.Connection.State <> ConnectionState.Open Then
                cmd.Connection.Open()
                mustCloseConnection = True
            End If

            Return cmd.ExecuteNonQuery()
        Finally
            'cmd.Parameters.Clear()
            If mustCloseConnection Then
                cmd.Connection.Close()
            End If
        End Try
    End Function

    Public Function ExecuteNonQuery(ByVal connstr As String, ByVal cmdtext As String, ByVal cmdtype As CommandType, ByVal ParamArray params As Object()) As Integer
        Dim conn As IDbConnection = Nothing

        Try
            conn = GetConnection(connstr)
            Return ExecuteNonQuery(conn, cmdtext, cmdtype, params)
        Finally
            If Not conn Is Nothing Then
                conn.Dispose()
            End If
        End Try
    End Function

    Public Function ExecuteNonQuery(ByVal conn As IDbConnection, ByVal cmdtext As String, ByVal cmdtype As CommandType, ByVal ParamArray params As Object()) As Integer
        Dim cmd As IDbCommand

        cmd = conn.CreateCommand()
        cmd.CommandText = cmdtext
        cmd.CommandType = cmdtype

        If cmdtype = CommandType.StoredProcedure Then
            MakeParameters(cmd)
            SetParameterValues(cmd.Parameters, params)
        End If

        Dim result As Integer
        result = ExecuteNonQuery(cmd)
        GetParameterValues(cmd.Parameters, params)
        cmd.Parameters.Clear()
        Return result
    End Function

    Public Function ExecuteNonQuery(ByVal trans As IDbTransaction, ByVal cmdtext As String, ByVal cmdtype As CommandType, ByVal ParamArray params As Object()) As Integer
        Dim cmd As IDbCommand

        cmd = trans.Connection.CreateCommand()
        cmd.Transaction = trans
        cmd.CommandText = cmdtext
        cmd.CommandType = cmdtype

        If cmdtype = CommandType.StoredProcedure Then
            MakeParameters(cmd)
            SetParameterValues(cmd.Parameters, params)
        End If

        cmd.Transaction = trans

        Dim result As Integer
        result = ExecuteNonQuery(cmd)
        GetParameterValues(cmd.Parameters, params)
        cmd.Parameters.Clear()
        Return result
    End Function
End Class

Public Class ParameterCache
    Private Shared mParameterCache As New Hashtable()

    Public Shared Function CloneParameters(ByVal params As IDataParameterCollection) As IDataParameter()
        Dim ps(params.Count - 1) As IDataParameter

        Dim p As IDataParameter
        Dim i As Integer

        For Each p In params
            ps(i) = DirectCast(DirectCast(p, ICloneable).Clone, IDataParameter)
            i += 1
        Next

        Return ps
    End Function

    Public Shared Function CloneParameters(ByVal params() As IDataParameter) As IDataParameter()
        Dim ps(params.Length - 1) As IDataParameter
        Dim p As IDataParameter
        Dim i As Integer

        For Each p In params
            ps(i) = DirectCast(DirectCast(p, ICloneable).Clone, IDataParameter)
            i += 1
        Next

        Return ps
    End Function

    Private Shared Function MakeKey(ByVal connstr As String, ByVal cmdtext As String) As String
        Return connstr + "::" + cmdtext
    End Function

    Public Shared Property CachedParameterSet(ByVal connstr As String, ByVal cmdtext As String) As IDataParameter()
        Get
            Return DirectCast(mParameterCache(MakeKey(connstr, cmdtext)), IDataParameter())
        End Get
        Set(ByVal Value As IDataParameter())
            mParameterCache(MakeKey(connstr, cmdtext)) = Value
        End Set
    End Property

    Public Shared Function GetCachedParameters(ByVal connstr As String, ByVal cmdtext As String) As IDataParameter()
        Dim ps As IDataParameter()

        ps = CachedParameterSet(connstr, cmdtext)

        If Not ps Is Nothing Then
            Return CloneParameters(ps)
        End If

        Return Nothing
    End Function
End Class
