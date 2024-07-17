using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MessageManager : ClassSingleton<MessageManager>
{

    private Dictionary<Type, Dictionary<MessageChannel, List<Action<Message>>>> observers =
        new Dictionary<Type, Dictionary<MessageChannel, List<Action<Message>>>>();

    public void Register<T>(Action<T> callback, params MessageChannel[] channels) where T : Message
    {
        Type messageType = typeof(T);
        if (!observers.ContainsKey(messageType))
        {
            observers[messageType] = new Dictionary<MessageChannel, List<Action<Message>>>();
        }

        foreach (var channel in channels)
        {
            if (!observers[messageType].ContainsKey(channel))
            {
                observers[messageType][channel] = new List<Action<Message>>();
            }
            observers[messageType][channel].Add(callback as Action<Message>);
#if UNITY_EDITOR
            Debug.Log($"Register {messageType.Name} on channel {channel}");
#endif
        }
    }

    public void Unregister<T>(Action<T> callback, params MessageChannel[] channels) where T : Message
    {
        Type messageType = typeof(T);
        if (observers.TryGetValue(messageType, out var channelObservers))
        {
            foreach (var channel in channels)
            {
                if (channelObservers.TryGetValue(channel, out var callbacks))
                {
                    callbacks.Remove(callback as Action<Message>);
#if UNITY_EDITOR
                    Debug.Log($"Unregister {messageType.Name} on channel {channel}");
#endif
                }
            }
        }
    }

    public async Task SendMessageAsync<T>(T message) where T : Message
    {
        Type messageType = typeof(T);
        if (observers.TryGetValue(messageType, out var channelObservers))
        {
            var tasks = new List<Task>();
            foreach (var channel in channelObservers.Keys)
            {
                if (channelObservers.TryGetValue(channel, out var callbacks))
                {
                    foreach (var callback in callbacks)
                    {
                        tasks.Add(Task.Run(() => callback?.Invoke(message)));
                    }
#if UNITY_EDITOR
                    Debug.Log($"Message sent: {message.GetType().Name} on channel {channel}");
#endif
                }
            }
            await Task.WhenAll(tasks);
        }

    }

    public void SendMessage<T>(T message)
    {
        Type messageType = typeof(T);
        if (observers.TryGetValue(messageType, out var channelObservers))
        {
#if UNITY_EDITOR
            Debug.Log($"Message sent: {message.GetType().Name} on channel {channelObservers.Count}");
#endif
            foreach (var channel in channelObservers.Keys)
            {
                if (channelObservers.TryGetValue(channel, out var callbacks))
                {
#if UNITY_EDITOR
                    Debug.Log($"channel {channel} has {callbacks.Count} callbacks");
#endif
                    foreach (var callback in callbacks)
                    {
                        callback?.Invoke(message as Message);
                    }
                }
            }
        }
    }
}
