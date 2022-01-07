using System;
using System.Reflection;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.GameContent;

namespace CompatibilityLib.Extensions
{
    public static class TreeSeedExtensions
    {
        /// <summary>
        /// This is a duplication of logic that can be found in <see cref="ItemTreeSeed.OnHeldInteractStart"/>
        /// </summary>
        /// <param name="src">The tree seed to plant</param>
        /// <param name="selectedBlock">The block the tree seed will be planted on</param>
        /// <param name="slot">The slot this seed is in</param>
        /// <param name="player">The player triggering the placement</param>
        /// <returns></returns>
        public static bool TryPlanting(this ItemTreeSeed src, BlockSelection selectedBlock, ItemSlot slot, IPlayer player)
        {
            var result = src.TryPlanting(selectedBlock, slot.Itemstack, player);
            if(result && !(player.WorldData?.CurrentGameMode == EnumGameMode.Creative))
            {
                slot.TakeOut(1);
                slot.MarkDirty();
            }
            return result;
        }

        /// <summary>
        /// This is a duplication of logic that can be found in <see cref="ItemTreeSeed.OnHeldInteractStart"/>
        /// </summary>
        /// <param name="src">The tree seed to plant</param>
        /// <param name="selectedBlock">The block the tree seed will be planted on</param>
        /// <param name="slot">The slot this seed is in</param>
        /// <param name="player">The player triggering the placement</param>
        /// <returns></returns>
        public static bool TryPlanting(this ItemTreeSeed src, BlockSelection selectedBlock, ItemStack stack)
        {
            var result = src.TryPlanting(selectedBlock, stack, null);
            if(result)
            {
                stack.StackSize = Math.Max(0, stack.StackSize - 1);
            }
            return result;
        }

        // Basically a wholecloth copy of the logic from the base game for planting a sapling
        private static bool TryPlanting(this ItemTreeSeed src, BlockSelection selectedBlock, ItemStack stack, IPlayer player)
        {
            var api = src.GetApi();
            var world = api.World;

            var block = world.GetBlock(AssetLocation.Create($"sapling-{src.Variant["type"]}-free", src.Code.Domain));
            if (block == null)
            {
                return false;
            }

            // Getting our own selection and moving it up to where the seed will actualy go
            selectedBlock = selectedBlock.Clone();
            selectedBlock.Position.Up(1);

            string failureCode = "";
            if (!block.TryPlaceBlock(world, player, stack, selectedBlock, ref failureCode))
            {
                if (api is ICoreClientAPI clientApi && failureCode != null && failureCode != "__ignore__")
                {
                    clientApi.TriggerIngameError(src, failureCode, Lang.Get($"placefailure-{failureCode}", Array.Empty<object>()));
                }

                return false;
            }

            return true;
        }

        // Normally this api is protected and meant for internal use but the extension method up above is
        // basically turning internal logic into a public function so we need access to this internal field.
        private static ICoreAPI GetApi(this ItemTreeSeed src)
        {
            var flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var field = src.GetType().GetField("api", flags);
            return (ICoreAPI)field.GetValue(src);
        }
    }
}
