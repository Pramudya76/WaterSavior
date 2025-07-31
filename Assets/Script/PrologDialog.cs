using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class PrologDialog : MonoBehaviour
{
    private String[] line = new string[] { "Di malam yang indah, ketika dunia terlelap dalam keheningan, seorang anak pun menutup matanya tanpa tahu bahwa malam itu akan membawanya ke sesuatu yang luar biasa.", "Tidurnya yang damai perlahan berubah menjadi pengalaman aneh, seperti ada kekuatan tak terlihat yang menariknya menjauh dari kenyataan.", "Tubuhnya terasa ringan, langkahnya melayang tanpa arah, dan semua yang dikenalnya perlahan menghilang dari pandangan.", "Ini bukan sekadar mimpi biasa, tapi sebuah perjalanan menuju tempat yang belum pernah ia bayangkan sebelumnya.", "Tanpa suara, tanpa petunjuk, ia tiba di sebuah dunia asing yang terasa nyata namun tak masuk akal.", "Dunia yang jauh dari rumah, tapi penuh tanda-tanda bahwa kehadirannya di sana bukanlah sebuah kebetulan." };
    public TextMeshProUGUI textNarasi;
    private int index = 0;
    public GameObject DialogPanel;
    private bool isTyping = false;
    public GameObject EnterText;
    private float t = 0.1f;
    public CanvasGroup Dialog;
    // Start is called before the first frame update
    void Start()
    {
        EnterText.gameObject.SetActive(false);
        DialogPanel.gameObject.SetActive(false);
        StartCoroutine(CDOutputDialogPanel());
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
            }
        }
    }

    IEnumerator CDOutputDialogPanel()
    {
        yield return new WaitForSeconds(2f);
        DialogPanel.gameObject.SetActive(true);
        StartCoroutine(DialogShowUpOut(0, 1));
        yield return new WaitForSeconds(1f);
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
