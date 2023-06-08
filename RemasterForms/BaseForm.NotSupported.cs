using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    internal partial class DisableDesigner { }

    public partial class BaseForm : Form // NotSupported
    {
        #region ## filter

        private static readonly string[] removedProperties = new[]
        {
            "DoubleBuffered"
        };

        private IDesignerHost Host;
        private FilterService<BaseForm> Service;

        public override ISite Site
        {
            get => base.Site;
            set
            {
                if (Host != null)
                {
                    RemoveFilter();
                    Host = null;
                }

                base.Site = value;

                if (value != null)
                {
                    Host = (IDesignerHost)Site.GetService(typeof(IDesignerHost));

                    if (Host != null)
                    {
                        if (Host.Loading)
                            Host.LoadComplete += Host_LoadComplete;
                        else
                            AddFilter();
                    }
                }
            }
        }

        private void Host_LoadComplete(object sender, EventArgs e)
        {
            Host.LoadComplete -= Host_LoadComplete;
            AddFilter();
        }

        private void AddFilter()
        {
            var baseFilter = (ITypeDescriptorFilterService)Host.GetService(typeof(ITypeDescriptorFilterService));

            if (baseFilter != null)
            {
                Host.RemoveService(typeof(ITypeDescriptorFilterService));
                Service = new FilterService<BaseForm>(baseFilter, removedProperties);
                Host.AddService(typeof(ITypeDescriptorFilterService), Service);
                TypeDescriptor.Refresh(GetType());
            }
        }

        private void RemoveFilter()
        {
            if (Service != null)
            {
                Host.RemoveService(typeof(ITypeDescriptorFilterService));
                Host.AddService(typeof(ITypeDescriptorFilterService), Service.BaseService);
                Service = null;
            }
        }

        #endregion filter

        #region ## properties

        // AutoScroll
        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoScroll
        {
            get => base.AutoScroll;
            set { }
        }

        // AutoScrollMargin
        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size AutoScrollMargin
        {
            get => base.AutoScrollMargin;
            set { }
        }

        // AutoScrollMinSize
        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size AutoScrollMinSize
        {
            get => base.AutoScrollMinSize;
            set { }
        }

        // BackgroundImage
        /// <summary>
        /// Currently not supported.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage
        {
            get => base.BackgroundImage;
            set => base.BackgroundImage = value;
        }

        // BackgroundImageLayout
        /// <summary>
        /// Currently not supported.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ImageLayout BackgroundImageLayout
        {
            get => base.BackgroundImageLayout;
            set => base.BackgroundImageLayout = value;
        }

        // IsMdiChild
        /// <summary>
        /// Not supported.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool IsMdiChild
        {
            get => base.IsMdiChild;
        }

        // IsMdiContainer
        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool IsMdiContainer
        {
            get => base.IsMdiContainer;
        }

        // MainMenuStrip
        /// <summary>
        /// Currently not supported.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new MenuStrip MainMenuStrip
        {
            get => base.MainMenuStrip;
            set => base.MainMenuStrip = value;
        }

        // MdiChildren
        /// <summary>
        /// Not supported.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Form[] MdiChildren
        {
            get => base.MdiChildren;
        }

        // MdiParent
        /// <summary>
        /// Not supported.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Form MdiParent
        {
            get => base.MdiParent;
            set => base.MdiParent = value;
        }

        #endregion properties

        #region ## methods

        // ActivateMdiChild
        /// <summary>
        /// Not supported.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected new void ActivateMdiChild(Form form)
        {
        }

        // AdjustFormScrollbars
        /// <summary>
        /// Not supported.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void AdjustFormScrollbars(bool displayScrollbars)
        {
            base.AdjustFormScrollbars(false);
        }

        // LayoutMdi
        /// <summary>
        /// Not supported.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new void LayoutMdi(MdiLayout value)
        {
        }

        // OnMdiChildActivate
        /// <summary>
        /// Not supported.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void OnMdiChildActivate(EventArgs e)
        {
        }

        #endregion methods

        #region ## events

        // BackgroundImageChanged
        /// <summary>
        /// Currently not supported.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new event EventHandler BackgroundImageChanged
        {
            add { }
            remove { }
        }

        // BackgroundImageLayoutChanged
        /// <summary>
        /// Currently not supported.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new event EventHandler BackgroundImageLayoutChanged
        {
            add { }
            remove { }
        }

        // MdiChildActivate
        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new event EventHandler MdiChildActivate
        {
            add { }
            remove { }
        }

        // HelpButtonClicked
        /// <summary>
        /// Not supported.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new event EventHandler HelpButtonClicked
        {
            add { }
            remove { }
        }

        #endregion events
    }
}
