using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.GameContent;

namespace NaturalRegrowth.Systems
{
    public class SeedPlanting : Subsystem
    {
        public readonly static SeedPlanting Instance = new SeedPlanting();

        public override TimeSpan FireRate { get; } = new TimeSpan(0, 0, 20);

        private readonly List<ItemTreeSeed> _groundedTreeSeeds = new List<ItemTreeSeed>();

        public override void Fire(float delta)
        {
            throw new NotImplementedException();
        }

        public void AddTreeSeed(ItemTreeSeed seed)
        {
            if(_groundedTreeSeeds.Contains(seed))
            {
                return;
            }

            _groundedTreeSeeds.Add(seed);
        }
    }
}
