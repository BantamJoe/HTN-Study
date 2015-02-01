using UnityEngine;
using System.Collections.Generic;
using System.Reflection;


namespace HTNPlanner
{

    public interface IHTNDomain
    {
        void LoadDomainStates();
        Dictionary<string, MethodInfo[]> GetActionMethods();
    }
}
