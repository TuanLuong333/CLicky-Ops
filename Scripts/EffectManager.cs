using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject effectScreen;
    private TextMeshProUGUI effectText;
    private GameManager gameManager;
    public enum EffectType { DoubleScore, ScoreBonus, SlowTime, FreezeTime }
    private string[] announceEffect = { "you get doubled score", "you get a bonus score ", "you get time slow down ", " you get freeze time" };


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ApplyEffect(int effectId)
    {
        Debug.Log("Effect triggered: " + effectId);
        switch (effectId)
        {
            case 0: // Double current score
                gameManager.UpdateScore(gameManager.getScore());
                break;

            case 1: // Get random bonus score
                int[] possibleScores = { 100, 150, 200, 250 };
                int randomScore = possibleScores[Random.Range(0, possibleScores.Length)];
                gameManager.UpdateScore(randomScore);
                break;

            case 2: // Slow time down by 2
                StartCoroutine(ApplyTimeEffect(0.5f, 5f));
                break;

            case 3: //freeze time in 5s
                StartCoroutine(ApplyTimeEffect(0f, 5f));
                break;

            default:
                Debug.LogWarning("Invalid effect ID: " + effectId);
                break;
        }
    }
    private IEnumerator ApplyTimeEffect(float timeMultiplier, float duration)
    {
        gameManager.setTimeMultiplier(timeMultiplier);
        yield return new WaitForSecondsRealtime(duration);
        gameManager.setTimeMultiplier(1);

    }

    public void ShowEffect(int effectId)
    {
        StartCoroutine(EffectAnnouncement(effectId));
    }    

    private IEnumerator EffectAnnouncement(int effectId)
    {
        Time.timeScale = 0f;
        gameManager.isGameActive = false;
        Debug.Log("Being in effect announcement");
        effectText = effectScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        effectText.text = "The gift: " + announceEffect[effectId];
        effectScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(3.0f);
        effectScreen.SetActive(false);
        Time.timeScale = 1f;
        gameManager.isGameActive = true;
        ApplyEffect(effectId);
    }
}
