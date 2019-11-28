using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI dialogTextGUI;

    public void Say(string text)
    {
        dialogTextGUI.text = "";
        StartCoroutine(SetText(text));
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
