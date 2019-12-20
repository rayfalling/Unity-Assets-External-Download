using System.Runtime.InteropServices;

namespace Assets.Editor.rayfalling {
    public class OpenFileDialog {
        //链接指定系统函数       打开文件对话框
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] OpenFileItem ofn);

        public static bool GetOfn([In, Out] OpenFileItem ofn) {
            return GetOpenFileName(ofn);
        }

        //链接指定系统函数        另存为对话框
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetSaveFileName([In, Out] OpenFileItem ofn);

        public static bool GetSfn([In, Out] OpenFileItem ofn) {
            return GetSaveFileName(ofn);
        }
    }
}