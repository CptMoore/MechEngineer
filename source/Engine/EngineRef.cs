﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using BattleTech;

namespace MechEngineMod
{
    internal class EngineRef
    {
        internal bool IsDHS;
        internal bool IsSHS
        {
            get { return !IsDHS; }
        }

        internal int AdditionalDHSCount;
        internal int AdditionalSHSCount;
        internal int AdditionalHeatSinkCount
        {
            get { return AdditionalSHSCount + AdditionalDHSCount; }
        }

        internal string UUID;

        internal readonly MechComponentRef mechComponentRef;
        internal readonly EngineDef engineDef;

        private static readonly Regex Regex = new Regex(@"^(?:([^/]*))(?:/(dhs)?(?:\+(\d+)shs)?(?:\+(\d+)dhs)?)?$", RegexOptions.Singleline | RegexOptions.Compiled);

        internal EngineRef(MechComponentRef mechComponentRef, EngineDef engineDef)
        {
            this.mechComponentRef = mechComponentRef;
            this.engineDef = engineDef;

            var text = mechComponentRef.SimGameUID;

            if (string.IsNullOrEmpty(text))
            {
                if (text != null)
                {
                    mechComponentRef.SetSimGameUID(null);
                }
                return;
            }

            var match = Regex.Match(text);
            if (!match.Success)
            {
                return;
            }

            UUID = string.IsNullOrEmpty(match.Groups[1].Value) ? null : match.Groups[1].Value;
            IsDHS = !string.IsNullOrEmpty(match.Groups[2].Value);
            AdditionalSHSCount = !string.IsNullOrEmpty(match.Groups[3].Value) ? int.Parse(match.Groups[3].Value) : 0;
            AdditionalDHSCount = !string.IsNullOrEmpty(match.Groups[4].Value) ? int.Parse(match.Groups[4].Value) : 0;
        }

        internal string GetNewSimGameUID()
        {
            var props = ToString();
            if (string.IsNullOrEmpty(props))
            {
                return UUID;
            }

            return (string.IsNullOrEmpty(UUID) ? "" : UUID) + "/" + props;
        }

        public override string ToString()
        {
            return (IsDHS ? "dhs" : "")
                   + (AdditionalSHSCount > 0 ? "+" + AdditionalSHSCount + "shs": "")
                   + (AdditionalDHSCount > 0 ? "+" + AdditionalDHSCount + "dhs": "");
        }

        internal float GetEngineHeatDissipation()
        {
            int minHeatSinks, maxHeatSinks;
            Control.calc.CalcHeatSinks(engineDef, out minHeatSinks, out maxHeatSinks);

            var dissipation = AdditionalDHSCount * Control.Combat.Heat.DefaultHeatSinkDissipationCapacity * 2;
            dissipation += AdditionalSHSCount * Control.Combat.Heat.DefaultHeatSinkDissipationCapacity;
            dissipation += (IsDHS ? 2 : 1) * minHeatSinks * Control.Combat.Heat.DefaultHeatSinkDissipationCapacity;

            //Control.mod.Logger.LogDebug("GetHeatDissipation rating=" + engineDef.Rating + " minHeatSinks=" + minHeatSinks + " additionalHeatSinks=" + engineProps.AdditionalHeatSinkCount + " dissipation=" + dissipation);

            return dissipation;
        }
    }
}