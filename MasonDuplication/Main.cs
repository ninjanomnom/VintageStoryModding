using HarmonyLib;
using System.Collections.Generic;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace MasonDuplication
{
    /// <summary>
    /// Redirects all log entries into the visual studio output window. Only for your convenience during development and testing.
    /// </summary>
    public class Main : ModSystem
    {
        public static Main Instance;
        public Dictionary<string, bool[,,]> VoxelClipboards = new Dictionary<string, bool[,,]>();
        public Dictionary<string, byte[,,]> MaterialClipboards = new Dictionary<string, byte[,,]>();

        public override bool ShouldLoad(EnumAppSide side)
        {
            return true;
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            Instance = this;
            StartHarmony();
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            Instance = this;
            StartHarmony();
        }

        private void StartHarmony()
        {
            var harmony = new Harmony("ninjanomnom.MasonDuplication");
            harmony.PatchAll();
        }
    }
}
