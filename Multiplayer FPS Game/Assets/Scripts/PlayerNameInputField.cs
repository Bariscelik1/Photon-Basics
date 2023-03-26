using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

namespace myNamespace {

    [RequireComponent(typeof(InputField))] //scriptin atandığı objede input field olması gerekiyor.
    public class PlayerNameInputField : MonoBehaviour
    {

        const string playerNamePrefKey = "PlayerName"; //Bir değişkenin değerinin program boyunca sabit olarak tutulması istendiğinde const (sabit) ifadesinden yararlanılır



        void Start()
        {
            string defaultName = string.Empty;
            InputField _inputField = this.GetComponent<InputField>();
            if (_inputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;

                }
            }

            PhotonNetwork.NickName = defaultName;
        }

        public void SetPlayerName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Debug.Log("player name is null or empty");
                return;

            }
            PhotonNetwork.NickName = value;
            PlayerPrefs.SetString(playerNamePrefKey, value);
        }

    }
}

