using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tetriskit
{
    public class MinoMovementController : MonoBehaviour
    {





       
   public float transitionInterval = 0.8f;
        public void DeleteGuide()
        {
            List<Transform> delete_list = new List<Transform>();
            foreach (Transform t in transform)
            {
                if (t.tag == "guide")
                {
                    delete_list.Add(t);
                }
            }
            foreach( Transform t in delete_list)
            {
                DestroyImmediate(t.gameObject);
            }
        }
        public void ShowGuide()
        {
            DeleteGuide();
            for (int y = 0; y < Mino.GRID_SIZE; y++)
            {
                for (int x = 0; x < Mino.GRID_SIZE; x++)
                {
                    string guide_name = (x == 0 && y == 0) ? "mino_guide_anchor" : "mino_guide";

                    GameObject guide = Instantiate(Resources.Load(guide_name), transform) as GameObject;
                    guide.transform.localPosition = new Vector3(
                        (float)x - 1f, (float)y - 1f);
                }
            }
        }
        public void Rotate( bool _bIsCw)
        {
            //currentMino.movementController.RotateClockWise(_bIsCw);
            float rotationDegree = (_bIsCw) ? 90.0f : -90.0f;
            transform.RotateAround(
                transform.position + transform.up * 0.5f + transform.right * 0.5f,
                Vector3.forward,
                rotationDegree);

            transform.position = new Vector3(
                Mathf.Round(transform.position.x),
                Mathf.Round(transform.position.y));
        }
    }
}


