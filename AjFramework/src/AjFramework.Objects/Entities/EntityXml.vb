'
' +---------------------------------------------------------------------+
' | ajobjects - Tool for Dynamic Objects in .NET                        |
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

Imports System.Xml

Public Class EntityXml
    Shared Function GetTypeFromNode(ByVal xmlnode As XmlNode) As IType
        If xmlnode.ChildNodes.Count > 0 Then
            Return GetTypeFromXml(xmlnode)
        End If

        Dim attr As XmlAttribute

        attr = xmlnode.Attributes("type")

        If attr Is Nothing Then
            If xmlnode.Name.StartsWith("Id") Then
                Return IntegerType.GetInstance
            End If
            Return StringType.GetInstance
        End If

        Dim tp As IType

        tp = TypeManager.Type(attr.Value)

        If tp Is Nothing Then
            Throw New ArgumentException("Tipo " + xmlnode.Name + " desconocido")
        End If

        Return tp
    End Function

    Public Shared Function GetTypeFromXml(ByVal xmlnode As XmlNode) As EntityType
        Dim tp As New EntityType()

        Dim child As xmlnode

        For Each child In xmlnode.ChildNodes
            tp.SetType(child.Name, GetTypeFromNode(child))
        Next

        Return tp
    End Function

    Public Shared Function GetTypeFromXml(ByVal xmldoc As XmlDocument) As EntityType
        Return GetTypeFromXml(xmldoc.DocumentElement)
    End Function

    Public Shared Function GetTypeFromXml(ByVal filename As String) As EntityType
        Dim xmldoc As New XmlDocument()
        xmldoc.Load(filename)
        Return GetTypeFromXml(xmldoc)
    End Function
End Class
