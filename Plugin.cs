using UnityEngine;
using MelonLoader;
using Il2CppInterop.Runtime.Injection;
using KnightPose;

[assembly: MelonInfo(typeof(Plugin), ModInfo.MOD_NAME, ModInfo.MOD_VERSION, ModInfo.MOD_AUTHOR, $"{ModInfo.MOD_LINK}/releases/latest/download/Release.zip")]
[assembly: MelonGame("Landfall Games", "Knightfall")]

namespace KnightPose;

internal class Plugin : MelonMod
{
    public override void OnInitializeMelon()
    {
        LoggerInstance.Msg($"Plugin {ModInfo.MOD_NAME} loaded successfully!");

        RegisterComponents();
        AddComponents();
    }

    private void AddComponents()
    {
        GameObject progressCanvasGO = new("ProgressCanvas");
        progressCanvasGO.AddComponent<ProgressCanvas>();

        progressCanvasGO.hideFlags = HideFlags.HideAndDontSave;
    }

    /// <summary>
    /// Register custom classes to Il2Cpp.
    /// </summary>
    private static void RegisterComponents()
    {
        if (!ClassInjector.IsTypeRegisteredInIl2Cpp(typeof(ProgressCanvas)))
            ClassInjector.RegisterTypeInIl2Cpp(typeof(ProgressCanvas));
    }
}
