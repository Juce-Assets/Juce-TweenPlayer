using Juce.TweenPlayer;
using UnityEngine;

namespace JuceNew.Example1
{
    public class Example1StatsPanelUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform progressBarContainer = default;

        [Header("TweenPlayers")]
        [SerializeField] private TweenPlayer updateProgressBarsTweenPlayer = default;

        public void UpdateStatsBars(
            float attackNormalized,
            float defenseNormalized,
            float superPowerNormalized,
            bool instantly
            )
        {
            Example1PanelStatsDataBinding panelStatsData = new Example1PanelStatsDataBinding();

            panelStatsData.AttackProgressBarSize = NormalizedFillToRectTransformSizeDelta(attackNormalized);
            panelStatsData.DefenseProgressBarSize = NormalizedFillToRectTransformSizeDelta(defenseNormalized);
            panelStatsData.SuperPowerProgressBarSize = NormalizedFillToRectTransformSizeDelta(superPowerNormalized);

            updateProgressBarsTweenPlayer.Play(panelStatsData, instantly);
        }

        public float NormalizedFillToRectTransformSizeDelta(float normalizedFill)
        {
            float totalSize = progressBarContainer.rect.size.x;

            if(totalSize <= 0)
            {
                return 0.0f;
            }

            return totalSize * normalizedFill;
        }
    }
}
