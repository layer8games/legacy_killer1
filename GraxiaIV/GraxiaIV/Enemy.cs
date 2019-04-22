using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Killer1;
using Tao.OpenGl;
using System.Drawing;

namespace GraxiaIV
{
    class Enemy : Entity
    {
    	EffectsManager _effectsManager;
        BulletManager _bulletManager;
        Texture _bulletTexture;
        SoundManager _soundManager;
        double _scale = 0.23;
        public int Health {get; set;}
        public bool IsDead{get {return Health == 0;}}
        public Path Path {get; set;}
        public double MaxTimeToShoot {get; set;}
        public double MinTimeToShoot {get; set;}
        Random _random = new Random();
        double _shootCountDown;

    	public Enemy(TextureManager textureManager, EffectsManager effectsManager, BulletManager bulletManager, SoundManager soundManager)
    	{
    		_sprite.Texture = textureManager.Get("fish_ship");
            _effectsManager = effectsManager;
            _soundManager = soundManager;

            _bulletManager = bulletManager;
            _bulletTexture = textureManager.Get("enemy_lazer");
            MaxTimeToShoot = 12;
            MinTimeToShoot = 1;
            RestartShootCountDown();

            _sprite.SetScale(_scale, _scale);
            _sprite.SetUVs(new Killer1.Point(0,0), new Killer1.Point(-1,1));
    		_sprite.SetPosition(200, 0);
            Health = 2;
    	}

    	public void Update(double elapsedTime)
    	{
            if(Path != null)
            {
                Path.UpdatePosition(elapsedTime, this);
            }

            _shootCountDown = _shootCountDown - elapsedTime;
            if(_shootCountDown <= 0)
            {
                Bullet bullet = new Bullet(_bulletTexture);
                bullet.Speed = 350;
                bullet.Direction = new Vector(-1,0,0);
                bullet.SetPosition(_sprite.GetPosition());
                bullet.SetColor(new Killer1.Color(1,1,0,1));
                _bulletManager.EnemyShoot(bullet);
                _soundManager.PlaySound("enemy_lazer1");
                RestartShootCountDown();
            }

            if(_hitFlashCountdown != 0)
            {
                _hitFlashCountdown = Math.Max(0, _hitFlashCountdown - elapsedTime);
                double scaledTime = 1 - (_hitFlashCountdown / HitFlashTime);
                _sprite.SetColor(new Killer1.Color (1,1,(float)scaledTime, 1));
            }
    	}

    	public void Render(Renderer renderer)
    	{
    		renderer.DrawSprite(_sprite);
    		//Render_Debug();
    	}

    	internal void OnCollision(PlayerCharacter playerCharacter)
    	{
    		
    	}

        static readonly double HitFlashTime = 0.25;
        double _hitFlashCountdown = 0;

        internal void OnCollision(Bullet bullet)
        {
            if(Health == 0)
            {
                return;
            }

            Health = Math.Max(0, Health - 1);
            _hitFlashCountdown = HitFlashTime;
            _sprite.SetColor(new Killer1.Color(1,1,0,1));

            if(Health == 0)
            {
                OnDestoryed();
            }
        }

        internal void OnCollision(Missle missle)
        {
            if(Health == 0)
            {
                return;
            }

            Health = Math.Max(0, Health - 2);
            _sprite.SetColor(new Killer1.Color(1,1,0,1));

            if(Health == 0)
            {
                OnDestoryed();
            }   
        }

        private void OnDestoryed()
        {
            _effectsManager.AddExplosion(_sprite.GetPosition());
            _soundManager.PlaySound("explosion");
        }


        public void RestartShootCountDown()
        {
            _shootCountDown = MinTimeToShoot + (_random.NextDouble() * MaxTimeToShoot);
        }
    }
}
