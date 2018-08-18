using System.Collections;
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
        //audioManager.PlayBGM(AudioType.TitleBGM);
        Debug.Log("Title");
    }

    private void LifeState()
    {
        SceneManager.LoadScene(SceneState.Life.ToString());
        // var text = GameObject.Find("Canvas/LifeNumber").GetComponent<Text>();  // くっそ最悪じゃああｗｗｗ
        // text.text = playerLife.ToString();

        Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_ =>
        {
            ChangeScene(SceneState.Game);
        }).AddTo(this);
    }

    private void GameState()
    {
        SceneManager.LoadScene(SceneState.Game.ToString());
        //audioManager.PlayBGM(AudioType.GameBGM);
    }

    private void GameOverState()
    {
        audioManager.StopBGM();
    }
}
