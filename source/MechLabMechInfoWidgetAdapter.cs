﻿using BattleTech.UI;
using DynModLib;
using TMPro;

namespace MechEngineMod
{
    internal class MechLabMechInfoWidgetAdapter : Adapter<MechLabMechInfoWidget>
    {
        internal MechLabMechInfoWidgetAdapter(MechLabMechInfoWidget instance) : base(instance)
        {
        }

        public MechLabPanel mechLab
        {
            get { return traverse.Field("mechLab").GetValue<MechLabPanel>(); }
        }

        public TextMeshProUGUI totalTonnage
        {
            get { return traverse.Field("totalTonnage").GetValue<TextMeshProUGUI>(); }
        }

        public UIColorRefTracker totalTonnageColor
        {
            get { return traverse.Field("totalTonnageColor").GetValue<UIColorRefTracker>(); }
        }

        public TextMeshProUGUI remainingTonnage
        {
            get { return traverse.Field("remainingTonnage").GetValue<TextMeshProUGUI>(); }
        }

        public UIColorRefTracker remainingTonnageColor
        {
            get { return traverse.Field("remainingTonnageColor").GetValue<UIColorRefTracker>(); }
        }
        
        public MechLabHardpointElement[] hardpoints
        {
            get { return traverse.Field("hardpoints").GetValue<MechLabHardpointElement[]>(); }
        }
    }
}