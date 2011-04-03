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

Public Class SqlObjectAdapter
    Inherits ObjectAdapter

    Private mStore As SqlStore
    Private mObjectType As Type
    Private mObjectName As String
    Private mTableName As String
    Private mKeyName As String
    Private mTableMapping As Mapping
    Private mParameterMapping As Mapping

    Private mInsertProcedure As SqlProcedure
    Private mDeleteProcedure As SqlProcedure
    Private mUpdateProcedure As SqlProcedure
    Private mSelectProcedure As SqlProcedure
    Private mSelectAllProcedure As SqlProcedure

    Public Sub New(ByVal objname As String, ByVal tabname As String, ByVal keyname As String, ByVal type As Type, ByVal store As SqlStore)
        Me.New(objname, tabname, keyname, type, store, Nothing, Nothing)
    End Sub

    Public Sub New(ByVal objname As String, ByVal tabname As String, ByVal keyname As String, ByVal type As Type, ByVal store As SqlStore, ByVal maps As Mapping, ByVal parmaps As Mapping)
        If type Is Nothing Then
            Throw New ArgumentNullException("Debe especificar Tipo")
        End If
        mObjectName = objname
        mTableName = tabname
        mObjectType = type
        mKeyName = keyname
        mStore = store
        mTableMapping = maps
        mParameterMapping = parmaps
    End Sub

    Public Sub SetInsertProcedure(ByVal txt As String)
        mInsertProcedure = New SqlProcedure(mStore, txt, mTableMapping, mParameterMapping)
    End Sub

    Public Sub SetDeleteProcedure(ByVal txt As String)
        mDeleteProcedure = New SqlProcedure(mStore, txt, mTableMapping, mParameterMapping)
    End Sub

    Public Sub SetUpdateProcedure(ByVal txt As String)
        mUpdateProcedure = New SqlProcedure(mStore, txt, mTableMapping, mParameterMapping)
    End Sub

    Public Sub SetSelectProcedure(ByVal txt As String)
        mSelectProcedure = New SqlProcedure(mStore, txt, mTableMapping, mParameterMapping)
    End Sub

    Public Sub SetSelectAllProcedure(ByVal txt As String)
        mSelectAllProcedure = New SqlProcedure(mStore, txt, mTableMapping, mParameterMapping)
    End Sub

    Public Sub Insert(ByVal obj As Object)
        mInsertProcedure.ExecutePutObject(obj)
    End Sub

    Public Sub Update(ByVal obj As Object)
        mUpdateProcedure.ExecutePutObject(obj)
    End Sub

    Public Sub Delete(ByVal key As Object)
        mDeleteProcedure.ExecuteUpdate(key)
    End Sub

    Public Sub [Select](ByVal key As Object, ByVal obj As Object)
        mSelectProcedure.ExecuteGetObject(obj, key)
    End Sub

    Public Function SelectAll() As IList
        Return mSelectAllProcedure.ExecuteQuery(mObjectType)
    End Function
End Class
