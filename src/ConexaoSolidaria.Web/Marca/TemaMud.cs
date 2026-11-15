using MudBlazor;

namespace ConexaoSolidaria.Web.Marca;

// Paleta da marca (mesmos valores do protótipo) nos componentes MudBlazor: gráfico, seletores e diálogos.
public static class TemaMud
{
    public static readonly MudTheme Marca = new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#0E7C66",
            Secondary = "#CF4820",
            Tertiary = "#F9B935",
            Info = "#2F67B5",
            Success = "#0E7C66",
            Warning = "#8F5B00",
            Error = "#B23A2E",
            Background = "#F3F6F4",
            Surface = "#FFFFFF",
            TextPrimary = "#10241F",
            TextSecondary = "#5B6F69",
            LinesDefault = "#D9E3DF",
            AppbarBackground = "#FFFFFF"
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#3FBF9C",
            Secondary = "#FF7A52",
            Tertiary = "#FFC94D",
            Info = "#83ABEB",
            Success = "#3FBF9C",
            Warning = "#E8B04A",
            Error = "#F08478",
            Background = "#0C1714",
            Surface = "#13221E",
            TextPrimary = "#E7F0EC",
            TextSecondary = "#9DB3AC",
            LinesDefault = "#253A34"
        },
        Typography = new Typography
        {
            Default = new DefaultTypography { FontFamily = ["Figtree", "system-ui", "sans-serif"] }
        },
        LayoutProperties = new LayoutProperties { DefaultBorderRadius = "12px" }
    };
}
