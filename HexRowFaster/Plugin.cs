using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace HexRowFaster
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        const string PluginGuid = "com.hex.rowfaster";
        const string PluginName = "HexRowFaster";
        const string PluginVersion = "1.0.0";

        internal static ManualLogSource Log;
        internal static Plugin Instance;
        internal static Harmony HarmonyInstance;

        private void Awake()
        {
            Instance = this;

            Log = Logger;

            HarmonyInstance = new Harmony(PluginGuid);
            HarmonyInstance.PatchAll();

            Log.LogInfo($"Plugin {PluginName} v{PluginVersion} loaded.");
        }

        private void OnDestroy()
        {
            Log.LogInfo($"Plugin {PluginName} v{PluginVersion} unloaded.");

            HarmonyInstance?.UnpatchSelf();
            HarmonyInstance = null;
            Instance = null;
        }
    }
}
