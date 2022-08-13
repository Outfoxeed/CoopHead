using UnityEngine;

namespace CoopHead
{
    public partial class PlayerController : MonoBehaviour
    {
        private Transform[] boostsClose;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Boost"))
            {   
                AddCloseBoost(other.transform);
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Boost"))
            {
                RemoveCloseBoost(other.transform);
            }
        }
        
        void AddCloseBoost(Transform newBoost)
        {
            for (int i = 0; i < boostsClose.Length; i++)
            {
                if (boostsClose[i])
                    continue;
                boostsClose[i] = newBoost;
                return;
            }
        }
        void RemoveCloseBoost(Transform boost)
        {
            for (int i = 0; i < boostsClose.Length; i++)
            {
                if (boostsClose[i] != boost)
                    continue;
                boostsClose[i] = null;
                return;
            }
        }

        bool HasCloseBoosts()
        {
            for (int i = 0; i < boostsClose.Length; i++)
            {
                if (boostsClose[i])
                    return true;
            }
            return false;
        }
        Transform GetClosestBoost()
        {
            float closestDistance = Mathf.Infinity;
            int closestIndex = -1;

            for (int i = 0; i < boostsClose.Length; i++)
            {
                Transform boost = boostsClose[i];
                if (!boost) 
                    continue;

                float distance = Vector2.Distance(transform.position, boost.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }
            return closestIndex < 0 ? null : boostsClose[closestIndex];
        }

    }
}