using System;
using HarmonyLib;
using NaturalRegrowth.Systems;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace NaturalRegrowth
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
            InitializeSubsystems(api);
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            StartHarmony();
            InitializeSubsystems(api);
        }

        private void StartHarmony()
        {
            var harmony = new Harmony("ninjanomnom.NaturalRegrowth");
            harmony.PatchAll();
        }

        private void InitializeSubsystems(ICoreAPI api)
        {
            foreach(var subsystem in Subsystem.Instances)
            {
                subsystem.Initialize(api);
            }
        }
    }
}
