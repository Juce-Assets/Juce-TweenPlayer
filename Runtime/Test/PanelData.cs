using Juce.TweenPlayer.BindableData;
using UnityEngine;

namespace JuceNew
{
    [System.Serializable]
    [BindableData("Panel Data", "Panels/Data", "a4b718b0-9e3b-11eb-a8b3-0242ac130003")]
    public class PanelData : IBindableData
    {
        [SerializeField] public float ShowDelay;
        [SerializeField] public string EventNameString;
        [SerializeField] public Sprite MainImageSprite;
        [SerializeField] public string Progress;
        [SerializeField] [Range(0, 3)] public int Stars;
        [SerializeField] public bool Locked;

        public bool HasStar1 => Stars >= 1;
        public bool HasStar2 => Stars >= 2;
        public bool HasStar3 => Stars >= 3;
    }
}
