using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Killer1;
using Killer1.Input;
using Tao.OpenGl;
using Tao.Sdl;

namespace GraxiaIV
{
    class GameOverState : IGameObject
    {
    	const double TIMEOUT = 4;
    	double _countDown = TIMEOUT;

    	StateSystem 		_system;
    	Input 				_input;
    	Font 				_generalFont;
    	Font 				_titleFont;
    	PersistantGameData 	_gameData;
    	Renderer 			_renderer = new Renderer();

    	Text _titleWin;
    	Text _blurbWin;
    	Text _titleLose;
    	Text _blurbLose;

    	public GameOverState(PersistantGameData gameData, StateSystem system, Input input, Font generalFont, Font titleFont)
    	{
    		_gameData = gameData;
    		_system = system;
    		_input = input;
    		_generalFont = generalFont;
    		_titleFont = titleFont;

    		_titleWin = new Text("Complete!", _titleFont);
    		_blurbWin = new Text("Congratulations, you won!", _generalFont);
    		_titleLose = new Text("Game Over!", _titleFont);
    		_blurbLose = new Text("Please try again...", _generalFont);

    		FormatText(_titleWin, 300);
    		FormatText(_blurbWin, 200);

    		FormatText(_titleLose, 300);
    		FormatText(_blurbLose, 200);
    	}

    	private void FormatText(Text text, int yPos)
    	{
    		text.SetPosition(-text.Width / 2, yPos);
    		text.SetColor(new Color(0,0,0,1));
    	}

    	public void Update(double elapsedTime)
    	{
    		_countDown -= elapsedTime;

    		if(Sdl.SDL_NumJoysticks() > 0)
    		{
    			if(_countDown <= 0 || _input.Controller.ButtonA.Pressed)
    			{
    				Finish();
    			}
    		}

    		if(_countDown <= 0 || _input.Keyboard.IsKeyPressed(System.Windows.Forms.Keys.Enter))
    		{
    			Finish();
    		}
    	}

    	private void Finish()
    	{
    		_system.ChangeState("start_menu");
    		_countDown = TIMEOUT;
    	}

    	public void Render()
    	{
    		Gl.glClearColor(1,1,1,0);
    		Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
    		if(_gameData.JustWon)
    		{
    			_renderer.DrawText(_titleWin);
    			_renderer.DrawText(_blurbWin);
    		}
    		else
    		{
    			_renderer.DrawText(_titleLose);
    			_renderer.DrawText(_blurbLose);
    		}
    		_renderer.Render();
    	}
    }
}
