using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStandbyIndex : MonoBehaviour
{
	public static int[] Get(int _iLength)
	{
		int[] ret = new int[_iLength];
		int[] prob = new int[_iLength];
		for( int i = 0; i < _iLength; i++)
		{
			ret[i] = 0;
			prob[i] = 100;
		}

		for( int i = 0; i < _iLength; i++)
		{
			int total_prob = 0;
			foreach( int temp_prob in prob)
			{
				total_prob += temp_prob;
			}

			int index = 0;
			int get_rand = Random.Range(0, total_prob);
			foreach( int temp_prob in prob)
			{
				if( get_rand < temp_prob)
				{
					prob[index] = 0;
					break;
				}
				else
				{
					index += 1;
					get_rand -= temp_prob;
				}
			}
			ret[i] = index;
		}
		return ret;
	}
}




