# Blazorous

_Maintainable CSS with Blazor_

## Docs

View the detailed [docs](https://chanan.github.io/Blazorous/) at https://chanan.github.io/Blazorous/.

## Install


[![NuGet Pre Release](https://img.shields.io/nuget/vpre/Blazorous.svg)](https://www.nuget.org/packages/Blazorous/)

### Change log

**Please note**: Version 0.1.0 has breaking changes. View the [change log](https://github.com/chanan/Blazorous/releases) for more details

## Setup

Add the tag helper to your page or `_ViewImports.cshtml`

```
@addTagHelper *,Blazorous
```

## Define your CSS inline

You can use one of two ways to create tags, using the Dynamic component:

```
<Dynamic TagName="div" css="@css1">this is a nice box.</Dynamic>

@functions {
  string css1 = "{ \"color\": \"red\" }";
}
```

Or using the Blazorous Css() function directly with any component:

```
<div class="@class1">this is a nice box.</div>

@functions {
  string class1 = Blazorous.BlazorousInterop.Css("{ \"color\": \"red\" }");
}
```

## Define your CSS inline using C# syntax

You can also define your styles using C# syntax:

```
<Dynamic TagName="div" css="@cssCSharp">this is a nice box.</Dynamic>

@functions {
  ICss cssCSharp = Css.CreateNew().AddRule("color", "red");
}
```

Or using the Blazorous Css() function directly with any component:

```
<div class="@classCSharp">this is a nice box.</div>

@functions {
  string classCSharp = Blazorous.BlazorousInterop.Css(Css.CreateNew().AddRule("color", "red").ToCss());
}
```

## Dynamic Rules

Dynamic rules allow you to define styles that change dynamically based on attributes on your Component. For example, if you define a Css attribute:

```
ICss coloredDiv = Css.CreateNew()
  .AddDynamicRule((css, attributes) =>
  {
    css.AddRule(<span style="color:#093">"color"</span>, attributes.GetStringAttribute(<span style="color:#093">"color"</span>, <span style="color:#093">"black"</span>));
  });
```

You can then use it wil different colors:

```
<Dynamic TagName="div" css="@coloredDiv" color="red">This &lt;div&gt; has an attribute: color="red"</Dynamic>

<Dynamic TagName="div" css="@coloredDiv" color="blue">This &lt;div&gt; has an attribute: color="blue"</Dynamic>
```

A more complete example is in the [Dynamic Rules](https://chanan.github.io/Blazorous/dynamic.html) section of the docs.

## Style Attributes

For many simple cases, you can define your CSS properties directly on the `Dynamic` tag:

```
<Dynamic TagName="div" background-color="red" color="white" font-size="50">This is awesome!</Dynamic>
```

You may define pseudo selectors, such as "hover", as attributes as well.

## Polished

Blazorous is integrated with [Polished](https://github.com/styled-components/polished) so you can use mixins such as `ellipsis`
and functions such as `lighten` and `darken`.

## Animations

Css keyframe animation can be added via the Css object:

```
<Dynamic TagName="div" css="@css"></Dynamic>

@functions {
  ICss css = Css.CreateNew()
    .AddAnimation("0.2s infinite ease-in-out alternate", animation =>
    {
        animation
        .AddKeyframe("0%", css => css.AddRule("transform", "scale(0.1)").AddRule("opacity", 0))
        .AddKeyframe("60%", css => css.AddRule("transform", "scale(1.2)").AddRule("opacity", 1))
        .AddKeyframe("100%", css => css.AddRule("transform", "scale(1)"));
    }).AddRule("width", 50)
    .AddRule("height", 50)
    .AddRule("background-color", "red");
}

```

## Fonts

Fonts can be added to elements by calling the `AddFontFace`:

```
<Dynamic TagName="h1" css="@css">Font Faces</Dynamic>

@functions {
  ICss css = Css.CreateNew()
    .AddFontface(css =>
    {
      css.AddRule("fontFamily", "Indie Flower")
        .AddRule("fontStyle", "normal")
        .AddRule("fontWeight", 400)
        .AddRule("src", "local('Indie Flower'), local('IndieFlower'), url(https://fonts.gstatic.com/s/indieflower/v9/m8JVjfNVeKWVnh3QMuKkFcZVaUuH.woff2) format('woff2')")
        .AddRule("unicodeRange", "U+0000-00FF, U+0131, U+0152-0153, U+02BB-02BC, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC, U+2122, U+2191, U+2193, U+2212, U+2215, U+FEFF, U+FFFD");
    });
}
``` 

## Themes

You can predefine themes to allow your user to switch the look of your site. First define your themes in `Program.cs`:

```
var serviceProvider = new BrowserServiceProvider(services =>
{
  services.DefineBlazorousThemes(themes =>
  {
    var theme1 = themes.CreateTheme("Soothing Web Colors");
    theme1.Variables.Add("primary", "#f23d5d");
    theme1.Variables.Add("secondary", "#8c3d5d");
    theme1.Variables.Add("font_color", "white");
    theme1.Snippets.Add("heading", Css.CreateNew().AddRule("color", "#423d5d").AddFontface(css =>
    {
      css.AddRule("fontFamily", "Fira Sans")
        .AddRule("fontStyle", "normal")
        .AddRule("fontWeight", 400)
        .AddRule("src", "local('Fira Sans Regular'), local('FiraSans-Regular'), url(https://fonts.gstatic.com/s/firasans/v8/va9E4kDNxMZdWfMOD5Vvl4jL.woff2) format('woff2')")
        .AddRule("unicodeRange", "U+0000-00FF, U+0131, U+0152-0153, U+02BB-02BC, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC, U+2122, U+2191, U+2193, U+2212, U+2215, U+FEFF, U+FFFD");
    }));

    var theme2 = themes.CreateTheme("Harry Potter");
    theme2.Variables.Add("primary", "#e10000");
    theme2.Variables.Add("secondary", "#12159f");
    theme2.Variables.Add("font_color", "white");
    theme2.Snippets.Add("heading", Css.CreateNew().AddRules("color", "#008709").AddFontface(css =>
    {
      css.AddRule("fontFamily", "Butterfly Kids")
        .AddRule("fontStyle", "normal")
        .AddRule("fontWeight", 400)
        .AddRule("src", "local('Butterfly Kids Regular'), local('ButterflyKids-Regular'), url(https://fonts.gstatic.com/s/butterflykids/v6/ll8lK2CWTjuqAsXDqlnIbMNs5R4dpRA.woff2) format('woff2')")
        .AddRule("unicodeRange", "U+0000-00FF, U+0131, U+0152-0153, U+02BB-02BC, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC, U+2122, U+2191, U+2193, U+2212, U+2215, U+FEFF, U+FFFD");
    }));
  });
});
```

Then use the theme throughout your site:

```
@functions {
    ICss _primary = Css.CreateNew().AddRules("color", "{font_color}", "background-color", "{primary}");
    ICss _secondary = Css.CreateNew().AddRules("color", "{font_color}", "background-color", "{secondary}");
    ICss _heading = Css.CreateNew().ApplyThemeSnippet("heading");
}
```

## Docs

You can see more examples in the [docs](https://chanan.github.io/Blazorous/).
