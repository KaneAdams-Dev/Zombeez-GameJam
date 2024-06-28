using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombeezGameJam.Stats;

namespace ZombeezGameJam.Interfaces
{
    public interface IStatsApplicable
    {
        void SetCurrentStatSO(BaseEntityStats a_newStats);
        BaseEntityStats GetEntityStats();
        int GetCurrentHealth();
        void SetInitialHealth(int a_startHealth);
    }
}
