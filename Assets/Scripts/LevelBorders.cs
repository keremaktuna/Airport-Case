using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelBorders : MonoBehaviour
{
    public GameObject cautionSign;
    public GameObject restartButton;

    public TextMeshProUGUI timeLeftText;

    public float failTime = 5f;

    private float remainingTime;

    private bool isTimerStarted = false;

    private void Awake()
    {
        if(cautionSign.activeSelf == true)
            cautionSign.SetActive(false);

        if (restartButton.activeSelf == true)
            restartButton.SetActive(false);
    }

    private void Update()
    {
        if(isTimerStarted == true)
        {
            timeLeftText.text = Mathf.Round(remainingTime * 1000.0f) * 0.001f + "secs";
            remainingTime = remainingTime - Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            if (!isTimerStarted)
                StartCoroutine(CheckForTime());
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            StopCoroutine(CheckForTime());
            if (cautionSign.activeSelf == true)
                cautionSign.SetActive(false);
            isTimerStarted = false;
        }
    }

    private IEnumerator CheckForTime()
    {
        isTimerStarted = true;
        remainingTime = failTime;
        cautionSign.SetActive(true);

        yield return new WaitForSeconds(failTime);

        isTimerStarted = false;
        timeLeftText.text = "";
        if (cautionSign.activeSelf == true)
            cautionSign.SetActive(false);
        Time.timeScale = 0f;
        restartButton.SetActive(true);
    }
}
