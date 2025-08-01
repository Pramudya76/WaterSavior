using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialoginDreamWordlAfterDone : MonoBehaviour
{
    private string[] lineDialog = new string[] {
        "Hah...? Apa yang terjadi?",
        "Tempat ini lagi... sepertinya aku mulai terbiasa dengan perasaan aneh ini...",
        "Halo, wahai pemuda.",
        "Selamat datang kembali, Penjaga Air.",
        "Apakah semua air yang tercemar... sudah berhasil dibersihkan?",
        "Ya. Engkau telah menunaikan tugasmu dengan baik.",
        "Sungai kini mengalir lebih jernih. Hujan kembali membawa harapan.",
        "Jadi... tugasku sudah selesai?",
        "Untuk saat ini, ya. Tapi menjaga alam bukanlah tugas sesaat.",
        "Ia adalah panggilan hati... yang harus terus kau jaga sepanjang hidup.",
        "Aku mengerti. Aku akan terus menjaga apa yang telah aku mulai.",
        "Terpujilah engkau, wahai pemuda.",
        "Semoga di masa depan, ketika dunia kembali memanggil, engkau siap membantu lagi.",
        "Ingatlah... kita akan selalu terhubung melalui aliran air dan harapan."
    };
    private String[] lineRole = new string[] {
        "Player",
        "Player",
        "Spirit",
        "Spirit",
        "Player",
        "Spirit",
        "Spirit",
        "Player",
        "Spirit",
        "Spirit",
        "Player",
        "Spirit",
        "Spirit",
        "Spirit"

    };
    public GameObject PlayerImageDialog;
    public GameObject SpiritImageDialog;
    public GameObject EnterText;
    public TextMeshProUGUI DialogText;
    private bool isTyping = false;
    private int index = 0;
    public GameObject DialogPanel;
    public CanvasGroup Dialog;
    public GameObject TamatPanel;
    public CanvasGroup TamatPanelCanvas;
    // Start is called before the first frame update
    void Start()
    {
        EnterText.gameObject.SetActive(false);
        DialogPanel.gameObject.SetActive(false);
        PlayerImageDialog.gameObject.SetActive(false);
        SpiritImageDialog.gameObject.SetActive(false);
        TamatPanel.gameObject.SetActive(false);
        StartCoroutine(CDPanelShow());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isTyping)
        {
            index++;
            if (index < lineDialog.Length)
            {
                StartCoroutine(Typing());
            }
            else
            {
                StartCoroutine(DialogShowUpOut(1, 0));
                DialogPanel.gameObject.SetActive(false);
                PlayerImageDialog.gameObject.SetActive(false);
                SpiritImageDialog.gameObject.SetActive(false);
                StartCoroutine(CDTamatPanel(0, 1));
            }
        }
    }

    IEnumerator CDPanelShow()
    {

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

        if (lineRole[index] == "Player")
        {
            PlayerImageDialog.gameObject.SetActive(true);
            SpiritImageDialog.gameObject.SetActive(false);
        }
        else if (lineRole[index] == "Spirit")
        {
            SpiritImageDialog.gameObject.SetActive(true);
            PlayerImageDialog.gameObject.SetActive(false);
        }

        foreach (Char c in lineDialog[index])
        {
            DialogText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        isTyping = false;
        EnterText.gameObject.SetActive(true);
    }

    IEnumerator CDTamatPanel(int start, int target)
    {
        yield return new WaitForSeconds(1f);
        float elapsed = 0f;
        float duration = 1f;
        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            TamatPanelCanvas.alpha = Mathf.Lerp(start, target, t);
            TamatPanel.gameObject.SetActive(true);
            yield return null;
        }
    }

}
