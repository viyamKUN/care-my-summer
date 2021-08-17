namespace CharacterNamespace
{

    [System.Serializable]
    public class CharacterStatus
    {
        public int Level;
        public int GrowGage;
        public int Money;
        public CharacterStatus()
        {
            Level = 0;
            GrowGage = 0;
            Money = 2000;
        }
    }
}
