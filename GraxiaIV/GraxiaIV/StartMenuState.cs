using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Killer1;
using Killer1.Input;
using Tao.OpenGl;

namespace GraxiaIV
{
    class StartMenuState : IGameObject
    {
    	Renderer 		_renderer = new Renderer();
    	Text 	 		_title;
    	Killer1.Font 	_generalFont;
        Killer1.Font    _computerFont;
    	Input 			_input;
    	VerticalMenu 	_menu;
    	StateSystem 	_system;
        PersistantGameData _gameData;

    	public StartMenuState(Killer1.Font titleFont, Killer1.Font generalFont, Killer1.Font computerFont, Input input, StateSystem system, PersistantGameData gameData)
    	{
    		_system = system;
    		_generalFont = generalFont;
            _computerFont = computerFont;
            _input = input;
            _gameData = gameData;
    		InitializeMenu();
    		

    		_title = new Text("Shooter", _computerFont);
    		_title.SetColor(new Color(0,0,0,1));
    		_title.SetPosition(_title.Width + 340 , 300);
    	}

    	private void InitializeMenu()
    	{
    		_menu = new VerticalMenu(0, 150, _input);
    		Button startGame = new Button(
    			delegate(object o, EventArgs e)
    			{
                    _system.ChangeState("inner_game");
    			},
    			new Text("Start", _generalFont));

    		Button exitGame = new Button(
    			delegate(object o, EventArgs e)
    			{
    				System.Windows.Forms.Application.Exit();
    			},
    			new Text("Exit", _generalFont));

    		_menu.AddButton(startGame);
    		_menu.AddButton(exitGame);
    	}

    	public void Update(double elapsedTime) 
    	{
    		_menu.HandleInput();
    	}

    	public void Render()
    	{
    		Gl.glClearColor(1,1,1,0);
    		Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
    		_renderer.DrawText(_title);
    		_menu.Render(_renderer);
    		_renderer.Render();
    	}
    }
}
