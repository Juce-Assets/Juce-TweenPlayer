using Juce.TweenPlayer;
using System.Diagnostics;
using UnityEngine;

namespace JuceNew
{
    public class BindingTests : MonoBehaviour
    {
        [SerializeField] private int iterations = default;
        [SerializeField] private TweenPlayer tweenPlayer = default;

        bool firstUpdate = false;

        private void Update()
        {
            if(firstUpdate)
            {
                return;
            }

            firstUpdate = true;

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            SuperLongData superLong = new SuperLongData();

            for (int i = 0; i < iterations; ++i)
            {
                tweenPlayer.Bind(superLong);
            }

            stopwatch.Stop();

            UnityEngine.Debug.Log($"Bindings took {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
