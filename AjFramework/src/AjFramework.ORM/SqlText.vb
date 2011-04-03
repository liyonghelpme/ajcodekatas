'
' +---------------------------------------------------------------------+
' | ajstorm - aj Simple Tool for Object Relational Mapping              |
' +---------------------------------------------------------------------+
' | Copyright (c) 2003-2004 Angel J. Lopez. All rights reserved.        |
' +---------------------------------------------------------------------+
' | This source file is subject to the ajstorm Software License,        |
' | Version 1.0, that is bundled with this package in the file LICENSE. |
' | If you did not receive a copy of this file, you may read it online  |
' | at http://ajstorm.sourceforge.net/license.php.                      |
' +---------------------------------------------------------------------+
'
'

Imports AjFramework.Objects

Imports System.Data
Imports System.Data.SqlClient

Public Class SqlText
    Private mStore As SqlStore
    Private mText As String
    Private mParameters As SqlParameter()
    Private mParameterNames As String()
    Private mTableMapping As Mapping
    Private mParameterMapping As Mapping

    Public Sub New(ByVal store As SqlStore, ByVal text As String)
        Me.New(store, text, Nothing, Nothing, Nothing)
    End Sub

    Public Sub New(ByVal store As SqlStore, ByVal text As String, ByVal pars As SqlParameter())
        Me.New(store, text, pars, Nothing, Nothing)
    End Sub

    Public Sub New(ByVal store As SqlStore, ByVal text As String, ByVal pars As SqlParameter(), ByVal map As Mapping, ByVal parmap As Mapping)
        mStore = store
        mText = text

        If Not pars Is Nothing Then
            mParameters = New SqlParameter(pars.Length - 1) {}
            pars.CopyTo(mParameters, 0)
            mParameterNames = System.Array.CreateInstance(GetType(String), mParameters.Length)
        Else
            mParameters = New SqlParameter(-1) {}
            mParameterNames = New String(-1) {}
        End If

        Dim k As Integer

        For k = 0 To mParameters.Length - 1
            mParameterNames(k) = mParameters(k).ParameterName.Substring(1)
        Next

        If Not map Is Nothing Then
            mTableMapping = map
        End If

        If Not parmap Is Nothing Then
            mParameterMapping = parmap.Reverse
            For k = 0 To mParameters.Length - 1
                mParameterNames(k) = mParameterMapping.Map(mParameterNames(k))
            Next
        End If
    End Sub

    Private Function GetConnection() As SqlConnection
        Return mStore.connection
    End Function

    Private Function CreateParameters(ByVal ParamArray objs() As Object)
        Dim params(mParameters.Length - 1) As SqlParameter

        Dim param As SqlParameter
        Dim k As Integer
        Dim j As Integer

        For Each param In mParameters
            params(k) = CType(param, ICloneable).Clone()
            If params(k).Direction = ParameterDirection.Input Or params(k).Direction = ParameterDirection.InputOutput Then
                params(k).Value = objs(j)
                j += 1
            ElseIf params(k).Direction = ParameterDirection.Output Then
                params(k).Value = Nothing
            End If
            k += 1
        Next

        Return params
    End Function

    Private Function CreateParameters(ByVal obj As IObject)
        Dim params(mParameters.Length - 1) As SqlParameter

        Dim param As SqlParameter
        Dim k As Integer

        For Each param In mParameters
            params(k) = CType(param, ICloneable).Clone()
            If params(k).Direction = ParameterDirection.Input Or params(k).Direction = ParameterDirection.InputOutput Then
                params(k).Value = obj.GetValue(mParameterNames(k))
            ElseIf params(k).Direction = ParameterDirection.Output Then
                params(k).Value = Nothing
            End If
            k += 1
        Next

        Return params
    End Function

    Private Sub GetParameters(ByVal params As SqlParameter(), ByVal obj As IObject)
        Dim param As SqlParameter
        Dim k As Integer

        For Each param In params
            If param.Direction = ParameterDirection.Output Or param.Direction = ParameterDirection.InputOutput Then
                obj.SetValue(mParameterNames(k), param.Value)
            End If
            k += 1
        Next
    End Sub

    Private Shared Sub AttachParameters(ByVal cmd As SqlCommand, ByVal params() As SqlParameter)
        Dim p As SqlParameter
        For Each p In params
            If p.Direction = ParameterDirection.InputOutput And p.Value Is Nothing Then
                p.Value = Nothing
            End If
            cmd.Parameters.Add(p)
        Next
    End Sub

    Public Sub ExecuteUpdate(ByVal ParamArray pars() As Object)
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        Dim params As SqlParameter()

        conn = mStore.connection
        cmd = New SqlCommand(mText, conn)
        cmd.CommandType = CommandType.Text
        params = CreateParameters(pars)
        AttachParameters(cmd, params)

        Dim retval As Integer

        Try
            conn.Open()
            retval = cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Public Sub ExecutePutObject(ByVal obj As Object)
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        Dim params As SqlParameter()
        Dim iobj As IObject = ToObject(obj)

        conn = mStore.connection
        cmd = New SqlCommand(mText, conn)
        cmd.CommandType = CommandType.Text
        params = CreateParameters(iobj)
        AttachParameters(cmd, params)

        Dim retval As Integer

        Try
            conn.Open()
            retval = cmd.ExecuteNonQuery()
            GetParameters(params, iobj)
            cmd.Parameters.Clear()
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Public Function ExecuteGetObject(ByVal obj As Object, ByVal ParamArray keys() As Object) As Integer
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        Dim params As SqlParameter()
        Dim iobj As IObject = ToObject(obj)

        conn = mStore.connection
        cmd = New SqlCommand(mText, conn)
        cmd.CommandType = CommandType.Text
        params = CreateParameters(keys)
        AttachParameters(cmd, params)

        Dim dr As SqlDataReader

        Try
            conn.Open()
            dr = cmd.ExecuteReader()
            If Not dr.Read() Then
                Return 0
            End If
            Dim dro As IObject = New DataReaderObject(dr)
            If Not mTableMapping Is Nothing Then
                dro = New MappedObject(dro, mTableMapping)
            End If
            Objects.Copy(obj, dro)
            cmd.Parameters.Clear()
            Return 1
        Finally
            dr.Close()
            cmd.Dispose()
            conn.Close()
        End Try
    End Function

    Public Function ExecuteQuery(ByVal type As Type, ByVal ParamArray pars() As Object) As IList
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        Dim params As SqlParameter()

        conn = mStore.connection
        cmd = New SqlCommand(mText, conn)
        cmd.CommandType = CommandType.Text
        params = CreateParameters(pars)
        AttachParameters(cmd, params)

        Dim dr As SqlDataReader
        Dim result As New ArrayList()

        Try
            conn.Open()
            dr = cmd.ExecuteReader()
            Dim dro As IObject = New DataReaderObject(dr)
            If Not mTableMapping Is Nothing Then
                dro = New MappedObject(dro, mTableMapping)
            End If
            Dim obj As Object

            While dr.Read
                obj = System.Activator.CreateInstance(type)
                Objects.Copy(obj, dro)
                result.Add(obj)
            End While

            cmd.Parameters.Clear()

            Return result
        Finally
            dr.Close()
            cmd.Dispose()
            conn.Close()
        End Try
    End Function

    Public Sub ExecuteDataset(ByVal ds As DataSet, ByVal tablename As String, ByVal ParamArray pars() As Object)
        Dim conn As SqlConnection
        Dim cmd As SqlCommand
        Dim params As SqlParameter()

        conn = mStore.connection
        cmd = New SqlCommand(mText, conn)
        cmd.CommandType = CommandType.Text
        params = CreateParameters(pars)
        AttachParameters(cmd, params)

        Dim da As New SqlDataAdapter(cmd)

        Try
            conn.Open()
            da.Fill(ds, tablename)
        Finally
            da.Dispose()
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub
End Class
