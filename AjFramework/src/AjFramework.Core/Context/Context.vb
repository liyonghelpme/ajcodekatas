Public Class Context
    Inherits GenericContext

    Private Const DotSeparator As Char = "."c

    Default Public Overrides Property Item(ByVal name As String) As Object
        Get
            Dim p As Integer

            p = name.IndexOf(DotSeparator)

            If p >= 0 Then
                Dim ctx As IContext

                ctx = DirectCast(MyBase.Item(name.Substring(0, p)), IContext)

                If ctx Is Nothing Then
                    Return Nothing
                End If

                Return ctx(name.Substring(p + 1))
            End If

            Return MyBase.Item(name)
        End Get
        Set(ByVal Value As Object)
            Dim p As Integer

            p = name.IndexOf(DotSeparator)

            If p >= 0 Then
                Dim ctx As IContext
                Dim simpleName As String

                simpleName = name.Substring(0, p)

                ctx = DirectCast(MyBase.Item(simpleName), IContext)

                If ctx Is Nothing Then
                    ctx = New Context()
                    MyBase.Item(simpleName) = ctx
                End If

                ctx.Item(name.Substring(p + 1)) = Value
            Else
                MyBase.Item(name) = Value
            End If
        End Set
    End Property
End Class

