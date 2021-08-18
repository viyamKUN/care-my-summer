using System.Collections.Generic;

namespace CharacterNamespace
{
    [System.Serializable]
    public class UserData
    {
        public int Money;
        public CharacterStatus cStatus;
        public List<int> Endings;
        public List<int> Environments;
        public UserData()
        {
            cStatus = new CharacterStatus();
            Endings = new List<int>();
            Environments = new List<int>();
            Money = 2000;

        }
    }

    [System.Serializable]
    public class CharacterStatus
    {
        public int Level;
        public int GrowGage;
        public float Temper;
        public float Water;
        public bool HasSeed;
        public CharacterStatus()
        {
            Level = 0;
            GrowGage = 0;
            Temper = 30;
            Water = 0.2f;
            HasSeed = false;
        }
    }
}
