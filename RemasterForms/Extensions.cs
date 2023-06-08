using System;
using System.Drawing;
using System.Windows.Forms;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    using static NativeMethods;

    public static partial class Extensions
    {
        // Padding.IsEmpty
        public static bool IsEmpty(this Padding _this)
        {
            return _this == Padding.Empty;
        }

        // Rectangle.Deflate
        public static Rectangle Deflate(this Rectangle _this, int left, int top, int right, int bottom)
        {
            return Rectangle.FromLTRB(
                _this.Left   + left  ,
                _this.Top    + top   ,
                _this.Right  - right ,
                _this.Bottom - bottom);
        }
        //
        public static Rectangle Deflate(this Rectangle _this, int all)
        {
            return Rectangle.FromLTRB(
                _this.Left   + all,
                _this.Top    + all,
                _this.Right  - all,
                _this.Bottom - all);
        }
        //
        public static Rectangle Deflate(this Rectangle _this, Padding padding)
        {
            return Rectangle.FromLTRB(
                _this.Left   + padding.Left  ,
                _this.Top    + padding.Top   ,
                _this.Right  - padding.Right ,
                _this.Bottom - padding.Bottom);
        }

        // Rectangle.Exclude
        public static Padding Exclude(this Rectangle _this, Rectangle other)
        {
            return new Padding(
                Math.Max(0, other.Left   - _this.Left  ),
                Math.Max(0, other.Top    - _this.Top   ),
                Math.Max(0, _this.Right  - other.Right ),
                Math.Max(0, _this.Bottom - other.Bottom));
        }

        // Rectangle.Inflate
        public static Rectangle Inflate(this Rectangle _this, int left, int top, int right, int bottom)
        {
            return Rectangle.FromLTRB(
                _this.Left   - left  ,
                _this.Top    - top   ,
                _this.Right  + right ,
                _this.Bottom + bottom);
        }
        //
        public static Rectangle Inflate(this Rectangle _this, int all)
        {
            return Rectangle.FromLTRB(
                _this.Left   - all,
                _this.Top    - all,
                _this.Right  + all,
                _this.Bottom + all);
        }
        //
        public static Rectangle Inflate(this Rectangle _this, Padding padding)
        {
            return Rectangle.FromLTRB(
                _this.Left   - padding.Left  ,
                _this.Top    - padding.Top   ,
                _this.Right  + padding.Right ,
                _this.Bottom + padding.Bottom);
        }

        // Rectangle.Translate
        public static Rectangle Translate(this Rectangle _this, Form form)
        {
            if (form.RightToLeftLayout && form.RightToLeft == RightToLeft.Yes)
                _this.Offset(-1, 0);

            return _this;
        }
    }
}
