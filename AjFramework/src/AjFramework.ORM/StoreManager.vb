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

Public Class StoreManager
    Private Shared stores As IDictionary
    Private Shared defaultstore As String

    Shared Sub New()
        stores = New ListDictionary()
    End Sub

    Shared Sub Register(ByVal name As String, ByVal store As DataStore)
        stores(name) = store
    End Sub

    Shared Sub SetDefault(ByVal name As String)
        defaultstore = name
    End Sub

    Shared Function GetStore(ByVal name As String) As DataStore
        If name Is Nothing Then
            name = defaultstore
        End If

        Return stores(name)
    End Function
End Class
