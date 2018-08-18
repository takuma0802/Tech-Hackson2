using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GimmickManager : MonoBehaviour
{
    public IEnumerator SetAllGimmicks()
    {
		var gimmicks = GameObject.FindGameObjectsWithTag("Enemy");
		Debug.Log(gimmicks.Length);
        for (var i = 0; i < gimmicks.Length; i++)
        {
			var enemy = gimmicks[i].GetComponent<IBaseGimmick>();
			if(enemy != null) enemy.OnInitialize();
        }
		yield return null;
    }
}
