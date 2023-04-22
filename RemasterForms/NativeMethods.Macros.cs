using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemasterForms
{
    internal static partial class NativeMethods // Macros
    {
        // HiWord
        public static int HiWord(int n) => (n >> 16) & 0xffff;
        public static int HiWord(IntPtr n) => HiWord(unchecked((int)(long)n));

        // LoWord
        public static int LoWord(int n) => n & 0xffff;
        public static int LoWord(IntPtr n) => LoWord(unchecked((int)(long)n));

        // MakeLong
        public static int MakeLong(int low, int high) => (high << 16) | (low & 0xffff);

        // MakeLParam
        public static IntPtr MakeLParam(int low, int high) => (IntPtr)((high << 16) | (low & 0xffff));

        // SignedHiWord
        public static int SignedHiWord(int n) => (short)((n >> 16) & 0xffff);
        public static int SignedHiWord(IntPtr n) => SignedHiWord(unchecked((int)(long)n));

        // SignedLoWord
        public static int SignedLoWord(int n) => (short)(n & 0xFFFF);
        public static int SignedLoWord(IntPtr n) => SignedLoWord(unchecked((int)(long)n));
    }
}
