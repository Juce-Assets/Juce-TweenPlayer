using Juce.TweenPlayer.BindableData;
using UnityEngine;

namespace JuceNew.Example1
{
    [System.Serializable]
    [BindableData("Character Panel", "Examples/1/Character Panel", "a4b718b0-9e3b-11eb-a8b3-0242ac2")]
    public class Example1CharacterPanelDataBinding : IBindableData
    {
        public string CharacterName;
        public Sprite CharacterSprite;
        public bool Left;
    }

}
