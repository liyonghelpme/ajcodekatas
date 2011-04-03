'
' +---------------------------------------------------------------------+
' | ajstorm - aj Simple Tool for Object Relational Mapping              |
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

Imports System.Collections
Imports System.Collections.Specialized

Public Class ObjectAdapterManager
    Private Shared adapters As IDictionary
    Private Shared adaptersbytype As IDictionary

    Shared Sub New()
        adapters = New ListDictionary()
        adaptersbytype = New ListDictionary()
    End Sub

    Shared Sub Register(ByVal name As String, ByVal adapter As ObjectAdapter)
        adapters(name) = adapter
    End Sub

    Shared Sub Register(ByVal type As Type, ByVal adapter As ObjectAdapter)
        adaptersbytype(type) = adapter
    End Sub

    Shared Function GetAdapter(ByVal name As String) As ObjectAdapter
        Return adapters(name)
    End Function

    Shared Function GetAdapter(ByVal type As Type) As ObjectAdapter
        Return adaptersbytype(type)
    End Function
End Class
