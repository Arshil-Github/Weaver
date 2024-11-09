using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI optionText;
    public int indexToCall;
    
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetButtonEvent(Action<int> action)
    {
        button.onClick.AddListener(() => { action(indexToCall); });
    }

    public void SetOption(OptionData optionData)
    {
        optionText.text = optionData.dialogueLine;
    }
}
