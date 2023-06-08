using System;
using System.Windows.Forms;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RemasterForms
{
    public struct BorderMetrics
    {
        #region ## fields

        internal static readonly BorderMetrics Empty = new BorderMetrics(Padding.Empty, Padding.Empty);

        /// <summary>
        /// Client area of the window border.
        /// </summary>
        public Padding ClientEdges;
        /// <summary>
        /// Non-client area of the window border.
        /// </summary>
        public Padding WindowEdges;

        #endregion fields

        #region ## constructors

        internal BorderMetrics(Padding clientEdges, Padding windowEdges)
        {
            ClientEdges = clientEdges;
            WindowEdges = windowEdges;
        }

        #endregion constructors

        #region ## methods

        public override bool Equals(object obj)
        {
            return
                obj != null &&
                obj is BorderMetrics other &&
                Equals(other);
        }

        public bool Equals(BorderMetrics other)
        {
            return
                ClientEdges == other.ClientEdges &&
                WindowEdges == other.WindowEdges;
        }

        public override int GetHashCode()
        {
            return (ClientEdges, WindowEdges).GetHashCode();
        }

        #endregion methods

        #region ## operators

        public static bool operator ==(BorderMetrics left, BorderMetrics right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BorderMetrics left, BorderMetrics right)
        {
            return !(left == right);
        }

        public static implicit operator Padding(BorderMetrics metrics)
        {
            return metrics.ClientEdges + metrics.WindowEdges;
        }

        #endregion operators
    }
}
