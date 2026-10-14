using System.Collections;
using UnityEngine;

namespace Assets.FrameworkDesign.FrameWork.Architecture.Rule
{
    public interface ICanSendQuery : IBelongToArchitecture
    {

        
    }
    public static class CanSendQueryExtension
    {
        public static TResult SendQuery<TResult>(this ICanSendQuery self,IQuery<TResult> query)
        {
            return self.GetArchitecture().SendQuery(query);
        }
    }
}