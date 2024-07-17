using System;
using System.Collections;
using UnityEngine;

public class MessageDispatcher : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key pressed");
            SendLevelUpMessage(10);
        }
    }
    // Ví dụ các phương thức gửi tin nhắn cụ thể
    public void SendLevelUpMessage(int newLevel)
    {
        MessageManager.Instance.SendMessage(new LevelUpMessage(newLevel));
        // void OnLevelUpMessageResponse(int obj)
        // {
        //     Debug.Log($"Level up response: {obj}");
        // }
    }

    public void SendPlayerHealthChangedMessage(float newHealth)
    {
        MessageManager.Instance.SendMessage(new PlayerHealthChangedMessage(newHealth));
    }
}
