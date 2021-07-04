using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;

public static class XcodePostProcessBuild
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string path)
    {
        var plistPath = Path.Combine(path, "Info.plist");
        var plist = new PlistDocument();
        plist.ReadFromFile(plistPath);

        // バイナリアップロード時にエラーが出るため
        plist.root.values.Remove("UIRequiredDeviceCapabilities");

        plist.WriteToFile(plistPath);

    }
}
#endif