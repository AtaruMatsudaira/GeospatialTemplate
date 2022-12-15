using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace BuildEssentials
{
    public static class OnPostBuildProcess 
    {
        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget buildTarget, string path)
        {
            if (buildTarget == BuildTarget.iOS)
            {
                ProcessForiOS(path);
            }
        }
        static void ProcessForiOS(string path)
        {
            string     pbxPath = PBXProject.GetPBXProjectPath(path);
            PBXProject pbx     = new PBXProject();
            pbx.ReadFromString(File.ReadAllText(pbxPath));
            string target = pbx.GetUnityMainTargetGuid();
            pbx.SetBuildProperty(target, "ENABLE_BITCODE", "NO");
            target = pbx.GetUnityFrameworkTargetGuid();
            pbx.SetBuildProperty(target, "ENABLE_BITCODE", "NO");
            File.WriteAllText (pbxPath, pbx.WriteToString());
        }
    }
}