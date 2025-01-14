﻿/*
    Copyright(c) 2021 WesternGamer

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using RageLib.Resources.GTA5.PC.Drawables;
using RageLib.ResourceWrappers.Drawables;
using System;

namespace RageLib.GTA5.ResourceWrappers.PC.Drawables
{
    public class DrawableDictionaryWrapper_GTA5_pc : IDrawableDictionary
    {
        private GtaDrawableDictionary drawableDictionary;

        public DrawableDictionaryWrapper_GTA5_pc(GtaDrawableDictionary drawableDictionary)
        {
            this.drawableDictionary = drawableDictionary;
        }

        public IDrawableList Drawables
        {
            get
            {
                return new DrawableListWrapper_GTA5_pc(drawableDictionary.Drawables);
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public uint GetHash(int index)
        {
            return (uint)drawableDictionary.Hashes[index];
        }
    }
}