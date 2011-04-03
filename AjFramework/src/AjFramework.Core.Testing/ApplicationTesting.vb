Imports AjFramework.Core
Imports NUnit.Framework

<TestFixture()> Public Class ApplicationTesting
    <Test()> Public Sub TestApplication01()
        Application.Context("Valor") = 1
        Assert.AreEqual(Application.Context("Valor"), 1)
    End Sub

    <Test()> Public Sub TestApplication02()
        Application.Context("Valor") = 1
        Assert.AreEqual(Application.Context("Valor"), 1)
        Application.Context("Valor") = 2
        Assert.AreEqual(Application.Context("Valor"), 2)
    End Sub

    <Test()> Public Sub TestApplication03()
        Application.Context("Contexto.Valor") = 1
        Assert.AreEqual(Application.Context("Contexto.Valor"), 1)
    End Sub

    <Test()> Public Sub TestContext04()
        Application.Context("Valor") = 2
        Application.Context("Contexto.Valor") = 1
        Assert.AreEqual(Application.Context("Valor"), 2)
        Assert.AreEqual(Application.Context("Contexto.Valor"), 1)
    End Sub
End Class
