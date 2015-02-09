using UnityEngine;
using System.Collections.Generic;
using System.Reflection;



public interface IHTNDomain
    {
        void LoadDomainStates();
        Dictionary<string, MethodInfo[]> GetActionMethods();
    }

