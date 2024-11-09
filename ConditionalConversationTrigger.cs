using System;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalConversationTrigger : ConversationTrigger
{
    public Condition condition;
    
    public override List<DialogueData> GetDialogues()
    {
        return condition.GetDialogues();
    }
    public void SetConditionSatisfied(bool value)
    {
        condition.SetConditionSatisfied(value);
    }
}

[Serializable]
public class Condition
{
    public List<DialogueData> satisfiedDialogues;
    public List<DialogueData> unsatisfiedDialogues;

    private bool conditionSatisfied = false;
    
    public void SetConditionSatisfied(bool value)
    {
        conditionSatisfied = value;
    }
    public bool GetConditionSatisfied()
    {
        return conditionSatisfied;
    }
    public List<DialogueData> GetDialogues()
    {
        return conditionSatisfied ? satisfiedDialogues : unsatisfiedDialogues;
    }
}
