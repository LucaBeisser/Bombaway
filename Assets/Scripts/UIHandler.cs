using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIHandler : Singleton<UIHandler>
{
    [SerializeField] string StartSceneName = "StartScene";
    [SerializeField]
    RectTransform goblinUIParent;
    [SerializeField]
    GameObject goblinUIPrefab;

    [SerializeField]
    GameObject scoreSection;
    [SerializeField]
    TextMeshProUGUI gnomesLeftText, bombsLeftText, destroyedObjectsCountText, inTimeText, scoreDestroyedObjectsText, scoreTimeText, scorebombsText, scoreGoblinsText, finalScoreText;

    private void Start()
    {
        BombsAndGoblinsTracker.Instance.OutOfBombs += OnEnd;
        BombsAndGoblinsTracker.Instance.CollectedAllGoblins += OnEnd;
        BombsAndGoblinsTracker.Instance.GoblinAdded += OnGoblinAdded;

        gameObject.SetActive(true);
        scoreSection.SetActive(false);
        UpdateGoblinUI(0, BombsAndGoblinsTracker.Instance.TotalGoblins);
    }

    private void OnGoblinAdded()
    {
        UpdateGoblinUI(BombsAndGoblinsTracker.Instance.CollectedGoblins, BombsAndGoblinsTracker.Instance.TotalGoblins);
    }

    private void OnEnd()
    {
        var bombs = GameObject.FindObjectsOfType<Bomb>();

        ShowFinalScore(BombsAndGoblinsTracker.Instance.CollectedGoblins, BombsAndGoblinsTracker.Instance.TotalGoblins, bombs.Length, BombsAndGoblinsTracker.Instance.TotalDestroyedObjects);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenue();
        }
    }

    void UpdateGoblinUI(int found, int max)
    {
        for (int i = goblinUIParent.childCount - 1; i >= 0; i--)
        {
            Transform child = goblinUIParent.GetChild(i);
            if (child != null)
                Destroy(child.gameObject);
        }

        for (int i = 0; i < max; i++)
        {
            GameObject goblin = Instantiate(goblinUIPrefab, goblinUIParent);
            goblin.transform.localPosition = (Vector3.right * 60 * i);

            if (i >= found)
            {
                Image image = goblin.GetComponent<Image>();
                if (image != null)
                {
                    image.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                }
            }
        }
    }

    public void ShowFinalScore(int gnomesCollected, int gnomesMax, int bombsLeft, int destroyedObjects)
    {
        scoreSection.SetActive(true);
        gnomesLeftText.text = gnomesCollected + " / " + gnomesMax + " Gnomes saved";
        bombsLeftText.text = bombsLeft + " bombs left";
        destroyedObjectsCountText.text = $"Destroyed {destroyedObjects} objects";
        inTimeText.text = $"in {(int)Time.timeSinceLevelLoad} seconds";
        finalScoreText.text = Score.Instance.FinalScore.ToString();

        SetText(scoreGoblinsText, Score.Instance.GoblinScore);
        SetText(scoreTimeText, Score.Instance.TimeScore);
        SetText(scorebombsText, Score.Instance.BombScore);
        SetText(scoreDestroyedObjectsText, Score.Instance.DestroyedObjectScore);
    }
    private void SetText(TextMeshProUGUI textMesh, int score)
    {
        if (score < 0)
        {
            textMesh.color = Color.red;
        }
        else
        {
            textMesh.color = Color.white;
        }

        textMesh.text = score.ToString();
    }

    public void ReturnToMenue()
    {
        SceneManager.LoadScene(StartSceneName);
    }

    public void NextScene()
    {
        Debug.Log("TRYING TO LOAD NEXT SCENE");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
