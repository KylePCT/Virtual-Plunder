using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cannes_Shooter
{
    public class PortalVisual : MonoBehaviour
    {
        public GameObject portalLeft;
        public GameObject portalRight;

        public GameObject portalLeft_inner;
        public GameObject portalRight_inner;

        private Vector3 _portalActualL;
        private Vector3 _portalActualR;

        private void Start()
        {
            //Get correct scale to set the scale back.
            _portalActualL = new Vector3(portalLeft_inner.transform.localScale.x, portalLeft_inner.transform.localScale.y, portalLeft_inner.transform.localScale.z);
            _portalActualR = new Vector3(portalRight_inner.transform.localScale.x, portalRight_inner.transform.localScale.y, portalRight_inner.transform.localScale.z);

            portalLeft.SetActive(false);
            portalRight.SetActive(false);
        }

        public void expandPortals()
        {
            //Scale the sphere with the shader on to show the portals and have correct pivots.
            portalLeft.SetActive(true);
            portalLeft_inner.transform.localScale = Vector3.zero;
            LeanTween.scale(portalLeft_inner, _portalActualL, .2f);
            portalLeft.GetComponentInChildren<ParticleSystem>().Play();

            portalRight.SetActive(true);
            portalRight_inner.transform.localScale = Vector3.zero;
            LeanTween.scale(portalRight_inner, _portalActualR, .2f);
            portalRight.GetComponentInChildren<ParticleSystem>().Play();
        }

        public void reducePortals()
        {
            //Reduce scale to hide portals.
            LeanTween.scale(portalLeft_inner, Vector3.zero, .2f);
            portalLeft.GetComponentInChildren<ParticleSystem>().Stop(); 
            
            LeanTween.scale(portalRight_inner, Vector3.zero, .2f);
            portalRight.GetComponentInChildren<ParticleSystem>().Stop();
        }
    }
}
