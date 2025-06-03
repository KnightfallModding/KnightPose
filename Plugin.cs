using HarmonyLib;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;

namespace KnightPose;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;

    public override void Load()
    {
        Log = base.Log;
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_NAME} loaded successfully!");

        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();

        AddComponents();
    }

    private void AddComponents()
    {
        AddComponent<ProgressCanvas>();
    }
}
