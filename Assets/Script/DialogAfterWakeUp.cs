using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogAfterWakeUp : MonoBehaviour
{
    private String[] line = new string[] {
        "Hah...? Aku kembali di kamarku...",
        "Apa yang barusan terjadi...?",
        "Itu tadi... mimpi?",
        "Tapi rasanya terlalu nyata... aku bahkan bisa mendengar suara roh itu...",
        "Roh Air... dia memintaku untuk membersihkan air yang tercemar...",
        "Apa itu cuma imajinasiku? Atau... sebuah pertanda?",
        "Hmm...",
        "Entah kenapa... aku merasa harus melakukan sesuatu...",
        "Kalau itu memang cuma mimpi, kenapa hatiku terasa berat begini?",
        "Aku... harus mencari tahu lebih lanjut."
    };
    public TextMeshProUGUI textNarasi;
    private int index = 0;
    public GameObject DialogPanel;
    private bool isTyping = false;
    public GameObject EnterText;
    public CanvasGroup Dialog;
    public GameObject PlayerImageDialog;
    private PlayerMovement PM;
    private bool isDoneDialog = false;
    // Start is called before the first frame update
    void Start()
    {
        PM = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        EnterText.gameObject.SetActive(false);
        DialogPanel.gameObject.SetActive(false);
        PlayerImageDialog.gameObject.SetActive(false);
        //PlayerPrefs.DeleteKey("DialogWakeUp");
        if (PlayerPrefs.GetInt("DialogWakeUp") == 0)
        {
            StartCoroutine(CDOutputDialogPanel());
            PlayerPrefs.SetInt("DialogWakeUp", 1);
            PlayerPrefs.Save();
            isDoneDialog = false;
        }
        else
        {
            isDoneDialog = true; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isTyping && !isDoneDialog)
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
