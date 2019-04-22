using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;
using Tao.Sdl;

namespace Killer1.Input
{
    public class ControllerButton
    {
    	IntPtr _joystick;
    	int _buttonId;
        bool _wasHeld = false;

        public bool Pressed {get; private set;}

    	public bool Held {get; private set;}

    	public ControllerButton(IntPtr joystick, int buttonId)
    	{
    		_joystick = joystick;
    		_buttonId = buttonId;
    	}

    	public void Update()
    	{
    		byte buttonState = Sdl.SDL_JoystickGetButton(_joystick, _buttonId);
    		
            Pressed = false;

            Held = (buttonState == 1);

            if(Held)
            {
                if(_wasHeld == false)
                {
                    Pressed = true;
                }
                _wasHeld = true;
            }
            else
            {
                _wasHeld = false;
            }
    	}
    }
}
