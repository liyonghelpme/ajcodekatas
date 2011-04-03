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

Public Class Entity
    Inherits DynamicObject
    Implements IEntity

    Private mType As IEntityType

    Public Sub New(ByVal type As IEntityType)
        mType = type
    End Sub

    Public Overrides Function GetNames() As IList
        Return mType.GetNames
    End Function

    Protected Overrides Function GetLeftValue(ByVal name As String) As Object
        Dim value As Object

        value = GetSimpleValue(name)

        If Not value Is Nothing Then
            Return value
        End If

        Dim tp As IType

        tp = mType.GetType(name)

        value = tp.NewInstance
        SetSimpleValue(name, value)

        Return value
    End Function

    Protected Overrides Sub SetSimpleValue(ByVal name As String, ByVal value As Object)
        Dim tp As IType = mType.GetType(name)

        If tp Is Nothing Then
            Throw New ArgumentException("Dato " + name + " desconocido")
        End If

        MyBase.SetSimpleValue(name, tp.Convert(value))
    End Sub

    Protected Overrides Function GetSimpleValue(ByVal name As String) As Object
        Dim tp As IType = mType.GetType(name)

        If tp Is Nothing Then
            Throw New ArgumentException("Dato " + name + " desconocido")
        End If

        Dim value As Object

        value = MyBase.GetSimpleValue(name)

        If value Is Nothing Then
            Return mType.GetDefaultValue
        End If

        Return tp.Convert(value)
    End Function

    Public Overrides Function AcceptValue(ByVal name As String) As Boolean
        Return mType.HasName(name)
    End Function

    Public Overloads Function [GetType](ByVal name As String) As IType Implements IEntity.GetType
        Return mType.GetType(name)
    End Function

    Public Function GetEntityType() As IEntityType Implements IEntity.GetEntityType
        Return mType
    End Function
End Class
