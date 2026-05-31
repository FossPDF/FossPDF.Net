### Font subsetting glue package

This package makes it trivial to enable font subsetting in FossPDF.

In your application initialisation logic (e.g. in `Program.cs`), add the following line:

```c#
FontManager.RegisterSubsetCallback(BuiltInFontSubsetting.Callback);
```

This will apply to all documents created, as the `FontManager` is a global singleton.
