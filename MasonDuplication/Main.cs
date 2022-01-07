using HarmonyLib;
using System;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.API.Util;

namespace MasonDuplication
{
    /// <summary>
    /// Redirects all log entries into the visual studio output window. Only for your convenience during development and testing.
    /// </summary>
    public class Main : ModSystem
    {
        public static Main Instance;
        public bool[,,] VoxelClipboard;
        public byte[,,] MaterialClipboard;

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
