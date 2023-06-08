using System.ComponentModel;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    public enum ColorMode
    {
        Light,
        Dark,
        [Browsable(false)]
        HighContrast
    }
}
