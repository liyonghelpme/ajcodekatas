Public Class Application
    Private Shared mContext As Context

    Public Shared ReadOnly Property Context() As Context
        Get
            If mContext Is Nothing Then
                mContext = New Context()
                System.Configuration.ConfigurationManager.GetSection("AjFramework")
            End If

            Return mContext
        End Get
    End Property
End Class
