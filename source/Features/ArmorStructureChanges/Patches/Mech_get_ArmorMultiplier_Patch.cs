﻿using System.Linq;
using BattleTech;
using CustomComponents;
using Harmony;

namespace MechEngineer
{
    [HarmonyPatch(typeof(Mech), "get_ArmorMultiplier")]
    public static class Mech_get_ArmorMultiplier_Patch
    {
        public static void Postfix(Mech __instance, ref float __result)
        {
            __result = __result * GetFactorForMech(__instance);
        }

        internal static float GetFactorForMech(Mech mech)
        {
            const string key = "ArmorMultiplier";
            var statistic = mech.StatCollection.GetStatistic(key)
                            ?? mech.StatCollection.AddStatistic(key, GetFactorForMechDef(mech.MechDef));
            return statistic.Value<float>();
        }

        internal static float GetFactorForMechDef(MechDef mechDef)
        {
            return mechDef.Inventory
                .Select(r => r.GetComponent<ArmorStructureChanges>())
                .Where(c => c != null)
                .Select(c => c.ArmorFactor)
                .Aggregate(1.0f, (acc, val) => acc * val);
        }
    }
}