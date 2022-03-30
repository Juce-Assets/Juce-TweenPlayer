using UnityEngine;

namespace Juce.TweenComponent.Example3
{
    public class PlayInstantlyExample3 : MonoBehaviour
    {
        [SerializeField] private TweenPlayer tweenPlayer = default;

        private void Start()
        {
            tweenPlayer.Play(instantly: true);

            tweenPlayer.Play(instantly: false);
        }
    }
}
