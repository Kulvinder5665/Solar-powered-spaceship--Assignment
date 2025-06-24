using Solar.Orb;
using UnityEngine;

namespace Solar.Utilities
{
    public class OrbPool : GenericObjectPool<OrbController>
    {
        public OrbView orbPrefab;
        public OrbData orbData;
        public OrbPool(OrbView _orbView, OrbData _orbData)
        {
            orbPrefab = _orbView;
            orbData = _orbData;

        }

        public OrbController GetOrb() => GetItem<OrbController>();

        protected override OrbController CreateItem<T>() => new OrbController(orbPrefab, orbData);

    }
}