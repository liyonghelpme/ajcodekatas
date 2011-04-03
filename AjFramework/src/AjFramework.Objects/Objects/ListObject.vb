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

Public Class ListObject
    Implements IListObject

    Private mList As IList

    Public Sub New(ByVal list As IList)
        mList = list
    End Sub

    Public Sub AddValue(ByVal value As Object) Implements IListObject.AddValue
        mList.Add(value)
    End Sub
End Class
