using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.GameContent;

namespace NaturalRegrowth.Patches
{
    [HarmonyPatch(typeof(ItemTreeSeed))]
    public static class TreeSeedPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(ItemTreeSeed.OnGroundIdle))]
        public static void OnGroundIdlePostfix()
        {

        }
    }
}
