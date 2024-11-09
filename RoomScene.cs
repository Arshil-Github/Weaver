using UnityEngine;

public class RoomScene : MonoBehaviour
{
    [SerializeField] private ConditionalConversationTrigger door;
    
    public void OpenDoor()
    {
        door.condition.SetConditionSatisfied(true);
        Player.Instance.ShowFlyingText("Found the key! The door is now open!");
    }

    public void LockedDoor()
    {
        Player.Instance.ShowFlyingText("Door is locked!");
    }
}
