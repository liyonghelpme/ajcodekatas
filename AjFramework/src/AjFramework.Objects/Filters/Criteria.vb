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

Public Class Criteria
    Inherits Filter

    Private mFilters As IList

    Public Sub New(ByVal filter As IFilter)
        mFilters = New ArrayList()
        mFilters.Add(filter)
    End Sub

    Public Sub New(ByVal filter As IFilter, ByVal filter2 As IFilter)
        mFilters = New ArrayList()
        mFilters.Add(filter)
        mFilters.Add(filter2)
    End Sub

    Public Overrides Function Apply(ByVal obj As Object) As Boolean
        Dim filter As IFilter

        For Each filter In mFilters
            If Not filter.Apply(obj) Then
                Return False
            End If
        Next
    End Function

    Public Overrides Function Add(ByVal filter As IFilter) As IFilter
        mFilters.Add(filter)
        Return Me
    End Function
End Class
