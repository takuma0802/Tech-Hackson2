using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : PlayerCore {

	private GameObject cameraTarget;
	
	protected override void OnInitialize()
    {
		cameraTarget = this.gameObject;
	}
}
