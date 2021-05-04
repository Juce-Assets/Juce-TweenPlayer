using UnityEngine;

namespace JuceNew.Example1
{
    [CreateAssetMenu(fileName = nameof(Example1CharacterConfiguration), menuName = "Juce/TweenPlayer/Example1/" + nameof(Example1CharacterConfiguration), order = 1)]
    public class Example1CharacterConfiguration : ScriptableObject
    {
        [SerializeField] private string characterName = default;
        [SerializeField] private Sprite characterSprite = default;
        [SerializeField] [Range(0, 1)] private float attack = default;
        [SerializeField] [Range(0, 1)] private float deffense = default;
        [SerializeField] [Range(0, 1)] private float superPower = default;

        public string CharacterName => characterName;
        public Sprite CharacterSprite => characterSprite;
        public float Attack => attack;
        public float Defense => deffense;
        public float SuperPower => superPower;
    }
}
