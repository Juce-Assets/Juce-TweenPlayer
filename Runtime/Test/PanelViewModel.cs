using Juce.TweenPlayer;
using System.Threading;
using UnityEngine;

namespace JuceNew
{
    public class PanelViewModel : MonoBehaviour
    {
        [SerializeField] private TweenPlayer setDataBindingPlayer = default;

        public void Init(PanelData panelData)
        {
            PlayAsync(panelData);
        }

        private async void PlayAsync(PanelData panelData)
        {
            await setDataBindingPlayer.Play(panelData, instantly: true, default(CancellationToken));
        }
    }
}
