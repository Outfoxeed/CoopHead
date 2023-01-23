using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OutFoxeed.PoolsManager
{
    /// <summary>
    /// Not a fan of the use of static class: should use PoolsManager<T> if possible
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class StaticPoolsManager<T> where T : Component 
    {
        private static List<Pool<T>> allPools = new List<Pool<T>>();
        private static Transform poolsParent;

        public static T Deploy(T prefab, Vector2 position, System.Action<T> onDeployed)
        {
            T deployedObject = Deploy(prefab, position);
            onDeployed?.Invoke(deployedObject);
            return deployedObject;
        }
        public static T Deploy(T prefab, Vector2 position)
        {
            SceneManager.sceneUnloaded -= Reset;
            SceneManager.sceneUnloaded += Reset;
            
            // Try to find if we already have a pool with this prefab
            foreach (Pool<T> pool in allPools)
            {
                if (pool.Prefab == prefab)
                    return pool.Deploy(position);
            }   
            
            // Else, we create a new pool with this prefab
            if (!poolsParent) poolsParent = new GameObject("Pools").transform;
            Transform newPoolParent = new GameObject($"{prefab.name} Pool").transform;
            newPoolParent.SetParent(poolsParent);
            Pool<T> newPool = new Pool<T>(prefab, newPoolParent);
            allPools.Add(newPool);

            return newPool.Deploy(position);
        }

        public static void Reset(Scene scene)
        {
            allPools.Clear();
            poolsParent = null;
            SceneManager.sceneUnloaded -= Reset;
        }
    }
}