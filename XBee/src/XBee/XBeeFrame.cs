﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBee
{
    public abstract class XBeeFrame
    {
        protected XBeeAPICommandId commandId;
        public byte FrameId { get; set; }

        public XBeeAPICommandId GetCommandId()
        {
            return this.commandId;
        }

        public abstract byte[] ToByteArray();
    }
}
