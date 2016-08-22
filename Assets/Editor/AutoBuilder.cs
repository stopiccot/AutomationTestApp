using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class AutoBuilder {

    [MenuItem("File/AutoBuilder/iOS")]
    static void PerformiOSBuild()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iOS);
        BuildPipeline.BuildPlayer(new EditorBuildSettingsScene[] { 
            new EditorBuildSettingsScene("Assets/Game.unity", true),
        }, "Builds/iOS", BuildTarget.iOS, BuildOptions.None);
    }

    [MenuItem("File/AutoBuilder/Android")]
    static void PerformAndroidBuild()
    {
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
        BuildPipeline.BuildPlayer(new EditorBuildSettingsScene[] { 
            new EditorBuildSettingsScene("Assets/Game.unity", true),
		}, "Builds/Android", BuildTarget.Android, BuildOptions.None);
    }
}