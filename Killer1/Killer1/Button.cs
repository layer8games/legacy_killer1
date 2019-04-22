using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killer1
{
    public class Button
    {
    	EventHandler _onPressEvent;
    	Text _label;
    	Vector _position = new Vector();
    	Color _gainFocus = new Color(1,0,0,1);
    	Color _loseFocus = new Color(0,0,0,1);
    	const int HALF = 2;

    	public Vector Position
    	{
    		get {return _position;}
    		set 
    		{
    			_position = value;
    			UpdatePosition();
    		}
    	}

    	public Button(EventHandler onPressEvent, Text label)
    	{
    		_onPressEvent = onPressEvent;
    		_label = label;
    		_label.SetColor(_loseFocus);
    		UpdatePosition();
    	}

    	public void UpdatePosition()
    	{
    		_label.SetPosition(_position.X - (_label.Width / HALF), _position.Y + (_label.Height / HALF));
    	}

    	public void OnGainFocus()
    	{
    		_label.SetColor(_gainFocus);
    	}

    	public void OnLoseFocus()
    	{
    		_label.SetColor(_loseFocus);
    	}

    	public void OnPress()
    	{
    		_onPressEvent(this, EventArgs.Empty);
    	}

    	public void Render(Renderer renderer)
    	{
    		renderer.DrawText(_label);
    	}
    }
}
