using System;
using System.Collections;
using System.Collections.Generic;
using GFA.TPS.Utils;
using UnityEngine;

namespace GFA.TPS
{
    public class XPCollectibleAttractor : MonoBehaviour
    {
        [SerializeField]
        private float _tickInterval = 0.7f;

        [SerializeField]
        private float _attractionRadius = 5;

        private Collider[] _collectiblesInRange = new Collider[20];

        [SerializeField]
        private LayerMask _layerMask;

        public event Action<float> XPCollected;

        private void Start()
        {
            StartCoroutine(Execute());
        }

        private IEnumerator Execute()
        {
            while (true)
            {
                yield return new WaitForSeconds(_tickInterval);
                if (!enabled) yield return null;


                var hitCount = Physics.OverlapSphereNonAlloc(transform.position, _attractionRadius,
                _collectiblesInRange, _layerMask);

                for (int i = 0; i < hitCount; i++)
                {
                    var collider = _collectiblesInRange[i];
                    collider.enabled = false;

                    var follower = collider.gameObject.AddComponent<SmoothFollower>();
                    follower.Target = transform;

                    follower.ReachedDestination += () =>
                    {
                        var collectible = follower.GetComponent<XPCollectible>();
                        XPCollected?.Invoke(collectible.XP);
                        Destroy(follower.gameObject);
                    };
                }

            }
        }
    }
}