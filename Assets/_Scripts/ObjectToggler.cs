using OutFoxeed.UsefulStructs;
using UnityEngine;

namespace CoopHead
{
    public class ObjectToggler : MonoBehaviour
    {
        [Header("IDK"), SerializeField] private bool enableOnlyOne;
        [SerializeField] private int nextGroupToToggle;
        [SerializeField] private List2D<GameObject> groups;

        [Header("IDK"), SerializeField] private ManualCooldown toggleCooldown;

        private void Start()
        {
            ToggleGroups();
            toggleCooldown = new ManualCooldown(toggleCooldown.Duration, () =>
            {
                ToggleGroups();
                toggleCooldown.Reset();
            });
        }

        private void Update()
        {
            toggleCooldown.Decrease(Time.deltaTime);
        }

        private void ToggleGroups()
        {
            for (int i = 0; i < groups.Length; i++)
            {
                bool enable = nextGroupToToggle == i;
                if (!enableOnlyOne) enable = !enable;

                for (int j = 0; j < groups[i].Length; j++)
                {
                    groups[i][j].SetActive(enable);
                }
            }
            nextGroupToToggle = (nextGroupToToggle+1) % groups.Length;
        }

        [System.Serializable]
        public class List2D<T> where T : UnityEngine.Object
        {
            [SerializeField] private List<T>[] arrays;
            public int Length => arrays.Length;
            public List<T> this[int index] => arrays[index];
        }
        [System.Serializable]
        public class List<T> where T : UnityEngine.Object
        {
            [SerializeField] private T[] values;
            public int Length => values.Length;
            public T this[int index] => values[index];
        }
    }

}