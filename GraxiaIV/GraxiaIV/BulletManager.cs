using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Killer1;
using System.Drawing;

namespace GraxiaIV
{
    class BulletManager
    {
    	List<Bullet> _bullets       = new List<Bullet>();
       public List<Missle> _missles       = new List<Missle>();
        List<Bullet> _enemyBullets  = new List<Bullet>();
    	RectangleF _bounds;
        public bool MissleHit = false;

    	public BulletManager(RectangleF playArea)
    	{
    		_bounds = playArea;
    	}

    	public void ShootLazer(Bullet bullet)
    	{
    		_bullets.Add(bullet);
    	}

        public void ShootMissle(Missle missle)
        {
            _missles.Add(missle);
        }

        public void EnemyShoot(Bullet bullet)
        {
            _enemyBullets.Add(bullet);
        }

    	public void Update(double elapsedTime)
        {
            UpdateBulletList(_bullets, elapsedTime);
            UpdateBulletList(_missles, elapsedTime);
            UpdateBulletList(_enemyBullets, elapsedTime);
        }

        public void UpdateBulletList(List<Bullet> bulletList, double elapsedTime)
        {
            bulletList.ForEach(x => x.Update(elapsedTime));
            CheckOutOfBounds(_bullets);
            RemoveDeadBullets(bulletList);
        } 

        public void UpdateBulletList(List<Missle> bulletList, double elapsedTime)
        {
            bulletList.ForEach(x => x.Update(elapsedTime));
            CheckOutOfBounds(_missles);
            RemoveDeadBullets(bulletList);
        }

    	private void CheckOutOfBounds(List<Bullet> bulletList)
    	{
    		foreach(Bullet bullet in bulletList)
    		{
    			if(!bullet.GetBoundingBox().IntersectsWith(_bounds))
    			{
    				bullet.Dead = true;
    			}
    		}
    	}

        private void CheckOutOfBounds(List<Missle> bulletList)
        {
            foreach(Missle missle in bulletList)
            {
                if(!missle.GetBoundingBox().IntersectsWith(_bounds))
                {
                    missle.Dead = true;
                }
            }
        }

    	private void RemoveDeadBullets(List<Bullet> bulletList)
    	{
    		for(int i = bulletList.Count - 1; i >= 0; i--)
    		{
    			if(bulletList[i].Dead)
    			{
    				bulletList.RemoveAt(i);
    			}
    		}
    	}

        private void RemoveDeadBullets(List<Missle> bulletList)
        {
            for(int i = bulletList.Count - 1; i >= 0; i--)
            {
                if(bulletList[i].Dead)
                {
                    bulletList.RemoveAt(i);
                }
            }
        }

        public void ResetBullets()
        {
            for(int i = _bullets.Count - 1; i >= 0; i--)
            {
                _bullets.RemoveAt(i);
            }

            for(int i = _missles.Count -1; i >= 0; i--)
            {
                _missles.RemoveAt(i);
            }

            for(int i = _enemyBullets.Count -1; i >= 0; i--)
            {
                _enemyBullets.RemoveAt(i);
            }
        }   
        

        public void UpdatePlayerCollisions(PlayerCharacter playerCharacter)
        {
            foreach(Bullet bullet in _enemyBullets)
            {
                if(bullet.GetBoundingBox().IntersectsWith(playerCharacter.GetBoundingBox()))
                {
                    bullet.Dead = true;
                    playerCharacter.OnCollision(bullet);
                }
            }
        }

        internal void UpdateEnemyCollisions(Enemy enemy)
        {
            foreach(Bullet bullet in _bullets)
            {
                if(bullet.GetBoundingBox().IntersectsWith(enemy.GetBoundingBox()))
                {
                    bullet.Dead = true;
                    enemy.OnCollision(bullet);
                }
            }

            foreach(Missle missle in _missles)
            {
                if(missle.GetBoundingBox().IntersectsWith(enemy.GetBoundingBox()))
                {
                    missle.Dead = true;
                    missle.Hit = true;
                    enemy.OnCollision(missle);
                }
            }
        }


    	internal void Render(Renderer renderer)
    	{
    		_bullets.ForEach(x => x.Render(renderer));
            //_bullets.ForEach(x => x.Render_Debug());
            _missles.ForEach(x => x.Render(renderer));
            //_missles.ForEach(x => x.Render_Debug());
            _enemyBullets.ForEach(x => x.Render(renderer));
            //_enemyBullets.ForEach(x => x.Render_Debug());
    	}
    }
}
