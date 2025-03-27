namespace SystemDeSauvegarde
{
    [System.Serializable]

    public class SaveData
    {

        private static SaveData _currentData;

        public static SaveData currentData
        {
            get
            {
                if (_currentData == null)
                {
                    _currentData = new SaveData();
                }

                return _currentData;
            }

            set { _currentData = value; }

        }

        public PlayerProfile playerProfile = new PlayerProfile();

    }
}
