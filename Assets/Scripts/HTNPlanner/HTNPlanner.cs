using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class HTNPlanner
{

    /// <summary>
    /// A list containing the names of all tasks of the given planning domain for which there are primitive actions
    /// </summary>
    private List<string> operators = new List<string>();

    /// <summary>
    /// A dictionary containing the names of all non-primitive tasks of the given planning domain as dictionary keys, 
    /// and a list containing the names of all HTN-methods, which can decompose the non-primitive tasks into subtasks, as dictionary values
    /// </summary>
    private Dictionary<string, List<string>> methods = new Dictionary<string, List<string>>();

    /// <summary>
    /// The type of the class containing the HTN-methods of the given planning domain
    /// </summary>
    private Type methodsType;

    /// <summary>
    /// The type of the class containing the operators of the given planning domain
    /// </summary>
    private Type operatorsType;
    private int searchDepth = 30;
    private bool cancelSearch;

    public bool CancelSearch
    {
        set
        {
            cancelSearch = value;
        }
    }


    // CONSTRUCTORS

    /// <summary>
    /// Creates a new HTN planner
    /// </summary>
    /// <param name="methodsType">The type of the class containing the HTN-methods of the planning domain</param>
    /// <param name="methodsDict">Dictionary containing task names as keys, and arrays containing all HTN-methods (to decompose the given task into subtasks) as values</param>
    /// <param name="operatorsType">The type of the class containing the operators of the planning domain</param>
    public HTNPlanner(Type methodsType, Dictionary<string, MethodInfo[]> methodsDict, Type operatorsType)
    {
        this.methodsType = methodsType;
        this.operatorsType = operatorsType;

        InitializePlanner(methodsDict);
    }


    // METHODS

    /// <summary>
    /// Initializes the planner by declaring operators and methods.
    /// </summary>
    /// <param name="methodsDict">Dictionary containing task names as keys, and arrays containing all HTN-methods (to decompose the given task into subtasks) as values</param>
    private void InitializePlanner(Dictionary<string, MethodInfo[]> methodsDict)
    {
        DeclareOperators();

        foreach (KeyValuePair<string, MethodInfo[]> method in methodsDict)
        {
            DeclareMethods(method.Key, method.Value);
        }
    }

    /// <summary>
    /// Finds all operators in the given domain and adds their names to the list "operators" for later use.
    /// </summary>
    /// <returns>Returns the list "operators" (for debugging purposes)</returns>
    public List<string> DeclareOperators()
    {
        MethodInfo[] methodInfos = operatorsType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

        operators = new List<string>();

        foreach (MethodInfo info in methodInfos)
        {
            if (info.ReturnType.Name.Equals("State"))
            {
                string methodName = info.Name;
                if (!operators.Contains(methodName))
                    operators.Add(methodName);
            }
        }

        return operators;
    }

    /// <summary>
    /// Finds the non-primitive action matching the task name and the HTN-methods in the given domain which can be used to decompose 
    /// the given task into subtasks, and adds their names to a list.
    /// </summary>
    public List<string> DeclareMethods(string taskName, MethodInfo[] methodInfos)
    {
        List<string> methodList = new List<string>();

        foreach (MethodInfo info in methodInfos)
        {
            if (info != null && info.ReturnType.Name.Equals("List`1"))
            {
                methodList.Add(info.Name);
            }
        }

        if (methods.ContainsKey(taskName))
            methods.Remove(taskName);
        methods.Add(taskName, methodList);

        return methods[taskName];
    }

    public List<string> SolvePlanningProblem(State state, List<List<string>> tasks, int verbose = 0)
    {
        if (verbose > 0)
        {
            //Debug.Log("State = " + state.Name);
            //Debug.Log("Tasks = " + tasks.ToString());
        }
        List<string> result = SeekPlan(state, tasks, new List<string>(), verbose);

        if (cancelSearch)
        {
            if (verbose > 0)
                Debug.Log("No result, search was cancelled");
            cancelSearch = false;
        }
        //else if (verbose > 0)
        //    Debug.Log("Result = " + result.ToString());
        return result;
    }

    public List<string> SeekPlan(State state, List<List<string>> tasks, List<string> plan, int depth = 0, int verbose = 0)
    {
        if (cancelSearch)
            return null;

        if (searchDepth > 0)
        {
            if (depth >= searchDepth)
                return null;
        }
      
        List<string> task = tasks[0];
        if (operators.Contains(task[0]))
        {
            if (verbose > 2)
                Debug.Log("Depth: " + depth + ", action: " + task.ToString());

            MethodInfo info = operatorsType.GetMethod(task[0]);
            object[] parameters = new object[task.Count];
            parameters[0] = new State(state);
            if (task.Count > 1)
            {
                int x = 1;
                List<string> paramets = task.GetRange(1, (task.Count - 1));
                foreach (string param in paramets)
                {
                    parameters[x] = param;
                    x++;
                }
            }
            State newState = (State)info.Invoke(null, parameters);

            if (verbose > 2)
                Debug.Log("Depth: " + depth + ", new state: " + newState.ToString());

            if (newState != null)
            {
                string toAddToPlan = "(" + task[0];
                if (task.Count > 1)
                {
                    List<string> paramets = task.GetRange(1, (task.Count - 1));
                    foreach (string param in paramets)
                    {
                        toAddToPlan += (", " + param);
                    }
                }
                toAddToPlan += ")";
                plan.Add(toAddToPlan);
                List<string> solution = SeekPlan(newState, tasks.GetRange(1, (tasks.Count - 1)), plan, (depth + 1), verbose);
                if (solution != null)
                    return solution;
            }
        }
        if (methods.ContainsKey(task[0]))
        {
            if (verbose > 2)
                Debug.Log("Depth: " + depth + ", method instance: " + task.ToString());

            List<string> relevant = methods[task[0]];
            foreach (string method in relevant)
            {
                // Decompose non-primitive task into subtasks by use of a HTN method (invoke C# method)
                MethodInfo info = methodsType.GetMethod(method);
                object[] parameters = new object[task.Count];
                parameters[0] = new State(state);
                if (task.Count > 1)
                {
                    int x = 1;
                    List<string> paramets = task.GetRange(1, (task.Count - 1));
                    foreach (string param in paramets)
                    {
                        parameters[x] = param;
                        x++;
                    }
                }
                List<List<string>> subtasks = null;
                try
                {
                    subtasks = (List<List<string>>)info.Invoke(null, parameters);
                }
                catch (Exception e)
                {
                    if (verbose > 2)
                        Debug.LogException(e);
                }

                if (verbose > 2)
                    Debug.Log("Depth: " + depth + ", new tasks: " + subtasks.ToString());
                if (subtasks != null)
                {
                    List<List<string>> newTasks = new List<List<string>>(subtasks);
                    newTasks.AddRange(tasks.GetRange(1, (tasks.Count - 1)));
                    try
                    {
                        List<string> solution = SeekPlan(state, newTasks, plan, (depth + 1), verbose);
                        if (solution != null)
                            return solution;
                    }
                    catch (StackOverflowException e)
                    {
                        if (verbose > 2)
                            Debug.LogException(e);
                    }
                }
            }
        }
        if (verbose > 2)
            Debug.Log("Depth " + depth + " returns failure!");
        return null;
    }
}

