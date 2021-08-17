namespace CharacterNamespace
{

    [System.Serializable]
    public class CharacterStatus
    {
        public int Level;
        public int GrowGage;
        public int Money;
        public int Temper;
        public float Water;
        public CharacterStatus()
        {
            Level = 0;
            GrowGage = 0;
            Money = 2000;
            Temper = 25;
            Water = 0.5f;
        }
    }
}
