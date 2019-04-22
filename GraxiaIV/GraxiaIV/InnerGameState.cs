using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Killer1;
using Killer1.Input;
using Tao.OpenGl;

namespace GraxiaIV
{
    class InnerGameState : IGameObject
    {
    	Renderer 		   _renderer   = new Renderer();
    	Input 	 		   _input	   = new Input();
    	StateSystem		   _system;
    	PersistantGameData _gameData;
        Level              _level;
        TextureManager     _textureManager;
        SoundManager       _soundManager;
        BulletManager _bulletManager = new BulletManager(new RectangleF(-1400 / 2, -800 / 2, 1400, 800));
        EffectsManager _effectsManager;
        PlayerCharacter    _playerCharacter;
    	Killer1.Font _generalFont;
    	double _gameTime;
        //const double TIMEOUT = 4;
        //double _countDown = TIMEOUT;

    	public InnerGameState(StateSystem system, Input input, PersistantGameData gameData, Killer1.Font generalFont, TextureManager textureManager, SoundManager soundManager)
    	{
    		_system = system;
    		_input = input;
    		_gameData = gameData;
    		_generalFont = generalFont;
            _textureManager = textureManager;
            _effectsManager = new EffectsManager(_textureManager);
            _soundManager = soundManager;
            _playerCharacter = new PlayerCharacter(_textureManager, _bulletManager, _soundManager, _effectsManager);
            _playerCharacter.MissleAmmo = 10;
    		OnGameStart();
    	}

    	public void OnGameStart()
    	{
    		_level = new Level(_input, _gameData, _textureManager, _soundManager, _effectsManager, _bulletManager, _playerCharacter);
            _playerCharacter.IsNotDead();
            _playerCharacter.SetPosition(_playerCharacter.DefaultPos);
            _bulletManager.ResetBullets();
            _effectsManager.ResetExplosions();
            _gameTime = _gameData.CurrentLevel.Time;
            
            
            if(_gameData.CurrentLevel.Stage == 1)
            {
                _playerCharacter.MissleAmmo = 10;
            }
    	}

    	public void Update(double elapsedTime)
    	{
            _level.Update(elapsedTime, _gameTime);
            _gameTime -= elapsedTime;

    		if(_gameTime <= 0)
    		{    			
                if(_gameData.CurrentLevel.Stage < _gameData.TotalLevels)
                {
                    _gameData.CurrentLevel.Stage++;
                    OnGameStart();
                }
                else if(_gameData.CurrentLevel.Stage >= _gameData.TotalLevels)
                {
                  
                 _gameData.CurrentLevel.Stage = 1;
                 OnGameStart();
                  _gameData.JustWon = true;
                  _system.ChangeState("game_over"); 
                  
                } 
    		}

            if(_level.HasPlayerDied())
            {
                
                _gameData.CurrentLevel.Stage = 1;
                OnGameStart();
                _gameData.JustWon = false;
                //_countDown -= elapsedTime;
               // if(_countDown <= 0)
                //{
                    _system.ChangeState("game_over");
                //}

            }
    	}

    	public void Render()
    	{
    		Gl.glClearColor(1,0,1,0);
    		Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            _level.Render(_renderer);
    		_renderer.Render();
    	}
    }
}
