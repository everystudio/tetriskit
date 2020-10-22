using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tetriskit
{
    public class GameMain : MonoBehaviour
    {
        Mino currentMino;
        public GameObject[] m_prefMinoArr;
        public Transform m_tfWaitingHolder;

        public Queue<GameObject> m_queStandbyMino = new Queue<GameObject>();

        public GameObject m_goBlockHolder;

        public bool m_bInitialized = false;

        void Start()
        {
            Debug.Log("Start");
            int[] make_arr = GetStandbyIndex.Get(m_prefMinoArr.Length);

            for (int loop = 0; loop < 2; loop++)
            {
                for (int i = 0; i < m_prefMinoArr.Length; i++)
                {
                    int index = make_arr[i];
                    GameObject temp = Instantiate(m_prefMinoArr[index], m_tfWaitingHolder);

                    float fPitchSclae = 0.5f;
                    temp.transform.localScale = Vector3.one * fPitchSclae;
                    temp.transform.localPosition = new Vector3(
                        0.0f,
                        (loop * m_prefMinoArr.Length + i) * -4.0f * fPitchSclae,
                        0.0f);

                    m_queStandbyMino.Enqueue(temp);
                }
            }
            m_bInitialized = true;
        }




        public void Spawn()
        {
            Debug.Log("Spawn");

            // Spawn Group at current Position
            GameObject temp = m_queStandbyMino.Dequeue();

            if (currentMino != null)
            {
                currentMino.movementController.DeleteGhost();
            }
            currentMino = temp.GetComponent<Mino>();
            temp.transform.parent = m_goBlockHolder.transform;
            temp.transform.localScale = Vector3.one;
            temp.transform.localPosition = Vector3.zero;


            if (GridManager.Instance != null && !GridManager.Instance.Fall(currentMino, 0))
            {
                // GameOver
                Debug.Log("GameOver");
            }
            else
            {
                Debug.Log("大丈夫だった");
            }

            GridManager.Instance.GhostFix(currentMino);


            int iLoopIndex = 0;
            foreach (GameObject obj in m_queStandbyMino)
            {
                float fPitchSclae = 0.5f;
                obj.transform.localScale = Vector3.one * fPitchSclae;
                obj.transform.localPosition = new Vector3(
                    0.0f,
                    iLoopIndex * -4.0f * fPitchSclae,
                    0.0f);

                iLoopIndex += 1;
            }

        }

        public void RotateClockWise(bool _bIsCw)
        {
            GridManager.Instance.RotateClockWise(_bIsCw, currentMino);
            /*
            //currentMino.movementController.RotateClockWise(_bIsCw);
            float rotationDegree = (_bIsCw) ? 90.0f : -90.0f;
            currentMino.transform.RotateAround(currentMino.movementController.transform.position, Vector3.forward, rotationDegree);
            */
        }

        public int Fall()
        {
            if (GridManager.Instance != null && !GridManager.Instance.Fall(currentMino))
            {
                Debug.Log("FallFix");

                if (!GridManager.Instance.IsAlive(currentMino))
                {
                    Debug.LogError("GameOver");
                    return -1;
                }

                currentMino.movementController.enabled = false;
                currentMino.enabled = false;
                currentMino = null;

                GridManager.Instance.PlaceMinos();
                return 1;
            }
            return 0;
        }
        public void MoveLeft(bool _bIsLeft)
        {
            GridManager.Instance.MoveHorizontal(currentMino, _bIsLeft);
        }


    }
}




