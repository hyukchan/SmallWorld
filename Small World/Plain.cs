﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Small_World
{
    public interface PlainInterface : TileInterface
    {

    }

    [Serializable()]
    public class Plain : Tile, PlainInterface
    {

        /// <summary>
        /// Constructeur d'une case "Plaine"
        /// </summary>
        public Plain()
        {

        }
    }
}
