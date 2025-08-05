using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogInBattle : MonoBehaviour
{
    private String[] line = new string[] {
        "Ugh... tempat ini terasa berat...",
        "Kenapa air di sini terlihat begitu keruh...?",
        "Aku ingat... roh air memintaku membersihkan tempat ini...",
        "Tapi... musuh sebanyak ini...?",
        "Apa aku harus mengalahkan mereka semua untuk membersihkan air ini?",
        "*menarik napas*",
        "Baiklah... kalau ini satu-satunya cara...",
        "Demi roh air, demi tempat ini... aku akan mengalahkan kalian semua!"
    };
    public GameObject PlayerImageDialog;
    public GameObject EnterText;
    public TextMeshProUGUI DialogText;
    private bool isTyping = false;
    private int index = 0;
    public GameObject DialogPanel;
    public CanvasGroup Dialog;
    private bool isFinished = false;
    private PlayerAttack PA;
    // Start is called before the first frame update
    void Start()
    {
        PA = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        EnterText.gameObject.SetActive(false);
        DialogPanel.gameObject.SetActive(false);
        PlayerImageDialog.gameObject.SetActive(false);
        if (!PlayerPrefs.HasKey("DialogBattle"))
        {
            StartCoroutine(CDPanelShow());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isTyping && !isFinished)
        {
            index++;
            if (index < line.Length)
            {
                StartCoroutine(Typing());
            }
            else
            {
                StartCoroutine(DialogShowUpOut(1, 0));
                PA.enabled = true;
                DialogPanel.gameObject.SetActive(false);
                PlayerImageDialog.gameObject.SetActive(false);
                isFinished = true;
            }
            PlayerPrefs.SetInt("DialogBattle", 1);
        }
    }
    
    IEnumerator CDPanelShow()
    {
        PA.enabled = false;
        yield return new WaitForSeconds(1f);
        DialogPanel.gameObject.SetActive(true);
        StartCoroutine(DialogShowUpOut(0, 1));
        yield return new WaitForSeconds(1f);
        PlayerImageDialog.gameObject.SetActive(true);
        StartCoroutine(Typing());

    }

    IEnumerator DialogShowUpOut(int start, int target)
    {
        float elapsed = 0f;
        float duration = 1f;
        while (elapsed < duration)
        {
            
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            Dialog.alpha = Mathf.Lerp(start, target, t);
            DialogPanel.gameObject.SetActive(true);
            yield return null;
        }
    }

    IEnumerator Typing()
    {
        EnterText.gameObject.SetActive(false);
        DialogText.text = "";
        isTyping = true;
        foreach (Char c in line[index])
        {
            DialogText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
        EnterText.gameObject.SetActive(true);
    }
    

}
