using System;
using UnityEngine;

namespace CoopHead
{
    public class Checkpoint : MonoBehaviour
    {
        private void Start()
        {
            gameObject.tag = "Checkpoint";
        }
    }
}