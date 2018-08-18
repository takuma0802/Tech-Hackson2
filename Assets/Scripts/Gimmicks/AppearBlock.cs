using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class AppearBlock : MonoBehaviour,IBaseGimmick {

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
                AppearGimmick();
            });
    }

    // void OnTriggerEnter2D(Collider2D col)
    // {
	// 	GetComponent<BoxCollider2D>().enabled = false;
    //     var player = col.GetComponent<IDamagable>();
    //     if (player != null)
    //     {
	// 		player.Damage();
    //     }
    // }

    private void AppearGimmick()
    {
        gimmickBody.SetActive(true);
		
    }
}
