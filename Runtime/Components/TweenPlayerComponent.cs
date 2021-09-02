﻿using Juce.Tweening;
using Juce.TweenPlayer.BindableData;
using Juce.TweenPlayer.Flow;
using Juce.TweenPlayer.Validation;
using UnityEngine;

namespace Juce.TweenPlayer.Components
{
    [System.Serializable]
    public class TweenPlayerComponent
    {
        [SerializeField] private bool folded = default;
        [SerializeField] private bool enabled = true;

        public bool Folded { get => folded; set => folded = value; }
        public bool Enabled { get => enabled; set => enabled = value; }

        public ComponentExecutionResult ExecutionResult { get; protected set; }

        public virtual void Validate(ValidationBuilder validationBuilder) { }
        public virtual string GenerateTitle() { return string.Empty; }
        public virtual void OnBind(IBindableData bindableData) { }
        public virtual void Execute(FlowContext context, ISequenceTween sequence) { }
    }
}
