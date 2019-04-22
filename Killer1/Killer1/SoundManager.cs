using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenAl;
using System.IO;

namespace Killer1
{
    public class SoundManager : IDisposable
    {
    	struct SoundSource
    	{
    		public SoundSource(int bufferId, string filePath)
    		{
    			_bufferId = bufferId;
    			_filePath = filePath;
    		}
    		public int _bufferId;
    		string _filePath;
    	}

    	Dictionary<string, SoundSource> _soundIdentifier = new Dictionary<string, SoundSource>();

    	readonly int MaxSoundChannels = 256;
    	List<int> _soundChannels = new List<int>();
        float _masterVolume = 1.0f;

    	public SoundManager()
    	{
    		Alut.alutInit();
    		DiscoverSoundChannels();
    	}

    	public void DiscoverSoundChannels()
    	{
    		while(_soundChannels.Count < MaxSoundChannels)
    		{
    			int src;
    			Al.alGenSources(1, out src);
    			if(Al.alGetError() == Al.AL_NO_ERROR)
    			{
    				_soundChannels.Add(src);
    			}
    			else
    			{
    				break;
    			}
    		}
    	}

    	public void LoadSound(string soundId, string path)
    	{
			int buffer = -1;
    		Al.alGenBuffers(1, out buffer);

    		int errorCode = Al.alGetError();
    		System.Diagnostics.Debug.Assert(errorCode == Al.AL_NO_ERROR);

    		int format;
    		float frequency;
    		int size;
    		System.Diagnostics.Debug.Assert(File.Exists(path));
    		IntPtr data = Alut.alutLoadMemoryFromFile(path, out format, out size, out frequency);
    		
              //System.Diagnostics.Debug.Assert(data != IntPtr.Zero);
    		
    		Al.alBufferData(buffer, format, data, size, (int)frequency);
    		_soundIdentifier.Add(soundId, new SoundSource(buffer, path));
    	}

    	public Sound PlaySound(string soundId)
    	{
    		return PlaySound(soundId, false);
    	}

        public Sound PlaySound(string soundId, bool loop)
        {
            int channel = FindNextFreeChannel();
            
            if(channel != -1)
            {
                Al.alSourceStop(channel);
                Al.alSourcei(channel, Al.AL_BUFFER, _soundIdentifier[soundId]._bufferId);
                Al.alSourcef(channel, Al.AL_PITCH, 1.0f);
                Al.alSourcef(channel, Al.AL_GAIN, 1.0f);

                if(loop)
                {
                    Al.alSourcei(channel, Al.AL_LOOPING, 1);
                }
                else
                {
                    Al.alSourcei(channel, Al.AL_LOOPING, 0);
                }
                Al.alSourcef(channel, Al.AL_GAIN, _masterVolume);
                Al.alSourcePlay(channel);
                return new Sound(channel);
            }
            else 
            {
                return new Sound(-1);
            }
        }

    	public bool IsSoundPlaying(Sound sound)
    	{
    		return IsChannelPlaying(sound.Channel);
    	}

    	public void StopSound(Sound sound)
    	{
            if(sound.Channel == -1)
            {
                return;
            }
            Al.alSourceStop(sound.Channel);
    	}

        private bool IsChannelPlaying(int channel)
        {
            int value = 0;
            Al.alGetSourcei(channel, Al.AL_SOURCE_STATE, out value);
            return (value == Al.AL_PLAYING);
        }

        private int FindNextFreeChannel()
        {
            foreach (int slot in _soundChannels)
            {
                if(!IsChannelPlaying(slot))
                {
                    return slot;
                }
            }
            return -1;
        }

        public void MasterVolume(float value)
        {
            _masterVolume = value;
            foreach(int channel in _soundChannels)
            {
                Al.alSourcef(channel, Al.AL_GAIN, value);
            }
        }

        public void ChangeVolume(Sound sound, float value)
        {
            Al.alSourcef(sound.Channel, Al.AL_GAIN, _masterVolume * value);
        }

        public void Dispose()
        {
            foreach(SoundSource soundSource in _soundIdentifier.Values)
            {
                SoundSource temp = soundSource;
                Al.alDeleteBuffers(1, ref temp._bufferId);
            }
            _soundIdentifier.Clear();
            foreach(int slot in _soundChannels)
            {
                int target = _soundChannels[slot];
                Al.alDeleteSources(1, ref target);
            }
            Alut.alutExit();
        }
    }
}
