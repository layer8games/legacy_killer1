using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Killer1;

namespace GraxiaIV
{
    class PlayerCharacter : Entity
    {
    	BulletManager _bulletManager;
        SoundManager _soundManager;
        EffectsManager _effectsManager;
        Texture _bulletTexture;
        Texture _missleTexture;
        Vector _gunOffset = new Vector(55,0,0);
        static readonly double FireRecovery = 0.01;
        static readonly double MissleRecovery = 0.01;
        double _fireRecoveryTime = FireRecovery;
        double _missleRecoveryTime = MissleRecovery;
        public int MissleAmmo {get; set;}
        public Vector DefaultPos = new Vector(-200,0,0);
         

        double _speed = 512;
        bool _dead = false;
        public bool IsDead
        {
            get{return _dead;}
        }

        public void IsNotDead()
        {
            _dead = false;
        }

    	public PlayerCharacter(TextureManager textureManager, BulletManager bulletManager, SoundManager soundManager, EffectsManager effectsManager)
    	{
    		_sprite.Texture = textureManager.Get("player");
    		_sprite.SetScale(0.15, 0.15);
            _bulletManager = bulletManager;
            _bulletTexture = textureManager.Get("player_lazer");
            _missleTexture = textureManager.Get("missle");
            MissleAmmo = 10;
            _soundManager = soundManager;
            _effectsManager = effectsManager;
    	}

    	public void Update(double elapsedTime)
        {
            _fireRecoveryTime = Math.Max(0, (_fireRecoveryTime -elapsedTime));
            _missleRecoveryTime = Math.Max(0, (_missleRecoveryTime -elapsedTime));
        }

        public void Render(Renderer renderer)
    	{
    		//Render_Debug();
            renderer.DrawSprite(_sprite);
    	}

    	public void Move(Vector amount)
    	{
    		
            amount *= _speed;
    		_sprite.SetPosition(_sprite.GetPosition() + amount);
    	}

        public void Move(Vector amount, double angle)
        {
            _sprite.SetRotation(angle);
            amount *= _speed;
            _sprite.SetPosition(_sprite.GetPosition() + amount);
            
        }

        internal void OnCollision(Bullet bullet)
        {
            _dead = true;
            _effectsManager.AddExplosion(_sprite.GetPosition());
        }

        internal void OnCollision(Enemy enemy)
        {
            _dead = true;
            _effectsManager.AddExplosion(_sprite.GetPosition());
        }

        public void FireLazer()
        {
            if(_fireRecoveryTime > 0)
            {
                return;
            }
            else
            {
                _fireRecoveryTime = FireRecovery;
            }

            Bullet lazer = new Bullet(_bulletTexture);
            //lazer.SetColor(new Color(0,1,0,1));
            lazer.Speed = 912;
            lazer.SetPosition(_sprite.GetPosition() + _gunOffset);
            _bulletManager.ShootLazer(lazer);
            _soundManager.PlaySound("player_lazer");
        }

        public void FireMissle()
        {
            if(_missleRecoveryTime > 0)
            {
                return;
            }
            else
            {
                _missleRecoveryTime = MissleRecovery;
            }

            Missle missle = new Missle(_missleTexture);
            missle.SetScale(0.8,0.8);
            //missle.SetColor(new Color(0,0,1,1));
            missle.SetPosition(_sprite.GetPosition() + _gunOffset);
            _bulletManager.ShootMissle(missle);
            _soundManager.PlaySound("missle");
        }
    }
}
