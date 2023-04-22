using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    using static NativeMethods;

    internal partial class DisableDesigner { }

    public partial class BaseForm : Form // EventRaisers
    {
        // OnHighContrastChanged
        /// <summary>
        /// Triggered when the system changes the high contrast theme.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnHighContrastChanged(EventArgs e) { }

        // OnNonClientActiveChanged
        protected virtual void OnNonClientActiveChanged(EventArgs e) { }

        // OnNonClientMetricsChanged
        /// <summary>
        /// Triggered when the system changes non-client metrics.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnNonClientMetricsChanged(EventArgs e) { }

        // OnPaintNonClient
        /// <summary>
        /// Used at design-time.
        /// </summary>
        /// <param name="e"></param>
        internal virtual void OnPaintNonClient(PaintEventArgs e)
        {
            var renderer = new VisualStyleRenderer(VisualStyleElement.Window.Dialog.Normal);

            e.Graphics.ExcludeClip(e.ClipRectangle.Shrink(NonClientPadding));
            renderer.DrawParentBackground(e.Graphics, e.ClipRectangle, this);
        }

        // OnStyleChanged
        protected override void OnStyleChanged(EventArgs e)
        {
            base.OnStyleChanged(e);

            if (IsMaximized)
                UpdateMaximizedBounds();
        }

        // OnTextChanged
        protected override void OnTextChanged(EventArgs e)
        {
            // overriding prevents the handle from being re-created
            TextChanged?.Invoke(this, e);
        }

        // OnWindowStateChanged
        protected virtual void OnWindowStateChanged(EventArgs e) { }
    }
}
