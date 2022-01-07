using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace MasonDuplication
{
    public static class BlockEntityMicroBlockExtensions
    {
        private static MethodInfo exportMethod = typeof(BlockEntityMicroBlock).GetMethod("convertToVoxels", BindingFlags.Instance | BindingFlags.NonPublic);
        private static MethodInfo rebuildMethod = typeof(BlockEntityMicroBlock).GetMethod("RebuildCuboidList", BindingFlags.Instance | BindingFlags.NonPublic);

        public static void ConvertToVoxels(this BlockEntityMicroBlock src, out bool[,,] voxels, out byte[,,] materials)
        {
            var parameters = new object[] { null, null };
            exportMethod.Invoke(src, parameters);
            voxels = (bool[,,])parameters[0];
            materials = (byte[,,])parameters[1];
        }

        public static void RebuildCuboid(this BlockEntityMicroBlock src, bool[,,] voxels, byte[,,] voxelMaterials)
        {
            var parameters = new object[] { voxels, voxelMaterials };
            rebuildMethod.Invoke(src, parameters);
        }
    }
}
