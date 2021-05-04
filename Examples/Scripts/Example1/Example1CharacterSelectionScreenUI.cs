using Juce.TweenPlayer;
using JuceNew.Examples;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JuceNew.Example1
{
    public class Example1CharacterSelectionScreenUI : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private List<Example1CharacterConfiguration> characterConfigurations = default;

        [Header("References")]
        [SerializeField] private ExamplePointerCallbacks nextCharacterLeftPointerCallbacks = default;
        [SerializeField] private ExamplePointerCallbacks nextCharacterRightPointerCallbacks = default;
        [SerializeField] private ExamplePointerCallbacks selectCharacterPointerCallbacks = default;
        [SerializeField] private ExamplePointerCallbacks backToCharacterSelectionPointerCallbacks = default;
        [SerializeField] private Example1StatsPanelUI statsPanel = default;
        [SerializeField] private Example1CharacterPanelUI characterPanel = default;

        [Header("TweenPlayers")]
        [SerializeField] private TweenPlayer characterSelectedTweenPlayer = default;
        [SerializeField] private TweenPlayer backToCharacterSelectionTweenPlayer = default;

        private readonly CooldownHelper cooldown = new CooldownHelper(0.4f);

        private int currentCharacterIndex;

        private void Awake()
        {
            nextCharacterLeftPointerCallbacks.OnClick += OnNextCharacterLeftPointerCallbacksClick;
            nextCharacterRightPointerCallbacks.OnClick += OnNextCharacterRightPointerCallbacksClick;
            selectCharacterPointerCallbacks.OnClick += OnSelectCharacterPointerCallbacksClick;
            backToCharacterSelectionPointerCallbacks.OnClick += OnBackToCharacterSelectionPointerCallbacksClick;

            backToCharacterSelectionTweenPlayer.Play(instantly: true);
            SwapCharacterStats(left: default, instantly: true);
        }

        public void SwapCharacterStats(bool left, bool instantly)
        {
            if(cooldown.CooldownValue)
            {
                return;
            }

            cooldown.Reset();

            Example1CharacterConfiguration characterConfiguration = characterConfigurations[currentCharacterIndex];

            statsPanel.UpdateStatsBars(
                characterConfiguration.Attack,
                characterConfiguration.Defense,
                characterConfiguration.SuperPower,
                instantly
                );

            characterPanel.UpdateCharacter(
                characterConfiguration.CharacterName,
                characterConfiguration.CharacterSprite,
                left,
                instantly
                );
        }

        private void SetNextCharacterIndex()
        {
            currentCharacterIndex++;

            if(currentCharacterIndex >= characterConfigurations.Count)
            {
                currentCharacterIndex = 0;
            }
        }

        private void SetLastCharacterIndex()
        {
            currentCharacterIndex--;

            if (currentCharacterIndex < 0)
            {
                currentCharacterIndex = characterConfigurations.Count - 1;
            }
        }

        private void OnNextCharacterLeftPointerCallbacksClick(ExamplePointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            SetLastCharacterIndex();

            SwapCharacterStats(left: true, instantly: false);
        }

        private void OnNextCharacterRightPointerCallbacksClick(ExamplePointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            SetNextCharacterIndex();

            SwapCharacterStats(left: false, instantly: false);
        }

        private void OnSelectCharacterPointerCallbacksClick(ExamplePointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            characterSelectedTweenPlayer.Play();
        }

        private void OnBackToCharacterSelectionPointerCallbacksClick(ExamplePointerCallbacks pointerCallbacks, PointerEventData pointerEventData)
        {
            backToCharacterSelectionTweenPlayer.Play();
        }
    }
}
