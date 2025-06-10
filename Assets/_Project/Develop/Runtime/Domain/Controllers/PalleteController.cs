using _Project.Develop.Runtime.Domain.Models;
using _Project.Develop.Runtime.Presentation.Pallete.Views;
using UnityEngine;
using System;
using Zenject;
using R3;

namespace _Project.Develop.Runtime.Domain.Controllers
{
    public class PalleteController : MonoBehaviour
    {
        [SerializeField] private PalleteView _palleteView;

        private PalleteModel _model;

        private IDisposable _disappearTimer;

        [Inject]
        private void Construct(PalleteModel palleteModel)
        {
            _model = palleteModel;
        }

        private void Awake()
        {
            _palleteView.AppearPallete();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Fork"))
            {
                _model.SetGrabState(true);
                CancelDisappearCountdown();
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.collider.CompareTag("Fork"))
            {
                _model.SetGrabState(false);
                if (_model.IsAvailableToDisappear()) StartDisappearCountdown();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("UnloadPoint"))
            {
                _model.SetUnloadState(true);
                if (_model.IsAvailableToDisappear()) StartDisappearCountdown();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("UnloadPoint"))
            {
                _model.SetUnloadState(false);
                CancelDisappearCountdown();
            }
        }

        private void StartDisappearCountdown()
        {
            CancelDisappearCountdown();

            _disappearTimer = Observable.Timer(TimeSpan.FromSeconds(5))
                .Subscribe(_ =>
                {
                    if (_model.IsAvailableToDisappear()) _palleteView.DisappearPallete();
                })
                .AddTo(this);
        }

        private void CancelDisappearCountdown()
        {
            if (_disappearTimer != null)
            {
                _disappearTimer.Dispose();
                _disappearTimer = null;
            }
        }
    }
}