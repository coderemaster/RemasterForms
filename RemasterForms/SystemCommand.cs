
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    using static NativeMethods;

    public enum SystemCommand : int
    {
        Size        = SC_SIZE,
        Move        = SC_MOVE,
        Minimize    = SC_MINIMIZE,
        Maximize    = SC_MAXIMIZE,
        Close       = SC_CLOSE,
        Restore     = SC_RESTORE,
        Default     = SC_DEFAULT,
        ContextHelp = SC_CONTEXTHELP
    }
}
