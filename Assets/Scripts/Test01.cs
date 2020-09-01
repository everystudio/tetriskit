using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tetriskit
{
    public class Test01 : MonoBehaviour
    {
        Mino currentMino;
        public GameObject m_pref;
        public GameObject m_goBlockHolder;

        public void Spawn()
        {
            // Spawn Group at current Position
            GameObject temp = Instantiate(m_pref);
            currentMino = temp.GetComponent<Mino>();
            temp.transform.parent = m_goBlockHolder.transform;
            temp.transform.localPosition = Vector3.zero;
        }

        public void RotateClockWise(bool _bIsCw)
        {

            currentMino.movementController.Rotate(_bIsCw);
            /*
            //currentMino.movementController.RotateClockWise(_bIsCw);
            float rotationDegree = (_bIsCw) ? 90.0f : -90.0f;
            currentMino.transform.RotateAround(currentMino.movementController.transform.position, Vector3.forward, rotationDegree);
            */
        }

        public void Fall()
        {
            if (GridManager.Instance != null && !GridManager.Instance.Fall(currentMino))
            {
                currentMino.movementController.enabled = false;
                currentMino.enabled = false;
                currentMino = null;

                GridManager.Instance.PlaceMinos();
            }
        }
        public void MoveLeft(bool _bIsLeft)
        {
            GridManager.Instance.MoveHorizontal(currentMino, _bIsLeft);
        }



    }
}