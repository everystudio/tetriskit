using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace tetriskit
{
	public class Singleton<T> : StateBehavor where T : Singleton<T>
	{
		public static T Instance;
        public virtual void Initialize() { }
		private void Awake()
		{
            if (Instance == null)
            {
                System.Type type = typeof(T);
                Instance = GameObject.FindObjectOfType(type) as T;
                Initialize();
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(gameObject);
                }
            }
            DontDestroyOnLoad(gameObject);
        }
	}
}
