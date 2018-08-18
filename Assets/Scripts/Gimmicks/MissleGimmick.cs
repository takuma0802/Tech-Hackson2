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
		OnInitialize();
	}

    public void OnInitialize()
	{
		gimmickBody.SetActive(false);

		triggerObject = GetComponentInChildren<BaseGimmickTrigger>();
		triggerObject.OnTrigger
			.SkipLatestValueOnSubscribe()
			.Subscribe(_ => 
			{
				StartCoroutine(AppearGimmick());
			});
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		GetComponent<BoxCollider2D>().enabled = false;
        var player = col.GetComponent<IDamagable>();
        if (player != null)
        {
			player.Damage();
        }
	}

	private IEnumerator AppearGimmick()
    {
        gimmickBody.SetActive(true);
		gameObject.transform.DOLocalMoveY(-0.8f,0.1f);
		yield return new WaitForSeconds(1f);
		gameObject.transform.DOLocalMoveY(3f,0.1f);
		yield return new WaitForSeconds(1f);
		gameObject.transform.DOLocalMoveY(-0.8f,0.1f);
		gameObject.SetActive(false);
    }
}