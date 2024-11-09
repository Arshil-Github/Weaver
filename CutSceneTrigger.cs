using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
    public UnityEvent OnCutSceneStart;
    public UnityEvent OnCutSceneEnd;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            PlayerPrefs.DeleteAll();
            if (!PlayerPrefs.HasKey("PortalCutScenePlayed") || PlayerPrefs.GetInt("PortalCutScenePlayed") == 0)
            {
                PlayCutScene();
                //Store played in playerprefs
                //PlayerPrefs.SetInt("PortalCutScenePlayed", 1);
            
                StartCoroutine(ExecuteEvents());
            }
            else
            {
                OnCutSceneEnd?.Invoke();
            }
        }
    }
    IEnumerator ExecuteEvents()
    {
        OnCutSceneStart?.Invoke();
        yield return new WaitForSeconds((float)playableDirector.duration);
        OnCutSceneEnd?.Invoke();
    }
    public void PlayCutScene()
    {
        playableDirector.Play();
    }
}
