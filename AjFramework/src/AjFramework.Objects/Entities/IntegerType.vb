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

Public Class IntegerType
    Implements IType

    Private Shared mInstance As New IntegerType()

    Shared Function GetInstance()
        Return mInstance
    End Function

    Private Sub New()

    End Sub

    Public Function GetDefaultValue() As Object Implements IType.GetDefaultValue
        Return Nothing
    End Function

    Function Convert(ByVal value As Object) As Object Implements IType.Convert
        If value Is Nothing Then
            Return 0
        End If

        If TypeOf value Is String AndAlso value = "" Then
            Return 0
        End If

        Return CInt(value)
    End Function

    Function NewInstance() As Object Implements IType.NewInstance
        Return 0
    End Function
End Class
