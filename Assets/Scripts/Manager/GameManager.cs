using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private SceneStateReactiveProperty currentScene = new SceneStateReactiveProperty(SceneState.Title);
    public IReadOnlyReactiveProperty<SceneState> CurrentSceneState { get { return currentScene; } }

	private IntReactiveProperty playerLife = new IntReactiveProperty(3);

	private GimmickManager gimmickManager;

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
		gimmickManager = GetComponent<GimmickManager>();
		
        CurrentSceneState.Subscribe(state =>
            {
                OnStateChanged(state);
            });
    }

    private void OnStateChanged(SceneState nextScene)
    {
        switch (nextScene)
        {
            case SceneState.Title:
				TitleState();
                break;
            case SceneState.Life:
				LifeState();
                break;
            case SceneState.Game:
				GameState();
                break;
            case SceneState.GameOver:
				GameOverState();
                break;
            default:
                break;
        }
    }

    public void ChangeScene(SceneState stete)
    {
        currentScene.Value = stete;
    }

	private void TitleState()
	{

	}

	private void LifeState()
	{

	}

	private void GameState()
	{

	}

	private void GameOverState()
	{

	}
}
