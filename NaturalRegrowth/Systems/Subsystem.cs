using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;

namespace NaturalRegrowth.Systems
{
    public abstract class Subsystem
    {
        public static IReadOnlyList<Subsystem> Instances => _instances;
        private readonly static List<Subsystem> _instances = new List<Subsystem>();

        private ICoreAPI _api;
        protected ICoreAPI Api => _api;

        public abstract TimeSpan FireRate { get; }

        [Flags]
        public enum SystemFlags
        {
            None = 0,
            Realtime = 1 << 0
        }
        public virtual SystemFlags OptionFlags { get; } = SystemFlags.None;

        public Subsystem()
        {
            _instances.Add(this);
        }

        public void Initialize(ICoreAPI api)
        {
            _api = api;
            api.World.RegisterGameTickListener(Fire, (int)FireRate.TotalMilliseconds);
            InternalInitialize(api);
        }

        /// <summary>
        /// If set up is needed before firing, override this function to do so
        /// </summary>
        /// <param name="api"></param>
        protected virtual void InternalInitialize(ICoreAPI api) { }

        public abstract void Fire(float delta);
    }
}
