using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
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
        Debug.Log("lengthtext "+text.Length);
        foreach(char character in text)
        {
            dialogTextGUI.text += character;
            Debug.Log("text " + dialogTextGUI.text);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
