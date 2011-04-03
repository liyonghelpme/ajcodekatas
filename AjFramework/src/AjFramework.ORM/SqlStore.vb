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

Imports System.Data.SqlClient

Public Class SqlStore
    Inherits DataStore

    Public Sub New(ByVal strconn As String)
        MyBase.New(strconn)
    End Sub

    Public Overrides ReadOnly Property Connection() As IDbConnection
        Get
            Return New SqlConnection(ConnectionString)
        End Get
    End Property
End Class
