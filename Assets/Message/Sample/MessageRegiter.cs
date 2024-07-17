using System;
using System.Collections;
using UnityEngine;

public class MessageRegiter : MonoBehaviour
{

    private void OnEnable()
    {
        // Đăng ký observer
        MessageManager.Instance.Register<LevelUpMessage>(OnLevelUpMessageReceived, MessageChannel.Gameplay, MessageChannel.UI);
    }
    private void OnDisable()
    {
        // Hủy đăng ký observer
        MessageManager.Instance.Unregister<LevelUpMessage>(OnLevelUpMessageReceived, MessageChannel.Gameplay, MessageChannel.UI);
    }
    // Callback function
    void OnLevelUpMessageReceived(LevelUpMessage message)
    {
        Debug.Log($"Level up");
        // message.Callback(100);
    }
}
