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
            var chiselModes = ChiselPatchHelpers.GetChiselModes(__instance);

            if(chiselModes.Any(m => m is CopyModeData))
            {
                return;
            }

            ChiselPatchHelpers.SetChiselModes(__instance, chiselModes.Append(new ChiselModeData[] {
                new CopyModeData(),
                new PasteModeData()
            }));

            if(api is ICoreClientAPI capi)
            {
                var toolModes = ChiselPatchHelpers.GetToolModes(__instance);
                ChiselPatchHelpers.SetToolModes(__instance, toolModes.Append(new SkillItem[]
                {
                    new SkillItem()
                    {
                        Code = new AssetLocation("copy"),
                        Name = Lang.Get("copy"),
                        Data = new CopyModeData()
                    }.WithIcon(capi, capi.Gui.Icons.Drawduplicate_svg),

                    new SkillItem()
                    {
                        Code = new AssetLocation("paste"),
                        Name = Lang.Get("paste"),
                        Data = new PasteModeData()
                    }.WithIcon(capi, capi.Gui.Icons.Drawimport_svg)
                }));
            }
        }
    }

    internal static class ChiselPatchHelpers
    {
        private static PropertyInfo ChiselModesProperty => GetChiselModesInfo();
        private static FieldInfo ToolModesField => GetToolModesInfo();

        internal static PropertyInfo GetChiselModesInfo()
        {
            return typeof(ItemChisel).GetProperty(nameof(ItemChisel.ChiselModes));
        }

        internal static FieldInfo GetToolModesInfo()
        {
            return typeof(ItemChisel).GetField("toolModes", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        internal static ChiselModeData[] GetChiselModes(ItemChisel src)
        {
            return (ChiselModeData[])ChiselModesProperty.GetValue(src);
        }

        internal static void SetChiselModes(ItemChisel src, ChiselModeData[] modes)
        {
            ChiselModesProperty.SetValue(src, modes);
        }

        internal static SkillItem[] GetToolModes(ItemChisel src)
        {
            return (SkillItem[])ToolModesField.GetValue(src);
        }

        internal static void SetToolModes(ItemChisel src, SkillItem[] toolModes)
        {
            ToolModesField.SetValue(src, toolModes);
        }
    }
}
