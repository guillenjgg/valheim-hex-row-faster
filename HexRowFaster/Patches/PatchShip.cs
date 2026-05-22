using HarmonyLib;

namespace HexRowFaster.Patches
{
    // Note: this method runs constantly when a ship is active. Tested in game
    [HarmonyPatch(typeof(Ship), nameof(Ship.CustomFixedUpdate))]
    internal static class PatchShip
    {
        [HarmonyPrefix]
        private static void ModifyBackwardForceValue(Ship __instance, ref float? __state)
        {
            __state = null;

            if(Plugin.Instance == null || __instance == null)
            {
                return;
            }

            if(!Plugin.IsModEnabled || Plugin.ForceMultiplier == ForceMultiplier.Vanilla)
            {
                return;
            }

            if(Player.m_localPlayer == null)
            {
                return;
            }

            // Only modify the ship that the local player is controlling.
            if(Player.m_localPlayer.GetControlledShip() != __instance)
            {
                return;
            }

            __state = __instance.m_backwardForce;
            __instance.m_backwardForce *= (float)Plugin.ForceMultiplier;
        }

        [HarmonyPostfix]
        private static void RestoreBackwardForceValue(Ship __instance, float? __state)
        {
            if(Plugin.Instance == null || __instance == null)
            {
                return;
            }

            if(!__state.HasValue)
            {
                return;
            }

            __instance.m_backwardForce = __state.Value;
        }
    }
}
