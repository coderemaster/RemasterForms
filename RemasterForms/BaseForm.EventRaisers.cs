using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    internal partial class DisableDesigner { }

    public partial class BaseForm : Form // EventRaisers
    {
        // OnColorModeChanged
        protected virtual void OnColorModeChanged(EventArgs e)
        {
        }

        // OnNonClientActiveChanged
        internal virtual void OnNonClientActiveChanged(EventArgs e)
        {
            if (!BorderMetrics.ClientEdges.IsEmpty())
            {
                var rect = ClientRectangle.Translate(this);

                using (var region = new Region(rect))
                {
                    region.Exclude(rect.Deflate(BorderMetrics.ClientEdges));
                    Invalidate(region, true);
                }
            }
        }

        // OnNonClientMetricsChanged
        protected virtual void OnNonClientMetricsChanged(EventArgs e)
        {
        }

        // OnPaintBackground
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var clip = ClientRectangle.Translate(this);
            var rect = WindowStyles.LayoutRtl 
                ? clip.Deflate(1, 0, 0, 1) 
                : clip.Deflate(0, 0, 1, 1);

            e.Graphics.SetClip(clip);

            if (HighContrastLayout)
            {
                var captionColor = NonClientActive
                    ? SystemColors.ActiveCaption
                    : SystemColors.InactiveCaption;

                e.Graphics.Clear(captionColor);

                if (!BorderMetrics.ClientEdges.IsEmpty())
                {
                    var borderColor = NonClientActive
                        ? SystemColors.ActiveCaptionText
                        : SystemColors.InactiveCaptionText;

                    using (var pen = new Pen(borderColor, 1))
                        e.Graphics.DrawRectangle(pen, rect);

                    clip = clip.Deflate(BorderMetrics.ClientEdges);
                }
            }
            else
            {
                e.Graphics.Clear(Color.Transparent);

                if (DwmFrame != null && DwmFrame.GlassInsets.Top == 2)
                {
                    if (WindowStyles.ToolWindow)
                    {
                        if (!NonClientActive)
                        {
                            var windowEdgeColor = DwmFrame.DarkMode
                                ? Color.FromArgb(60, 60, 60)
                                : Color.FromArgb(200, 200, 200);

                            using (var pen = new Pen(windowEdgeColor, 1))
                                e.Graphics.DrawRectangle(pen, rect);
                        }

                        clip = clip.Deflate(1);
                    }
                    else
                    {
                        clip = clip.Deflate(0, 1, 0, 0);
                    }
                }
            }

            e.Graphics.SetClip(clip);

            using (var brush = new SolidBrush(BackColor))
                e.Graphics.FillRectangle(brush, clip);
        }

        // OnPaintNonClient
        /// <summary>
        /// Used only at design-time.
        /// </summary>
        /// <param name="e"></param>
        internal virtual void OnPaintNonClient(PaintEventArgs e)
        {
            new VisualStyleRenderer(VisualStyleElement.Window.Dialog.Normal)
                .DrawParentBackground(e.Graphics, e.ClipRectangle, this);
        }

        // OnRightToLeftChanged
        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);

            if (RightToLeftLayout)
                Invalidate();
        }

        // OnRightToLeftLayoutChanged
        protected override void OnRightToLeftLayoutChanged(EventArgs e)
        {
            base.OnRightToLeftLayoutChanged(e);

            if (RightToLeft == RightToLeft.Yes)
                Invalidate();
        }

        // OnStyleChanged
        protected override void OnStyleChanged(EventArgs e)
        {
            if (!IsHandleCreated)
            {
                WindowStyles = new WindowStyles(CreateParams);

                if (!_designTime)
                {
                    DefaultFrameMetrics = FrameMetrics.Default(WindowStyles.ToolWindow);
                    SystemFrameMetrics  = new FrameMetrics(WindowStyles);
                }
            }

            base.OnStyleChanged(e);

            RestoreClientArea();
        }

        // OnTextChanged
        protected override void OnTextChanged(EventArgs e)
        {
            // overriding prevents the handle from being re-created
            TextChanged?.Invoke(this, e);
        }

        // OnWindowStateChanged
        /// <summary>
        /// Triggered only if handle is created.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnWindowStateChanged(EventArgs e)
        {
        }
    }
}
