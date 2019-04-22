using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Killer1;
using Killer1.Input;
using Tao.OpenGl;
using Tao.DevIl;

namespace GraxiaIV
{
    public partial class Form1 : Form
    {
    	bool			   _fullScreen		   = false;
    	FastLoop		   _fastLoop;
    	StateSystem 	   _system			   = new StateSystem();
    	Input			   _input 			   = new Input();
    	TextureManager     _textureManager     = new TextureManager();
    	SoundManager 	   _soundManager 	   = new SoundManager();
        PersistantGameData _persistantGameData = new PersistantGameData();
        Killer1.Font       _titleFont;
        Killer1.Font       _generalFont;
        Killer1.Font       _computerFont;

        public Form1()
        {
            InitializeComponent();
            simpleOpenGlControl1.InitializeContexts();

            _input.Mouse = new Mouse(this, simpleOpenGlControl1);
            _input.Keyboard = new Keyboard(simpleOpenGlControl1);

            InitializeDisplay();
            InitializeSounds();
            InitializeTextures();
            InitializeFonts();
            InitializeGameData();
            InitializeGameState();
            
            _fastLoop = new FastLoop(GameLoop);
        }

        private void InitializeDisplay()
        {
        	if(_fullScreen)
        	{
        		FormBorderStyle = FormBorderStyle.None;
        		WindowState = FormWindowState.Maximized;
        	}
        	else 
        	{
        		ClientSize = new Size(1366, 768);
        	}
        	Setup2DGraphics(ClientSize.Width, ClientSize.Height);
        }

        private void InitializeSounds()
        {
        	//Load Sounds
            _soundManager.LoadSound("bullet1", "Assets/soundeffect1.wav");
            _soundManager.LoadSound("player_lazer", "Assets/player_lazer.wav");
            _soundManager.LoadSound("bullet2", "Assets/soundeffect2.wav");
            _soundManager.LoadSound("missle", "Assets/missle_sound.wav");
            _soundManager.LoadSound("enemy_lazer1", "Assets/enemy_lazer1.wav");
            _soundManager.LoadSound("explosion", "Assets/explosion.wav");

        }

        private void InitializeTextures()
        {
        	Il.ilInit();
        	Ilu.iluInit();
        	Ilut.ilutInit();
        	Ilut.ilutRenderer(Ilut.ILUT_OPENGL);

        	//Load Texutures
            _textureManager.LoadTexture("title_font", "Assets/title_font.tga");
            _textureManager.LoadTexture("general_font", "Assets/general_font.tga");
            _textureManager.LoadTexture("computer_font2", "Assets/computer_font2.tga");
            _textureManager.LoadTexture("player_ship", "Assets/spaceship.tga");
            _textureManager.LoadTexture("player", "Assets/player.tga");
            _textureManager.LoadTexture("test", "Assets/test_ship.tga");
            _textureManager.LoadTexture("enemy_ship", "Assets/spaceship2.tga");
            _textureManager.LoadTexture("enemy1", "Assets/enemy1.tga"); 
            _textureManager.LoadTexture("fish_ship", "Assets/fish_ship.tga");
            _textureManager.LoadTexture("player_lazer", "Assets/player_lazer2.tga");
            _textureManager.LoadTexture("enemy_lazer", "Assets/enemy_lazer.tga");
            _textureManager.LoadTexture("alt_bullet", "Assets/alt_bullet.tga");
            _textureManager.LoadTexture("missle", "Assets/missle.tga");
            _textureManager.LoadTexture("background", "Assets/space_back.tga");
            _textureManager.LoadTexture("background_layer_1", "Assets/background_p.tga");
            _textureManager.LoadTexture("cave_background1", "Assets/cave_background1.tga");
            _textureManager.LoadTexture("cave_background_layer1", "Assets/cave_background_layer1.tga");
            _textureManager.LoadTexture("earth", "Assets/earth.tga");
            _textureManager.LoadTexture("explosion", "Assets/explode.tga");
        }

        private void InitializeGameData()
        {
            LevelDescription level = new LevelDescription(_textureManager);
            level.Stage = 1;//default level
            _persistantGameData.CurrentLevel = level;
        }

        private void InitializeFonts()
        {
        	//Load Fonts
            _titleFont = new Killer1.Font(_textureManager.Get("title_font"), FontParser.Parse("Assets/title_font.fnt"));
            _generalFont = new Killer1.Font(_textureManager.Get("general_font"), FontParser.Parse("Assets/general_font.fnt"));
            _computerFont = new Killer1.Font(_textureManager.Get("computer_font2"), FontParser.Parse("Assets/computer_font2.fnt"));
        }

        private void InitializeGameState()
        {
        	//Load Game States
            _system.AddState("start_menu", new StartMenuState(_titleFont, _generalFont, _computerFont, _input, _system, _persistantGameData));
            _system.AddState("inner_game", new InnerGameState(_system, _input, _persistantGameData, _generalFont, _textureManager, _soundManager));
            _system.AddState("game_over", new GameOverState(_persistantGameData, _system, _input, _generalFont, _titleFont));
            _system.ChangeState("start_menu");//default state
        }

        private void UpdateInput(double elapsedTime)
        {
        	_input.Update(elapsedTime);
        }

        private void GameLoop(double elapsedTime)
        {
        	UpdateInput(elapsedTime);
        	_system.Update(elapsedTime);
        	_system.Render();
        	simpleOpenGlControl1.Refresh();
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
        	base.OnClientSizeChanged(e);
        	Gl.glViewport(0, 0, this.ClientSize.Width, this.ClientSize.Height);
        	Setup2DGraphics(ClientSize.Width, ClientSize.Height);
        }

        private void Setup2DGraphics(double width, double height)
        {
        	double halfWidth = width / 2;
        	double halfHeight = height / 2;
        	Gl.glMatrixMode(Gl.GL_PROJECTION);
        	Gl.glLoadIdentity();
        	Gl.glOrtho(-halfWidth, halfWidth, -halfHeight, halfHeight, -100, 100);
        	Gl.glMatrixMode(Gl.GL_MODELVIEW);
        	Gl.glLoadIdentity();
        }
    }
}
