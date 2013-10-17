using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JogoNave
{
    class Fundo
    {
        public Texture2D textura;
        public Rectangle retangulo;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, retangulo, Color.White);
        }

    }
    class Scrolling : Fundo
    {

        public Scrolling(Texture2D newtextura, Rectangle newRetangulo)
        {
            textura = newtextura;
            retangulo = newRetangulo;
        }

        public void Update()
        {
            retangulo.X -= 1;
        }
    }
    
}
