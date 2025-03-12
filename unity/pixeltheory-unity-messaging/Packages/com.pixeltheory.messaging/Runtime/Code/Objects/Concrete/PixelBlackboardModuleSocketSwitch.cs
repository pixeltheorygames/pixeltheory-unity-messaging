using System;
using System.Collections;
using System.Collections.Generic;
using Pixeltheory.Blackboard;
using Pixeltheory.Messaging;
using UnityEngine;

public class PixelBlackboardModuleSocketSwitch : PixelBlackboardModule
{
    #region Fields
    #region Public
    public const string moduleKey = "BlackboardModuleSocketSwitch";
    #endregion //Public

    #region Inspector
    [SerializeField] private List<PixelSocket> pixelSocketsList;
    #endregion //Inspector
    
    #region Private
    //[NonSerialized] private Dictionary< PixelSocket>
    #endregion //Private
    #endregion //Fields

    #region Properties
    #region PixelBlackboardModule Overrides
    public override string ModuleKey => PixelBlackboardModuleSocketSwitch.moduleKey;
    #endregion //PixelBlackboardModule Overrides
    #endregion //Properties

    #region Methods
    #region PixelBlackboardModule Overrides
    public override void OnBlackboardLoad(PixelBlackboard blackboard)
    {
        
    }
    
    public override void OnBlackboardUnload(PixelBlackboard blackboard)
    {
        
    }
    #endregion //PixelBlackboardModule Overrides

    //#region Public
    //#endregion //Public
    #endregion //Methods
}
