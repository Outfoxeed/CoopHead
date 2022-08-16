using UnityEngine;

namespace OutFoxeed.MonoBehaviourBase
{
    public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
                _instance = this as T;
            else
            {
                Destroy(this);
                return;
            }
        }
    }
}