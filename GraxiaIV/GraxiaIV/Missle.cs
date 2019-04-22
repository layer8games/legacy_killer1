using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Killer1;
using System.Drawing;

namespace GraxiaIV
{
    class Missle : Entity
    {
    	public bool Dead{get; set;}
    	public Vector Direction{get; set;}
    	public double Speed{get; set;}
        public bool Hit {get; set;}

    	public Missle(Texture missleTexture)
    	{
    		_sprite.Texture = missleTexture;

    		Dead = false;
            Hit = false;
    		Direction = new Vector(1,0,0);
    		Speed = 312;//pixles per second
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

        public void SetScale(double x, double y)
        {
            _sprite.SetScale(x,y);
        }

        public RectangleF GetMissleBoundingBox()
        {
            float width = (float)(_sprite.Texture.Width * _sprite.ScaleX * 10);
            float height = (float)(_sprite.Texture.Height * _sprite.ScaleY * 10);
            return new RectangleF((float)_sprite.GetPosition().X - width / 2, (float)_sprite.GetPosition().Y - height / 2, width, height);
        }
    }
}
