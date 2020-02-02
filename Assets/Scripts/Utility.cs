using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public static class Utility
    {
        public static T GetClosest<T>(IEnumerable<T> values, Func<T, float> distanceProvider)
        {
            var currClosest = values.First();
            var currClosestDistance = distanceProvider(currClosest);
            foreach (var value in values)
            {
                var distance = distanceProvider(value);
                if (distance < currClosestDistance)
                {
                    currClosest = value;
                    currClosestDistance = distance;
                }
            }
            return currClosest;
        }
        
        
        public static Destroyable GetClosestDestroyableInRange(Vector3 position, float radius, LayerMask layerMask, Func<Destroyable, bool> filter)
        {
            var hits = Physics2D.OverlapCircleAll(position, radius, layerMask)
                .Where(x =>
                {
                    var destroyable = x.GetComponent<Destroyable>();
                    return destroyable != null && filter(destroyable);
                })
                .Select(x => x.GetComponent<Destroyable>());

            if (hits.Count() <= 1)
                return hits.FirstOrDefault();
            
            return GetClosest(hits,
                destroyable => (position - destroyable.transform.position).magnitude);
        }
    }
}