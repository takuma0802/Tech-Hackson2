using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using System;

public class MissleGimmick : MonoBehaviour , IBaseGimmick
{
	[SerializeField] private GameObject gimmickBody;
	
	private BaseGimmickTrigger triggerObject;

	void Start()
	{
		
	}

    public void OnInitialize()
	{
		gimmickBody.SetActive(false);

		triggerObject = GetComponentInChildren<BaseGimmickTrigger>();
		triggerObject.OnTrigger
			.SkipLatestValueOnSubscribe()
			.Subscribe(_ => 
			{
				AppearGimmick();
			});
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("死んだ！");
	}

	private void AppearGimmick()
    {
        gimmickBody.SetActive(true);
    }
}