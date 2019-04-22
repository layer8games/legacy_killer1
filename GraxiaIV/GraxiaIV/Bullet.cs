using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Killer1;

namespace GraxiaIV
{
    class Bullet : Entity
    {
    	public bool Dead{get; set;}
    	public Vector Direction{get; set;}
    	public double Speed{get; set;}

    	public Bullet(Texture bulletTexture)
    	{
    		_sprite.Texture = bulletTexture;

    		Dead = false;
    		Direction = new Vector(1,0,0);
    		Speed = 512;//pixles per second

    	}

    	public void Render(Renderer renderer)
    	{
    		if(Dead)
    		{
    			return;
    		}
    		renderer.DrawSprite(_sprite);
    	}

    	public void Update(double elapsedTime)
    	{
    		if(Dead)
    		{
    			return;
    		}
    		Vector position = _sprite.GetPosition();
    		position += Direction * Speed * elapsedTime;
    		_sprite.SetPosition(position);
    	}
    }
}
