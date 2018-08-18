﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    [SerializeField] private GameObject playerPrefab;
    private PlayerCore player;

    private SceneStateReactiveProperty currentScene = new SceneStateReactiveProperty(SceneState.Title);
    public IReadOnlyReactiveProperty<SceneState> CurrentSceneState { get { return currentScene; } }

    private IntReactiveProperty playerLife = new IntReactiveProperty(3);
    private GimmickManager gimmickManager;
    private AudioManager audioManager;

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
        audioManager = GetComponent<AudioManager>();

        CurrentSceneState.Subscribe(state =>
            {
                OnStateChanged(state);
            });

        playerLife
        .SkipLatestValueOnSubscribe()
        .Subscribe(x =>
        {
                ChangeScene(SceneState.Life);
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
                StartCoroutine(GameState());
                break;
            case SceneState.GameOver:
                GameOverState();
                break;
            default:
                break;
        }
    }

    public void OnClickGameStartButton()
    {
        ChangeScene(SceneState.Life);
    }

    public void ChangeScene(SceneState stete)
    {
        currentScene.Value = stete;
    }

    private void TitleState()
    {
        audioManager.PlayBGM(AudioType.TitleBGM);
    }

    private void LifeState()
    {
        SceneManager.LoadScene(SceneState.Life.ToString());
        // var text = GameObject.Find("Canvas/LifeNumber").GetComponent<Text>();  // くっそ最悪じゃああｗｗｗ
        // text.text = playerLife.ToString();

        Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ =>
        {
            ChangeScene(SceneState.Game);
        }).AddTo(this);
    }

    private IEnumerator GameState()
    {
        SceneManager.LoadScene(SceneState.Game.ToString());
        yield return gimmickManager.SetAllGimmicks();
        audioManager.PlayBGM(AudioType.TitleBGM);
        
        player = Instantiate(playerPrefab).GetComponent<PlayerCore>();
        player.IsAlive
            .SkipLatestValueOnSubscribe()
            .Where(x => x == false)
            .Subscribe(_ =>
            {
                playerLife.Value -= 1;
            });

    }

    private void GameOverState()
    {
        SceneManager.LoadScene(SceneState.GameOver.ToString());
        
        Observable.Timer(TimeSpan.FromSeconds(3)).Subscribe(_ =>
        {
            ChangeScene(SceneState.Title);
            SceneManager.LoadScene(SceneState.Title.ToString());
        }).AddTo(this);
    }
}
