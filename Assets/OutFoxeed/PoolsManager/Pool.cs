using System.Collections.Generic;
using UnityEngine;

namespace OutFoxeed.PoolsManager
{
    public class Pool<T> where T : Component
    {
        private readonly Transform objectsParent;
        private List<T> objects;
        public T Prefab { get; private set; }

        public Pool(T prefab, Transform poolObjectsParent)
        {
            Prefab = prefab;
            objectsParent = poolObjectsParent;
            objects = new List<T>();
        }
        
        public T Deploy(Vector2 position, System.Action<T> onDeployed)
        {
            T deployedObj = Deploy(position);
            onDeployed?.Invoke(deployedObj);
            return deployedObj;
        }
        public T Deploy(Vector2 position)
        {
            // We check if we already have an instance ready for deployment
            foreach (var obj in objects.ToArray())
            {
                if (obj == null)
                {
                    objects.Remove(obj);
                    continue;
                }
                    
                if (IsDeployed(obj))
                    continue;

                DeployObject(obj, position);
                return obj;
            }

            // If we haven't found one, we create one and add it to our list
            var newInstance = GameObject.Instantiate(Prefab, position, Quaternion.identity, objectsParent);
            objects.Add(newInstance);

            DeployObject(newInstance, position);
            return newInstance;
        }

        // Helpers
        private void DeployObject(T obj, Vector2 position)
        {
            obj.transform.position = position;
            obj.gameObject.SetActive(true);
        }
        bool IsDeployed(T obj) => obj.gameObject.activeSelf;
        
        // Destroy
        public void Destroy()
        {
            GameObject.Destroy(objectsParent.gameObject);
            objects.Clear();
        }
    }
}