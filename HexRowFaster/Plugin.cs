using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace HexRowFaster
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string PluginGuid = "com.hex.rowfaster";
        private const string PluginName = "HexRowFaster";
        private const string PluginVersion = "1.2.0";

        private ConfigEntry<bool> _isModEnabled;
        private ConfigEntry<ForceMultiplier> _forceMultiplier;
        private ConfigEntry<KeyboardShortcut> _brakeKey;

        internal static ManualLogSource Log;
        internal static Plugin Instance;
        internal static Harmony HarmonyInstance;
        internal static bool IsModEnabled => Instance?._isModEnabled.Value ?? false;
        internal static ForceMultiplier ForceMultiplier => Instance?._forceMultiplier.Value ?? ForceMultiplier.Cruising;
        internal static KeyboardShortcut BrakeKey => Instance?._brakeKey.Value ?? new KeyboardShortcut(KeyCode.LeftShift);

        private void Awake()
        {
            Instance = this;

            Log = Logger;
            
            _isModEnabled = Config.Bind(
                "General",
                "Enable", 
                true, 
                "Enables or disables HexRowFaster for newly spawned ships.");

            _forceMultiplier = Config.Bind(
                "General",
                "Force Multiplier",
                ForceMultiplier.Cruising,
                "How fast do you want to go?");

            _brakeKey = Config.Bind(
                "Brake",
                "Brake Key",
                new KeyboardShortcut(KeyCode.LeftShift),
                "Hold this key while piloting a ship to apply the brakes.");

            HarmonyInstance = new Harmony(PluginGuid);
            HarmonyInstance.PatchAll();

            Log.LogInfo($"{PluginName} v{PluginVersion} loaded.");
        }

        private void OnDestroy()
        {
            Log.LogInfo($"{PluginName} v{PluginVersion} unloaded.");

            HarmonyInstance?.UnpatchSelf();
            HarmonyInstance = null;
            Instance = null;
            Log = null;
        }
    }

    public enum ForceMultiplier
    {
        Vanilla = 1,
        LittleFaster = 5,
        Cruising = 10,
        Speeding = 20,
        Insane = 40
    }
}
