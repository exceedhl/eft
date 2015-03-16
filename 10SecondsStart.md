1 Prepare the enviroment
> Eft is tested under my Windows XP with .Net 3.0 installed. Reference Eft.dll and Windows UI automation dlls: UIAutomationTypes.dll and UIAutomationClient.dll in your test project, then you can start writing Eft tests.

2 Use Eft with unit test framework, for example:
```

Application app = Application.Run("calc");
Window main = app.FindTopWindow("Calculator");

main.FindFirst("[name='1']").Click();
main.FindFirst("[name='+']").Click();
main.FindFirst("[name='2']").Click();
main.FindFirst("[name='=']").Click();

Assert.AreEqual("3. ", main.FindFirst("Edit").Text);

```

For more features in Eft, you can download the introduction [ppt](http://eft.googlecode.com/files/eft.ppt) and have a look.

