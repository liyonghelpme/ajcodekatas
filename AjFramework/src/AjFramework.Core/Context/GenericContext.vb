Public Class GenericContext
    Implements IContext

    Private mValues As New Hashtable()

    Default Public Overridable Property Item(ByVal name As String) As Object Implements IContext.Item
        Get
            Return mValues(name)
        End Get
        Set(ByVal Value As Object)
            mValues(name) = Value
        End Set
    End Property
End Class

