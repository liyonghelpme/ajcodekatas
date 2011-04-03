Imports System.Xml

Public Class ParametersConfigurationHandler
    Implements IConfigurationHandler

    Sub Process(ByVal parent As IConfigurationHandler, ByVal ctx As IContext, ByVal section As XmlNode) Implements IConfigurationHandler.Process
        Dim node As XmlNode

        For Each node In section.SelectNodes("Parameter")
            Dim name As String
            Dim value As Object

            name = node.Attributes("Name").Value
            value = node.Attributes("Value").Value

            If Not node.Attributes("Type") Is Nothing Then
                Dim tp As Type
                tp = Type.GetType(node.Attributes("Type").Value)
                value = Convert.ChangeType(value, tp)
            End If

            ctx(name) = value
        Next
    End Sub
End Class
