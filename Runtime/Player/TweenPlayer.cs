using Juce.Tweening;
using Juce.TweenComponent.BindableData;
using Juce.TweenComponent.Components;
using Juce.TweenComponent.Flow;
using Juce.TweenComponent.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Juce.TweenComponent
{
    public class TweenPlayer : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        private bool showProgressBars = default;

        [SerializeField, HideInInspector]
        private ExecutionMode executionMode = ExecutionMode.Manual;

        [SerializeField, HideInInspector]
        private LoopMode loopMode = LoopMode.Disabled;

        [SerializeField, HideInInspector]
        private ResetMode loopResetMode = ResetMode.InitialValues;

        [SerializeField, HideInInspector] 
        private int loops = default;

        [SerializeField, HideInInspector, SerializeReference]
        public List<TweenPlayerComponent> Components = new List<TweenPlayerComponent>();

        [SerializeField, HideInInspector]
        private bool bindingEnabled = default;

        [SerializeField, HideInInspector] 
        private string bindableDataUid = default;

        private bool destroyed;

        private TweenPlayerCache tweenPlayerCache = new TweenPlayerCache();

        private ISequenceTween currMainSequence;

        public bool BindingEnabled => bindingEnabled;
        public string BindableDataUid => bindableDataUid;

        public float TimeScale { get; private set; } = 1.0f;

        public bool IsPlaying
        {
            get
            {
                if(currMainSequence == null)
                {
                    return false;
                }

                return currMainSequence.IsPlaying;
            }
        }

        private void Awake()
        {
            TryPlay(ExecutionMode.Awake);
        }

        private void Start()
        {
            TryPlay(ExecutionMode.Start);
        }

        private void OnEnable()
        {
            TryPlay(ExecutionMode.OnEnable);
        }

        private void OnDestroy()
        {
            destroyed = true;
        }

        public TweenPlayerComponent AddTweenPlayerComponent(Type type)
        {
            return AddTweenPlayerComponent(type, Components.Count);
        }

        public TweenPlayerComponent AddTweenPlayerComponent(Type type, int index) 
        {
            index = Math.Max(index, 0);
            index = Math.Min(index, Components.Count);

            TweenPlayerComponent instance = Activator.CreateInstance(type) as TweenPlayerComponent;

            if(instance == null)
            {
                return null;
            }

            Components.Insert(index, instance);

            tweenPlayerCache.Clear();

            return instance;
        }

        public T GetTweenPlayerComponent<T>() where T : TweenPlayerComponent
        {
            Type type = typeof(T);

            foreach(TweenPlayerComponent component in Components)
            {
                if(component.GetType() == type)
                {
                    return component as T;
                }
            }

            return null;
        }

        public bool TryGetTweenPlayerComponent<T>(out T component) where T : TweenPlayerComponent
        {
            component = GetTweenPlayerComponent<T>();

            return component != null;
        }

        public void Bind(IBindableData bindableData)
        {
            if (destroyed)
            {
                return;
            }

            bool couldBind = TweenPlayerUtils.TryBindData(this, tweenPlayerCache, bindableData);

            if(!couldBind)
            {
                return;
            }

            foreach (TweenPlayerComponent component in Components)
            {
                if (component == null)
                {
                    continue;
                }

                component.OnBind(bindableData);
            }
        }

        private void TryPlay(ExecutionMode mode)
        {
            if(executionMode != mode)
            {
                return;
            }

            Play();
        }

        public float GetNormalizedProgress()
        {
            if (currMainSequence == null)
            {
                return 0.0f; 
            }
;
            return currMainSequence.GetNormalizedProgress();
        }

        public float GetDuration()
        {
            if (currMainSequence == null)
            {
                return 0.0f;
            }
;
            return currMainSequence.GetDuration();
        }

        public ISequenceTween GenerateSequence()
        {
            if(destroyed)
            {
                return JuceTween.Sequence();
            }

            FlowContext context = new FlowContext();

            foreach (TweenPlayerComponent component in Components)
            {
                if (component == null)
                {
                    UnityEngine.Debug.LogError($"Null component found at {nameof(TweenPlayer)}", this);

                    continue;
                }

                if (!component.Enabled)
                {
                    continue;
                }

                ISequenceTween sequence = JuceTween.Sequence();

                component.Execute(context, sequence);

                context.CurrentSequence.Join(sequence);
            }

            context.MainSequence.Append(context.CurrentSequence);

            SetLoop(context.MainSequence, loopMode);

            currMainSequence = context.MainSequence;

            return context.MainSequence;
        }
         
        public void Play(bool instantly = false)
        {
            ISequenceTween sequence = GenerateSequence();

            sequence.SetTimeScale(TimeScale);

            if (!instantly)
            {
                sequence.Play();
            }
            else
            {
                sequence.Complete();
            }
        }

        public void Complete()
        {
            if (currMainSequence == null)
            {
                return;
            }
;
            currMainSequence.Complete();
        }

        public void Kill()
        {
            if (currMainSequence == null)
            {
                return;
            }

            currMainSequence.Kill();
        }

        public void Reset()
        {
            if (currMainSequence == null)
            {
                return;
            }
;
            currMainSequence.Reset(kill: true);
        }

        public void Replay()
        {
            if (currMainSequence == null)
            {
                return;
            }
;
            currMainSequence.Replay();
        }

        public void Play(IBindableData bindableData, bool instantly = false)
        {
            Bind(bindableData);

            Play(instantly);
        }

        public Task Play(bool instantly, CancellationToken cancellationToken)
        {
            TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();

            currMainSequence.OnCompleteOrKill += () => taskCompletionSource.TrySetResult(default);

            Play(instantly);

            cancellationToken.Register(Kill);

            return taskCompletionSource.Task;
        }

        public Task Play(IBindableData bindableData, CancellationToken cancellationToken)
        {
            Bind(bindableData);

            return Play(instantly: false, cancellationToken);
        }

        public Task Play(IBindableData bindableData, bool instantly, CancellationToken cancellationToken)
        {
            Bind(bindableData);

            return Play(instantly, cancellationToken);
        }

        public void SetTimeScale(float timeScale)
        {
            TimeScale = timeScale;

            if (currMainSequence == null)
            {
                return;
            }

            currMainSequence.SetTimeScale(TimeScale);
        }

        public Task AwaitCompleteOrKill(CancellationToken cancellationToken)
        {
            if(currMainSequence == null)
            {
                return Task.CompletedTask; 
            }

            TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();

            currMainSequence.OnCompleteOrKill += () => taskCompletionSource.TrySetResult(default);

            cancellationToken.Register(() =>
            {
                taskCompletionSource.TrySetResult(default);
            });

            return taskCompletionSource.Task;
        }

        private void SetLoop(ITween tween, LoopMode loopMode)
        {
            switch (loopMode)
            {
                case LoopMode.XTimes:
                    {
                        tween.SetLoops(loops, loopResetMode);
                    }
                    break;

                case LoopMode.UntilManuallyStopped:
                    {
                        tween.SetLoops(int.MaxValue, loopResetMode);
                    }
                    break;
            }
        }
    }
}
