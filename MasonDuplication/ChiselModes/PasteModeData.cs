using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;
using VSSurvivalMod.Systems.ChiselModes;

namespace MasonDuplication.ChiselModes
{
    public class PasteModeData : ChiselModeData
    {
        public override bool Act(BlockEntityChisel chiselEntity, IPlayer byPlayer, Vec3i voxelPos, BlockFacing facing, bool isBreak, byte currentMaterialIndex)
        {
            var main = Main.Instance;

            if (main.VoxelClipboard == null)
            {
                return false;
            }

            var maxMatId = chiselEntity.MaterialIds.Length - 1;

            // This is to make sure if we copy paste from a block with more materials to one with less, we dont try using more materials than exist.
            var temporaryMaterials = (byte[,,])main.MaterialClipboard.Clone();
            for(var x = 0; x < temporaryMaterials.GetLength(0); x++)
            {
                for (var y = 0; y < temporaryMaterials.GetLength(1); y++)
                {
                    for (var z = 0; z < temporaryMaterials.GetLength(2); z++)
                    {
                        if (temporaryMaterials[x, y, z] > maxMatId)
                        {
                            temporaryMaterials[x, y, z] = 0;
                        }
                    }
                }
            }
            
            chiselEntity.RebuildCuboid(main.VoxelClipboard, main.MaterialClipboard);

            return true;
        }
    }
}
