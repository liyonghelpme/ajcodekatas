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

Public Class ViewObject
    Inherits BaseObject

    Private mObject As IObject
    Private mNames As IList

    Public Sub New(ByVal obj As Object, ByVal names As IList)
        mObject = ToObject(obj)
        mNames = names
    End Sub

    Public Overrides Function GetNames() As IList
        Return mNames
    End Function

    Protected Overrides Function GetLeftValue(ByVal name As String) As Object
        If AcceptValue(name) Then
            Return mObject.GetLeftValue(name)
        End If

        Throw New ArgumentException("Dato " + name + " desconocido")
    End Function

    Protected Overrides Function GetSimpleValue(ByVal name As String) As Object
        If AcceptValue(name) Then
            Return mObject.GetValue(name)
        End If

        Throw New ArgumentException("Dato " + name + " desconocido")
    End Function

    Protected Overrides Sub SetSimpleValue(ByVal name As String, ByVal value As Object)
        If AcceptValue(name) Then
            mObject.SetValue(name, value)
            Return
        End If

        Throw New ArgumentException("Dato " + name + " desconocido")
    End Sub
End Class
