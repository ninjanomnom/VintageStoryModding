using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.GameContent;
using Vintagestory.API.Util;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Cairo;
using VSSurvivalMod.Systems.ChiselModes;
using System.Reflection;
using MasonDuplication.ChiselModes;

namespace MasonDuplication
{
    [HarmonyPatch(typeof(ItemChisel))]
    public static class ChiselPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(ItemChisel.OnLoaded))]
        public static void OnLoadedPostfix(ItemChisel __instance, ICoreAPI api)
        {
            var toolModes = __instance.ToolModes;

            if(toolModes.Any(m => m.Data is CopyModeData))
            {
                return;
            }

            var newToolModes = new SkillItem[]
            {
                new SkillItem()
                {
                    Code = new AssetLocation("copy"),
                    Name = Lang.Get("copy"),
                    Data = new CopyModeData()
                },

                new SkillItem()
                {
                    Code = new AssetLocation("paste"),
                    Name = Lang.Get("paste"),
                    Data = new PasteModeData()
                }
            };

            if (api is ICoreClientAPI capi)
            {
                newToolModes = newToolModes.Select(t => {
                    var chiselMode = (ChiselMode)t.Data;
                    return t.WithIcon(capi, chiselMode.DrawAction(capi));
                }).ToArray();
            }

            __instance.ToolModes = toolModes.Append(newToolModes);
        }
    }
}
