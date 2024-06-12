namespace Wuthering_Waves_comfort_vision.Data.Team
{
    public class Team
    {
        public string name;
        public string description;
        public Character firstHero = new();
        public Character secondHero = new();
        public Character thirdHero = new();


        public void SaveAllHero()
        {
            firstHero.SaveSkillData();
            secondHero.SaveSkillData();
            thirdHero.SaveSkillData();
        }

        public void ResetData()
        {
            firstHero.ResetData();
            secondHero.ResetData();
            thirdHero.ResetData();
        }


    }
}
