using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class HTNPlanner
    {
        private IHTNDomain _domain;

        public HTNPlanner()
        {

        }

        public bool StopSearch { get { return false; } }

        public void InitializePlanner(IHTNDomain domain)
        {
            //Need to check if current planner is running another domain?
            _domain = domain;
        }

        public List<string> DeclareOperators()
        {
            return null;
        }

        public List<string> DeclareMethods()
        {

            return null;
        }

        public List<string> SolvePlanningProblem(State state, List<List<string>> tasks, int verbose = 0)
        {
            return null;
        }

        public List<string> SeekPlan(State state, List<List<string>> tasks, int verbose = 0)
        {
            return null;
        }

    }

