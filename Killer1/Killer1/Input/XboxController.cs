using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.Sdl;

namespace Killer1.Input
{
     public class XboxController : IDisposable
    {
    	IntPtr _joystick;
    	public ControlStick LeftControlStick {get; private set;}
    	public ControlStick RightControlStick {get; private set;}

        public ControllerButton ButtonA {get; private set;}
        public ControllerButton ButtonB {get; private set;}
        public ControllerButton ButtonX {get; private set;}
        public ControllerButton ButtonY {get; private set;}
        public ControllerButton ButtonLB {get; private set;}
        public ControllerButton ButtonRB {get; private set;}
        public ControllerButton ButtonL3 {get; private set;}
        public ControllerButton ButtonR3 {get; private set;}
        public ControllerButton ButtonBack {get; private set;}
        public ControllerButton ButtonStart {get; private set;}

        public ControlTrigger RightTrigger {get; private set;}
        public ControlTrigger LeftTrigger {get; private set;}
        

        public DPad DPad {get; private set;}

    	public XboxController(int player)
    	{
    		_joystick = Sdl.SDL_JoystickOpen(player);
    		LeftControlStick = new ControlStick(_joystick, 0, 1);
    		RightControlStick = new ControlStick(_joystick, 4, 3);
            ButtonA = new ControllerButton(_joystick, 0);
            ButtonB = new ControllerButton(_joystick, 1);
            ButtonX = new ControllerButton(_joystick, 2);
            ButtonY = new ControllerButton(_joystick, 3);
            ButtonLB = new ControllerButton(_joystick, 4);
            ButtonRB = new ControllerButton(_joystick, 5);
            ButtonL3 = new ControllerButton(_joystick, 6);
            ButtonR3 = new ControllerButton(_joystick, 7);
            ButtonBack = new ControllerButton(_joystick, 8);
            ButtonStart = new ControllerButton(_joystick, 9);
            LeftTrigger = new ControlTrigger(_joystick, 2, true);
            RightTrigger = new ControlTrigger(_joystick, 2, false);
            DPad = new DPad(_joystick, 0);
    	}

    	public void Update()
    	{
    		LeftControlStick.Update();
    		RightControlStick.Update();
            ButtonA.Update();
            ButtonB.Update();
            ButtonX.Update();
            ButtonY.Update();
            ButtonLB.Update();
            ButtonRB.Update();
            ButtonL3.Update();
            ButtonR3.Update();
            ButtonBack.Update();
            ButtonStart.Update();
            LeftTrigger.Update();
            RightTrigger.Update();
            DPad.Update();
    	}

    	public void Dispose()
    	{
    		Sdl.SDL_JoystickClose(_joystick);
    	}
    }
}
