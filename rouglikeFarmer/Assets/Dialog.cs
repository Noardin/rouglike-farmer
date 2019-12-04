using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI dialogTextGUI;
    public Image Background;

    public void LeaveDialog()
    {
        Destroy(gameObject);
    }
    public void Say(string text, bool background)
    {
        dialogTextGUI.text = "";
        StartCoroutine(SetText(text));
        Background.enabled = background;
    }
    private IEnumerator SetText(string text)
    {
        foreach(char character in text)
        {
            dialogTextGUI.text += character;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
