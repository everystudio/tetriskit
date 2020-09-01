using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace tetriskit
{
	public class MinoPiece : MonoBehaviour
	{
		public UnityEvent OnDestroyed = new UnityEvent();
		private void OnDestroy()
		{
			Debug.Log("Destroyed");
			OnDestroyed.Invoke();
		}
	}
}
