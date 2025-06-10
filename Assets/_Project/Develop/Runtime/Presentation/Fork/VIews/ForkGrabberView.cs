using UnityEngine;
using R3;

namespace _Project.Develop.Runtime.Presentation.Fork.Views
{
    public class ForkGrabberView : MonoBehaviour
    {
        public Observable<Rigidbody> OnCollisionEnterEvent => _onCollisionEnter;
        public Observable<Rigidbody> OnCollisionExitEvent => _onCollisionExit;

        private Subject<Rigidbody> _onCollisionEnter = new();
        private Subject<Rigidbody> _onCollisionExit = new();

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Pallete") && collision.rigidbody != null)
            {
                _onCollisionEnter.OnNext(collision.rigidbody);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.collider.CompareTag("Pallete") && collision.rigidbody != null)
            {
                _onCollisionExit.OnNext(collision.rigidbody);
            }
        }
    }
}