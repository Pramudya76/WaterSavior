using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialoginDreamWordl : MonoBehaviour
{
    private String[] Startline = new string[] {
        "Hah...? Apa yang terjadi?",
        "Tempat ini... aneh sekali...",
        "Apakah ini mimpi?",
        "Aku tidak ingat bagaimana bisa sampai di sini...",
        "Semuanya terasa begitu nyata, tapi... tidak masuk akal.",
        "Apa ini dunia lain...? Atau hanya mimpi buruk?",
        "Halo...? Apakah ada orang di sini?",
    };
    public GameObject PlayerImageDialog;
    public GameObject SpiritImageDialog;
    public GameObject EnterText;
    public TextMeshProUGUI DialogText;
    private bool isTyping = false;
    private int index = 0;
    public GameObject DialogPanel;
    public CanvasGroup Dialog;
    private PlayerMovement PM;
    private bool isFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        PM = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        EnterText.gameObject.SetActive(false);
        DialogPanel.gameObject.SetActive(false);
        PlayerImageDialog.gameObject.SetActive(false);
        SpiritImageDialog.gameObject.SetActive(false);
        StartCoroutine(CDPanelShow());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isTyping && !isFinished)
        {
            index++;
            if (index < Startline.Length)
            {
                StartCoroutine(Typing());
            }
            else
            {
                StartCoroutine(DialogShowUpOut(1, 0));
                PM.enabled = true;
                DialogPanel.gameObject.SetActive(false);
                PlayerImageDialog.gameObject.SetActive(false);
                isFinished = true;
            }
        }
    }

    IEnumerator CDPanelShow()
    {
        PM.enabled = false;
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
        foreach (Char c in Startline[index])
        {
            DialogText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
        EnterText.gameObject.SetActive(true);
    }
    

}
