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

Imports System.Data.SqlClient

Public Class DataReaderObject
    Inherits BaseObject

    Private mDataReader As SqlDataReader

    Public Sub New(ByVal dr As SqlDataReader)
        mDataReader = dr
    End Sub

    Protected Overrides Sub SetSimpleValue(ByVal name As String, ByVal value As Object)
        Throw New NotSupportedException("No se puede poner valor a un DataReader")
    End Sub

    Protected Overrides Function GetSimpleValue(ByVal name As String) As Object
        If mDataReader(name) Is System.DBNull.Value Then
            Return Nothing
        End If

        Return mDataReader(name)
    End Function

    Public Overrides Function GetNames() As IList
        Dim result As New ArrayList(mDataReader.FieldCount)
        Dim k As Integer

        For k = 0 To mDataReader.FieldCount - 1
            result.Add(mDataReader.GetName(k))
        Next

        Return result
    End Function

    Protected Overrides Function GetLeftValue(ByVal name As String) As Object
        Throw New NotImplementedException("GetLeftValue no se implementa en DataReaderObject")
    End Function

    Public Overrides Sub RemoveValue(ByVal name As String, ByVal value As Object)
        Throw New NotSupportedException("RemoveValue no está soportada en DataReaderObject")
    End Sub
End Class
