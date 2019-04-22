using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Killer1;

namespace GraxiaIV
{
    class EnemyDef
    {
    	public string EnemyType {get; set;}
    	public double LaunchTime{get; set;}
        public Vector StartPos{get; set;}

    	public EnemyDef()
    	{
    		EnemyType = "cannon_fodder";
    		LaunchTime = 0;
    	}

    	public EnemyDef(string enemyType, double launchTime)
    	{
    		EnemyType = enemyType;
    		LaunchTime = launchTime;
            StartPos = new Vector(-1400,0,0);
    	}

        public EnemyDef(string enemyType, double launchTime, Vector startPos)
        {
            EnemyType = enemyType;
            LaunchTime = launchTime;
            StartPos = startPos;
        }
    }
}
