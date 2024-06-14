using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Wuthering_Waves_comfort_vision.Data.Skill
{
    public class Skill : INotifyPropertyChanged
    {
        private bool _isGlobal = true;
        private bool _isForNextCharacter;
        private bool _isCanselWhenChangeCharacter;
        private bool _isReusedIfSpam = true;
        private bool _isShowCooldown = true;
        private bool _isShowHotkeys = false;
        private bool _isShowHotkeysWhenHPressKey = true;
        private bool _isOutroActivate = false;
        private bool _isIntroActivate = false;
        private bool _isResonanceActivate = false;
        private bool _isEchoActivate = false;
        private bool _isUltimateActivate = false;

        public event PropertyChangedEventHandler PropertyChanged;
        #region data binding
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool isGlobal
        {
            get => _isGlobal;
            set
            {
                if (_isGlobal != value)
                {
                    _isGlobal = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool isForNextCharacter
        {
            get => _isForNextCharacter;
            set
            {
                if (_isForNextCharacter != value)
                {
                    _isForNextCharacter = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool isCanselWhenChangeCharacter
        {
            get => _isCanselWhenChangeCharacter;
            set
            {
                if (_isCanselWhenChangeCharacter != value)
                {
                    _isCanselWhenChangeCharacter = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool isReusedIfSpam
        {
            get => _isReusedIfSpam;
            set
            {
                if (_isReusedIfSpam != value)
                {
                    _isReusedIfSpam = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool isShowCooldown
        {
            get => _isShowCooldown;
            set
            {
                if (_isShowCooldown != value)
                {
                    _isShowCooldown = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool isShowHotkeys
        {
            get => _isShowHotkeys;
            set
            {
                if (_isShowHotkeys != value)
                {
                    _isShowHotkeys = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool isShowHotkeysWhenHPressKey
        {
            get => _isShowHotkeysWhenHPressKey;
            set
            {
                if (_isShowHotkeysWhenHPressKey != value)
                {
                    _isShowHotkeysWhenHPressKey = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool isOutroActivate
        {
            get => _isOutroActivate;
            set
            {
                if (_isOutroActivate != value)
                {
                    _isOutroActivate = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool isIntroActivate
        {
            get => _isIntroActivate;
            set
            {
                if (_isIntroActivate != value)
                {
                    _isIntroActivate = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool isResonanceActivate
        {
            get => _isResonanceActivate;
            set
            {
                if (_isResonanceActivate != value)
                {
                    _isResonanceActivate = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool isEchoActivate
        {
            get => _isEchoActivate;
            set
            {
                if (_isEchoActivate != value)
                {
                    _isEchoActivate = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool isUltimateActivate
        {
            get => _isUltimateActivate;
            set
            {
                if (_isUltimateActivate != value)
                {
                    _isUltimateActivate = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion


        public enum SkillType
        {
            Intro,
            Echo,
            Resonance,
            Inherit1,
            Inherit2,
            Ultimate,
            Outro,
            Elemental,
            Weapon
        }

        public enum SkillActionType
        {
            Buff,
            Cooldown
        }

        public enum CharacterVariant
        {
            Weapon,
            Other,
            Aalto,
            Baizhi,
            Calcharo,
            Chixia,
            Danjin,
            Encore,
            Jianxin,
            Jiyan,
            Lingyang,
            Mortefi,
            Rover,
            Sanhua,
            Taoqi,
            Verina,
            Yangyang,
            Yinlin,
            Yuanwu
        }

        public string image { get; set; } = "";
        public string name { get; set; } = "NewSkill";
        public string description { get; set; } = "NewDescription";
        public string hotkey { get; set; } = "xxxxxx";
        public double duration { get; set; } = 666;
        public int chargesCount { get; set; } = 1;
        public Character sourceCharacter { get; set; }
        public Character destintyCharacter { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SkillType skillType { get; set; } = SkillType.Elemental;

        [JsonConverter(typeof(StringEnumConverter))]
        public SkillActionType skillActionType { get; set; } = SkillActionType.Buff;

        [JsonConverter(typeof(StringEnumConverter))]
        public CharacterVariant characterVariant { get; set; } = CharacterVariant.Weapon;
    }
}
