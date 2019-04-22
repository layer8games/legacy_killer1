using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Killer1;
using Killer1.Input;
using System.Windows.Forms;
using System.Drawing;
using Tao.Sdl;

namespace GraxiaIV
{
    class Level
    {
    	Input _input;
    	PersistantGameData _gameData;
    	PlayerCharacter _playerCharacter;
    	TextureManager _textureManager;
        ScrollingBackground _background;
        ScrollingBackground _backgroundLayer;
        SoundManager _soundManager;
        BulletManager _bulletManager;
        EffectsManager _effectsManager;
        EnemyManager _enemyManager;

    	public Level(Input input, PersistantGameData data, TextureManager textureManager, SoundManager soundManager, EffectsManager efffectManager, BulletManager bulletManager, PlayerCharacter playerCharacter)
    	{
    		_input = input;
    		_gameData = data;
    		_textureManager = textureManager;
            _effectsManager = efffectManager;
            _soundManager = soundManager;
            _bulletManager = bulletManager;
            _enemyManager = new EnemyManager(_textureManager, _effectsManager, _bulletManager, _soundManager, -1400);
            _playerCharacter = playerCharacter;
            SetLevel();
    	}
        
        public bool HasPlayerDied()
        {
            return _playerCharacter.IsDead;
        }

        private void UpdateCollisions()
        {
            _bulletManager.UpdatePlayerCollisions(_playerCharacter);

            foreach (Enemy enemy in _enemyManager.EnemyList)
            {
                if(enemy.GetBoundingBox().IntersectsWith(_playerCharacter.GetBoundingBox()))
                {
                    enemy.OnCollision(_playerCharacter);
                    _playerCharacter.OnCollision(enemy);
                }
                _bulletManager.UpdateEnemyCollisions(enemy);
            }

            foreach(Missle missle in _bulletManager._missles)
            {
                if(missle.Hit)
                {
                    foreach(Enemy enemy in _enemyManager.EnemyList)
                    {
                        if(enemy.GetBoundingBox().IntersectsWith(missle.GetMissleBoundingBox()))
                        {
                            enemy.OnCollision(missle);
                        }
                    }
                }
            }
        }

        public void Update(double elapsedTime, double gameTime)
    	{
            _effectsManager.Update(elapsedTime);

            UpdateCollisions();
            
            _playerCharacter.Update(elapsedTime);

            _bulletManager.Update(elapsedTime);
            _background.Update((float)elapsedTime);
            _backgroundLayer.Update((float)elapsedTime);

            _enemyManager.Update(elapsedTime, gameTime);

            UpdateInput(elapsedTime);

            double _x = 0;
    		double _y = 0;
    		Vector controlInput = new Vector(_x, _y, 0);
            double rotation = 0;

            if(Sdl.SDL_NumJoysticks() > 0)
            {
                _x = _input.Controller.LeftControlStick.X;
                _y = _input.Controller.LeftControlStick.Y * -1;
                controlInput.X = _x;
                controlInput.Y = _y;
    		}
            
            if (Math.Abs(controlInput.Length()) < 0.0001)
			{
                if(_input.Keyboard.IsKeyHeld(Keys.Left))
                {
                    controlInput.X = -1;
                }

                if(_input.Keyboard.IsKeyHeld(Keys.Right))
                {
                    controlInput.X = 1;
                }

                if(_input.Keyboard.IsKeyHeld(Keys.Up))
                {
                    controlInput.Y = 1;
                }

                if(_input.Keyboard.IsKeyHeld(Keys.Down))
                {
                    controlInput.Y = -1;
                }
                if(_input.Keyboard.IsKeyHeld(Keys.D))
                {
                    rotation = 0.175;
                }
                if(_input.Keyboard.IsKeyHeld(Keys.A))
                {
                    rotation = -0.175;
                }

                
            }
                _playerCharacter.Move(controlInput * elapsedTime, rotation);            
        }

        private void UpdateInput(double elapsedTime)
        {
            if(_input.Keyboard.IsKeyPressed(Keys.Space))
            {
                _playerCharacter.FireLazer();
            }

            if(_input.Keyboard.IsKeyPressed(Keys.Z))
            {
                if(_playerCharacter.MissleAmmo > 0)
                {
                    _playerCharacter.FireMissle();
                    _playerCharacter.MissleAmmo -= 1;
                }
            }

            if(Sdl.SDL_NumJoysticks() > 0)
            {
                if(_input.Controller.ButtonA.Pressed)
                {
                    _playerCharacter.FireLazer();
                }

                if(_input.Controller.ButtonB.Pressed)
                {
                    _playerCharacter.FireMissle();
                }
            }
        }
    	

    	public void Render(Renderer renderer)
    	{
            _background.Render(renderer);
            _backgroundLayer.Render(renderer);
            _enemyManager.Render(renderer);
            _playerCharacter.Render(renderer);
            _bulletManager.Render(renderer);
            //_bulletManager.Render_Debug();
            _effectsManager.Render(renderer);
    	}

        public void SetLevel()
        {
            _gameData.CurrentLevel.SetLevel();

            _background = new ScrollingBackground(_gameData.CurrentLevel.Background);
            _background.SetScale(2.2,2);
            _background.Speed = 0.015f;

            _backgroundLayer = new ScrollingBackground(_gameData.CurrentLevel.BackgroundLayer1);
            _backgroundLayer.SetScale(2.15, 2.1);
            _backgroundLayer.Speed = 0.2f;

            _enemyManager.UpComingEnemies = _gameData.CurrentLevel.LevelEnemies;
            _enemyManager.UpComingEnemies.Sort(delegate(EnemyDef firstEnemy, EnemyDef secondEnemy)
                                    {
                                        return firstEnemy.LaunchTime.CompareTo(secondEnemy.LaunchTime);
                                    });
        }
    }//End Class
}
