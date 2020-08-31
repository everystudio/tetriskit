using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tetriskit
{
    [RequireComponent(typeof(tetriskit.MinoMovementController))]
    public class Mino : MonoBehaviour
    {
        public static readonly int GRID_SIZE = 4;

        [HideInInspector]
        public MinoMovementController movementController;

        private void Awake()
        {
            movementController = GetComponent<MinoMovementController>();
        }
    }
}