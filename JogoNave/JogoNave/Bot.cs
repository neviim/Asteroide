using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace JogoNave
{
    class Bot
    {

        //variaveis publicas
        public Rectangle personagem;
        public Texture2D textura;
        public Rectangle retangulo;
        public int movi = 6;
        public Random random = new Random();
        public int larguraTela = 800;
        public int AlturaTela  = 480;
        public int Diferenca   = 50;
        //variaveis privadas (usadas somente dentro desta classe)
        private Vector2 novoMovimento;


        //Metodo de construção
        public Bot(Texture2D novatextura, Rectangle novoretangulo, int movimento)
        {
            textura = novatextura;
            retangulo = novoretangulo;
            novoMovimento.X = movimento;
            novoMovimento.Y = movimento;

        }


        //Armazena a resolução da tela atual
        public void resolucaoTela(int altura, int largura, int deslocamento)
        {
            larguraTela  = largura;
            AlturaTela   = altura;
            Diferenca    = deslocamento;
        }




        //Metodo que exibe na tela
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, retangulo, Color.White);
        }


        //Metodo movimentação do Personagem, usando as setas, cima, baixo, esquerda e direita
        public void movimentoPersonagem()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) //Seta direita
            {
                retangulo.X += movi;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) //Seta esquerda
            {
                retangulo.X -= movi;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) //Seta cima
            {
                retangulo.Y -= movi;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) //Seta baixo
            {
                retangulo.Y += movi;
            }
        }


        //metodo movimentação do Objeto
        public void movimenta()
        {
            personagem.X = personagem.X + (int)novoMovimento.X;

            if (personagem.X >= 900) //defini a resoluçao da tela, mais 100 px
            {
                novoMovimento.X = -novoMovimento.X;
            }
            if (personagem.X <= -250)
            {
                personagem.X = 850;

                personagem.Y = random.Next(40, 370);
                novoMovimento.X = +novoMovimento.X;
            }


        }

       /* internal void resolucaoTela(int alturatela, int larguraTela, int deslocamento)
        {
            throw new NotImplementedException();
        }*/
    }
}
