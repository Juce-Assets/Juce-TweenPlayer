using Juce.TweenPlayer;
using UnityEngine;

namespace JuceNew.Example1
{
    public class Example1CharacterPanelUI : MonoBehaviour
    {
        [Header("TweenPlayers")]
        [SerializeField] private TweenPlayer updateCharacterTweenPlayer = default;

        public void UpdateCharacter(
            string characterName,
            Sprite characterSprite,
            bool left,
            bool instantly
            )
        {
            Example1CharacterPanelDataBinding characterPanelDataBinding = new Example1CharacterPanelDataBinding();

            characterPanelDataBinding.CharacterName = characterName;
            characterPanelDataBinding.CharacterSprite = characterSprite;
            characterPanelDataBinding.Left = left;

            //updateCharacterTweenPlayer.Complete();
            updateCharacterTweenPlayer.Play(characterPanelDataBinding, instantly);
        }
    }
}
