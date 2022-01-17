using UnityEngine;

namespace Juce.TweenPlayer.Example6
{
    public class Example6 : MonoBehaviour
    {
        [SerializeField] private Renderer rendererToBind = default;
        [SerializeField] private TweenPlayer tweenPlayer = default;

        private void Awake()
        {
            Example6BindableData bindableData = new Example6BindableData()
            {
                Renderer = rendererToBind
            };

            tweenPlayer.Play(bindableData: bindableData);
        }
    }
}
