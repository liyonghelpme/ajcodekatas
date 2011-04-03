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

Imports System.Reflection

Module Types
    Private mTypeNames As IDictionary = New Hashtable()

    Function GetNames(ByVal type As Type) As IList
        Dim names As IList

        names = mTypeNames(type)

        If Not names Is Nothing Then
            Return names
        End If

        names = New ArrayList()

        Dim fld As FieldInfo

        For Each fld In type.GetFields()
            names.Add(fld.Name)
        Next

        Dim prop As PropertyInfo

        For Each prop In type.GetProperties()
            If prop.CanRead And prop.CanWrite Then
                names.Add(prop.Name)
            End If
        Next

        mTypeNames(type) = names

        Return names
    End Function
End Module
