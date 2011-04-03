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

Public Class SumFunctor
    Inherits CollectionFunctor

    Private mName As String

    Public Sub New(ByVal prop As String)
        mName = prop
    End Sub

    Protected Overrides Function Init() As Object
        Return 0
    End Function

    Protected Overrides Sub Eval(ByVal item As Object, ByRef data As Object)
        data = data + ToObject(item).GetValue(mName)
    End Sub
End Class
