using System;
using System.Collections.Generic;
using OutFoxeed.Attributes;
using UnityEngine;

namespace OutFoxeed.PoolsManager
{
    public abstract class PoolsManagerBase<T> : MonoBehaviour where T : Component
    {
        private List<Pool<T>> allPools = new List<Pool<T>>();
        [SerializeField] private Transform poolsParent;

        private void Reset()
        {
            poolsParent = transform;
        }
        private void OnDestroy()
        {
            // Destroy objects of all pools if they are not child of PoolsManager instance
            if (poolsParent == transform)
                return;
            for (int i = 0; i < allPools.Count; i++)
                allPools[i].Destroy();
        }

        T Deploy(T prefab, Vector2 position, System.Action<T> onDeployed)
        {
            T deployedObj = Deploy(prefab, position);
            onDeployed?.Invoke(deployedObj);
            return deployedObj;
        }
        T Deploy(T prefab, Vector2 position)
        {
            // Try to find if we already have an existing pool with this prefab
            foreach (Pool<T> pool in allPools)
            {
                if (pool.Prefab == prefab)
                    return pool.Deploy(position);
            }

            // Else, we create a new pool with this prefab
            // Create parent of pools object
            Transform newPoolParent = new GameObject($"{prefab.name} Pool").transform;
            newPoolParent.SetParent(poolsParent);
            // Create pool itself
            Pool<T> newPool = new Pool<T>(prefab, newPoolParent);
            allPools.Add(newPool);

            return newPool.Deploy(position);
        }
    }
}