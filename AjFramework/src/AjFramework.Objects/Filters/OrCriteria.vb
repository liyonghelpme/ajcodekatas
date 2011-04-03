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

Public Class OrCriteria
    Inherits Filter

    Private mLeft As IFilter
    Private mRight As IFilter

    Public Sub New(ByVal left As IFilter, ByVal right As IFilter)
        mLeft = left
        mRight = right
    End Sub

    Public Overrides Function Apply(ByVal obj As Object) As Boolean
        If mLeft.Apply(obj) Then
            Return True
        End If

        Return mRight.Apply(obj)
    End Function
End Class
