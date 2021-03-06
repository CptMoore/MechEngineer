﻿using System.Collections.Generic;
using BattleTech;
using CustomComponents;
using Harmony;
using MechEngineer.Misc;

namespace MechEngineer.Features.AutoFix
{
    internal class SensorsBHandler : IAdjustUpgradeDef, IPreProcessor
    {
        internal static MELazy<SensorsBHandler> Lazy = new();
        internal static SensorsBHandler Shared => Lazy.Value;

        private readonly IdentityHelper identity;
        private readonly AdjustCompDefInvSizeHelper resizer;

        public SensorsBHandler()
        {
            identity = AutoFixerFeature.settings.SensorsBCategorizer;

            if (identity == null)
            {
                return;
            }

            if (AutoFixerFeature.settings.SensorsBSlotChange != null)
            {
                resizer = new AdjustCompDefInvSizeHelper(identity, AutoFixerFeature.settings.SensorsBSlotChange);
            }
        }

        public void PreProcess(object target, Dictionary<string, object> values)
        {
            identity?.PreProcess(target, values);
        }

        public void AdjustUpgradeDef(UpgradeDef upgradeDef)
        {
            if (identity.IsCustomType(upgradeDef))
            {
                upgradeDef.AllowedLocations = ChassisLocations.Head;
            }
            resizer?.AdjustComponentDef(upgradeDef);
        }
    }
}