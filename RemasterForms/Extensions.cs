using System;
using System.Drawing;
using System.Windows.Forms;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    using static NativeMethods;

    public static partial class Extensions
    {
        // Padding.Divide
        public static Padding Divide(this Padding _this, int divider)
        {
            return new Padding(
                _this.Left   / divider,
                _this.Top    / divider,
                _this.Right  / divider,
                _this.Bottom / divider);
        }

        // Padding.IsEmpty
        public static bool IsEmpty(this Padding _this)
        {
            return _this == Padding.Empty;
        }

        // Padding.Multiply
        public static Padding Multiply(this Padding _this, int multiplier)
        {
            return new Padding(
                _this.Left   * multiplier,
                _this.Top    * multiplier,
                _this.Right  * multiplier,
                _this.Bottom * multiplier);
        }

        // Point.ToLParam
        public static IntPtr ToLParam(this Point _this)
        {
            return MakeLParam(_this.X, _this.Y);
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

        // Rectangle.Grow
        public static Rectangle Grow(this Rectangle _this, int left, int top, int right, int bottom)
        {
            return Rectangle.FromLTRB(
                _this.Left   - left  ,
                _this.Top    - top   ,
                _this.Right  + right ,
                _this.Bottom + bottom);
        }
        //
        public static Rectangle Grow(this Rectangle _this, Padding padding)
        {
            return Rectangle.FromLTRB(
                _this.Left   - padding.Left  ,
                _this.Top    - padding.Top   ,
                _this.Right  + padding.Right ,
                _this.Bottom + padding.Bottom);
        }

        // Rectangle.Shrink
        public static Rectangle Shrink(this Rectangle _this, int left, int top, int right, int bottom)
        {
            return Rectangle.FromLTRB(
                _this.Left   + left  ,
                _this.Top    + top   ,
                _this.Right  - right ,
                _this.Bottom - bottom);
        }
        //
        public static Rectangle Shrink(this Rectangle _this, Padding padding)
        {
            return Rectangle.FromLTRB(
                _this.Left   + padding.Left  ,
                _this.Top    + padding.Top   ,
                _this.Right  - padding.Right ,
                _this.Bottom - padding.Bottom);
        }

        // Rectangle.Xor (union minus intersection)
        public static Padding Xor(this Rectangle _this, Rectangle other)
        {
            return new Padding(
                Math.Abs(other.Left   - _this.Left  ),
                Math.Abs(other.Top    - _this.Top   ),
                Math.Abs(_this.Right  - other.Right ),
                Math.Abs(_this.Bottom - other.Bottom));
        }
    }
}
