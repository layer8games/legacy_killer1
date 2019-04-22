using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraxiaIV
{
    class PersistantGameData
    {
    	public bool JustWon {get; set;}
    	public LevelDescription CurrentLevel {get; set;}
    	public int TotalLevels = 3;
        

    	public PersistantGameData()
    	{
    		JustWon = false;
    	}
    }
}
