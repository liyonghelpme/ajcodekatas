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

Imports System.Xml
Imports System.IO

Public Class StoresConfiguration
    Public Shared Sub Configuration(ByVal filename As String)
        Dim doc As New XmlDocument()
        doc.Load(filename)
        Configuration(doc)
    End Sub

    Public Shared Sub Configuration(ByVal doc As XmlDocument)
        Dim node As XmlNode

        node = doc.SelectSingleNode("ajstorm/stores")

        If Not node Is Nothing Then
            ConfigureStores(node)
        End If

        node = doc.SelectSingleNode("ajstorm/objects")

        If Not node Is Nothing Then
            ConfigureObjects(node)
        End If
    End Sub

    Private Shared Sub ConfigureStores(ByVal node As XmlNode)
        Dim stores As XmlNodeList

        stores = node.SelectNodes("store")

        Dim store As XmlNode

        For Each store In stores
            ConfigureStore(store)
        Next

        Dim defaultname As String

        defaultname = GetAttribute(node, "default")

        If Not defaultname Is Nothing Then
            StoreManager.SetDefault(defaultname)
        End If
    End Sub

    Private Shared Function GetAttribute(ByVal node As XmlNode, ByVal name As String) As String
        If node Is Nothing Then
            Return Nothing
        End If

        Dim attr As XmlAttribute

        attr = node.Attributes(name)

        If attr Is Nothing Then
            Return Nothing
        End If

        Return attr.Value
    End Function

    Private Shared Sub ConfigureStore(ByVal node As XmlNode)
        Dim name As String
        Dim connstr As String

        name = GetAttribute(node, "name")
        connstr = GetAttribute(node, "connectionString")

        If name Is Nothing Or connstr Is Nothing Then
            Throw New ArgumentException("store mal configurado")
        End If

        Dim store As SqlStore = New SqlStore(connstr)

        StoreManager.Register(name, store)
    End Sub

    Private Shared Sub ConfigureObjects(ByVal node As XmlNode)
        Dim objects As XmlNodeList

        objects = node.SelectNodes("object")

        Dim obj As XmlNode

        For Each obj In objects
            ConfigureObject(obj)
        Next
    End Sub

    Private Shared Sub ConfigureObject(ByVal node As XmlNode)
        Dim name As String
        Dim tableName As String
        Dim typeName As String
        Dim keyName As String
        Dim storeName As String

        name = GetAttribute(node, "name")
        tableName = GetAttribute(node, "table")
        typeName = GetAttribute(node, "type")
        storeName = GetAttribute(node, "store")
        keyName = GetAttribute(node, "key")

        Dim asss As System.Reflection.Assembly() = AppDomain.CurrentDomain.GetAssemblies()
        Dim ass As System.Reflection.Assembly
        Dim type As type

        For Each ass In asss
            type = ass.GetType(typeName)
            If Not type Is Nothing Then
                Exit For
            End If
        Next

        Dim fields As XmlNode

        fields = node.SelectSingleNode("fields")

        Dim map As Mapping
        Dim parmap As Mapping

        If Not fields Is Nothing Then
            map = New Mapping()
            parmap = New Mapping()
            ConfigureFields(fields, map, parmap)
        End If

        Dim adapter As SqlObjectAdapter

        adapter = New SqlObjectAdapter(name, tableName, keyName, type, StoreManager.GetStore(storeName), map, parmap)

        ObjectAdapterManager.Register(name, adapter)
        If Not type Is Nothing Then
            ObjectAdapterManager.Register(type, adapter)
        End If

        Dim cmd As XmlNode
        Dim cmdtext As String

        cmdtext = GetCommandText(node, "selectCommand")

        If Not cmdtext Is Nothing Then
            adapter.SetSelectProcedure(cmdtext)
        End If

        cmdtext = GetCommandText(node, "insertCommand")

        If Not cmdtext Is Nothing Then
            adapter.SetInsertProcedure(cmdtext)
        End If

        cmdtext = GetCommandText(node, "updateCommand")

        If Not cmdtext Is Nothing Then
            adapter.SetUpdateProcedure(cmdtext)
        End If

        cmdtext = GetCommandText(node, "insertCommand")

        If Not cmdtext Is Nothing Then
            adapter.SetInsertProcedure(cmdtext)
        End If

        cmdtext = GetCommandText(node, "deleteCommand")

        If Not cmdtext Is Nothing Then
            adapter.SetDeleteProcedure(cmdtext)
        End If

        cmdtext = GetCommandText(node, "selectAllCommand")

        If Not cmdtext Is Nothing Then
            adapter.SetSelectAllProcedure(cmdtext)
        End If

    End Sub

    Private Shared Sub ConfigureFields(ByVal node As XmlNode, ByVal map As Mapping, ByVal parmap As Mapping)
        Dim fields As XmlNodeList

        fields = node.SelectNodes("field")

        Dim field As XmlNode

        For Each field In fields
            Dim name As String
            Dim column As String
            Dim parameter As String

            name = GetAttribute(field, "name")
            column = GetAttribute(field, "column")
            parameter = GetAttribute(field, "parameter")

            If column Is Nothing Then
                column = name
            End If

            If name Is Nothing Then
                name = column
            End If

            If parameter Is Nothing Then
                parameter = name
            End If

            If Not name Is Nothing And Not column Is Nothing Then
                map.Add(name, column)
                parmap.add(name, parameter)
            End If
        Next
    End Sub

    Private Shared Function GetCommandText(ByVal node As XmlNode, ByVal name As String) As String
        Dim child As XmlNode

        child = node.SelectSingleNode(name)

        If child Is Nothing Then
            Return Nothing
        End If

        Return GetAttribute(child, "text")
    End Function
End Class
