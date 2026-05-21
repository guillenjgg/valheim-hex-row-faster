using HarmonyLib;

namespace HexRowFaster.Patches
{
    [HarmonyPatch(typeof(Ship), nameof(Ship.Awake))]
    internal static class PatchShip
    {
        [HarmonyPostfix]
        private static void ModifyBackwardForceValue(Ship __instance)
        {
            if(Plugin.Instance == null || __instance == null || !Plugin.IsModEnabled || Plugin.ForceMultiplier == ForceMultiplier.Vanilla)
            {
                return;
            }

            /***
             * This field is used in various force physics calculations
             * Should only affect the initial push, rowing, reverse, and straight line movement
             */
            __instance.m_backwardForce *= (float)Plugin.ForceMultiplier;

            Plugin.Log.LogInfo($"Updated ship paddle force: {__instance.name}, m_backwardForce={__instance.m_backwardForce}");
        }
    }
}
