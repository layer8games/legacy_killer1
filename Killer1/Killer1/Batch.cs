using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.Platform.Windows;
using Tao.OpenGl;

namespace Killer1
{
    public class Batch
    {
        const int MaxVertexNumber = 1000;
        const int VertexDimensions = 3;
        const int ColorDimensions = 4;
        const int UVDimensions = 2;
        Vector[] _vertexPositions = new Vector[MaxVertexNumber];
        Color[] _vertexColors = new Color[MaxVertexNumber];
        Point[] _vertexUVs = new Point[MaxVertexNumber];
        int _batchSize = 0;

        public void AddSprite(Sprite sprite)
        {
            //if batch = full, draw, empty, start again
            if (sprite.VertexPositions.Length + _batchSize > MaxVertexNumber)
            {
                Draw();
            }
            //Add current sprite vertices to batch
            for (int i = 0; i < sprite.VertexPositions.Length; i++)
            {
                _vertexPositions[_batchSize + i] = sprite.VertexPositions[i];
                _vertexColors[_batchSize + i] = sprite.VertexColors[i];
                _vertexUVs[_batchSize + i] = sprite.VertexUVs[i];
            }
            _batchSize += sprite.VertexPositions.Length;
        }

        public void SetupPointers()
        {
            Gl.glEnableClientState(Gl.GL_COLOR_ARRAY);
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glEnableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
            Gl.glVertexPointer(VertexDimensions, Gl.GL_DOUBLE, 0, _vertexPositions);
            Gl.glColorPointer(ColorDimensions, Gl.GL_FLOAT, 0, _vertexColors);
            Gl.glTexCoordPointer(UVDimensions, Gl.GL_FLOAT, 0, _vertexUVs);
        }

        public void Draw()
        {
            if (_batchSize == 0)
            {
                return;
            }
            SetupPointers();
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, _batchSize);
            _batchSize = 0;
        }
    }
}
