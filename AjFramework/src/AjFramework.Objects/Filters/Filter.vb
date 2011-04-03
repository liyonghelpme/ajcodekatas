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

Public MustInherit Class Filter
    Implements IFilter

    Public MustOverride Function Apply(ByVal obj As Object) As Boolean Implements IFilter.Apply

    Public Overridable Function ApplyTo(ByVal col As ICollection) As ICollection Implements IFilter.ApplyTo
        Dim result As New ArrayList()

        Dim obj As Object

        For Each obj In col
            If Apply(obj) Then
                result.Add(obj)
            End If
        Next

        Return result
    End Function

    Public Overridable Function Add(ByVal filter As IFilter) As IFilter
        Return New Criteria(Me, filter)
    End Function

    Public Overridable Function AddOr(ByVal filter As IFilter) As IFilter
        Return New OrCriteria(Me, filter)
    End Function
End Class
