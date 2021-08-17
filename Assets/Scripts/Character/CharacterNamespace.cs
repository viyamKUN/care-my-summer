namespace CharacterNamespace
{

    [System.Serializable]
    public class CharacterStatus
    {
        public uint Level;
        public uint GrowGage;
        public int Money;
        public CharacterStatus()
        {
            Level = 0;
            GrowGage = 0;
            Money = 2000;
        }
    }
}
