
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    using static NativeMethods;

    public enum CornerStyle : int
    {
        Default    = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_DEFAULT,
        DoNotRound = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_DONOTROUND,
        Round      = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND,
        RoundSmall = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUNDSMALL
    }
}
