using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject optionsParent;
    [SerializeField] private Transform optionSingleUIpf;

    private List<OptionSingleUI> createdOptions = new List<OptionSingleUI>();

    private void Awake()
    {
        createdOptions = new List<OptionSingleUI>();
    }

    public void ShowDialogueBox()
    {
        gameObject.SetActive(true);
    }
    public void HideDialogueBox()
    {
        HideOptions();
        gameObject.SetActive(false);
    }
    
    public void SetDialogue(DialogueData dialogueData)
    {
        nameText.text = dialogueData.name;
        dialogueText.text = dialogueData.dialogueLine;
    }
    public void ShowOptions(List<OptionData> options, Action<int> indexAction)
    {
        optionsParent.SetActive(true);
        createdOptions = new List<OptionSingleUI>();
        
        //Create Option SIngle UI and append them to parent
        List<int> optionIndices = new List<int>();
        foreach (var option in options)
        {
            var optionSingleUI = Instantiate(optionSingleUIpf).GetComponent<OptionSingleUI>();
            optionSingleUI.transform.SetParent(optionsParent.transform);
            optionSingleUI.SetOption(option);
            optionSingleUI.indexToCall = option.nextDialogueIndex;
            
            createdOptions.Add(optionSingleUI);
            optionIndices.Add(option.nextDialogueIndex);
        }

        for (int i = 0; i < createdOptions.Count; i++)
        {
            createdOptions[i].SetButtonEvent((index) =>
            {
                HideOptions();
                indexAction(index);
            });
        }
    }

    public void HideOptions()
    {
        //Destroy all the options and hide the parent
        foreach (var option in createdOptions)
        {
            Destroy(option.gameObject);
        }
        createdOptions.Clear();
        optionsParent.SetActive(false);
    }
}
