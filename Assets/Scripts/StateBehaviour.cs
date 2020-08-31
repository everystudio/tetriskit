using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tetriskit
{
	public class StateBehavor : MonoBehaviour
	{
		public System.Action<bool> OnUpdate
		{
			set
			{
				if(on_update != value)
				{
					m_bInitOnUpdate = true;
					on_update_begin = value;
					on_update = value;
				}
			}
		}

		private System.Action<bool> on_update_begin;
		private System.Action<bool> on_update;
		//	, OnFixedUpdate, OnLateUpdate;
		private bool m_bInitOnUpdate = true;
		//private bool m_bInitOnFixedUpdate = true;
		//private bool m_bInitOnLateUpdate = true;

		void Update()
		{
			if (on_update != null)
			{
				if(on_update_begin != null)
				{
					on_update_begin(true);
					on_update_begin = null;
				}
				else
				{
					on_update(false);
				}
				m_bInitOnUpdate = false;
			}
		}
		/*
		void FixedUpdate()
		{
			if (OnFixedUpdate != null)
			{
				bool bInit = OnFixedUpdate != OnFixedUpdatePre;
				OnFixedUpdate(bInit);
				OnFixedUpdatePre = OnFixedUpdate;
			}
		}

		void LateUpdate()
		{
			if (OnLateUpdate != null)
			{
				bool bInit = OnLateUpdate != OnLateUpdatePre;
				OnLateUpdate(bInit);
				OnLateUpdatePre = OnLateUpdate;
			}
		}
		*/
	}
}




