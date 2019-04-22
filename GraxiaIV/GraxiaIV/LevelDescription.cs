using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Killer1;

namespace GraxiaIV
{
    class LevelDescription
    {
    	public double Time {get; set;}
    	public int Stage {get; set;}
    	public Texture Background {get; set;}
    	public Texture BackgroundLayer1 {get; set;}
    	TextureManager _textureManager;

    	public List<EnemyDef> LevelEnemies = new List<EnemyDef>();

    	public LevelDescription(TextureManager textureManager)
    	{
    		_textureManager = textureManager;
    	}

    	public void SetLevel()
    	{
            switch(Stage)
            {
        		case 1:
                {
        			Time = 15;
                    Vector defaultPos = new Vector(1400,0,0);

        			Background = _textureManager.Get("background");
        			BackgroundLayer1 = _textureManager.Get("background_layer_1");

        			LevelEnemies.Add(new EnemyDef("cannon_fodder", 30, defaultPos));
    				LevelEnemies.Add(new EnemyDef("cannon_fodder", 29.5, defaultPos));
    				LevelEnemies.Add(new EnemyDef("cannon_fodder", 29, defaultPos));
    				LevelEnemies.Add(new EnemyDef("cannon_fodder", 28.5, defaultPos));

    				LevelEnemies.Add(new EnemyDef("cannon_fodder_low", 30, defaultPos));
    				LevelEnemies.Add(new EnemyDef("cannon_fodder_low", 29.5, defaultPos));
    				LevelEnemies.Add(new EnemyDef("cannon_fodder_low", 29, defaultPos));
    				LevelEnemies.Add(new EnemyDef("cannon_fodder_low", 28.5, defaultPos));

    				LevelEnemies.Add(new EnemyDef("cannon_fodder", 25, defaultPos));
    				LevelEnemies.Add(new EnemyDef("cannon_fodder", 24.5, defaultPos));
    				LevelEnemies.Add(new EnemyDef("cannon_fodder", 24, defaultPos));
    				LevelEnemies.Add(new EnemyDef("cannon_fodder", 23.5, defaultPos));

    				LevelEnemies.Add(new EnemyDef("cannon_fodder_low", 20, defaultPos));
    				LevelEnemies.Add(new EnemyDef("cannon_fodder_low", 19.5, defaultPos));
    				LevelEnemies.Add(new EnemyDef("cannon_fodder_low", 19, defaultPos));
    				LevelEnemies.Add(new EnemyDef("cannon_fodder_low", 18.5, defaultPos));

    				LevelEnemies.Add(new EnemyDef("cannon_fodder_straight", 16, defaultPos));
    				LevelEnemies.Add(new EnemyDef("cannon_fodder_straight", 15.8, defaultPos));
    				LevelEnemies.Add(new EnemyDef("cannon_fodder_straight", 15.6, defaultPos));
    				LevelEnemies.Add(new EnemyDef("cannon_fodder_straight", 15.4, defaultPos));
    				LevelEnemies.Add(new EnemyDef("up_1", 10, new Vector(500,-375,0)));
    				LevelEnemies.Add(new EnemyDef("down_1", 9, new Vector(500,375,0)));
    				LevelEnemies.Add(new EnemyDef("up_1", 8, new Vector(500,-375,0)));
    				LevelEnemies.Add(new EnemyDef("down_1", 7, new Vector(500,375,0)));
    				LevelEnemies.Add(new EnemyDef("up_1", 6, new Vector(500,-375,0)));

    				LevelEnemies.Add(new EnemyDef("test_for_points", 30, new Vector(1400,0,0)));
                    break;
        		}
        		case 2:
        		{
        			Time = 15;

                    Background = _textureManager.Get("cave_background1");
                    BackgroundLayer1 = _textureManager.Get("cave_background_layer1");
        			
                    LevelEnemies.Add(new EnemyDef("formation1_1", 15, new Vector(1400,0,0)));
                    LevelEnemies.Add(new EnemyDef("formation1_2", 15, new Vector(1500,100,0)));
                    LevelEnemies.Add(new EnemyDef("formation1_3", 15, new Vector(1500,-100,0)));

                    LevelEnemies.Add(new EnemyDef("formation1_1", 10, new Vector(1400,100,0)));
                    LevelEnemies.Add(new EnemyDef("formation1_2", 10, new Vector(1500,200,0)));
                    LevelEnemies.Add(new EnemyDef("formation1_3", 10, new Vector(1500,0,0)));

                    LevelEnemies.Add(new EnemyDef("formation1_1", 5, new Vector(1400,0,0)));
                    LevelEnemies.Add(new EnemyDef("formation1_2", 5, new Vector(1500,100,0)));
                    LevelEnemies.Add(new EnemyDef("formation1_3", 5, new Vector(1500,-100,0)));
                    break;

        		}
                case 3:
                {
                    Time = 15;

                    Background = _textureManager.Get("earth");
                    BackgroundLayer1 = _textureManager.Get("background_layer_1");
                    break;
                }
               /* default:
                {
                    break;
                }*/
            }
    	}
    }
}
