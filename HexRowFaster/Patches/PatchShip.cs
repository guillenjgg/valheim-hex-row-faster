using HarmonyLib;
using UnityEngine;

namespace HexRowFaster.Patches
{
    [HarmonyPatch(typeof(Ship), nameof(Ship.CustomFixedUpdate))]
    internal static class PatchShip
    {
        private const float BrakeForce = 2.5f;

        [HarmonyPrefix]
        private static void Prefix(Ship __instance, out ShipState __state)
        {
            __state = new ShipState();

            if (!Plugin.IsModEnabled)
            {
                return;
            }

            Player localPlayer = Player.m_localPlayer;

            if (localPlayer == null || localPlayer.GetControlledShip() != __instance)
            {
                return;
            }

            __state.OriginalBackwardForce = __instance.m_backwardForce;
            __state.BackwardForceChanged = true;
            __instance.m_backwardForce *= (float)Plugin.ForceMultiplier;
        }

        [HarmonyPostfix]
        private static void Postfix(Ship __instance, Rigidbody ___m_body, ShipState __state)
        {
            if (__state != null && __state.BackwardForceChanged)
            {
                __instance.m_backwardForce = __state.OriginalBackwardForce;
            }

            if (!Plugin.IsModEnabled || !Plugin.BrakeKey.IsPressed())
            {
                return;
            }

            Player localPlayer = Player.m_localPlayer;

            if (localPlayer == null || localPlayer.GetControlledShip() != __instance || ___m_body == null)
            {
                return;
            }

            Vector3 velocity = ___m_body.linearVelocity;
            Vector3 horizontalVelocity = new Vector3(velocity.x, 0f, velocity.z);

            if (horizontalVelocity.sqrMagnitude < 0.01f)
            {
                return;
            }

            ___m_body.AddForce(-horizontalVelocity * BrakeForce, ForceMode.Acceleration);
        }
    }

    [HarmonyPatch(typeof(Ship), nameof(Ship.GetSailForce))]
    internal static class PatchShipGetSailForce
    {
        [HarmonyPostfix]
        private static void Postfix(Ship __instance, ref Vector3 __result)
        {
            if (!Plugin.IsModEnabled || !Plugin.BrakeKey.IsPressed())
            {
                return;
            }

            Player localPlayer = Player.m_localPlayer;

            if (localPlayer == null || localPlayer.GetControlledShip() != __instance)
            {
                return;
            }

            __result = Vector3.zero;
        }
    }

    internal sealed class ShipState
    {
        internal float OriginalBackwardForce;
        internal bool BackwardForceChanged;
    }
}