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

Public MustInherit Class CollectionFunctor
    Implements ICollectionFunctor

    Public Function Apply(ByVal obj As Object) As Object Implements IFunctor.Apply
        If TypeOf obj Is ICollection Then
            Return Apply(CType(obj, ICollection))
        End If

        Throw New ArgumentException("Se esperaba colección")
    End Function

    Protected Overridable Function Init() As Object
        Return Nothing
    End Function

    Protected Overridable Sub Eval(ByVal item As Object, ByRef data As Object)

    End Sub

    Public Overridable Function Apply(ByVal items As ICollection) As Object Implements ICollectionFunctor.Apply
        Dim data As Object

        data = Init()

        Dim item As Object

        For Each item In items
            Eval(item, data)
        Next

        Return data
    End Function
End Class
