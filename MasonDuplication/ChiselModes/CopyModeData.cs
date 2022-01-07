using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;
using VSSurvivalMod.Systems.ChiselModes;

namespace MasonDuplication.ChiselModes
{
    public class CopyModeData : ChiselModeData
    {
        public override bool Act(BlockEntityChisel chiselEntity, IPlayer byPlayer, Vec3i voxelPos, BlockFacing facing, bool isBreak, byte currentMaterialIndex)
        {
            var main = Main.Instance;

            chiselEntity.ConvertToVoxels(out var voxels, out var materials);

            main.VoxelClipboards[byPlayer.PlayerUID] = voxels;
            main.MaterialClipboards[byPlayer.PlayerUID] = materials;

            return false;
        }
    }
}
