using Juce.Tween;

namespace JuceNew.Examples
{
    public class CooldownHelper
    {
        private readonly float cooldownTime;

        public bool CooldownValue { get; private set; }

        public CooldownHelper(float time)
        {
            this.cooldownTime = time;
        }

        public void Reset()
        {
            if(CooldownValue)
            {
                return;
            }

            CooldownValue = true;

            ITween waitTween = new WaitTimeTween(cooldownTime);

            waitTween.OnComplete += OnCooldownComplete;

            waitTween.Play();
        }

        private void OnCooldownComplete()
        {
            CooldownValue = false;
        }
    }
}
