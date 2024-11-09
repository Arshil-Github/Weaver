using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWorldUI : MonoBehaviour
{
    [SerializeField] private GameObject promptText;
    [SerializeField] private TextMeshProUGUI flyingText;

    public void ShowPrompt()
    {
        promptText.SetActive(false);
        promptText.SetActive(true);
        
        StopAllCoroutines();
        StartCoroutine(HidePrompt());
    }

    public void TriggerFlyingText(string text)
    {
        flyingText.text = text;
        flyingText.gameObject.SetActive(false);
        flyingText.gameObject.SetActive(true);
        
        StopAllCoroutines();
        StartCoroutine(DisableFlyingText());
    }
    public IEnumerator DisableFlyingText()
    {
        yield return new WaitForSeconds(1f);
        flyingText.gameObject.SetActive(false);
    }
    public IEnumerator HidePrompt()
    {
        yield return new WaitForSeconds(1f);
        promptText.SetActive(false);
    }
}
