using Mario.Application.Components;
using System;
using UnityEngine;

namespace Mario.Game.Items
{
    public class BrokenBrick : ObjectPool
    {
        [SerializeField] private AudioSource _hitFXAudioSource;
        [SerializeField] private AudioSource _brickFXAudioSource;

        protected override void OnEnable()
        {
            base.OnEnable();
            _hitFXAudioSource.Play();
            _brickFXAudioSource.Play();
        }

        public void OnAnimationCompleted() => gameObject.SetActive(false);
    }
}