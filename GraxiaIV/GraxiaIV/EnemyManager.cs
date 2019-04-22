using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Killer1;

namespace GraxiaIV
{
    class EnemyManager
    {
    	List<Enemy> _enemies = new List<Enemy>();
    	TextureManager _textureManager;
    	EffectsManager _effectManager;
    	BulletManager _bulletManager;
        SoundManager _soundManager;
    	int _leftBound;

    	public List<Enemy> EnemyList
    	{
    		get{return _enemies;}
    	}

    	public List<EnemyDef> UpComingEnemies = new List<EnemyDef>();

    	public EnemyManager(TextureManager textureManager, EffectsManager effectsManager, BulletManager bulletManager, SoundManager soundManager, int leftBound)
    	{
    		_textureManager = textureManager;
    		_effectManager = effectsManager;
    		_bulletManager = bulletManager;
            _soundManager = soundManager;
    		_leftBound = leftBound;
    	}

    	private void CheckForOutOfBounds()
    	{
    		foreach(Enemy enemy in _enemies)
    		{
    			if(enemy.GetBoundingBox().Right < _leftBound)
    			{
    				enemy.Health = 0;
    			}
    		}
    	}

    	public void Render(Renderer renderer)
    	{
    		_enemies.ForEach(x => x.Render(renderer));
    	}

    	private void RemoveDeadEnemies()
    	{
    		for(int i = _enemies.Count - 1; i >= 0; i--)
    		{
    			if(_enemies[i].IsDead)
    			{
    				_enemies.RemoveAt(i);
    			}
    		}
    	}

    	private void UpdateEnemySpawns(double gameTime)
    	{
    		if(UpComingEnemies.Count == 0)
    		{
    			return;
    		}

    		EnemyDef lastElement = UpComingEnemies[UpComingEnemies.Count - 1];
    		if(gameTime < lastElement.LaunchTime)
    		{
    			UpComingEnemies.RemoveAt(UpComingEnemies.Count - 1);
    			_enemies.Add(CreateEnemyFromDef(lastElement));
    		}
    	}

    	private Enemy CreateEnemyFromDef(EnemyDef enemyDef)
    	{
    		Enemy enemy = new Enemy(_textureManager, _effectManager, _bulletManager, _soundManager);

    		switch(enemyDef.EnemyType)
            {
                case "cannon_fodder":
                {
                    List<Vector> _pathPoints = new List<Vector>();
                    _pathPoints.Add(enemyDef.StartPos);
                    _pathPoints.Add(new Vector(0,250,0));
                    _pathPoints.Add(new Vector(-1400,0,0));

                    enemy.Path = new Path(_pathPoints, 10);
                    return enemy;
                }
                case "cannon_fodder_low":
                {
                    List<Vector> _pathPoints = new List<Vector>();
                    _pathPoints.Add(enemyDef.StartPos);
                    _pathPoints.Add(new Vector(0,-250,0));
                    _pathPoints.Add(new Vector(-1400,0,0));

                    enemy.Path = new Path(_pathPoints, 10);
                    return enemy;
                }
                case "cannon_fodder_straight":
                {
                    List<Vector> _pathPoints = new List<Vector>();
                    _pathPoints.Add(enemyDef.StartPos);
                    _pathPoints.Add(new Vector(-1400,0,0));

                    enemy.Path = new Path(_pathPoints, 14);
                    return enemy;
                }
                case "up_1":
                {
                    List<Vector> _pathPoints= new List<Vector>();
                    _pathPoints.Add(new Vector(500,-375,0));
                    _pathPoints.Add(new Vector(500,0,0));
                    _pathPoints.Add(new Vector(500,0,0));
                    _pathPoints.Add(new Vector(-1400,200,0));

                    enemy.Path = new Path(_pathPoints, 10);
                    return enemy;
                }
                case "down_1":
                {
                    List<Vector> _pathPoints = new List<Vector>();
                    _pathPoints.Add(enemyDef.StartPos);
                    _pathPoints.Add(new Vector(500,0,0));
                    _pathPoints.Add(new Vector(500,0,0));
                    _pathPoints.Add(new Vector(-1400,-200,0));

                    enemy.Path = new Path(_pathPoints, 10);
                    return enemy;
                }
                case "test_for_points":
                {
                    List<Vector> _pathPoints = new List<Vector>();
                    _pathPoints.Add(enemyDef.StartPos);
                    _pathPoints.Add(new Vector(0,0,0));
                    _pathPoints.Add(new Vector(0,100,0));
                    _pathPoints.Add(new Vector(-250,375,0));
                    _pathPoints.Add(new Vector(-1400,375,0));

                    enemy.Path = new Path(_pathPoints, 5);
                    return enemy;
                }
                case "formation1_1":
                {
                    List<Vector> _pathPoints = new List<Vector>();
                    _pathPoints.Add(enemyDef.StartPos);
                    _pathPoints.Add(new Vector(-1400,0,0));

                    enemy.Path = new Path(_pathPoints, 10);
                    return enemy;
                }
                case "formation1_2":
                {
                    List<Vector> _pathPoints = new List<Vector>();
                    _pathPoints.Add(enemyDef.StartPos);
                    _pathPoints.Add(new Vector(-1400,100,0));

                    enemy.Path = new Path(_pathPoints, 10);
                    return enemy;
                }
                case "formation1_3":
                {
                    List<Vector> _pathPoints = new List<Vector>();
                    _pathPoints.Add(enemyDef.StartPos);
                    _pathPoints.Add(new Vector(-1400,-100,0));

                    enemy.Path = new Path(_pathPoints, 10);
                    return enemy;
                }
                default:
                {
                    System.Diagnostics.Debug.Assert(false, "Unknown Enemy Type.");
                    return enemy;
                }
            }
    	}

    	public void Update(double elapsedTime, double gameTime)
    	{
    		UpdateEnemySpawns(gameTime);
    		_enemies.ForEach(x => x.Update(elapsedTime));
    		CheckForOutOfBounds();
    		RemoveDeadEnemies();
    	}
    }
}
