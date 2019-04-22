using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Killer1;

namespace GraxiaIV
{
    class ScrollingBackground
    {
    	Sprite _background = new Sprite();

    	public float Speed {get; set;}
    	public Vector Direction {get; set;}
    	Point _topLeft = new Point(0,0);
    	Point _bottomRight = new Point(1,1);

    	public void SetScale(double x, double y)
    	{
    		_background.SetScale(x,y);
    	}

    	public ScrollingBackground(Texture background)
    	{
    	 	_background.Texture = background;
    	 	Speed = 0.15f;
    	 	Direction = new Vector(1,0,0);
    	}

    	public void Update(float elapsedTime)
    	{
    		_background.SetUVs(_topLeft, _bottomRight);
    		_topLeft.X += (float)(Speed * Direction.X * elapsedTime);
    		_bottomRight.X += (float)(Speed * Direction.X * elapsedTime);
    		_topLeft.Y += (float)(Speed * Direction.Y * elapsedTime);
    		_bottomRight.Y += (float)(Speed * Direction.Y * elapsedTime);
    	}

    	public void Render(Renderer renderer)
    	{
    		renderer.DrawSprite(_background);
    	}
    }
}
