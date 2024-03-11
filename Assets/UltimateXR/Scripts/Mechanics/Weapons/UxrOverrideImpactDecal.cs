﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UxrOverrideImpactDecal.cs" company="VRMADA">
//   Copyright (c) VRMADA, All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UltimateXR.Core.Components;
using UnityEngine;

namespace UltimateXR.Mechanics.Weapons
{
    /// <summary>
    ///     Component that, added to a GameObject, will allows to override the decal generated by a projectile impact.
    ///     When a projectile impact coming from an <see cref="UxrProjectileSource" /> hits anything, an
    ///     <see cref="UxrOverrideImpactDecal" /> will be looked for traversing upwards in the hierarchy starting from the
    ///     collider.
    ///     If no <see cref="UxrOverrideImpactDecal" /> was found, the decal specified in the
    ///     <see cref="UxrProjectileSource" /> component will be used.
    /// </summary>
    public class UxrOverrideImpactDecal : UxrComponent
    {
        #region Inspector Properties/Serialized Fields

        [SerializeField] private UxrImpactDecal _decalToUse;

        #endregion

        #region Public Types & Data

        /// <summary>
        ///     Gets the decal to override with when the object was hit.
        /// </summary>
        public UxrImpactDecal DecalToUse => _decalToUse;

        #endregion
    }
}