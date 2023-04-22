
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    using static NativeMethods;

    public enum NonClientCode : int
    {
        Nowhere     = HTNOWHERE,
        /// <summary>
        /// caption background
        /// </summary>
        Caption     = HTCAPTION,
        /// <summary>
        /// caption icon
        /// </summary>
        SysMenu     = HTSYSMENU,
        /// <summary>
        /// minimize button (restore when minimized)
        /// </summary>
        MinButton   = HTMINBUTTON,
        /// <summary>
        /// maximize button (restore when maximized)
        /// </summary>
        MaxButton   = HTMAXBUTTON,
        /// <summary>
        /// sizing border
        /// </summary>
        Left        = HTLEFT,
        /// <summary>
        /// sizing border
        /// </summary>
        Right       = HTRIGHT,
        /// <summary>
        /// sizing border
        /// </summary>
        Top         = HTTOP,
        /// <summary>
        /// sizing border
        /// </summary>
        TopLeft     = HTTOPLEFT,
        /// <summary>
        /// sizing border
        /// </summary>
        TopRight    = HTTOPRIGHT,
        /// <summary>
        /// sizing border
        /// </summary>
        Bottom      = HTBOTTOM,
        /// <summary>
        /// sizing border
        /// </summary>
        BottomLeft  = HTBOTTOMLEFT,
        /// <summary>
        /// sizing border
        /// </summary>
        BottomRight = HTBOTTOMRIGHT,
        /// <summary>
        /// fixed border
        /// </summary>
        Border      = HTBORDER,
        /// <summary>
        /// close button
        /// </summary>
        Close       = HTCLOSE,
        /// <summary>
        /// help button
        /// </summary>
        Help        = HTHELP
    }

}
