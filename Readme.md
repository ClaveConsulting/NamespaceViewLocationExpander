# NamespaceViewLocationExpander

This package enables structuring your MVC projects in a feature-based way, where the model, view and controller are all located together instead of in separate `Models/`, `Views/` and `Controllers/` folders. In other words, instead of grouping files by type they are grouped by feature.

```
📂 Project
  📓 Program.cs
  📓 Startup.cs
  📂 Pages
    📄 _ViewImports.cshtml
    📄 _ViewStart.cshtml
    📂 Home
      📄 About.cshtml
      📄 Contact.cshtml
      📄 Error.cshtml
      📓 ErrorViewModel.cs
      📓 HomeController.cs
      📄 Index.cshtml
      📄 Privacy.cshtml
    📂 Shared
      📄 _CookieConsentPartial.cshtml
      📄 _Layout.cshtml
      📄_ValidationScriptsPartial.cshtl
```

## How to use

To configure this layout you need to call `UseNamespaceViewLocations` in `ConfigureServices`

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Call UseNamespaceViewLocations()
        services
            .UseNamespaceViewLocations()
            .AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }
}
```

## How it works

The default view resolver uses the name of the controller (minus the Controller suffix) and the name of the action. This project uses the namespace of the controller instead. This way, as long as the namespace matches the folder structure, you can put the view in the same folder as the controller and things will just work.

## How it works in detail

The way razor views are found in ASP.Net MVC Core is by using [ViewLocationFormats](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razor.razorviewengineoptions.viewlocationformats?view=aspnetcore-2.1), which by default is the following list:

```csharp
new string[]
{
    "~/Views/{1}/{0}.cshtml",
    "~/Views/{1}/{0}.vbhtml",
    "~/Views/Shared/{0}.cshtml",
    "~/Views/Shared/{0}.vbhtml"
};
```

Each entry is a formatable string where `{0}` is the name of the action and `{1}` is the name of the controller. The `NamespaceViewLocationExpander` adds support for `{namespace}` which will be replace with the namespace of the controller, and the `UseNamespaceViewLocations()` extension method insterts the formatable string `/{namespace}/{0}.cshtml` in the list.

`UseNamespaceViewLocations` will by default use the assembly name as a prefix to remove from the namespace, since the namespace will most likely contain the project name too. You can call `UseNamespaceViewLocations("MyDemoProject")` with a custom prefix to remove instead, if the assembly name does not match the default namespace of the project.

## License

The MIT license