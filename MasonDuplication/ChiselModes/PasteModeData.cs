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

            if(main.VoxelClipboards[byPlayer.PlayerUID] == null)
            {
                return false;
            }

            var voxels = main.VoxelClipboards[byPlayer.PlayerUID];
            var materials = (byte[,,])main.MaterialClipboards[byPlayer.PlayerUID].Clone(); // Cloned because we modify it below

            var maxMatId = chiselEntity.MaterialIds.Length - 1;

            // This is to make sure if we copy paste from a block with more materials to one with less, we dont try using more materials than exist.
            for(var x = 0; x < materials.GetLength(0); x++)
            {
                for (var y = 0; y < materials.GetLength(1); y++)
                {
                    for (var z = 0; z < materials.GetLength(2); z++)
                    {
                        if (materials[x, y, z] > maxMatId)
                        {
                            materials[x, y, z] = 0;
                        }
                    }
                }
            }
            
            chiselEntity.RebuildCuboid(voxels, materials);

            return true;
        }
    }
}
