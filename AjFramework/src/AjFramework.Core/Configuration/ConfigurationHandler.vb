Imports System.Xml
Imports System.Configuration

Public Interface IConfigurationHandler
    Sub Process(ByVal parent As IConfigurationHandler, ByVal cfg As IContext, ByVal section As XmlNode)
End Interface

Public Interface IConfigurationHandlerFactory
    Function CreateHandler() As IConfigurationHandler
End Interface

Public Class ConfigurationHandler
    Implements IConfigurationSectionHandler, IConfigurationHandler

    Private Shared mHandlers As New System.Collections.Hashtable()

    Public Shared Property Handler(ByVal name As String) As String
        Get
            Dim typeName As String

            typeName = DirectCast(mHandlers(name), String)

            If typeName Is Nothing Then
                typeName = String.Format("AjFramework.Core.{0}ConfigurationHandler, AjFramework.Core", name)
            End If

            Return typeName
        End Get
        Set(ByVal Value As String)
            mHandlers(name) = Value
        End Set
    End Property

    Public Shared ReadOnly Property ConfigurationHandler(ByVal name As String) As IConfigurationHandler
        Get
            Dim handlerType As String = Handler(name)

         Try
            Return DirectCast(Activator.CreateInstance(Type.GetType(handlerType), New Object() {}), IConfigurationHandler)
         Catch ex As Exception
            Return Nothing
         End Try
        End Get
    End Property

    Public Function Create(ByVal parent As Object, ByVal configContext As Object, ByVal section As XmlNode) As Object Implements IConfigurationSectionHandler.Create
        Process(Nothing, Application.Context, section)

        Return Application.Context
    End Function

    Sub Process(ByVal parent As IConfigurationHandler, ByVal ctx As IContext, ByVal section As XmlNode) Implements IConfigurationHandler.Process
        Dim node As XmlNode

      For Each node In section.ChildNodes
         Dim ch As IConfigurationHandler

         ch = ConfigurationHandler(node.Name)

         If Not ch Is Nothing Then
            ch.Process(Me, ctx, node)
         End If
      Next
    End Sub
End Class
