using System;
using UnityEngine;
using WiSdom.DesignPattern;

namespace WiSdom
{
    public class ObserverExample : MonoBehaviour
    {
        public bool PublishMessage = false;
        public bool SubscribeMessage = false;
        private void Update()
        {
            if (!PublishMessage)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (SubscribeMessage)
                    {
                        SubscribeMessage = false;
                        MessageBus.I.Subscribe<ExampleMessage>(OnExampleMessage);
                    }
                    else
                    {
                        SubscribeMessage = true;
                        MessageBus.I.Unsubscribe<ExampleMessage>(OnExampleMessage);
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    MessageBus.I.Notify<ExampleMessage>(message =>
                    {
                        message.Score = 100;
                        message.PlayerId = "Player1";
                    });
                }
            }
        }

        private void OnExampleMessage(ExampleMessage message)
        {
            
        }
    }
}
