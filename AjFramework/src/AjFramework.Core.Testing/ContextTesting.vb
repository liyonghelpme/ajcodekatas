Imports AjFramework.Core
Imports NUnit.Framework

<TestFixture()> Public Class ContextTesting
    <Test()> Public Sub TestContext01()
        Dim ctx As New Context()
        ctx("Valor") = 1
        Assert.AreEqual(ctx("Valor"), 1)
    End Sub

    <Test()> Public Sub TestContext02()
        Dim ctx As New Context()
        ctx("Valor") = 1
        Assert.AreEqual(ctx("Valor"), 1)
        ctx("Valor") = 2
        Assert.AreEqual(ctx("Valor"), 2)
    End Sub

    <Test()> Public Sub TestContext03()
        Dim ctx As New Context()
        ctx("Contexto.Valor") = 1
        Assert.AreEqual(ctx("Contexto.Valor"), 1)
    End Sub

    <Test()> Public Sub TestContext04()
        Dim ctx As New Context()
        ctx("Valor") = 2
        ctx("Contexto.Valor") = 1
        Assert.AreEqual(ctx("Valor"), 2)
        Assert.AreEqual(ctx("Contexto.Valor"), 1)
    End Sub
End Class
