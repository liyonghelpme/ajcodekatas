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

Imports System.Data

Public MustInherit Class DataStore
    Private mConnectionString

    Public Sub New(ByVal connstr As String)
        mConnectionString = connstr
    End Sub

    Public ReadOnly Property ConnectionString()
        Get
            Return mConnectionString
        End Get
    End Property

    Public MustOverride ReadOnly Property connection() As IDbConnection
End Class
