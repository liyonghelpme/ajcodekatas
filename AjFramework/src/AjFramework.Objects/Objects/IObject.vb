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

Public Interface IObject
    Function GetValue(ByVal name As String) As Object
    Sub SetValue(ByVal name As String, ByVal value As Object)
    Function GetNames() As IList
    Function AcceptValue(ByVal name As String) As Boolean
    Function GetLeftValue(ByVal name As String) As Object
    Sub AddValue(ByVal name As String, ByVal obj As Object)
    Sub RemoveValue(ByVal name As String, ByVal obj As Object)
    Sub Copy(ByVal obj As Object)
    Function Equals(ByVal obj As Object) As Boolean
End Interface
