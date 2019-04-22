using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killer1
{
    public class Sound
    {
    	public int Channel {get; private set;}

    	public bool FailedToPlay
    	{
    		get {return (Channel == -1);}
    	}

    	public Sound(int channel)
    	{
    		Channel = channel;
    	}
    }
}
