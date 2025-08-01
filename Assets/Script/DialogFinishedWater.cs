using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogFinishedWater : MonoBehaviour
{
    public GameObject[] WaterIndex;
    private String[] line = new string[] {
        "Akhirnya selesai juga",
        "Hmmm...",
        "Selanjutnya apa lagi?",
        "Mungkin dengan tidur aku bisa bertemu dengan roh itu lagi."
    };
    public TextMeshProUGUI textNarasi;
    private int index = 0;
    public GameObject DialogPanel;
    private bool isTyping = false;
    public GameObject EnterText;
    public CanvasGroup Dialog;
    public GameObject PlayerImageDialog;
    private PlayerMovement PM;
    // Start is called before the first frame update
    void Start()
    {
        PlayerImageDialog.gameObject.SetActive(false);
        DialogPanel.gameObject.SetActive(false);
        EnterText.gameObject.SetActive(false);
        PM = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        if (PlayerPrefs.GetInt("WaterDone") == WaterIndex.Length)
        {
            StartCoroutine(CDOutputDialogPanel());
            PlayerPrefs.SetInt("WaterFinished", 1);
            PlayerPrefs.Save();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isTyping)
        {
            index++;
            if (index < line.Length)
            {
                StartCoroutine(Typing());
            }
            else
            {
                StartCoroutine(DialogShowUpOut(1, 0));
                DialogPanel.gameObject.SetActive(false);
                PlayerImageDialog.gameObject.SetActive(false);
                PM.enabled = true;
            }
        }
    }
    
    IEnumerator CDOutputDialogPanel()
    {
        PM.enabled = false;
        yield return new WaitForSeconds(1f);
        DialogPanel.gameObject.SetActive(true);
        StartCoroutine(DialogShowUpOut(0, 1));
        yield return new WaitForSeconds(1f);
        PlayerImageDialog.gameObject.SetActive(true);
        StartCoroutine(Typing());

    }

    IEnumerator Typing()
    {
        EnterText.gameObject.SetActive(false);
        textNarasi.text = "";
        isTyping = true;
        foreach (Char c in line[index])
        {
            textNarasi.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
        EnterText.gameObject.SetActive(true);
    }

    IEnumerator DialogShowUpOut(float Start, float target)
    {
        float elapsed = 0f;
        float duration = 1f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            Dialog.alpha = Mathf.Lerp(Start, target, t);
            DialogPanel.gameObject.SetActive(true);
            yield return null;
        }
    }

}
