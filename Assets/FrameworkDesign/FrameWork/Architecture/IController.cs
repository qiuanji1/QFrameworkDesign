using Assets.FrameworkDesign.FrameWork.Architecture.Rule;
using FrameworkDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController : IBelongToArchitecture, ICanGetSystem, ICanGetModel, ICanSendCommand,ICanRegisterEvent,ICanSendQuery
{
}
