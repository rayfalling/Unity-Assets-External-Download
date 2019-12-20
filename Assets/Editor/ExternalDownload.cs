using System;
using System.Runtime.InteropServices;
using Assets.Editor.rayfalling;
using UnityEditor;
using UnityEngine;
using OpenFileDialog = Assets.Editor.rayfalling.OpenFileDialog;

namespace Assets.Editor {
    public class ExternalDownload : MonoBehaviour {
        [UnityEditor.MenuItem("External Download/Download")]
        static void DecryptFile() {
            var inputFile = @"";
            var key = "";

            var unityEditor = typeof(UnityEditor.Editor).Assembly;

            var assetStoreUtils = unityEditor.GetType("UnityEditor.AssetStoreUtils");

            assetStoreUtils.Invoke("DecryptFile", inputFile, inputFile + ".unitypackage", key);
        }

        [UnityEditor.MenuItem("External Download/Import")]
        static void Import() {
            var openFileName = new OpenFileItem();
            openFileName.structSize = Marshal.SizeOf(openFileName);
            openFileName.filter = "unitypackage(*.unitypackage)\0*.*|\0*.*";
            openFileName.file = new string(new char[256]);
            openFileName.maxFile = openFileName.file.Length;
            openFileName.fileTitle = new string(new char[64]);
            openFileName.maxFileTitle = openFileName.fileTitle.Length;
            openFileName.initialDir = UnityEngine.Application.streamingAssetsPath.Replace('/', '\\'); //默认路径
            openFileName.title = "请选择路径或者文件";
            openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;

            if (!OpenFileDialog.GetSaveFileName(openFileName)) return;
            var fileName = openFileName.file;
            AssetDatabase.ImportPackage(packagePath: fileName, false);
        }
    }
}