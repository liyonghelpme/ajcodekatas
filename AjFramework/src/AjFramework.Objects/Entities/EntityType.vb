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

Imports System.Collections.Specialized

Public Class EntityType
    Implements IEntityType

    Private mTypes As ListDictionary = New ListDictionary()

    Public Sub New()

    End Sub

    Public Function GetNames() As IList Implements IEntityType.GetNames
        Dim names(mTypes.Count - 1) As String

        mTypes.Keys.CopyTo(names, 0)

        Return names
    End Function

    Public Overloads Function [GetType](ByVal name As String) As IType Implements IEntityType.GetType
        Return mTypes(name)
    End Function

    Public Function HasName(ByVal name As String) As Boolean Implements IEntityType.HasName
        Return mTypes.Contains(name)
    End Function

    Public Function NewInstance() As Object Implements IEntityType.NewInstance
        Return New Entity(Me)
    End Function

    Public Function Convert(ByVal value As Object) As Object Implements IEntityType.Convert
        If value Is Nothing Then
            Return Nothing
        End If

        If Not TypeOf value Is Entity OrElse Not CType(value, Entity).GetEntityType Is Me Then
            Dim target As Entity

            target = NewInstance()

            target.Copy(ToObject(value))

            Return target
        End If

        Return value
    End Function

    Public Function GetDefaultValue() As Object Implements IEntityType.GetDefaultValue
        Return Nothing
    End Function

    Public Sub SetType(ByVal name As String, ByVal type As IType)
        mTypes(name) = type
    End Sub
End Class
