﻿namespace MechEngineer.Features.TurretLimitedAmmo
{
    internal class TurretLimitedAmmoFeature : Feature<TurretLimitedAmmoSettings>
    {
        internal static TurretLimitedAmmoFeature Shared = new();

        internal override TurretLimitedAmmoSettings Settings => Control.settings.TurretLimitedAmmo;
    }
}