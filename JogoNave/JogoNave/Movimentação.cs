using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JogoNave
{
    class Movimentação
    {

        public Texture2D textura;
        public Rectangle retangulo;
        Vector2 novoMovimento;
        public int movi = 6;
        public Random random = new Random();

        public Movimentação(Texture2D novatextura, Rectangle novoretangulo, int movimento)
        {
            textura = novatextura;
            retangulo = novoretangulo;
            novoMovimento.X = movimento;
            novoMovimento.Y = movimento;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, retangulo, Color.White);
        }

        public void movimentoPersonagem()
        {
            

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                retangulo.X += movi;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                retangulo.X -= movi;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                retangulo.Y -= movi;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                retangulo.Y += movi;
            }
        }

        public void movimentoAsteroide()
        {
            retangulo.X = retangulo.X + (int)novoMovimento.X;

            if (retangulo.X >= 900)
            {
               novoMovimento.X = -novoMovimento.X;
            }
            if (retangulo.X <= -250)
            {
               retangulo.X = 850;

               retangulo.Y = random.Next(40, 370);
               novoMovimento.X = +novoMovimento.X;
            }
          
        }

        
    }
}