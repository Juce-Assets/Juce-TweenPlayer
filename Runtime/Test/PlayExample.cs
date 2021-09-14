using Juce.TweenPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class PlayExample : MonoBehaviour
    {
        [SerializeField] private TweenPlayer tweenPlayer = default;

        private void Start()
        {
            // Plays the sequence
            tweenPlayer.Play();

            // Instantly stops the sequence
            tweenPlayer.Kill();

            // Instantly reaches end of sequence
            tweenPlayer.Complete();
        }
    }
}
