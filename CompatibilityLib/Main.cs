using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace CompatibilityLib
{
    public class Main : ModSystem
    {

        public override bool ShouldLoad(EnumAppSide side)
        {
            return true;
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            StartHarmony();
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            StartHarmony();
        }

        private void StartHarmony()
        {
            var harmony = new Harmony("ninjanomnom.CompatibilityLib");
            harmony.PatchAll();
        }
    }
}
