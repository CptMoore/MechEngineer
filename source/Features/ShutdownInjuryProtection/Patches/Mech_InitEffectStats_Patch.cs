﻿using System;
using BattleTech;
using Harmony;
using MechEngineer.Helper;

namespace MechEngineer.Features.ShutdownInjuryProtection.Patches
{
    [HarmonyPatch(typeof(Mech), "InitEffectStats")]
    public static class Mech_InitEffectStats_Patch
    {
        public static void Prefix(Mech __instance)
        {
            try
            {
                if (ShutdownInjuryProtectionFeature.settings.ShutdownInjuryEnabled)
                {
                    __instance.StatCollection.ReceiveShutdownInjury().Create();
                }
                if (ShutdownInjuryProtectionFeature.settings.HeatDamageInjuryEnabled)
                {
                    __instance.StatCollection.ReceiveHeatDamageInjury().Create();
                }
            }
            catch (Exception e)
            {
                Control.Logger.Error.Log(e);
            }
        }
    }

    internal static class StatCollectionExtension
    {
        internal static StatisticAdapter<bool> ReceiveShutdownInjury(this StatCollection statCollection)
        {
            return new("ReceiveShutdownInjury", statCollection, false);
        }

        internal static StatisticAdapter<bool> ReceiveHeatDamageInjury(this StatCollection statCollection)
        {
            return new("ReceiveHeatDamageInjury", statCollection, false);
        }
    }
}