using Photon.Deterministic;

using UnityEngine.Scripting;

namespace Quantum
{
    [Preserve]
    public unsafe class LocationPrinter : SystemMainThreadFilter<LocationPrinter.Filter>
    {

        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            if (f.Number % 20 == 0)
            {
                var pos = filter.Transform->Position;
                Log.Error($"Location: {pos.X}, {pos.Y}, {pos.Z}");
            }
        }
    }
}
