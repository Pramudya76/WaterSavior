using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogWithSpirit : MonoBehaviour
{
    private string[] lineDialog = new string[] {
        "Huh...? Siapa kamu?",
        "Aku adalah Roh Penjaga Air. Terima kasih telah datang.",
        "Aku... datang? Maksudmu, ini bukan mimpi?",
        "Ini memang dunia mimpi, tapi juga lebih dari sekadar mimpi.",
        "Aku memanggilmu karena air... telah tercemar.",
        "Racun dan kotoran merusak sumber kehidupan kami. Kami perlahan melemah.",
        "Tercemar...? Bagaimana bisa?",
        "Dunia manusia mulai lupa pentingnya menjaga alam. Mereka mencemari sungai, danau, bahkan hujan.",
        "Aku tidak bisa memulihkan semuanya sendiri... Aku butuh bantuanmu.",
        "Tapi... aku hanya anak muda biasa.",
        "Tapi hatimu masih jernih. Itu cukup.",
        "Tolong bantu aku membersihkan air ini... sebelum semuanya terlambat.",
        "Baiklah... Aku akan membantumu.",
        "Terima kasih, Penjaga Baru. Perjalananmu dimulai sekarang...",
    };
    private String[] lineRole = new string[] {
        "Player",
        "Spirit",
        "Player",
        "Spirit",
        "Spirit",
        "Spirit",
        "Player",
        "Spirit",
        "Spirit",
        "Player",
        "Spirit",
        "Spirit",
        "Player",
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
    public LayerMask layer;
    public GameObject ButtonPrefabs;
    public Transform canvaPos;
    private GameObject Button;
    private bool isButton = false;
    private bool isPlayer = true;
    // Start is called before the first frame update
    void Start()
    {
        PlayerImageDialog.gameObject.SetActive(false);
        SpiritImageDialog.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        Collider2D area = Physics2D.OverlapCircle(transform.position, 4f, layer);
        if (area != null)
        {
            if (Button != null)
            {
                Button.transform.position = transform.position + new Vector3(0, 2, 0);
            }
            if (!isButton)
            {
                Button = Instantiate(ButtonPrefabs, transform.position + new Vector3(0, 1, 0), Quaternion.identity, canvaPos);
                isButton = true;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(CDPanel());
                isButton = true;
            }
            if (Input.GetKeyDown(KeyCode.Return) && !isTyping)
            {
                if (index < lineDialog.Length)
                {

                    StartCoroutine(TypingDialog());
                }
                else
                {
                    StartCoroutine(DialogShowUpOut(1, 0));
                    DialogPanel.gameObject.SetActive(false);
                    PlayerImageDialog.gameObject.SetActive(false);
                    SpiritImageDialog.gameObject.SetActive(false);
                    StartCoroutine(CDChangeScene());
                }
            }
        }
        else
        {
            if (isButton)
            {
                Destroy(Button);
                isButton = false;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 4f);
    }

    IEnumerator CDPanel()
    {
        DialogText.text = "";
        StartCoroutine(DialogShowUpOut(0, 1));
        //DialogPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        StartCoroutine(TypingDialog());

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

    IEnumerator TypingDialog()
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
        index++;
    }

    IEnumerator CDChangeScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("InsideHouse");
    }

}
