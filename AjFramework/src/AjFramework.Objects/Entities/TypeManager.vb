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

Imports System.Collections.Specialized

Public Class TypeManager
    Private Shared mTypes As IDictionary = New Hashtable()

    Private Shared Sub RegisterDefault()
        mTypes("Integer") = IntegerType.GetInstance
        mTypes("String") = StringType.GetInstance
        mTypes("Date") = DateType.GetInstance
        mTypes("Decimal") = DecimalType.GetInstance
    End Sub

    Shared Sub New()
        RegisterDefault()
    End Sub

    Public Shared Sub Register(ByVal name As String, ByVal type As IType)
        mTypes(name) = type
    End Sub

    Public Shared ReadOnly Property Type(ByVal name As String) As IType
        Get
            Return mTypes(name)
        End Get
    End Property
End Class
