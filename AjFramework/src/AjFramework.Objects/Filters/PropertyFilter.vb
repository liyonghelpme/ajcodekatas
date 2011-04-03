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

Public Class PropertyFilter
    Inherits Filter

    Private mName As String
    Private mOperator As PropertyOperator
    Private mValue As Object

    Public Sub New(ByVal name As String, ByVal [operator] As PropertyOperator, ByVal value As Object)
        mName = name
        mOperator = [operator]
        mValue = value
    End Sub

    Public Sub New(ByVal name As String, ByVal [operator] As String, ByVal value As Object)
        mName = name
        mValue = value

        Select Case [operator]
            Case "="
                mOperator = PropertyOperator.Equals
            Case "<>"
                mOperator = PropertyOperator.NotEquals
            Case "<"
                mOperator = PropertyOperator.LessThan
            Case ">"
                mOperator = PropertyOperator.GreaterThan
            Case "<="
                mOperator = PropertyOperator.LessOrEquals
            Case ">="
                mOperator = PropertyOperator.GreaterOrEquals
            Case "contains"
                mOperator = PropertyOperator.Contains
            Case Else
                Throw New ArgumentException("Operador " + [operator] + " desconocido")
        End Select
    End Sub

    Public Overrides Function Apply(ByVal obj As Object) As Boolean
        Dim objval = ToObject(obj).GetValue(mName)

        Select Case mOperator
            Case PropertyOperator.Equals
                Return objval = mValue
            Case PropertyOperator.GreaterOrEquals
                Return objval >= mValue
            Case PropertyOperator.GreaterThan
                Return objval > mValue
            Case PropertyOperator.LessOrEquals
                Return objval <= mValue
            Case PropertyOperator.LessThan
                Return objval < mValue
            Case PropertyOperator.NotEquals
                Return objval <> mValue
            Case PropertyOperator.Contains
                Return objval.ToString.IndexOf(mValue.ToString) >= 0
        End Select

    End Function
End Class

Public Enum PropertyOperator
    Equals = 0
    NotEquals = 1
    GreaterThan = 2
    LessThan = 3
    GreaterOrEquals = 4
    LessOrEquals = 5
    Contains = 6
End Enum
