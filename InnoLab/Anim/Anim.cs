using System;

namespace InnoLab
{
    namespace Anim
    {
        using Curve;
        using CurveFunc = Func<float, float>;
        public class Anim
        {
            string m_name;
            static int cnt = 0;
            private CurveFunc m_animFunc = null;
            private Action<float> m_action = null;
            private Action m_onComplete = null;
            private float m_duration = 1;
            public Anim SetCurve(CurveType curveType)
            {
                m_animFunc = Curve.Curve.GetFunc(curveType);
                return this;
            }
            
            public Anim SetCurve(CurveFunc animFunc)
            {
                m_animFunc = animFunc;
                return this;
            }

            public Anim SetAction(Action<float> action)
            {
                m_action = action;
                return this;
            }

            public Anim SetName(string name)
            {
                m_name = name;
                return this;
            }

            public Anim SetDuration(float duration)
            {
                m_duration = duration;
                return this;
            }

            public Anim OnComplete(Action onComplete)
            {
                m_onComplete = onComplete;
                return this;
            }

            public CurveFunc GetAnimFunc()
            {
                return m_animFunc;
            }

            public Action<float> GetAction()
            {
                return m_action;
            }
            
            public Action GetOnComplete()
            {
                return m_onComplete;
            }

            public float GetDuration()
            {
                return m_duration;
            }

            public string GetName()
            {
                if (m_name == null) m_name = "anim" + cnt++;
                return m_name;
            }
            
            public void Play()
            {
                GO.GO.Instance.StartAnim(this);
            }

            public void Stop()
            {
                GO.GO.Instance.StopAnim(m_name);
            }
        }

    }
}
