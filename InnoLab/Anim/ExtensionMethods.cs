using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InnoLab.Anim.Curve;
using System;

namespace InnoLab
{
    namespace Anim
    {
        public static class ExtensionMethods
        {
            /// <summary>
            ///         
            /// </summary>
            /// <param name="transform"></param>
            /// <param name="target"></param>
            /// <param name="duration"></param>
            /// <example>this.transform.ILMove(new Vector3(0, 5, 0), 3f).Play();</example>
            /// <returns></returns>
            public static Anim ILMove(this Transform transform, Vector3 target, float duration)
            {
                Anim anim = new Anim();
                anim.SetCurve(CurveType.LinearIn)
                     .SetAction((float t) =>
                     {
                         transform.position = Vector3.Lerp(transform.position, target, t);
                     })
                     .SetDuration(duration);
                return anim;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="transform"></param>
            /// <param name="point"></param>
            /// <param name="axis"></param>
            /// <param name="angle"></param>
            /// <param name="duration"></param>
            /// <example>this.transform.ILRotate(new Vector3(0, 0, 0), Vector3.up, 360, 4f).Play();</example>
            /// <returns></returns>
            public static Anim ILRotate(this Transform transform, Vector3 point, Vector3 axis, float angle, float duration)
            {
                Anim anim = new Anim();
                float progress = 0;
                float x = 0;
                anim.SetCurve(CurveType.LinearIn)
                     .SetAction((float t) =>
                     {
                         x = t - progress;
                         progress += x;
                         transform.RotateAround(point, axis, angle * x);
                     })
                     .SetDuration(duration);
                return anim;
            }

            public static Anim ILRotate(this Transform transform, Quaternion quaternion ,float duration,)
            {
                Anim anim = new Anim();

                // TODO

                return anim;
            }
        }

    }
}
