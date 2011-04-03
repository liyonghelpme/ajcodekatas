Imports System.Xml
Imports System.Reflection

Public Class ObjectsConfigurationHandler
    Implements IConfigurationHandler

    Sub Process(ByVal parent As IConfigurationHandler, ByVal ctx As IContext, ByVal section As XmlNode) Implements IConfigurationHandler.Process
        Dim node As XmlNode

        For Each node In section.SelectNodes("Object")
            Dim t As System.Type = System.Type.GetType(node.Attributes("Type").Value)
            Dim obj As Object

            Dim pars As New ArrayList()
            Dim par As XmlNode

            For Each par In node.SelectNodes("Parameter")
                Dim value As String = par.Attributes("Value").Value

                If par.Attributes("Type") Is Nothing Then
                    pars.Add(value)
                Else
                    Dim tp As Type
                    tp = Type.GetType(par.Attributes("Type").Value)
                    pars.Add(Convert.ChangeType(value, tp))
                End If
            Next

            Dim args As Object()

            args = DirectCast(pars.ToArray(GetType(Object)), Object())

            obj = Activator.CreateInstance(t, args)

            ctx(node.Attributes("Name").Value) = obj

            Dim prop As XmlNode

            For Each prop In node.SelectNodes("Property")
                Dim pi As PropertyInfo = t.GetProperty(prop.Attributes("Name").Value)
                Dim value As Object = Convert.ChangeType(prop.Attributes("Value").Value, pi.PropertyType)
                t.InvokeMember(prop.Attributes("Name").Value, Reflection.BindingFlags.SetProperty Or Reflection.BindingFlags.SetField, Nothing, obj, New Object() {value})
            Next
        Next
    End Sub
End Class
