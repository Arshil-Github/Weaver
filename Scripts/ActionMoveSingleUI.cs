using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionMoveSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionMoveName;
    public Move move;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    public void Setup(Move m, Action<Move> moveAction)
    {
        this.actionMoveName.text = m.moveName;
        this.move = m;
        button.onClick.AddListener(() => moveAction(move));
    }
}
