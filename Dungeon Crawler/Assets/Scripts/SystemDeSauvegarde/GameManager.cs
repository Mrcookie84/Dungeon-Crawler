using SystemDeSauvegarde.Seralization;
using TMPro;
using UnityEngine;

namespace SystemDeSauvegarde
{
    public class GameManager : MonoBehaviour
    {
        public TextMeshProUGUI PlayerLeveltext;
        public TextMeshProUGUI PlayerXPText;

        private void Start()
        {
            UpdatePlayerLevelUI();
            UpdatePlayerXPUI();
        }


        public void UpdatePlayerLevelUI()
        {
            PlayerLeveltext.text = "Player level : " + SaveData.currentData.playerProfile.PlayerLevel.ToString();
        }
    
        public void UpdatePlayerXPUI()
        {
            PlayerXPText.text = "Player XP : " + SaveData.currentData.playerProfile.PlayerXP.ToString();
        }

        public void AddPlayerLevel(int _numberOfLevel)
        {
            SaveData.currentData.playerProfile.PlayerLevel += _numberOfLevel;
            UpdatePlayerLevelUI();
        }
    
        public void AddPlayerXp(int _numberOfXP)
        {
            SaveData.currentData.playerProfile.PlayerXP += _numberOfXP;
            UpdatePlayerXPUI();
        }

        public void OnSave()
        {
            SerializationManager.Save("Save_1", SaveData.currentData);
        }
    
        public void OnLoad()
        {
            SaveData.currentData =   (SaveData)SerializationManager.Load("Save_1");
            UpdatePlayerLevelUI();
            UpdatePlayerXPUI();
        }
    
    }
}
