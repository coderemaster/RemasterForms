using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    internal partial class DisableDesigner { }

    public partial class BaseForm : Form // NotSupported
    {
        #region ## properties

        // AutoScroll
        /// <summary>
        /// not supported
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoScroll
        {
            get => false;
            set { }
        }

        // AutoScrollMargin
        /// <summary>
        /// not supported
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size AutoScrollMargin
        {
            get => Size.Empty;
            set { }
        }

        // AutoScrollMinSize
        /// <summary>
        /// not supported
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size AutoScrollMinSize
        {
            get => Size.Empty;
            set { }
        }

        // IsMdiChild
        /// <summary>
        /// not supported
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool IsMdiChild => false;

        // IsMdiContainer
        /// <summary>
        /// not supported
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool IsMdiContainer
        {
            get => false;
            set { }
        }

        // MainMenuStrip
        /// <summary>
        /// not supported
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new MenuStrip MainMenuStrip
        {
            get => null;
            set { }
        }

        // MdiChildren
        /// <summary>
        /// not supported
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Form[] MdiChildren { get; } = { };

        // MdiParent
        /// <summary>
        /// not supported
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Form MdiParent
        {
            get => null;
            set { }
        }

        // ShowIcon
        /// <summary>
        /// not supported
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool ShowIcon
        {
            get => true;
            set { }
        }

        // SizeGripStyle
        /// <summary>
        /// not supported
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new SizeGripStyle SizeGripStyle
        {
            get => SizeGripStyle.Hide;
            set { }
        }

        #endregion properties

        #region ## methods

        // ActivateMdiChild
        /// <summary>
        /// not supported
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected new void ActivateMdiChild(Form form) { }

        // AdjustFormScrollbars
        /// <summary>
        /// not supported
        /// </summary>
        /// <param name="displayScrollbars"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void AdjustFormScrollbars(bool displayScrollbars)
        {
            base.AdjustFormScrollbars(false);
        }

        // LayoutMdi
        /// <summary>
        /// not supported
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new void LayoutMdi(MdiLayout value) { }

        // OnMdiChildActivate
        /// <summary>
        /// not supported
        /// </summary>
        /// <param name="e"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void OnMdiChildActivate(EventArgs e) { }

        #endregion methods

        #region ## events

        // MdiChildActivate
        /// <summary>
        /// not supported
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new event EventHandler MdiChildActivate
        {
            add { }
            remove { }
        }

        #endregion events
    }
}
