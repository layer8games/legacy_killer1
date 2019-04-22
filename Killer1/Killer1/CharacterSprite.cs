﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Killer1
{
    public class CharacterSprite
    {
        public Sprite Sprite { get; set; }
        public CharacterData Data { get; set; }

        public CharacterSprite(Sprite sprite, CharacterData data)
        {
            Data = data;
            Sprite = sprite;
        }
    }
}
