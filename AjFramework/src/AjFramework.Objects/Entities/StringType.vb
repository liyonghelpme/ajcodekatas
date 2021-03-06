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

Public Class StringType
    Implements IType

    Private Shared mInstance As New StringType()

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
            Return Nothing
        End If

        If TypeOf value Is Byte() Then
            Return Nothing
        End If

        Return CStr(value)
    End Function

    Function NewInstance() As Object Implements IType.NewInstance
        Return String.Empty
    End Function

End Class
