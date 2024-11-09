using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBattleUI : MonoBehaviour
{
    [SerializeField] private Image characterSprite;
    [SerializeField] private Slider characterHealthSlider;
    [SerializeField] private TextMeshProUGUI characterHealthText;
    
    private float maxHealth;
    public void SetupMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        characterHealthSlider.maxValue = maxHealth;
        characterHealthSlider.value = maxHealth;
        characterHealthText.text = maxHealth + "/" + maxHealth;
    }
    public void SetCurrentHealth(float currentHealth)
    {
        characterHealthSlider.value = currentHealth;
        characterHealthText.text = currentHealth + "/" + maxHealth;
    }
}
